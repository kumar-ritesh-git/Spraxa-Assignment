using MedicinesApi.ServiceExtensions;
using MedicinesApi.Repositories;
using MedicinesApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Add Swagger documentation with JWT support
builder.Services.AddSwaggerDocumentation();

builder.Services.AddScoped<IMedicineService, MedicineService>();
builder.Services.AddScoped<IMedicineRepository, MedicineRepository>();

// Register user repository & service
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

// InMemory DB
builder.Services.AddDataServices(builder.Configuration);

// Authentication with Google-issued JWTs
builder.Services.AddAuthenticationWithGoogleJwt(builder.Configuration);

builder.Services.AddAuthorization();

// CORS for Angular dev server
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular", policy =>
    {
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAngular");
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

// Seed some sample data
app.Services.SeedData();

app.Run();
