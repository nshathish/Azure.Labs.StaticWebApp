using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Labs.StaticWebApp.Api;

public class GetWeather(ILogger<GetWeather> logger, IConfiguration configuration)
{
    private readonly HashSet<string> _allowedCorsOrigins = (configuration["AllowedCorsOrigins"] ?? string.Empty)
        .Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
        .ToHashSet(StringComparer.OrdinalIgnoreCase);

    private static IEnumerable<WeatherForecast> GetForecasts(int daysToForecast)
    {
        return Enumerable.Range(1, daysToForecast).Select(index =>
            new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = GetSummary(Random.Shared.Next(-20, 55))
            })
            .ToArray();

        static string GetSummary(int temp) => temp switch
        {
            > 35 => "Scorching",
            > 30 => "Sweltering",
            > 25 => "Hot",
            > 20 => "Balmy",
            > 15 => "Warm",
            > 10 => "Mild",
            > 5  => "Cool",
            > 0  => "Chilly",
            > -10 => "Bracing",
            _ => "Freezing",
        };
    }


    [Function(nameof(GetWeather))]
    public IActionResult Run(
        [HttpTrigger(AuthorizationLevel.Function, "get", Route = "weather-forecast/{daysToForecast=5}")]
        HttpRequest req, 
        int daysToForecast)
    {
        logger.LogInformation("C# HTTP trigger function processed a request.");

        if (req.Headers.TryGetValue("Origin", out var originValues))
        {
            var origin = originValues.ToString();
            if (_allowedCorsOrigins.Contains(origin))
            {
                req.HttpContext.Response.Headers.Append("Access-Control-Allow-Origin", origin);
                req.HttpContext.Response.Headers.Append("Vary", "Origin");
            }
        }

        return new OkObjectResult(GetForecasts(daysToForecast));
    }



    private class WeatherForecast
    {
        public DateOnly Date { get; init; }
        public int TemperatureC { get; init; }
        public string? Summary { get; init; }
        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
    }

}