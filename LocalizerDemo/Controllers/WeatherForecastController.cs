using LocalizerDemo.Localizer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace LocalizerDemo.Controllers;

[ApiController]
[Route("{culture:culture=en-US}/[controller]/[action]")]
public class WeatherForecastController(IStringLocalizer<Resources> localizer, ILogger<WeatherForecastController> logger)
    : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };


    [HttpGet]
    public IEnumerable<WeatherForecast> GetWeather()
    {
        var weatherForecasts = Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = localizer[Summaries[Random.Shared.Next(Summaries.Length)]]
            })
            .ToArray();
        return weatherForecasts;
    }

    [HttpGet]
    public string Get()
    {
        return localizer["date"];
    }
}