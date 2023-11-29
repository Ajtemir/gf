using Microsoft.Extensions.Options;

namespace WebAPI.Extensions;

public static class SettingsExtensions
{
    public static IServiceCollection AddValidatedSettings(this IServiceCollection services)
    {
        return services;
    }


    /// <summary>
    /// Добавляет настройки, которые можно получать через DI.
    /// При изменении настроек, например при редактировании файла appsettings.json настройки обновятся.
    /// </summary>
    private static IServiceCollection AddMonitoredSettings<T>(this IServiceCollection services)
        where T : class
    {
        var sectionName = typeof(T).Name;

        services.AddOptions<T>()
            .BindConfiguration(sectionName)
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services.AddScoped(resolver => resolver.GetRequiredService<IOptionsMonitor<T>>().CurrentValue);

        return services;
    }

    /// <summary>
    /// Добавляет настройки, которые можно получать через DI.
    /// </summary>
    private static IServiceCollection AddSettings<T>(this IServiceCollection services)
        where T : class
    {
        var sectionName = typeof(T).Name;

        services.AddOptions<T>()
            .BindConfiguration(sectionName)
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services.AddSingleton(resolver => resolver.GetRequiredService<IOptions<T>>().Value);

        return services;
    }
}