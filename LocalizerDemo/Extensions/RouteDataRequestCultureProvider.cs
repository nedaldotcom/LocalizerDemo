﻿using Microsoft.AspNetCore.Localization;

namespace LocalizerDemo.Extensions;

public class RouteDataRequestCultureProvider : RequestCultureProvider
{
    public int IndexOfCulture;
    public int IndexofUICulture;

    public override Task<ProviderCultureResult> DetermineProviderCultureResult(HttpContext httpContext)
    {
        if (httpContext == null)
            throw new ArgumentNullException(nameof(httpContext));

        string culture = null;
        string uiCulture = null;

        culture = uiCulture = httpContext.Request.Path.Value.Split('/')[IndexOfCulture];

        var providerResultCulture = new ProviderCultureResult(culture, uiCulture);

        return Task.FromResult(providerResultCulture);
    }
}