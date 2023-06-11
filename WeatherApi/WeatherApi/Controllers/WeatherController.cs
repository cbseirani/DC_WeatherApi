using Microsoft.AspNetCore.Mvc;
using WeatherApi.Dto;
using WeatherApi.Services.Interfaces;

namespace WeatherApi.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherController : ControllerBase
{
    private readonly IWeatherService _weatherService;
    
    // TODO: track user context so we can group forecasts by user in cosmos partitions
    private static readonly Guid UserGuid = Guid.Parse("E9BE9F22-3EA7-4FAA-ACBD-091B09A656B9");

    public WeatherController(IWeatherService weatherService) => _weatherService = weatherService;
    
    // GET forecast by coordinates
    // Returns 7 day forecast for a single location
    [HttpGet]
    public Task<ForecastDto> Get(double latitude, double longitude) =>
        _weatherService.Save(UserGuid, new CoordinatesDto() { Latitude = latitude, Longitude = longitude });
    
    // GET all recent forecasts for all locations
    // Returns 7 day forecast for each location
    [HttpGet("Locations")]
    public Task<IEnumerable<ForecastDto>> Get() => _weatherService.Get(UserGuid);
    
    // POST new location
    // Returns 7 day forecast for a single location
    [HttpPost]
    public Task<ForecastDto> Save([FromBody] CoordinatesDto coordinates) => _weatherService.Save(UserGuid, coordinates);
    
    // DELETE location
    // Returns a success flag
    [HttpDelete]
    public Task<bool> Delete(string forecastKey) => _weatherService.Delete(UserGuid, forecastKey);
}