using System.Text;
using Core.Interface.Infrastructure.Database;
using Core.Interface.Services;
using Core.Mapping;
using Core.Services;
using Core.Utility;
using Domain.Entities;
using Infrastructure.Database;
using Infrastructure.Database.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

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

#region JWT Authentication
JwtSettings jWTSettings = new();
builder.Configuration.Bind(nameof(jWTSettings), jWTSettings);
builder.Services.AddSingleton(jWTSettings);
builder.Services.AddAuthentication(u =>
{
    u.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    u.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(u =>
{
    u.RequireHttpsMetadata = false;
    u.SaveToken = true;
    u.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jWTSettings.SecertKey)),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});
#endregion

builder.Services.AddAutoMapper(typeof(CustomTripMapping).Assembly);
builder.Services.AddAutoMapper(typeof(AuthServiceMapping).Assembly);

#region Configure DI Container - Service Lifetimes - Infrastructure
builder.Services.AddTransient<ITripRepository, TripRepository>();
builder.Services.AddTransient<ICustomTripRepository, CustomTripRepository>();
builder.Services.AddTransient<IAuthRespository, AuthRespository>();
#endregion

#region  Configure DI Container - Service Lifetimes - Business Services
builder.Services.AddScoped<ITripService, TripService>();
builder.Services.AddScoped<ICustomTripService, CustomTripService>();
builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<DatabaseContext>();
builder.Services.AddScoped<IAuthService, AuthService>();
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//app.UseAuthentication();

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
