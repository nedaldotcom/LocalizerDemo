using System.Globalization;
using LocalizerDemo;
using LocalizerDemo.Extensions;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddLocalization();
builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var supportedCultures = new List<CultureInfo>
    {
        new("ar-LY"),
        new("en-US"),
        new("es-ES"),
    };
    options.DefaultRequestCulture = new RequestCulture(culture: "ar-LY", uiCulture: "ar-LY");
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
    options.RequestCultureProviders = new[]
        { new RouteDataRequestCultureProvider { IndexOfCulture = 1, IndexofUICulture = 1 } };
});

builder.Services.Configure<RouteOptions>(options =>
{
    options.ConstraintMap.Add("culture", typeof(LanguageRouteConstraint));
});
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var localizeOptions = app.Services.GetService<IOptions<RequestLocalizationOptions>>();
app.UseRequestLocalization(localizeOptions.Value);

app.UseAuthorization();

app.MapControllers(); 

app.MapControllerRoute(
    name: "default",
    pattern: "{culture}/{controller=WeatherForecast}/{action=Index}/{id?}"
);
app.Run();