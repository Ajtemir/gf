using Application.Common.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence;
using Infrastructure.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class ConfigureInfrastructureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
        string connectionString)
    {
        services.AddScoped<IDateTimeUtcService, DateTimeUtcService>();
        services.AddScoped<IAccountService, AccountService>();

        services.AddDatabase(connectionString);
        services.AddConfiguredIdentity();

        return services;
    }

    private static IServiceCollection AddDatabase(this IServiceCollection services, string connectionString)
    {
        services.AddScoped<AuditableEntitySaveChangesInterceptor>();
        services.AddScoped<ApplicationDbContextInitializer>();
        services.AddScoped<IApplicationDbContext, ApplicationDbContext>();

        var isDevelopment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development";
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(connectionString, builder =>
                    builder
                        .MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)
                        .MigrationsHistoryTable(HistoryRepository.DefaultTableName, "main"))
                .EnableSensitiveDataLogging(isDevelopment));


        return services;
    }

    public static IServiceCollection AddConfiguredIdentity(this IServiceCollection services)
    {
        services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 8;
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

        services.ConfigureApplicationCookie(options =>
        {
            options.Cookie.Name = "gosnagrada-cookie";
            options.Cookie.HttpOnly = false;
            options.Cookie.SameSite = SameSiteMode.None;
            options.ExpireTimeSpan = TimeSpan.FromDays(2);

            // Do not redirect to /account/login, but return 401
            options.Events.OnRedirectToLogin = context =>
            {
                context.Response.Headers["Location"] = context.RedirectUri;
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                return Task.CompletedTask;
            };

            options.Events.OnRedirectToAccessDenied = context =>
            {
                context.Response.Headers["Location"] = context.RedirectUri;
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                return Task.CompletedTask;
            };
        });

        return services;
    }
}