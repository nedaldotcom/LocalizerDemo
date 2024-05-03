namespace LocalizerDemo.Extensions;

public class LanguageRouteConstraint : IRouteConstraint
{
    private static readonly HashSet<string> SupportedCultures = new HashSet<string>
    {
        "en-US", // English (United States)
        "ar-LY", // Arabic (Egypt)
        "es-ES"  // Spanish (Spain)
    };
    
    public bool Match(HttpContext httpContext, IRouter route, string routeKey, RouteValueDictionary values, RouteDirection routeDirection)
    {
        var culture = values[routeKey]?.ToString();
        return culture != null && SupportedCultures.Contains(culture);
    }
}