using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Interface.Infrastructure.Database;
using Core.Interface.Services;
using Domain.Entities;

namespace Core.Services
{
    public class CustomTripService : ICustomTripService
    {
        private readonly ICustomTripRepository _repository;

        public CustomTripService(ICustomTripRepository repository)
        {
            _repository = repository;
        }

        private CustomTripServiceResponse ConvertToResponseModel(CustomTrip entity)
        {
            return new CustomTripServiceResponse
            {
                Id = entity.Id,
                Trip = entity.Trip,
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

        public async Task<CustomTripServiceResponseWithTrip> GetTripAsync(Guid customTripId)
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

            return new CustomTripServiceResponseWithTrip
            {
                CustomTrip = customTrip,
                Trip = Trip
            };
        }

        public Task<CustomTripServiceResponse> UpdateTripAsync(Guid provinceId, CustomTripServiceInput input)
        {
            throw new NotImplementedException();
        }

        public Task<CustomTripServiceResponse> CreateNewTripAsync(CustomTripServiceInput input)
        {
            throw new NotImplementedException();
        }
    }
}