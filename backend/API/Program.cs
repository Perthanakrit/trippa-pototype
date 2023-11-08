using Core.Interface.Infrastructure.Database;
using Core.Services;
using Infrastructure.Database;
using Infrastructure.Database.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DatabaseContext>(opt =>
{
    opt.UseNpgsql(builder.Configuration.GetConnectionString(
      "DefaultConnection"));
});


#region Configure IOption Pattern 
#endregion

#region Configure DI Container - Service Lifetimes - Infrastructure
builder.Services.AddTransient<ITripRepository, TripRepository>();
#endregion

#region  Configure DI Container - Service Lifetimes - Business Services
builder.Services.AddScoped<ITripService, TripService>();
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

await SeedDatabase(); // Seed data to the database

app.Run();

async Task SeedDatabase()
{
    using (var scope = app.Services.CreateScope())
    {
        var dbcontext =
          scope.ServiceProvider.GetRequiredService<DatabaseContext>();

        // Run migration scripts
        await dbcontext.Database.MigrateAsync();

        // Seed data to the project
        await Infrastructure.Seed.SeedData(dbcontext);
    }
}
