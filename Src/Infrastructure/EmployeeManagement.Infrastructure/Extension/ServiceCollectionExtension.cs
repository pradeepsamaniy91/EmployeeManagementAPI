using EmployeeManagement.Domain.Entities;
using EmployeeManagement.Domain.Interfaces;
using EmployeeManagement.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeManagement.Infrastructure.Extension;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddInfaDependencies(this IServiceCollection services,string connectionstring)
    {
        services.AddDbContext<EmployeeManagementContext>(options =>
        {
            options.UseSqlServer(connectionstring);

        },ServiceLifetime.Transient);
        services.AddScoped<EmployeeManagementContext>();
        services.AddScoped<IEmployeeRepository, EmployeeRepository>();
        services.AddScoped<ITimeSheet, TimesheetService>();
        services.AddScoped<ITokenService, TokenService>();

        return services;
    }

}
