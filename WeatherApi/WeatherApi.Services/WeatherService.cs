using WeatherApi.Dto;
using WeatherApi.Services.Interfaces;

namespace WeatherApi.Services;

public class WeatherService : IWeatherService
{
    public Task<IEnumerable<ForecastDto>> Get()
    {
        throw new NotImplementedException();
    }

    public Task<ForecastDto> Save(CoordinatesDto coordinates)
    {
        throw new NotImplementedException();
    }

    public Task<bool> Delete(string forecastKey)
    {
        throw new NotImplementedException();
    }
}