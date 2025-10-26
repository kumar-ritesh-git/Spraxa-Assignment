using Microsoft.EntityFrameworkCore;

namespace MedicinesApi.ServiceExtensions
{
    public static class DataExtension
    {
        public static IServiceCollection AddDataServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Database context
            var databaseProvider = configuration["Database:Provider"];
            var connectionString = configuration["Database:ConnectionString"];
            if (databaseProvider == "InMemory")
            {
                services.AddDbContext<AppDbContext>(opt => opt.UseInMemoryDatabase("MedicinesDb"));
            }
            else
            {
                services.AddDbContext<AppDbContext>(options =>
                {
                    options.UseSqlServer(connectionString);
                });
            }
            return services;
        }

        public static IServiceProvider SeedData(this IServiceProvider services)
        {

            using (var scope = services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                if (!db.Medicines.Any())
                {
                    db.Medicines.AddRange(new[]
                    {
                         new Medicine { Name = "Paracetamol", Company = "Acme Pharma", Price = 25.50m, ExpiryDate = DateTime.UtcNow.AddYears(1), Stock = 100 },
                         new Medicine { Name = "Ibuprofen", Company = "HealWell", Price = 40.00m, ExpiryDate = DateTime.UtcNow.AddMonths(8), Stock = 50 },
                         new Medicine { Name = "Amoxicillin", Company = "BioMed", Price = 120.00m, ExpiryDate = DateTime.UtcNow.AddYears(2), Stock = 30 },
                    });
                    db.SaveChanges();
                }
            }

            return services;
        }

    }
}
