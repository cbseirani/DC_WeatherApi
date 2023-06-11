using Flurl;
using Flurl.Http;
using Microsoft.Extensions.Configuration;
using WeatherApi.Dto;
using WeatherApi.OpenMeteo.Interfaces;
using WeatherApi.OpenMeteo.Models;
using Serilog;

namespace WeatherApi.OpenMeteo;

public class OpenMeteoClient : IOpenMeteoClient
{
    // TODO : add the constants to appconfig AT LEAST
    private readonly IConfiguration _configuration;
    private const int NumForecastDays = 7; //could be user chosen or appsetting
    private const string TempUnit = "fahrenheit"; //could be user chosen or appsetting
    private const string TimeZone = "America/New_York"; //could be user chosen or appsetting    
    private readonly ILogger _logger;
    
    public OpenMeteoClient(IConfiguration configuration, ILogger logger)
    {
        _configuration = configuration;
        _logger = logger;
    }

    public async Task<ForecastDto?> RequestForecast(CoordinatesDto coordinates)
    {
        try
        {
            // TODO : add/configure http retry policies using polly on Weather.Api
            _logger.Information("Requesting coordinates for lat: {0} long: {1}", coordinates.Latitude , coordinates.Longitude);
            var forecast = await _configuration["OpenMeteoUrl"].SetQueryParams(new ForecastRequest
            {
                latitude = coordinates.Latitude,
                longitude = coordinates.Longitude,
                daily = "weathercode,temperature_2m_max,temperature_2m_min,sunrise,sunset,windspeed_10m_max", // TODO : make these a parameter for user to choose
                current_weather = true,
                temperature_unit = TempUnit,
                forecast_days = NumForecastDays,
                timezone = TimeZone
            }).GetJsonAsync<ForecastResponse>();
            
            _logger.Information("Processing forecast payload for lat: {0} long: {1}", coordinates.Latitude , coordinates.Longitude);
            return OpenMeteoUtilities.MapForecast(forecast, NumForecastDays, TimeZone);
        }
        catch (Exception e)
        {
            _logger.Error("{0} : {1}", e.Message, e.StackTrace);
            throw;
        }
    }
}