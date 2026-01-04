using EmployeeManagement.Application.Dto.EmployeeDtos;
using EmployeeManagement.Application.Extensions;
using EmployeeManagement.Application.Queries.GetEmployeeByID;
using EmployeeManagement.Application.Services;
using EmployeeManagement.Domain.Entities;
using EmployeeManagement.Domain.Interfaces;
using EmployeeManagement.Infrastructure.Persistence;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(GetEmployeeByIdQuery).Assembly);
});

builder.Services.AddControllers();
//builder.Services.AddDbContext<EmployeeManagementContext>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection")));
//IOC
builder.Services.AddDbContext<EmployeeManagementContext>();
builder.Services.EmployeeManagementDependencies();

// Program.cs or Startup.cs
 // registers Scoped by default

//Add Policy
builder.Services.AddCors(options => options.AddPolicy("Corepolicy1", builder =>
{
    builder.AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod();
}));


builder.Services.AddEndpointsApiExplorer();


// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();


// Replaces manual registration for every single DTO
builder.Services.AddValidatorsFromAssemblyContaining<EmployeeDtoValidator>();

builder.Services.AddScoped<IValidator<EmployeeDto>, EmployeeDtoValidator>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();   // Serves the JSON endpoint
    app.UseSwaggerUI();
}

app.UseCors("Corepolicy1");
app.UseHttpsRedirection();
app.MapControllers();


app.Run();


