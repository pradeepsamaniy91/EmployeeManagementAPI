using EmployeeManagement.Application.Services;
using EmployeeManagement.Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace EmployeeManagement.Application.Extensions;

public static class EmployeeServiceExtensions
{
    public static IServiceCollection EmployeeManagementDependencies(this IServiceCollection services)
    {
        // Use AddScoped for standard Web API repositories (one per request)
        services.AddScoped<IEmployeeRepository, EmployeeService>();
        services.AddScoped<IUserRepository, UserService>();

        return services;
    }
}

