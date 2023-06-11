using WeatherApi.Dto;

namespace WeatherApi.Services.Interfaces;

public interface IWeatherService
{
    Task<IEnumerable<ForecastDto>> Get(Guid userGuid);
    
    Task<ForecastDto> Save(Guid userGuid, CoordinatesDto coordinates);
    
    Task<bool> Delete(Guid userGuid, string forecastKey);    
}