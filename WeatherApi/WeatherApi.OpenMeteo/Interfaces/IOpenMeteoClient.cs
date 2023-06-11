using WeatherApi.Dto;

namespace WeatherApi.OpenMeteo.Interfaces;

public interface IOpenMeteoClient
{
    Task<ForecastDto?> RequestForecast(CoordinatesDto coordinates);
}