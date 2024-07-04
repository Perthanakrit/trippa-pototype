using AutoMapper;
using AutoMapper.QueryableExtensions;
using Core.Interface.Infrastructure.Database;
using Core.Interface.Services;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database.Repositories
{
    public class CommunityTripRespository : BaseRepository<CommunityTrip>, ICommunityTripRespository
    {
        private readonly IMapper _mapper;

        public CommunityTripRespository(DatabaseContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public Task<CommuTripResponse> GetTripWithRelatedDataAsync(Guid tripId)
        {
            return _context.CommunityTrips
                        .AsNoTracking()
                        .AsSplitQuery()
                        .Where(t => t.Id == tripId)
                        .ProjectTo<CommuTripResponse>(_mapper.ConfigurationProvider)
                        .FirstOrDefaultAsync();
        }

        // public Task<CommuTripResponse> GetTripWithRelatedDataAsync(Guid tripId)
        // {
        //     return base._context.CommunityTrips
        //         .AsNoTracking()
        //         .Where(t => t.Id == tripId)
        //         .Select(t => new CommuTripResponse
        //         {
        //             Id = t.Id,
        //             Location = t.Location,
        //             Duration = t.Duration,
        //             AgeRange = t.AgeRange,
        //             MaxAttendees = t.MaxAttendees,
        //             Appointment = new CommuTripAppointmentDto
        //             {
        //                 Date = t.Appointment.Date,
        //                 Time = t.Appointment.Time,
        //                 Description = t.Appointment.Description
        //             },
        //             Attendees = t.Attendees.Select(a => new CommuTripAttendeeDto
        //             {
        //                 UserName = a.ApplicationUser.UserName,
        //                 UserPhoto = a.ApplicationUser.Image.Url,
        //                 IsHost = a.IsHost,
        //                 IsAccepted = a.IsAccepted
        //             }).ToList(),
        //             Photos = t.Photos.Select(p => new CommuTripPhoto
        //             {
        //                 Url = p.Url
        //             }).ToList()
        //         })
        //         .AsSplitQuery()
        //         .FirstOrDefaultAsync();
        // }
    }
}