using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Interface.Infrastructure.Database;
using Core.Interface.security;
using Core.Interface.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace Core.Services
{
    public class TripService : ITripService
    {
        private readonly ITripRepository _repository;
        private readonly IAuthRespository _authRepo;
        private readonly IUserAccessor _userAccessor;
        private readonly IMailService _mail;

        public TripService(ITripRepository repository, IAuthRespository auth, IUserAccessor userAccessor, IMailService mail) // Inject the repository
        {
            _repository = repository; // Assign the repository to the private field
            _userAccessor = userAccessor; ;
            _mail = mail;
            _authRepo = auth;
        }

        private TripServiceResponse ConvertToResponseModel(Trip entity)
        {
            return new TripServiceResponse
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
                Landmark = entity.Landmark,
                Duration = entity.Duration,
                Price = entity.Price,
                Fee = entity.Fee,
                Origin = entity.Origin,
                Destination = entity.Destination,
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt,
                IsActive = entity.IsActive
            };
        }

        public async Task CreateNewTripAsync(TripServiceInput input)
        {

            bool isTheNameAlreadyTaken = await _repository.Exists(x => x.Name == input.Name); // TODO: Check if the name is already taken
            if (isTheNameAlreadyTaken)
            {
                throw new ArgumentException("The name is already taken");
            }

            List<TripAgenda> tripAgendas = new();

            for (int i = 0; i < input.TripAgendas.Count; i++)
            {
                // string[] date = input.TripAgendas[i].Date;
                string[] time = input.TripAgendas[i].Time.Split(":");
                TripAgenda tripAgenda = new()
                {
                    Id = i + 1,
                    Description = input.TripAgendas[i].Description,
                    Date = input.TripAgendas[i].Date,
                    Time = new TimeOnly(int.Parse(time[0]), int.Parse(time[1]))
                };
                tripAgendas.Add(tripAgenda);
            }

            Trip trip = new()
            {
                Name = input.Name,
                Description = input.Description,
                Landmark = input.Landmark,
                Duration = input.Duration,
                Price = input.Price,
                Fee = input.Fee,
                Origin = input.Origin,
                Destination = input.Destination,
                MaxAttendees = input.MaxAttendee,
                TypeOfTripId = input.TypeOfTripId,
                TripAgenda = tripAgendas,
                Attendee = new List<TripAttendee>()
                {
                    new TripAttendee
                    {
                        ApplicationUserId = _userAccessor.GetUserId(),
                        IsHost = true,
                    }
                },
                Photos = input.Photos.Select(x => new TripPhoto
                {
                    Id = Guid.NewGuid(),
                    Url = x.Url,
                }).ToList(),
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                IsActive = true
            };

            Trip insertedEntity = await _repository.AddAsync(trip); // TODO: Check if the entity is inserted successfully
            bool invoke = await _repository.SaveChangesAsync<int>() > 0;
            if (!invoke)
            {
                throw new ArgumentException("The trip is not created");
            }
        }

        public async Task<TripServiceResponse> DeleteTripAsync(Guid tripId)
        {
            Trip existedTrip = await _repository.GetById(tripId);
            bool theTripIsNotExist = existedTrip == null;
            if (theTripIsNotExist)
            {
                throw new ArgumentException("The trip is not found");
            }

            existedTrip = _repository.Remove(existedTrip); // TODO: Check if the entity is removed successfully
            bool invoke = await _repository.SaveChangesAsync<int>() > 0;
            if (!invoke)
            {
                return null;
            }

            return ConvertToResponseModel(existedTrip);
        }

        public async Task<TripServiceResponse> GetTripAsync(Guid tripId)
        {
            TripServiceResponse result = await _repository.GetTripAsync(tripId);
            if (result == null)
            {
                throw new ArgumentException("The trip is not found");
            }

            return result;
        }

        public async Task<TripsServiceResponseWithPaging> GetListOfAllTripsAsync()
        {
            List<TripServiceResponse> listOfTrips = await _repository.GetTripsAsync();

            return new TripsServiceResponseWithPaging
            {
                TotalRows = listOfTrips.Count,
                Trips = listOfTrips
            };
        }

        public async Task<TripServiceResponse> UpdateTripAsync(Guid tripId, TripServiceInput input)
        {
            Trip existedTrip = await _repository.GetById(tripId);
            bool theTripIsNotExist = existedTrip == null;
            if (theTripIsNotExist)
            {
                throw new ArgumentException("The trip is not found");
            }

            existedTrip.Name = input.Name;
            existedTrip.Description = input.Description;
            existedTrip = _repository.Update(existedTrip); // TODO: Check if the entity is updated successfully, return null if not updated else return the updated entity 
            bool invoke = await _repository.SaveChangesAsync<int>() > 0;
            if (!invoke)
            {
                return null;
            }

            return ConvertToResponseModel(existedTrip);
        }

        public async Task UpdateAttendeeAsync(Guid tripId)
        {
            // ExistedTrip
            Trip trip = await _repository.ExistedTripAsync(tripId);
            if (trip == null)
            {
                throw new ArgumentException("The trip is not found");
            }
            // throw new ArgumentException($"{JsonConvert.SerializeObject(_userAccessor.GetUserId())}");
            // ExistedUser
            ApplicationUser user = await _authRepo.FindByUsername(_userAccessor.GetUsername());
            if (user == null)
            {
                throw new ArgumentException($"The user is not found, {JsonConvert.SerializeObject(_userAccessor.GetUsername())}");
            }

            string host = trip.Attendee.FirstOrDefault(x => x.IsHost).ApplicationUserId;

            TripAttendee attendee = trip.Attendee.FirstOrDefault(x => x.ApplicationUser.UserName == user.UserName);
            // If the user is the host, cancel the activity
            if (attendee != null && host == user.Id)
                trip.IsActive = !trip.IsActive;

            // If the user is not the host, add or remove the user from the attendees
            if (attendee != null && host != user.Id)
                trip.Attendee.Remove(attendee);

            // AddAttendee
            if (attendee == null)
            {
                attendee = new TripAttendee
                {
                    Trip = trip,
                    ApplicationUser = user,
                    IsHost = false
                };
                trip.Attendee.Add(attendee);
            }

            bool invoke = await _repository.SaveChangesAsync<int>() > 0;
            if (!invoke)
            {
                throw new ArgumentException("The attendee is not updated");
            }
        }

        public async Task AcceptAttendeeAsync(Guid tripId, string userId)
        {
            // Accept the attendee -> IsAccepted = true
            Trip trip = await _repository.GetOneTrip(tripId);

            string hostId = trip.Attendee.FirstOrDefault(x => x.IsHost).ApplicationUserId;

            if (trip == null || hostId != _userAccessor.GetUserId())
            {
                throw new ArgumentException("The trip is not found");
            }

            TripAttendee attendee = trip.Attendee.FirstOrDefault(x => x.ApplicationUser.Email == userId);

            if (attendee == null)
            {
                throw new ArgumentException("The attendee is not found");
            }

            attendee.IsAccepted = true;

            bool invoke = await _repository.SaveChangesAsync<int>() > 0;

            if (!invoke)
            {
                throw new ArgumentException("The attendee is not accepted");
            }

            await _mail.SendEmailResponeToJoinTrip(new MessageRequest
            {
                ToEmail = attendee.ApplicationUser.Email,
                Message = "Your request to join the trip has been accepted",
                UserName = attendee.ApplicationUser.UserName
            });
        }

        public async Task RejectAttendeeAsync(Guid tripId, string userId)
        {
            // Reject the attendee -> Remove the attendee from the list

            Trip trip = await _repository.GetOneTrip(tripId);
            string hostId = trip.Attendee.FirstOrDefault(x => x.IsHost).ApplicationUserId;
            if (trip == null || hostId != _userAccessor.GetUserId())
            {
                throw new ArgumentException("The trip is not found");
            }

            TripAttendee attendee = trip.Attendee.FirstOrDefault(x => x.ApplicationUser.Email == userId);

            if (attendee == null)
            {
                throw new ArgumentException("The attendee is not found");
            }

            trip.Attendee.Remove(attendee);

            bool isInvoke = await _repository.SaveChangesAsync<int>() > 0;
            if (!isInvoke)
            {
                throw new ArgumentException("Reject the attendee is not successful");
            }

            await _mail.SendEmailResponeToJoinTrip(new MessageRequest
            {
                ToEmail = attendee.ApplicationUser.Email,
                Message = "Your request to join the trip has been rejected",
                UserName = attendee.ApplicationUser.UserName
            });
        }
    }
}