using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeManagement.Infrastructure.Extension;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddInfaDependencies(this IServiceCollection services)
    {
        services.AddTransient<IServiceCollection>();
        
        return services;
    }

}
