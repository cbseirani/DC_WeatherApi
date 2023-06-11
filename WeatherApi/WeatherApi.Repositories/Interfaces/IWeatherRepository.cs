using WeatherApi.Dto;

namespace WeatherApi.Repositories.Interfaces;

public interface IWeatherRepository
{
    Task<IEnumerable<ForecastDto>> Get(Guid userGuid);
    
    Task<ForecastDto> Save(Guid userGuid, ForecastDto forecast);
    
    Task<bool> Delete(Guid userGuid, string forecastKey);    
}