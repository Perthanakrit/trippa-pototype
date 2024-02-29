using Core.Utility;
using Domain.Entities;
using Infrastructure.Database;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class Seed
    {
        public static async Task SeedData(DatabaseContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            List<ApplicationUser> users = null;
            List<TypeOfTrip> typeOfTrips = new List<TypeOfTrip>()
            {
                new()
                {
                    Id = Guid.NewGuid(),
                    Name = "สายเที่ยว"
                },
                new ()
                {
                    Id = Guid.NewGuid(),
                    Name = "สายมู"
                },
                new ()
                {
                    Id = Guid.NewGuid(),
                    Name = "สายกิน"
                },
            };

            if (!await roleManager.RoleExistsAsync(SD.GeneralUser))
            {
                await roleManager.CreateAsync(new IdentityRole(SD.GeneralUser));
                await roleManager.CreateAsync(new IdentityRole(SD.TourUser));
            }

            if (!await context.Users.AnyAsync())
            {
                users = new List<ApplicationUser> {
                    new ApplicationUser
                    {
                        Id = "13b51690-a6e1-40b5-ae7b-651626f07a80",
                        DisplayName = "User 1",
                        UserName = "user1",
                        Email = "user1@test.com",
                        Contacts = new List<Contact>
                        {
                            new Contact
                            {
                                Channel = "Instagram",
                                Name = "user1_"
                            },
                            new Contact
                            {
                                Channel = "Phone",
                                Name = "012345678"
                            }
                        },
                        Image = new UserPhoto
                        {
                            Url = "https://fastly.picsum.photos/id/1/367/267.jpg?hmac=jZdc5TviQPVhxLyvyU8siO-I5FMVXVZpBhsBYKbBJpM",
                            UserId = "13b51690-a6e1-40b5-ae7b-651626f07a80",
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow,
                            IsActive = true
                        }
                    },
                    new ApplicationUser
                    {
                        Id="b3f0d075-4c7c-453b-86f0-7c6187fa4f8c",
                        DisplayName = "User 2",
                        UserName = "user2",
                        Email = "user2@test.com",
                        Contacts = new List<Contact>
                        {
                            new Contact
                            {
                                Channel = "Instagram",
                                Name = "user2_"
                            },
                            new Contact
                            {
                                Channel = "Phone",
                                Name = "987654321"
                            }
                        },
                        Image = new UserPhoto
                        {
                            Url = "https://fastly.picsum.photos/id/1/367/267.jpg?hmac=jZdc5TviQPVhxLyvyU8siO-I5FMVXVZpBhsBYKbBJpM",
                            UserId = "13b51690-a6e1-40b5-ae7b-651626f07a80",
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow,
                            IsActive = true
                        }
                    },
                    new ApplicationUser
                    {
                        Id="36b0b829-dba7-4b0b-8186-6e0da09ccf4e",
                        DisplayName = "TourTrip",
                        UserName = "Tourtrip",
                        Email = "tourtrip@mail.com",
                        Contacts = new List<Contact>
                        {
                            new Contact
                            {
                                Channel = "Line",
                                Name = "@123tour"
                            },
                            new Contact
                            {
                                Channel = "Phone",
                                Name = "123456789"
                            }
                        },
                        Image = new UserPhoto
                        {
                            Url = "https://fastly.picsum.photos/id/1/367/267.jpg?hmac=jZdc5TviQPVhxLyvyU8siO-I5FMVXVZpBhsBYKbBJpM",
                            UserId = "13b51690-a6e1-40b5-ae7b-651626f07a80",
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow,
                            IsActive = true
                        }
                    },
                };

                for (int i = 0; i < users.Count; i++)
                {
                    await userManager.CreateAsync(users[i], "Pa$$w0rd");
                }

            }

            if (!await context.TypeOfTrips.AnyAsync())
            {
                await context.TypeOfTrips.AddRangeAsync(typeOfTrips);
                await context.SaveChangesAsync();
            }

            if (!await context.Trips.AnyAsync())
            {
                List<Trip> trips = new()
                {
                    new Trip
                    {
                        Id = Guid.NewGuid(),
                        Name = "Trip 1",
                        Description = "Trip 1 Description",
                        Landmark = "Landmark 1",
                        Duration = "Duration 1",
                        Price = 100,
                        Fee = 10,
                        Origin = "Origin 1",
                        Destination = "Destination 1",
                        MaxAttendees = 10,
                        // HostId = users[2].Id,
                        TripAgenda = new List<TripAgenda>
                        {
                            new TripAgenda
                            {
                                Id = 1,
                                Date = new DateOnly(2024, 3, 1),
                                Time = new TimeOnly(9, 30),
                                Description = "Start Trip"
                            },
                            new TripAgenda
                            {
                                Id = 2,
                                Date = new DateOnly(2024, 3, 3),
                                Time = new TimeOnly(16, 30),
                                Description = "End Trip"
                            }
                        },
                        Attendee = new List<TripAttendee>()
                        {
                            new TripAttendee
                            {
                                ApplicationUser = users[2],
                                IsHost = true,
                                AttendAt = DateTime.UtcNow,
                                CancelAt = null,
                            },
                        },
                        TypeOfTripId = typeOfTrips[0].Id,
                        TypeOfTrip = typeOfTrips[0],

                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow,
                        IsActive = true
                    },
                    new Trip
                    {
                        Id = Guid.NewGuid(),
                        Name = "Trip 2",
                        Description = "Trip 2 Description",
                        Landmark = "Landmark 2",
                        Duration = "Duration 2",
                        Price = 200,
                        Fee = 20,
                        Origin = "Origin 2",
                        Destination = "Destination 2",
                        MaxAttendees = 20,
                        TripAgenda = new List<TripAgenda>
                        {
                            new TripAgenda
                            {
                                Id = 1,
                                Date = new DateOnly(2024, 4, 1),
                                Time = new TimeOnly(9, 30),
                                Description = "Start Trip"
                            },
                            new TripAgenda
                            {
                                Id = 2,
                                Date = new DateOnly(2024, 4, 3),
                                Time = new TimeOnly(16, 30),
                                Description = "End Trip"
                            }
                        },
                        Attendee = new List<TripAttendee>()
                        {
                            new TripAttendee
                            {
                                ApplicationUser = users[2],
                                IsHost = true,
                                AttendAt = DateTime.UtcNow,
                                CancelAt = null,
                            },
                        },
                        TypeOfTripId = typeOfTrips[2].Id,
                        TypeOfTrip = typeOfTrips[2],
                        // IsCustomTrip = false,
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow,
                        IsActive = true
                    },

                };

                await context.Trips.AddRangeAsync(trips);
                await context.SaveChangesAsync();
            }

            if (!await context.CommunityTrips.AnyAsync())
            {
                List<CommunityTrip> communityTrips = new()
                {
                    new CommunityTrip
                    {
                        Id = Guid.NewGuid(),
                        Location = "Location 1",
                        Duration = "1 Day",
                        AgeRange = "20-30",
                        MaxAttendees = 10,
                        Appointment = new CommunityTripAppointment
                        {
                            Id = 1,
                            Description = "At Trian Station",
                            Date = new DateOnly(),
                            Time = new TimeOnly(),
                        },
                        Attendees = new List<CommunityTripAttendee>
                        {
                            new CommunityTripAttendee
                            {
                                ApplicationUser = users[2],
                                IsHost = true,
                                AttendAt = DateTime.UtcNow,
                                CancelAt = null,
                            }
                        },
                        Photos = new List<CommunityTripPhoto>
                        {
                            new CommunityTripPhoto
                            {
                                Url = "https://fastly.picsum.photos/id/10/367/267.jpg?hmac=XRdepQX9y39tepelazZaEAxb6SbCWtual9_w28FPb6U",
                                CreatedAt = DateTime.UtcNow,
                                UpdatedAt = DateTime.UtcNow,
                                IsActive = true
                            }
                        },
                        CreatedAt = DateTime.UtcNow,

                        UpdatedAt = DateTime.UtcNow,
                        IsActive = true
                    },
                    new CommunityTrip
                    {
                        Id = Guid.NewGuid(),
                        Location = "Location 2",
                        Duration = "2 Days 1 Night",
                        AgeRange = "20-30",
                        MaxAttendees = 10,
                        Appointment = new CommunityTripAppointment
                        {
                            Id = 1,
                            Description = "At Trian Station",
                            Date = new DateOnly(),
                            Time = new TimeOnly(),
                        },
                        Attendees = new List<CommunityTripAttendee>
                        {
                            new CommunityTripAttendee
                            {
                                ApplicationUser = users[2],
                                IsHost = true,
                                AttendAt = DateTime.UtcNow,
                                CancelAt = null,
                            }
                        },
                        Photos = new List<CommunityTripPhoto>
                        {
                            new CommunityTripPhoto
                            {
                                Url = "https://fastly.picsum.photos/id/11/367/267.jpg?hmac=Pqqy5lI70sWPJXIxlIgdd2tcga3zmI8Otf9rnP5t8T0",
                                CreatedAt = DateTime.UtcNow,
                                UpdatedAt = DateTime.UtcNow,
                                IsActive = true
                            }
                        },
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow,
                        IsActive = true
                    },
                };

                await context.CommunityTrips.AddRangeAsync(communityTrips);
                await context.SaveChangesAsync();
            }


        }
    }
}
