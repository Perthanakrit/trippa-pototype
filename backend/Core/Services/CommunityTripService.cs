using AutoMapper;
using AutoMapper.QueryableExtensions;
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
        private readonly IMailService _mail;
        private static string FolderName = "community_trip";

        public CommunityTripService(ICommunityTripRespository commuTripRespo, IMailService mail, IMapper mapper, IPhotoCloudinary cloudinary, IUserAccessor userAccessor, IAuthRespository authRespo)
        {
            _commuTripRespo = commuTripRespo;
            _mapper = mapper;
            _cloudinary = cloudinary;
            _userAccessor = userAccessor;
            _authRespo = authRespo;
            _mail = mail;
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

        public async Task RejectAttendeeAsync(Guid tripId, MailInput email)
        {
            // Reject the attendee -> Remove the attendee from the list

            CommunityTrip trip = await _commuTripRespo.GetByIdQueryable(tripId).Include(t => t.Attendees).ThenInclude(a => a.ApplicationUser).SingleOrDefaultAsync();
            string hostId = trip.Attendees.FirstOrDefault(x => x.IsHost).ApplicationUserId;
            if (trip == null || hostId != _userAccessor.GetUserId())
            {
                throw new ArgumentException("The trip is not found");
            }

            CommunityTripAttendee attendee = trip.Attendees.FirstOrDefault(x => x.ApplicationUser.Email == email.Email);

            if (attendee == null)
            {
                throw new ArgumentException("The attendee is not found");
            }

            trip.Attendees.Remove(attendee);

            bool isInvoke = await _commuTripRespo.SaveChangesAsync<int>() > 0;
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

        public async Task AcceptAttendeeAsync(Guid tripId, MailInput input)
        {
            CommunityTrip trip = await _commuTripRespo.GetByIdQueryable(tripId).Include(t => t.Attendees).ThenInclude(a => a.ApplicationUser).SingleOrDefaultAsync();

            CommunityTripAttendee attendee = trip.Attendees.FirstOrDefault(x => x.ApplicationUser.Email == input.Email);

            if (attendee == null)
            {
                throw new ArgumentException("The attendee is not found");
            }

            attendee.IsAccepted = true;

            bool invoke = await _commuTripRespo.SaveChangesAsync<int>() > 0;

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

        public async Task UpdateAttendeeAsync(Guid tripId)
        {
            // ExistedTrip
            CommunityTrip trip = await _commuTripRespo.GetByIdQueryable(tripId).Include(t => t.Attendees).ThenInclude(a => a.ApplicationUser)
                                                        .SingleOrDefaultAsync();

            if (trip == null)
            {
                throw new ArgumentException("The trip is not found");
            }

            // ExistedUser
            ApplicationUser user = await _authRespo.ExistedUserId(_userAccessor.GetUserId());

            if (user == null)
            {
                throw new ArgumentException($"The user is not found, {JsonConvert.SerializeObject(_userAccessor.GetUsername())}");
            }

            string host = trip.Attendees.FirstOrDefault(x => x.IsHost).ApplicationUserId;

            CommunityTripAttendee attendee = trip.Attendees.FirstOrDefault(x => x.ApplicationUser.UserName == user.UserName);

            // If the user is the host, cancel the activity
            if (attendee != null && host == user.Id)
                trip.IsActive = !trip.IsActive;

            // If the user is not the host, add or remove the user from the attendees
            if (attendee != null && host != user.Id)
                trip.Attendees.Remove(attendee);

            // AddAttendee
            if (attendee == null)
            {
                attendee = new CommunityTripAttendee
                {
                    CommunityTrip = trip,
                    ApplicationUser = user,
                    IsHost = false,
                };
                trip.Attendees.Add(attendee);
            }

            _commuTripRespo.Update(trip);

            bool invoke = await _commuTripRespo.SaveChangesAsync<int>() > 0;
            if (!invoke)
            {
                throw new Exception("The attendee is not updated");
            }
        }

        public async Task UpdateTripAsync(Guid id, CommuTripInput input)
        {
            CommunityTrip trip = await _commuTripRespo.GetByIdQueryable(id).Include(t => t.Appointment).SingleOrDefaultAsync();

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