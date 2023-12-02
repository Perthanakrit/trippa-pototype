using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class Seed
    {
        public static async Task SeedData(DatabaseContext context)
        {
            // if (!await context.CustomTrips.AnyAsync())
            // {
            //     List<CustomTrip> customTrips = new List<CustomTrip> {
            //         new CustomTrip
            //         {
            //             Trip = new Trip {
            //                 Name = "My Trip 1",
            //                 Description = "My Trip 1 Description",
            //                 Landmark = "Landmark 1",
            //                 Duration = "Duration 1",
            //                 Price = 500,
            //                 Fee = 5,
            //                 Origin = "Origin 1",
            //                 Destination = "Destination 1",
            //                 CreatedAt = DateTime.UtcNow,
            //                 UpdatedAt = DateTime.UtcNow,
            //                 IsActive = true
            //             },
            //             CreatedAt = DateTime.UtcNow,
            //             UpdatedAt = DateTime.UtcNow,
            //             IsActive = true
            //         },
            //     };
            //     await context.CustomTrips.AddRangeAsync(customTrips);
            //     await context.SaveChangesAsync();
            // }

            if (!context.Trips.Any())
            {
                List<Trip> trips = new List<Trip>
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
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow,
                        IsActive = true
                    },
                    new Trip
                    {
                        Name = "Trip 3",
                        Description = "Trip 3 Description",
                        Landmark = "Landmark 3",
                        Duration = "Duration 3",
                        Price = 300,
                        Fee = 30,
                        Origin = "Origin 3",
                        Destination = "Destination 3",
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow,
                        IsActive = true
                    },
                    new Trip
                    {
                        Name = "Trip 4",
                        Description = "Trip 4 Description",
                        Landmark = "Landmark 4",
                        Duration = "Duration 4",
                        Price = 400,
                        Fee = 40,
                        Origin = "Origin 4",
                        Destination = "Destination 4",
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow,
                        IsActive = true
                    },
                    new Trip
                    {
                        Name = "Trip 5",
                        Description = "Trip 5 Description",
                        Landmark = "Landmark 5",
                        Duration = "Duration 5",
                        Price = 500,
                        Fee = 50,
                        Origin = "Origin 5",
                        Destination = "Destination 5",
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow,
                        IsActive = true
                    },
                    new Trip
                    {
                        Name = "Trip 6",
                        Description = "Trip 6 Description",
                        Landmark = "Landmark 6",
                        Duration = "Duration 6",
                        Price = 600,
                        Fee = 60,
                        Origin = "Origin 6",
                        Destination = "Destination 6",
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow,
                        IsActive = true
                    },
                };

                await context.Trips.AddRangeAsync(trips);
                await context.SaveChangesAsync();
            }
        }
    }
}
