using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;

namespace Core.Services
{
    public interface ITripService
    {
        Task<TripServiceResponse> CreateNewTripAsync(TripServiceInput input);
        Task<TripServiceResponse> UpdateTripAsync(Guid provinceId, TripServiceInput input);
        Task<TripServiceResponse> DeleteTripAsync(Guid provinceId);
        Task<TripServiceResponse> GetTripAsync(Guid provinceId);
        Task<TripsServiceResponseWithPaging> GetListOfAllTripsAsync();
        //Task<ProvinceServiceResponseWithPaging> GetListOfAllActiveProvincesAsync(int pageNumber, int pageSize);
    }

    public class TripServiceInput
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class TripServiceResponse : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Landmark { get; set; }
        public string Duration { get; set; }
        public float Price { get; set; }
        public float Fee { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
    }

    public class TripsServiceResponseWithPaging
    {
        public int TotalRows { get; set; }
        //public int PageNumber { get; set; }
        //public int PageSize { get; set; }
        public List<TripServiceResponse> Trips { get; set; } = new List<TripServiceResponse>();
    }
}