using Application.Common.Interfaces;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using WebAPI.Extensions;
using WebAPI.Filters;
using WebAPI.Services;

namespace WebAPI;

public static class ConfigureWebAPIServices
{
    public static IServiceCollection AddWebApiServices(this IServiceCollection services, string connectionString)
    {
        services.AddScoped<ICurrentUserService, CurrentUserService>();
        services.AddHttpClient();

        services.AddConfiguredControllers();
        services.AddConfiguredCors();
        services.AddSwaggerDocumentation();
        services.AddApplicationHealthChecks(connectionString);

        return services;
    }

    private static IServiceCollection AddConfiguredCors(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddDefaultPolicy(policy =>
            {
                policy.WithOrigins("http://localhost:3000")
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
            });
        });

        return services;
    }

    private static IServiceCollection AddConfiguredControllers(this IServiceCollection services)
    {
        services.AddControllers(options =>
        {
            options.Filters.Add<ApiExceptionFilter>();
        });

        return services;
    }

    private static IServiceCollection AddApplicationHealthChecks(this IServiceCollection services,
        string connectionString)
    {
        services.AddHealthChecks()
            .AddCheck("self", () => HealthCheckResult.Healthy())
            .AddNpgSql(connectionString, name: "gosnagrada-api", tags: new[] { "gosnagrada-api-db" });

        return services;
    }
}