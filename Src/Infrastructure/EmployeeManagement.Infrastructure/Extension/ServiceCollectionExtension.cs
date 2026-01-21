using EmployeeManagement.Domain.Entities;
using EmployeeManagement.Domain.Interfaces;
using EmployeeManagement.Infrastructure.Model;
using EmployeeManagement.Infrastructure.Persistence.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
namespace EmployeeManagement.Infrastructure.Extension;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddInfaDependencies(this IServiceCollection services, string connectionstring)
    {
        services.AddDbContext<EmployeeManagementContext>(options =>
        {
            options.UseSqlServer(connectionstring);

        }, ServiceLifetime.Transient);
        services.AddScoped<EmployeeManagementContext>();
        services.AddScoped<IEmployeeRepository, EmployeeRepository>();
        services.AddScoped<ITimeSheet, TimesheetService>();
        services.AddScoped<ITokenService, TokenService>();

        return services;
    }
    public static IServiceCollection AddJwtInfraDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        // Bind JwtSettings using Options pattern
        services.Configure<JwtSettings>(configuration.GetSection("JsonWebTokenKeys"));
        var jwtSettings = configuration.GetSection("JsonWebTokenKeys").Get<JwtSettings>();

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.RequireHttpsMetadata = false;
            options.SaveToken = true;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = jwtSettings.ValidateIssuerSigningKey,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.IssuerSigningKey)),
                ValidateIssuer = jwtSettings.ValidateIssuer,
                ValidIssuer = jwtSettings.ValidIssuer,
                ValidateAudience = jwtSettings.ValidateAudience,
                ValidAudience = jwtSettings.ValidAudience,
                RequireExpirationTime = jwtSettings.RequireExpirationTime,
                ValidateLifetime = true, // Always validate lifetime
                ClockSkew = TimeSpan.Zero // Keep strict expiration
            };
        });

        return services;
    }



}
