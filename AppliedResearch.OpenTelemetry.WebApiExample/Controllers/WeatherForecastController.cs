using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace AppliedResearch.OpenTelemetry.WebApiExample.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
  private static readonly ActivitySource ActivitySource = new ActivitySource("AppliedResearch.OpenTelemetry.WebApiExample");
  
  private static readonly string[] Summaries = {
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
  };

  private readonly ILogger<WeatherForecastController> _logger;

  public WeatherForecastController(ILogger<WeatherForecastController> logger)
  {
    _logger = logger;
  }

  [HttpGet(Name = "GetWeatherForecast")]
  public IEnumerable<WeatherForecast> Get()
  {
    using var activity = ActivitySource.StartActivity("Getting weather forecast", ActivityKind.Server);

    activity?.SetTag("MyTag1", "This is my test tag");
    
    return Enumerable.Range(1, 5).Select(index => new WeatherForecast
      {
        Date = DateTime.Now.AddDays(index),
        TemperatureC = Random.Shared.Next(-20, 55),
        Summary = Summaries[Random.Shared.Next(Summaries.Length)]
      })
      .ToArray();
  }
}