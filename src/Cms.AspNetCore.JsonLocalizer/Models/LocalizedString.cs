namespace Cms.AspNetCore.JsonLocalizer.Models;

/// <summary>
/// Represents a localized string with additional information.
/// </summary>
public class LocalizedString(string value, bool resourceNotFound, string searchedLocation)
{
    /// <summary>
    /// Gets the localized value of the string.
    /// </summary>
    public string Value { get; } = value;

    /// <summary>
    /// Gets a value indicating whether the resource was not found.
    /// </summary>
    public bool ResourceNotFound { get; } = resourceNotFound;

    /// <summary>
    /// Gets the searched location for the resource.
    /// </summary>
    public string SearchedLocation { get; } = searchedLocation;
}
