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
        private readonly ICustomTripRepository _repository;
        private readonly IMapper _mapper;

        public CustomTripService(ICustomTripRepository repository, IMapper mapper)
        {
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
                UpdateUTC = entity.UpdateUTC,
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt,
            };
        }

        public Task<CustomTripServiceResponse> DeleteTripAsync(Guid provinceId)
        {
            throw new NotImplementedException();
        }

        public Task<TripsServiceResponseWithPaging> GetListOfAllTripsAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<CustomTripServiceResponse> GetTripAsync(Guid customTripId)
        {
            var mulitVal = await _repository.GetTripByCustomTripId(customTripId);
            Trip Trip = mulitVal.Item1;
            CustomTrip customTrip = mulitVal.Item2;
            //return ConvertToResponseModel(customTrip);

            bool customTripIsNotExist = Trip == null || customTrip == null;

            if (customTripIsNotExist)
            {
                throw new ArgumentException("The custom trip or trip is not found");
            }
            customTrip.Trip = Trip;
            return ConvertToResponseModel(customTrip);
        }

        public async Task<CustomTripServiceResponse> UpdateTripAsync(Guid customTripId, CustomTripServiceInput input)
        {
            var mulitVal = await _repository.GetTripByCustomTripId(customTripId); ;
            CustomTrip customTrip = mulitVal.Item2;

            bool customTripIsNotExist = customTrip == null;
            if (customTripIsNotExist)
            {
                throw new ArgumentException("The custom trip or trip is not found");
            }

            Trip trip = mulitVal.Item1;
            _mapper.Map(input, trip);
            customTrip.Trip = trip;
            customTrip.Trip.UpdatedAt = DateTime.UtcNow;
            customTrip.UpdatedAt = DateTime.UtcNow;

            await _repository.SaveChangesAsync();

            return ConvertToResponseModel(customTrip);
        }

        public async Task<CustomTripServiceResponse> CreateNewTripAsync(CustomTripServiceInput input)
        {
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
                Trip = trip,
            };

            var result = await _repository.AddAsync(customTrip);
            await _repository.SaveChangesAsync();

            return ConvertToResponseModel(result);
        }
    }
}