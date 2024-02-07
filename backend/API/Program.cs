using System.Text;
using Core.Extension;
using Core.Middleware;
using Core.Utility;
using Domain.Entities;
using Infrastructure.Database;
using Infrastructure.Extension;
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

builder.Services.AddMapping();

#region Configure DI Container - Service Lifetimes - Infrastructure
builder.Services.AddInfraDependencyInjection(builder.Configuration);
#endregion

#region  Configure DI Container - Service Lifetimes - Business Services
builder.Services.AddCoreDependencyInjection();
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
app.UseApiKeyMiddleware();
app.UseExceptionMiddleware();

app.UseAuthorization();

app.MapControllers();

await SeedDatabase(); // Seed data to the database

app.Run();

async Task SeedDatabase()
{
    using var scope = app.Services.CreateScope();
    var dbcontext =
        scope.ServiceProvider.GetRequiredService<DatabaseContext>();

    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

    try
    {
        // Run migration scripts
        await dbcontext.Database.MigrateAsync();

        // Seed data to the project
        await Infrastructure.Seed.SeedData(dbcontext, userManager);
    }
    catch (Exception ex)
    {
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred during migrating the database.");
    }
}
