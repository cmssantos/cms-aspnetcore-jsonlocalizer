using System.Collections.Concurrent;
using System.Text.Json;
using Cms.AspNetCore.JsonLocalizer.Models;

namespace Cms.AspNetCore.JsonLocalizer.Services;

/// <summary>
/// Internal class for accessing JSON resources.
/// </summary>
internal class JsonResourceAccessor
{
    private readonly ConcurrentDictionary<string, JsonElement> _resources = new();
    private readonly JsonSerializerOptions _jsonOptions;
    private readonly string _resourcesPath;

    /// <summary>
    /// Initializes a new instance of the JsonResourceAccessor class.
    /// </summary>
    /// <param name="resourcesPath">The path to the directory containing JSON resource files.</param>
    /// <exception cref="DirectoryNotFoundException">Thrown when the specified resources directory is not found.</exception>
    public JsonResourceAccessor(string resourcesPath)
    {
        _resourcesPath = resourcesPath;
        _jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            ReadCommentHandling = JsonCommentHandling.Skip,
            AllowTrailingCommas = true
        };

        if (!Directory.Exists(resourcesPath))
        {
            throw new DirectoryNotFoundException($"Resources directory not found: {resourcesPath}");
        }

        LoadAllResources();
    }

    private void LoadAllResources()
    {
        foreach (var file in Directory.GetFiles(_resourcesPath, "*.json"))
        {
            var cultureName = Path.GetFileNameWithoutExtension(file);
            LoadResources(file, cultureName);
        }
    }

    private void LoadResources(string filePath, string cultureName)
    {
        if (File.Exists(filePath))
        {
            var jsonString = File.ReadAllText(filePath);
            var jsonDocument = JsonSerializer.Deserialize<JsonElement>(jsonString, _jsonOptions);

            _resources[cultureName] = jsonDocument;
        }
    }

    /// <summary>
    /// Retrieves a localized string based on the provided key and culture name.
    /// </summary>
    /// <param name="key">The key of the string resource to retrieve.</param>
    /// <param name="cultureName">The name of the culture to use for localization.</param>
    /// <returns>A LocalizedString containing the localized value and metadata.</returns>
    internal LocalizedString GetString(string key, string cultureName)
    {
        string searchedLocation = $"{cultureName}.{key}";

        if (!_resources.TryGetValue(cultureName, out var resourceSet) &&
            !_resources.TryGetValue(cultureName.Split('-')[0], out resourceSet) &&
            !_resources.TryGetValue("en", out resourceSet))
        {
            return new LocalizedString(key, true, searchedLocation);
        }

        var keys = key.Split('.');
        var currentElement = resourceSet;

        foreach (var subKey in keys)
        {
            if (!currentElement.TryGetProperty(subKey, out var nextElement))
            {
                return new LocalizedString(key, true, searchedLocation);
            }
            currentElement = nextElement;
        }

        string value = currentElement.ValueKind == JsonValueKind.String
            ? currentElement.GetString() ?? key
            : key;

        return new LocalizedString(value, false, searchedLocation);
    }
}
