using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Interface.Infrastructure.Database;
using Core.Interface.security;
using Domain.Entities;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace Core.Services
{
    public class TripService : ITripService
    {
        private readonly ITripRepository _repository;
        private readonly IAuthRespository _authRepo;
        private readonly IUserAccessor _userAccessor;

        public TripService(ITripRepository repository, IAuthRespository authRepo, IUserAccessor userAccessor) // Inject the repository
        {
            _repository = repository; // Assign the repository to the private field
            _authRepo = authRepo;
            _userAccessor = userAccessor;
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

        public async Task<TripServiceResponse> CreateNewTripAsync(TripServiceInput input)
        {
            bool isTheNameAlreadyTaken = await _repository.Exists(x => x.Name == input.Name); // TODO: Check if the name is already taken
            if (isTheNameAlreadyTaken)
            {
                throw new ArgumentException("The name is already taken");
            }

            Trip entity = new()
            {
                Name = input.Name,
                Description = input.Description
            };

            Trip insertedEntity = await _repository.AddAsync(entity); // TODO: Check if the entity is inserted successfully
            await _repository.SaveChangesAsync();

            return ConvertToResponseModel(insertedEntity);
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
            await _repository.SaveChangesAsync();
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
            await _repository.SaveChangesAsync();

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

            // ExistedUser
            ApplicationUser user = await _authRepo.FindByUsername(_userAccessor.GetUsername());
            if (user == null)
            {
                throw new ArgumentException($"The user is not found, {JsonConvert.SerializeObject(_userAccessor.GetUsername())}");
            }

            string Host = trip.HostId;

            TripAttendee attendee = trip.Attendee.FirstOrDefault(x => x.ApplicationUser.UserName == user.UserName);
            // If the user is the host, cancel the activity
            if (attendee != null && Host == user.Id)
                trip.IsActive = !trip.IsActive;

            // If the user is not the host, add or remove the user from the attendees
            if (attendee != null && Host != user.Id)
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

            await _repository.SaveChangesAsync();
        }
    }
}