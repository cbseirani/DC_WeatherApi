using WeatherApi.Dto;

namespace WeatherApi.Repositories.Interfaces;

public interface IWeatherRepository
{
    Task<IEnumerable<ForecastDto>> Get();
    
    Task<ForecastDto> Save(ForecastDto forecast);
    
    Task<bool> Delete(string forecastKey);    
}