using System.Globalization;
using Cms.AspNetCore.JsonLocalizer.Interfaces;
using Cms.AspNetCore.JsonLocalizer.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Cms.AspNetCore.JsonLocalizer.Extensions;

/// <summary>
/// Extensões para configurar o serviço de localização.
/// </summary>
public static class LocalizerServiceCollectionExtensions
{
    /// <summary>
    /// Adiciona o serviço de localização JSON à coleção de serviços.
    /// </summary>
    /// <param name="services">A coleção de serviços.</param>
    /// <param name="resourcesPath">O caminho para os arquivos de recursos JSON.</param>
    /// <returns>A coleção de serviços atualizada.</returns>
    public static IServiceCollection AddJsonLocalizer(this IServiceCollection services, string resourcesPath)
    {
        services.AddHttpContextAccessor();
        services.AddSingleton<ILocalizer>(sp =>
        {
            var httpContextAccessor = sp.GetRequiredService<IHttpContextAccessor>();
            var cultureName = httpContextAccessor.HttpContext?.Request.Headers["Accept-Language"].FirstOrDefault()
                ?? CultureInfo.CurrentCulture.Name;
            return new Localizer(resourcesPath, cultureName);
        });

        return services;
    }
}
