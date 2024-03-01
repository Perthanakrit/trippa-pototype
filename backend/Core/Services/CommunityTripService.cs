using AutoMapper;
using Core.Interface.Infrastructure.Cloudinary;
using Core.Interface.Infrastructure.Database;
using Core.Interface.security;
using Core.Interface.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Core.Services
{
    public class CommunityTripService : ICommunityTripService
    {
        private readonly ICommunityTripRespository _commuTripRespo;
        private readonly IMapper _mapper;
        private readonly IPhotoCloudinary _cloudinary;
        private readonly IUserAccessor _userAccessor;
        private readonly IAuthRespository _authRespo;
        private static string FolderName = "community_trip";

        public CommunityTripService(ICommunityTripRespository commuTripRespo, IMapper mapper, IPhotoCloudinary cloudinary, IUserAccessor userAccessor, IAuthRespository authRespo)
        {
            _commuTripRespo = commuTripRespo;
            _mapper = mapper;
            _cloudinary = cloudinary;
            _userAccessor = userAccessor;
            _authRespo = authRespo;
        }

        public async Task CreateNewTripAsync(CommuTripInput input)
        {
            ApplicationUser user = await GetCurrentUserAsync();

            CommunityTrip trip = new()
            {
                Location = input.Location,
                Duration = input.Duration,
                AgeRange = input.AgeRange,
                MaxAttendees = input.MaxAttendees,
                Appointment = new CommunityTripAppointment
                {
                    Date = input.Appointment.Date,
                    Time = new TimeOnly(input.Appointment.TimeHour, input.Appointment.TimeMinute),
                    Description = input.Appointment.Description
                },
                Attendees = new List<CommunityTripAttendee>
                {
                    new ()
                    {
                        ApplicationUser = user,
                        ApplicationUserId = user.Id,
                        IsHost = true,
                        IsAccepted = true
                    }
                }
            };

            await _commuTripRespo.AddAsync(trip);
            bool isInvoke = await _commuTripRespo.SaveChangesAsync<int>() > 0;
            if (!isInvoke)
            {
                throw new ArgumentException("Failed to create new trip");
            }
        }

        public async Task DeleteTripAsync(Guid id)
        {
            CommunityTrip trip = await _commuTripRespo.GetByIdQueryable(id).Include(t => t.Photos).FirstOrDefaultAsync();
            if (trip is null)
            {
                throw new ArgumentException("Trip not found");
            }
            trip = _commuTripRespo.Remove(trip);
            bool isInvoke = await _commuTripRespo.SaveChangesAsync<int>() > 0;
            if (!isInvoke)
            {
                throw new ArgumentException($"Failed to delete {trip.Id}");
            }

            for (int i = 0; i < trip.Photos.Count; i++)
            {
                string result = await _cloudinary.DeletePhotoAsync(trip.Photos.ElementAt(i).PublicId);
                if (result is null)
                {
                    throw new ArgumentException("Failed to delete photo");
                }
            }
        }

        public async Task<List<CommuTripResponse>> GetListOfAllTripsAsync()
        {
            List<CommunityTrip> trips = await _commuTripRespo.GetAllQueryable().AsNoTracking()
                                                .Include(t => t.Attendees).ThenInclude(a => a.ApplicationUser).ThenInclude(u => u.Image)
                                                .Select(t => new CommunityTrip
                                                {
                                                    Id = t.Id,
                                                    Location = t.Location,
                                                    Duration = t.Duration,
                                                    AgeRange = t.AgeRange,
                                                    MaxAttendees = t.MaxAttendees,
                                                    Attendees = t.Attendees.Where(a => a.IsHost).Select(a => new CommunityTripAttendee
                                                    {
                                                        ApplicationUser = a.ApplicationUser,
                                                        IsHost = a.IsHost,
                                                        IsAccepted = a.IsAccepted
                                                    }).ToList(),
                                                })
                                                .ToListAsync();

            // throw new ArgumentException($"{JsonConvert.SerializeObject(trips)}");

            List<CommuTripResponse> res = trips.Select(t => _mapper.Map<CommuTripResponse>(t)).ToList();

            return res;
        }

        public async Task<CommuTripResponse> GetTripAsync(Guid id)
        {
            CommuTripResponse res = await _commuTripRespo.GetTripWithRelatedDataAsync(id);
            if (res is null)
            {
                throw new ArgumentException("Trip not found");
            }
            return res;
        }

        public Task RejectAttendeeAsync(Guid tripId, string userId)
        {
            throw new NotImplementedException();
        }

        public Task AcceptAttendeeAsync(Guid tripId, string userId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAttendeeAsync(Guid tripId)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateTripAsync(Guid id, CommuTripInput input)
        {
            CommunityTrip trip = await _commuTripRespo.GetByIdQueryable(id).Include(t => t.Appointment).Select(t =>
                new CommunityTrip
                {
                    Id = t.Id,
                    Location = t.Location,
                    Duration = t.Duration,
                    AgeRange = t.AgeRange,
                    MaxAttendees = t.MaxAttendees,
                    Appointment = t.Appointment
                }).FirstOrDefaultAsync(
            );

            if (trip is null)
            {
                throw new ArgumentException("Trip not found");
            }

            trip.Location = input.Location;
            trip.Duration = input.Duration;
            trip.AgeRange = input.AgeRange;
            trip.MaxAttendees = input.MaxAttendees;
            trip.Appointment.Date = input.Appointment.Date;
            trip.Appointment.Time = new TimeOnly(input.Appointment.TimeHour, input.Appointment.TimeMinute);
            trip.Appointment.Description = input.Appointment.Description;

            _commuTripRespo.Update(trip);

            bool isInvoke = await _commuTripRespo.SaveChangesAsync<int>() > 0;
            if (!isInvoke)
            {
                throw new ArgumentException("Failed to update trip");
            }
        }

        public async Task UploadPhotoAsync(Guid tripId, IFormFile file)
        {
            CommunityTrip trip = await _commuTripRespo.GetByIdQueryable(tripId).Include(t => t.Photos).FirstOrDefaultAsync();
            if (trip is null)
            {
                throw new ArgumentException("Trip not found");
            }

            if (trip.Photos.Count >= 3)
            {
                throw new ArgumentException("Photo limit reached");
            }

            UploadPhotoResult result = await _cloudinary.AddAsync(file, FolderName);
            if (result is null)
            {
                throw new ArgumentException("Failed to upload photo");
            }

            trip.Photos.Add(new CommunityTripPhoto
            {
                CommunityTripId = trip.Id,
                PublicId = result.PublicId,
                Url = result.Url
            });


            _commuTripRespo.Update(trip);
            bool isInvoke = await _commuTripRespo.SaveChangesAsync<int>() > 0;
            if (!isInvoke)
            {
                throw new ArgumentException("Failed to upload photo");
            }

            // throw new ArgumentException($"{JsonConvert.SerializeObject(trip)}");
        }

        private async Task<ApplicationUser> GetCurrentUserAsync()
        {
            string id = _userAccessor.GetUserId();
            // throw new ArgumentException($"User not found {JsonConvert.SerializeObject(id)}");
            ApplicationUser user = await _authRespo.ExistedUserId(id);
            if (user is null)
            {
                throw new ArgumentException($"User not found");
            }

            return user;
        }
    }
}