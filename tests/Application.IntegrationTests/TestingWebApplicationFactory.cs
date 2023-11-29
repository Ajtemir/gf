using Application.Common.Interfaces;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebAPI;

namespace Application.IntegrationTests;

public class TestingWebApplicationFactory : WebApplicationFactory<Program>
{
    private readonly Action<IServiceCollection> _configureServices;

    public TestingWebApplicationFactory(Action<IServiceCollection> configureServices)
    {
        _configureServices = configureServices;
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureAppConfiguration(configurationBuilder =>
        {
            var testingConfig = new ConfigurationBuilder()
                .AddJsonFile("appsettings.Testing.json")
                .AddEnvironmentVariables()
                .Build();

            configurationBuilder.AddConfiguration(testingConfig);
        });

        builder.ConfigureServices((builderContext, services) =>
        {
            services.Remove<ICurrentUserService>();
            services.Remove<DbContextOptions<ApplicationDbContext>>();
            
            var connectionString = builderContext.Configuration.GetConnectionString("DefaultConnection");
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new InvalidOperationException("Could not bind the connection string 'DefaultConnection'.");
            }

            services.AddDbContext<ApplicationDbContext>((_, options) =>
                options.UseNpgsql(connectionString,
                    dbContextOptionsBuilder =>
                        dbContextOptionsBuilder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)
                            .MigrationsHistoryTable(HistoryRepository.DefaultTableName, "main")));

            _configureServices(services);
        });
    }
    
    // Prefer this method if IConfiguration is not needed
    // builder.ConfigureTestServices(services =>
    // {
    // });
}