using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize(Roles = "user")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }

  
    [HttpGet(Name = "GetWeatherForecast")]
    public IActionResult Get(){
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)!.Value; // or use a custom claim key like "sub" if applicable

        if (userIdClaim == null)
        {
            return Unauthorized("User ID not found in the token.");
        }
        return Ok(userIdClaim);
    }

}

