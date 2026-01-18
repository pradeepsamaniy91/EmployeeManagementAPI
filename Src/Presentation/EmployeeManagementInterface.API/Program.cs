using EmployeeManagement.Application.Dto.EmployeeDtos;
using EmployeeManagement.Application.Exceptions.CustomExceptionMiddleware;
using EmployeeManagement.Application.Extensions;
using EmployeeManagement.Application.Queries.GetEmployeeByID;
using EmployeeManagement.Application.Services;
using EmployeeManagement.Domain.Entities;
using EmployeeManagement.Domain.Interfaces;
using EmployeeManagement.Infrastructure.Extension;
using EmployeeManagement.Infrastructure.Persistence;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NLog.Web;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(GetEmployeeByIdQuery).Assembly);
});

builder.Services.AddControllers();

string constr = builder.Configuration.GetConnectionString("DbConnection");
builder.Services.AddDbContext<EmployeeManagementContext>(options =>
{
    options.UseSqlServer(constr, sqloptions => sqloptions.EnableRetryOnFailure(

        maxRetryCount: 5,              // number of retries
            maxRetryDelay: TimeSpan.FromSeconds(10), // delay between retries
            errorNumbersToAdd: null));
}, ServiceLifetime.Scoped);
//IOC
builder.Host.UseNLog();
builder.Services.AddLogging(loggingbuilders =>
{
    loggingbuilders.ClearProviders();
    loggingbuilders.AddNLogWeb();
});

builder.Services.EmployeeManagementDependencies();
builder.Services.AddInfaDependencies(constr);

// Program.cs or Startup.cs
// registers Scoped by default

//Add Policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy => policy
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AngularPolicy", policy =>
    {
        policy.WithOrigins("http://localhost:4200") // Must be explicit
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials(); // Required for 'include' credentials mode
    });
});

builder.Services.AddEndpointsApiExplorer();


// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();


// Replaces manual registration for every single DTO
builder.Services.AddValidatorsFromAssemblyContaining<EmployeeDtoValidator>();

builder.Services.AddScoped<IValidator<EmployeeDto>, EmployeeDtoValidator>();

var app = builder.Build();
ExceptionHandlerMiddlewareExtensions.UseCustomExceptionHandMethod(app);
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

}
app.UseSwagger();   // Serves the JSON endpoint
app.UseSwaggerUI();
app.UseCors("AngularPolicy");
app.UseHttpsRedirection();
app.MapControllers();
app.Run();


