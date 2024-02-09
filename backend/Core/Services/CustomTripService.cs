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

        public Task<CustomTripServiceResponse> DeleteTripAsync(Guid provinceId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<CustomTripAndTrip>> GetListOfAllTripsAsync()
        {
            return await _repository.GetTripsInCustomTrips(); ;
        }

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

        public Task<CustomTripServiceResponse> UpdateTripAsync(Guid customTripId, CustomTripServiceInput input)
        {
            throw new NotImplementedException();
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
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
            };

            var result = await _repository.AddAsync(customTrip);
            await _repository.SaveChangesAsync();


        }


    }
}