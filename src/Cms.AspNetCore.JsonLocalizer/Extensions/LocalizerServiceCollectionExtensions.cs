using System.Globalization;
using Cms.AspNetCore.JsonLocalizer.Interfaces;
using Cms.AspNetCore.JsonLocalizer.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Cms.AspNetCore.JsonLocalizer.Extensions;

/// <summary>
/// Extensions for configuring the localization service.
/// </summary>
public static class LocalizerServiceCollectionExtensions
{
    /// <summary>
    /// Adds the JSON localization service to the service collection.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <param name="resourcesPath">The path to the JSON resource files.</param>
    /// <returns>The updated service collection.</returns>
    public static IServiceCollection AddJsonLocalizer(this IServiceCollection services, string resourcesPath)
    {
        services.AddHttpContextAccessor();
        services.AddSingleton(new JsonResourceAccessor(resourcesPath));
        services.AddScoped<ILocalizer>(sp =>
        {
            var httpContextAccessor = sp.GetRequiredService<IHttpContextAccessor>();
            var jsonResourceAccessor = sp.GetRequiredService<JsonResourceAccessor>();
            var cultureName = httpContextAccessor.HttpContext?.Request.Headers["Accept-Language"].FirstOrDefault()
                ?? CultureInfo.CurrentCulture.Name;

            return new Localizer(jsonResourceAccessor, cultureName);
        });

        return services;
    }
}
