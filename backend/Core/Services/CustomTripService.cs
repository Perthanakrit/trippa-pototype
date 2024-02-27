using System.Runtime.CompilerServices;
using System.Text.Json;
using AutoMapper;
using Core.Interface.Infrastructure.Database;
using Core.Interface.security;
using Core.Interface.Services;
using Domain.Entities;

namespace Core.Services
{
    public class CustomTripService : ICustomTripService
    {
        private readonly IAuthRespository _authRepository;
        private readonly ICustomTripRepository _repository;
        private readonly IMapper _mapper;
        private readonly IUserAccessor _userAccessor;

        public CustomTripService(IAuthRespository authRepository, ICustomTripRepository repository, IMapper mapper, IUserAccessor userAccessor)
        {
            _authRepository = authRepository;
            _repository = repository;
            _mapper = mapper;
            _userAccessor = userAccessor;
        }

        private CustomTripServiceResponse ConvertToResponseModel(CustomTrip entity)
        {
            return new CustomTripServiceResponse
            {
                Id = entity.Id,
                Trip = entity.Trip,
                TripId = entity.TripId,
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt,
            };
        }

        public async Task DeleteTripAsync(Guid id)
        {
            CustomTrip customTrip = await _repository.GetById(id);
            if (customTrip == null)
            {
                throw new ArgumentException("The custom trip is not found");
            }
            await _repository.DeleteIncludeTripAsync(id);
        }

        /*
            return {
                destitaion
                maxAttendee
                duration
                startDate
                endDate
                maxAttendee
                attendees (length)
                Host {
                    displayName
                    image
                }
            }
        */
        public async Task<List<CustomTripAndTrip>> GetListOfAllTripsAsync()
        {
            return await _repository.GetTripsInCustomTrips(); ;
        }

        /*
            return {
                destitaion
                maxAttendee
                duration
                startDate
                endDate
                Host {
                    displayName
                    image
                    contants
                }        
        */
        public async Task<CustomTripAndTrip> GetTripAsync(Guid customTripId)
        {
            var result = await _repository.GetTripByCustomTripId(customTripId);
            //return ConvertToResponseModel(customTrip);

            bool customTripIsNotExist = result == null;

            if (customTripIsNotExist)
            {
                throw new ArgumentException("The custom trip or trip is not found");
            }

            return result;
        }

        public async Task<CustomTripServiceResponse> UpdateTripAsync(Guid customTripId, CustomTripServiceInput input)
        {
            CustomTrip customTrip = await _repository.GetById(customTripId);
            if (customTrip != null)
            {
                throw new ArgumentException("The custom trip is not found");
            }

            throw new ArgumentException($"{JsonSerializer.Serialize(customTrip)}");

            customTrip.Trip.Name = input.Name;
            customTrip.Trip.Description = input.Description;
            customTrip.Trip.Landmark = input.Landmark;
            customTrip.Trip.Duration = input.Duration;
            customTrip.Trip.Price = input.Price;
            customTrip.Trip.Fee = input.Fee;
            customTrip.Trip.Origin = input.Origin;
            customTrip.Trip.Destination = input.Destination;
            customTrip.Trip.MaxAttendees = input.MaxAttendee;
            customTrip.Trip.TypeOfTripId = input.TypeOfTripId;
            customTrip.Trip.TripAgenda = input.TripAgendas.Select(t => new TripAgenda
            {
                Description = t.Description,
                Date = t.Date,
                Time = t.Time,
            }).ToList();

            // customTrip.Trip.Photos = input.Photos.Select(x => new TripPhoto
            // {
            //     Id = Guid.NewGuid(),
            //     Url = x.Url,
            // }).ToList();

            // If trip has images, then update the images


            throw new System.NotImplementedException();
        }

        public async Task CreateNewTripAsync(CustomTripServiceInput input)
        {
            // TODO: Check if the user is exist
            bool isUserExit = await _authRepository.ExistedUserId(_userAccessor.GetUserId());
            if (!isUserExit)
            {
                throw new ArgumentException($"The user is not exist");
            }

            // TODO: Check if the name is already taken
            bool isNameAlreadyTaken = await _repository.Exists(t => t.Trip.Name == input.Name);
            if (isNameAlreadyTaken)
            {
                throw new ArgumentException("The name is already taken");
            }

            // TODO: Check if the type of trip is exist
            //bool isTypeOfTripExist = await _repository.ExistedTypeOfTripId(input.TypeOfTripId);

            List<TripAgenda> tripAgendas = new();

            for (int i = 0; i < input.TripAgendas.Count; i++)
            {
                // string[] date = input.TripAgendas[i].Date;
                var time = input.TripAgendas[i].Time;
                TripAgenda tripAgenda = new()
                {
                    Id = i + 1,
                    Description = input.TripAgendas[i].Description,
                    Date = input.TripAgendas[i].Date,
                    Time = time,//new TimeOnly(int.Parse(time[0]), int.Parse(time[1]))
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
                Photos = input.Photos.Select(x => new TripPhoto
                {
                    Id = Guid.NewGuid(),
                    Url = x.Url,
                }).ToList(),
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
                HostId = _userAccessor.GetUserId(),
                IsCustomTrip = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                IsActive = true
            };

            CustomTrip customTrip = new()
            {
                Trip = trip,
            };

            var result = await _repository.AddAsync(customTrip);
            bool invoke = await _repository.SaveChangesAsync<int>() > 0;
            if (!invoke)
            {
                throw new ArgumentException("The custom trip is not created");
            }
        }
    }
}