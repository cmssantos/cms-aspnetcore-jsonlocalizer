using Cms.AspNetCore.JsonLocalizer.Interfaces;
using Cms.AspNetCore.JsonLocalizer.Models;

namespace Cms.AspNetCore.JsonLocalizer.Services;

/// <summary>
/// Main implementation of the localization service.
/// </summary>
internal class Localizer(JsonResourceAccessor resourceAccessor, string cultureName) : ILocalizer
{
    private readonly JsonResourceAccessor _resourceAccessor = resourceAccessor;
    private readonly string _cultureName = cultureName;

    /// <summary>
    /// Gets a localized string based on the provided key.
    /// </summary>
    /// <param name="key">The key of the string resource to retrieve.</param>
    /// <returns>A LocalizedString containing the localized value and metadata.</returns>
    public LocalizedString GetString(string key) => _resourceAccessor.GetString(key, _cultureName);

    /// <summary>
    /// Gets a localized string based on the provided key and formats it with the given arguments.
    /// </summary>
    /// <param name="key">The key of the string resource to retrieve.</param>
    /// <param name="arguments">An array of objects to format the string with.</param>
    /// <returns>A LocalizedString containing the formatted localized value and metadata.</returns>
    public LocalizedString GetString(string key, params object[] arguments)
    {
        var localizedString = _resourceAccessor.GetString(key, _cultureName);

        return new LocalizedString(
            string.Format(localizedString.Value, arguments),
            localizedString.ResourceNotFound,
            localizedString.SearchedLocation
        );
    }
}
