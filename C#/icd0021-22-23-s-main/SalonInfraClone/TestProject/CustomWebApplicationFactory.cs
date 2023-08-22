using BLL.Contracts.App;
using DAL;
using DAL.Contracts.App;
using Domain.App.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using BLL.App;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace TestProject
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                // Remove any existing registrations of ApplicationDbContext
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType == typeof(DbContextOptions<ApplicationDbContext>));
                if (descriptor != null)
                {
                    services.Remove(descriptor);
                }

                // Create a new in-memory database and register ApplicationDbContext
                services.AddDbContext<ApplicationDbContext>(options =>
                {
                    options.UseInMemoryDatabase("InMemoryDbForTesting");
                });

                // Register other required services
                services.AddScoped<IAppUOW, AppUOW>();
                services.AddScoped<IAppBLL, AppBLL>();
                services.AddScoped<UserManager<AppUser>>();

                // Build the service provider
                var serviceProvider = services.BuildServiceProvider();

                // Create a scope to resolve the services
                var scope = serviceProvider.CreateScope();

                // Resolve the services from the scope
                var scopedServices = scope.ServiceProvider;
                var dbContext = scopedServices.GetRequiredService<ApplicationDbContext>();
                var userManager = scopedServices.GetRequiredService<UserManager<AppUser>>();
                var userRoleManager = scopedServices.GetRequiredService<RoleManager<AppRole>>();
                var logger = scopedServices.GetRequiredService<ILogger<CustomWebApplicationFactory<TStartup>>>();

                // Ensure the database is created and seed data
                dbContext.Database.EnsureCreated();

                try
                {
                    // Seeding data
                    new Helpers.Helpers().DataSeeder(dbContext, userManager, userRoleManager);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "An error occurred seeding the database. Error: {Message}", ex.Message);
                }
            });
        }
    }
}
