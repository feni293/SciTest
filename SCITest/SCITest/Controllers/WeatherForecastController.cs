using Microsoft.AspNetCore.Mvc;
using SCITest.Application.DTOs.Weather;
using SCITest.Application.Handlers.Queries.Weather;

namespace SCITest.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WeatherController : ControllerBase
{
    private readonly GetWeatherHandler _getWeatherHandler;

    public WeatherController(GetWeatherHandler getWeatherHandler)
    {
        _getWeatherHandler = getWeatherHandler;
    }

    [HttpGet("{city}")]
    public async Task<ActionResult<WeatherDto>> Get(string city, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(city))
        {
            return BadRequest("City is required.");
        }

        var weather = await _getWeatherHandler.HandleAsync(city, cancellationToken);

        if (weather is null)
        {
            return NotFound(
                $"Weather information for '{city}' was not found.");
        }

        return Ok(weather);
    }
}