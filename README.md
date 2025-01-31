## Cms.AspNetCore.JsonLocalizer

#### Cms.AspNetCore.JsonLocalizer is a flexible and efficient JSON-based localization library for ASP.NET Core applications. It provides an easy way to manage and retrieve localized strings using JSON resource files, supporting culture-specific localization and seamless integration with dependency injection.

## Features

-   JSON-based localization
-   Culture-specific resource loading
-   Caching support for improved performance
-   Easy integration with ASP.NET Core dependency injection
-   Support for parameterized messages
-   Fallback to default culture if translation is not found

## Installation

Install the package via NuGet:

```
dotnet add package Cms.AspNetCore.JsonLocalizer
```

## Usage

### 1. Configure Services

In your Program.cs or Startup.cs, add the following:

```csharp
using Cms.AspNetCore.JsonLocalizer.Extensions;

// ...

services.AddJsonLocalizer(Path.Combine(Directory.GetCurrentDirectory(), "Resources"));

```

### 2. Create JSON Resource Files

Create JSON files for each supported culture in the `Resources` directory. For example:
`en-US.json`:

```json
{
    "Welcome": "Welcome to our application!",
    "Greeting": "Hello, {0}!",
    "Menu": {
        "Home": "Home",
        "About": "About",
        "Contact": "Contact"
    }
}
```

`es-ES.json:`

```json
{
    "Welcome": "¡Bienvenido a nuestra aplicación!",
    "Greeting": "¡Hola, {0}!",
    "Menu": {
        "Home": "Inicio",
        "About": "Acerca de",
        "Contact": "Contacto"
    }
}
```

### 3. Use in Controllers

Inject ILocalizer into your controllers:

```csharp
using Cms.AspNetCore.JsonLocalizer.Interfaces;

public class HomeController : Controller
{
    private readonly ILocalizer _localizer;

    public HomeController(ILocalizer localizer)
    {
        _localizer = localizer;
    }

    public IActionResult Index()
    {
        ViewBag.Welcome = _localizer.GetString("Welcome").Value;
        ViewBag.Greeting = _localizer.GetString("Greeting", "Alice").Value;
        return View();
    }
}
```

### 4. Use in Views

In your views, you can use the ILocalizer directly:

```xml
@inject Cms.AspNetCore.JsonLocalizer.Interfaces.ILocalizer Localizer

<h1>@Localizer.GetString("Welcome").Value</h1>
<p>@Localizer.GetString("Greeting", "User").Value</p>

<nav>
    <ul>
        <li>@Localizer.GetString("Menu.Home").Value</li>
        <li>@Localizer.GetString("Menu.About").Value</li>
        <li>@Localizer.GetString("Menu.Contact").Value</li>
    </ul>
</nav>
```

## Advanced Usage

### Handling Missing Translations

You can check if a translation was found using the ResourceNotFound property:

```csharp
var result = _localizer.GetString("NonExistentKey");
if (result.ResourceNotFound)
{
    // Handle missing translation
}
```

## Changing Cultures

The library uses the Accept-Language header from the HTTP request to determine the culture. You can also set the culture
manually in your application's middleware if needed.

## Contributing

Contributions are welcome! Please feel free to submit a Pull Request.

## License

This project is licensed under the MIT License.
