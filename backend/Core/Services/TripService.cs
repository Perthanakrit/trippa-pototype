using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Interface.Infrastructure.Database;
using Domain.Entities;

namespace Core.Services
{
    public class TripService : ITripService
    {
        private readonly ITripRepository _repository;

        public TripService(ITripRepository repository) // Inject the repository
        {
            _repository = repository; // Assign the repository to the private field
        }

        private TripServiceResponse ConvertToResponseModel(Trip entity)
        {
            return new TripServiceResponse
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
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

            Trip entity = new Trip
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

            return ConvertToResponseModel(existedTrip);
        }

        public async Task<TripServiceResponse> GetTripAsync(Guid tripId)
        {
            Trip existedTrip = await _repository.GetById(tripId);
            bool theTripIsNotExist = existedTrip == null;
            if (theTripIsNotExist)
            {
                throw new ArgumentException("The trip is not found");
            }

            return ConvertToResponseModel(existedTrip);
        }

        public async Task<TripsServiceResponseWithPaging> GetListOfAllTripsAsync()
        {
            List<Trip> listOfTrips = await _repository.GetAll();
            List<TripServiceResponse> listOfTripServiceResponse = listOfTrips.Select(x => ConvertToResponseModel(x)).ToList();

            return new TripsServiceResponseWithPaging
            {
                TotalRows = listOfTrips.Count,
                Trips = listOfTripServiceResponse
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
    }
}