using Serilog;
using WeatherApi.Common;
using WeatherApi.Dto;
using WeatherApi.OpenMeteo.Interfaces;
using WeatherApi.Repositories.Interfaces;
using WeatherApi.Services.Interfaces;

namespace WeatherApi.Services;

public class WeatherService : IWeatherService
{
    private readonly IWeatherRepository _weatherRepository;
    private readonly IOpenMeteoClient _openMeteoClient;
    private readonly ILogger _logger;

    public WeatherService(IWeatherRepository weatherRepository, IOpenMeteoClient openMeteoClient, ILogger logger)
    {
        _weatherRepository = weatherRepository;
        _openMeteoClient = openMeteoClient;
        _logger = logger;
    }

    public async Task<IEnumerable<ForecastDto>> Get(Guid userGuid)
    {
        // TODO : if locations list gets big we will need to refactor to execute multiple requests in parallel 
        var previousForecasts = await _weatherRepository.Get(userGuid);
        var forecasts = new List<ForecastDto>();
        foreach (var forecast in previousForecasts)
        {
            forecasts.Add(await Save(userGuid, new CoordinatesDto{Latitude = forecast.Location.Latitude, Longitude = forecast.Location.Longitude}));
        }

        return forecasts;
    }

    public async Task<ForecastDto> Save(Guid userGuid, CoordinatesDto coordinates)
    {
        var forecast = await _openMeteoClient.RequestForecast(coordinates);
        if (forecast is null)
        {
            _logger.Error("Location Forecast was not retrieved for lat: {0} long: {1}", coordinates.Latitude, coordinates.Longitude);
            throw new Exception("Location Forecast was not retrieved");
        }
        
        _logger.Information("Storing forecast in CosmosDB for lat: {0} long: {1}", coordinates.Latitude, coordinates.Longitude);
        return await _weatherRepository.Save(userGuid, forecast);
    }

    public async Task<bool> Delete(Guid userGuid, string forecastKey)
    {
        var (latitude, longitude) = ForecastUtilities.GetCoordinatesFromKey(forecastKey);
        _logger.Information("Deleting forecast in CosmosDB for lat: {0} long: {1}", latitude, longitude);
        return await _weatherRepository.Delete(userGuid, forecastKey);
    }
}