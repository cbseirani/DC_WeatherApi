using WeatherApi.Dto;

namespace WeatherApi.Services.Interfaces;

public interface IWeatherService
{
    Task<IEnumerable<ForecastDto>> Get();
    
    Task<ForecastDto> Save(CoordinatesDto coordinates);
    
    Task<bool> Delete(string forecastKey);    
}