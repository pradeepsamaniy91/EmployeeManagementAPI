
namespace EmployeeManagementInterface.API.Config
{
    public static class SwaggerConfiguration
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(option =>
            {

                option.SwaggerDoc("v1", new Microsoft.OpenApi.OpenApiInfo
                {
                    Version = "v1",
                    Title = "Employee Management API",
                    Description = "Employee Management",
                    //TermsOfService = new Uri("TERM URL"),  //need to configure
                    //Contact = new Microsoft.OpenApi.OpenApiContact
                    //{
                    //    Name = "Employee Management",
                    //    Url = new Uri("")   //Need to configure
                    //},
                    //License = new Microsoft.OpenApi.OpenApiLicense
                    //{
                    //    Name = "",
                    //    Url = new Uri("")
                    //}
                });
            });
            return services;
        }
    }
}
