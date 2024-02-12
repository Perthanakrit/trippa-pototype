using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
                        Name = "Trip 1",
                        Description = "Trip 1 Description",
                        Landmark = "Landmark 1",
                        Duration = "Duration 1",
                        Price = 100,
                        Fee = 10,
                        Origin = "Origin 1",
                        Destination = "Destination 1",
                        HostId = users[2].Id,
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
                        IsCustomTrip = false,
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow,
                        IsActive = true
                    },
                    new Trip
                    {
                        Name = "Trip 2",
                        Description = "Trip 2 Description",
                        Landmark = "Landmark 2",
                        Duration = "Duration 2",
                        Price = 200,
                        Fee = 20,
                        Origin = "Origin 2",
                        Destination = "Destination 2",
                        HostId = users[2].Id,
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
                        IsCustomTrip = false,
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow,
                        IsActive = true
                    },

                };

                await context.Trips.AddRangeAsync(trips);
                await context.SaveChangesAsync();
            }

            if (!await context.CustomTrips.AnyAsync())
            {
                List<CustomTrip> customTrips = new List<CustomTrip> {
                    new CustomTrip
                    {
                        Trip = new Trip {
                            Name = "My Trip 1",
                            Description = "My Trip 1 Description",
                            Landmark = "Landmark 1",
                            Duration = "3 Days 2 Nights",
                            Price = 500,
                            Fee = 5,
                            Origin = "Bangkok",
                            Destination = "Phuket",
                            HostId = users[0].Id,
                            Attendee = new List<TripAttendee>()
                            {
                                new TripAttendee
                                {
                                    ApplicationUser = users[0],
                                    IsHost = true,
                                    AttendAt = DateTime.UtcNow,
                                    CancelAt = null,
                                },
                                new TripAttendee
                                {
                                    ApplicationUser = users[1],
                                    IsHost = false,
                                    AttendAt = DateTime.UtcNow,
                                    CancelAt = null,
                                }
                            },
                            TypeOfTripId = typeOfTrips[2].Id,
                            TypeOfTrip = typeOfTrips[2],
                            TripAgenda = new List<TripAgenda>
                            {
                                new TripAgenda
                                {
                                    Id = 1,
                                    Date = new DateOnly(2024, 5, 12),
                                    Time = new TimeOnly(8, 30),
                                    Description = "Start Trip"
                                },
                                new TripAgenda
                                {
                                    Id = 2,
                                    Date = new DateOnly(2024, 5, 15),
                                    Time = new TimeOnly(17, 30),
                                    Description = "End Trip"
                                }
                            },
                            IsCustomTrip = true,
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow,
                            IsActive = true
                        },

                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow,
                        IsActive = true
                    },
                    new CustomTrip
                    {
                        Trip = new Trip {
                            Name = "My Trip 2",
                            Description = "My Trip 2 Description",
                            Landmark = "Landmark 2",
                            Duration = "3 Days 2 Nights",
                            Price = 500,
                            Fee = 5,
                            Origin = "Bangkok",
                            Destination = "Nakhon Ratchasima",
                            HostId = users[1].Id,
                            Attendee = new List<TripAttendee>()
                            {
                                new TripAttendee
                                {
                                    ApplicationUser = users[0],
                                    IsHost = false,
                                    AttendAt = DateTime.UtcNow,
                                    CancelAt = null,
                                },
                                new TripAttendee
                                {
                                    ApplicationUser = users[1],
                                    IsHost = true,
                                    AttendAt = DateTime.UtcNow,
                                    CancelAt = null,
                                }
                            },
                            TypeOfTripId = typeOfTrips[0].Id,
                            TypeOfTrip = typeOfTrips[0],
                            TripAgenda = new List<TripAgenda>
                            {
                                    new TripAgenda
                                {
                                    Id = 1,
                                    Date = new DateOnly(2024, 5, 12),
                                    Time = new TimeOnly(8, 30),
                                    Description = "Start Trip"
                                },
                                new TripAgenda
                                {
                                    Id = 2,
                                    Date = new DateOnly(2024, 5, 15),
                                    Time = new TimeOnly(17, 30),
                                    Description = "End Trip"
                                }
                            },
                            IsCustomTrip = true,
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow,
                            IsActive = true
                        },

                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow,
                        IsActive = true
                    },
                };
                await context.CustomTrips.AddRangeAsync(customTrips);
                await context.SaveChangesAsync();
            }


        }
    }
}
