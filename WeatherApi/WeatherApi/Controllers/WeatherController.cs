using Microsoft.AspNetCore.Mvc;
using WeatherApi.Dto;
using WeatherApi.Services.Interfaces;

namespace WeatherApi.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherController : ControllerBase
{
    private readonly IWeatherService _weatherService;

    public WeatherController(IWeatherService weatherService) => _weatherService = weatherService;
    
    // GET forecast by coordinates
    // Returns 7 day forecast for a single location
    [HttpGet]
    public Task<ForecastDto> Get(double latitude, double longitude) =>
        _weatherService.Save(new CoordinatesDto() { Latitude = latitude, Longitude = longitude });
    
    // GET all recent forecasts for all locations
    // Returns 7 day forecast for each location
    [HttpGet("Locations")]
    public Task<IEnumerable<ForecastDto>> Get() => _weatherService.Get();
    
    // POST new location
    // Returns 7 day forecast for a single location
    [HttpPost]
    public Task<ForecastDto> Save([FromBody] CoordinatesDto coordinates) =>
        _weatherService.Save(coordinates);
    
    // DELETE location
    // Returns a success flag
    [HttpDelete]
    public Task<bool> Delete(string forecastKey) => _weatherService.Delete(forecastKey);
}