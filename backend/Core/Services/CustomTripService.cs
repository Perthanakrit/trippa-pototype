using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Core.Interface.Infrastructure.Database;
using Core.Interface.Services;
using Domain.Entities;

namespace Core.Services
{
    public class CustomTripService : ICustomTripService
    {
        private readonly IAuthRespository _authRepository;
        private readonly ICustomTripRepository _repository;
        private readonly IMapper _mapper;

        public CustomTripService(IAuthRespository authRepository, ICustomTripRepository repository, IMapper mapper)
        {
            _authRepository = authRepository;
            _repository = repository;
            _mapper = mapper;
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

        public async Task<CustomTripServiceResponse> UpdateTripAsync(Guid customTripId, CustomTripServiceInput input)
        {
            throw new NotImplementedException();
        }

        public async Task<CustomTripServiceResponse> CreateNewTripAsync(CustomTripServiceInput input)
        {
            // TODO: Check if the user is exist
            bool isUserExit = await _authRepository.ExistedUserId(input.UserId);
            if (!isUserExit)
            {
                throw new ArgumentException("The user is not exist");
            }
            // TODO: Check if the name is already taken
            bool isNameAlreadyTaken = await _repository.Exists(t => t.Trip.Name == input.Name);
            if (isNameAlreadyTaken)
            {
                throw new ArgumentException("The name is already taken");
            }

            Trip trip = new Trip()
            {
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
            };

            _mapper.Map(input, trip);

            CustomTrip customTrip = new CustomTrip()
            {
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                Trip = trip
            };

            var result = await _repository.AddAsync(customTrip);
            await _repository.SaveChangesAsync();

            return ConvertToResponseModel(result);
        }


    }
}