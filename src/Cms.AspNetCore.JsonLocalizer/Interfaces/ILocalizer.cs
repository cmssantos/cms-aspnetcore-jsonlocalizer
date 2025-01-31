using Cms.AspNetCore.JsonLocalizer.Models;

namespace Cms.AspNetCore.JsonLocalizer.Interfaces;

/// <summary>
/// Interface for the localization service.
/// </summary>
public interface ILocalizer
{
    /// <summary>
    /// Gets a localized string based on the provided key.
    /// </summary>
    /// <param name="key">The key of the string resource to retrieve.</param>
    /// <returns>A LocalizedString containing the localized value and metadata.</returns>
    LocalizedString GetString(string key);

    /// <summary>
    /// Gets a localized string based on the provided key and formats it with the given arguments.
    /// </summary>
    /// <param name="key">The key of the string resource to retrieve.</param>
    /// <param name="arguments">An array of objects to format the string with.</param>
    /// <returns>A LocalizedString containing the formatted localized value and metadata.</returns>
    LocalizedString GetString(string key, params object[] arguments);
}
