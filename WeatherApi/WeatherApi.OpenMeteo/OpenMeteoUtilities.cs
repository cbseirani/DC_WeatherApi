using WeatherApi.Common;
using WeatherApi.Dto;
using WeatherApi.OpenMeteo.Models;

namespace WeatherApi.OpenMeteo;

public static class OpenMeteoUtilities
{
    public static ForecastDto MapForecast(ForecastResponse forecast, int numOfDays, string timeZone)
    {
        var forecasts = new List<WeatherDto>();
        for (var i = 0; i < numOfDays; i++)
        {
            forecasts.Add(new WeatherDto
            {
                MaxTemperature = forecast.daily.temperature_2m_max[i],
                MinTemperature = forecast.daily.temperature_2m_min[i],
                MaxWindSpeed = forecast.daily.windspeed_10m_max[i],
                Time = DateTime.Parse(forecast.daily.time[i]),
                Sunrise = DateTime.Parse(forecast.daily.sunrise[i]),
                Sunset = DateTime.Parse(forecast.daily.sunset[i]),
                Weather = (WmoWeatherCode) forecast.daily.weathercode[i]
            });
        }
        
        // TODO : use mapster/automapper
        var mappedForecast = new ForecastDto
        {
            ForecastKey = ForecastUtilities.GenerateCoordinateKey(forecast.latitude, forecast.longitude),
            Location = new CoordinatesDto { Latitude = forecast.latitude, Longitude = forecast.longitude },
            TimeZone = timeZone,
            Elevation = forecast.elevation,
            Units = new UnitDto
            {
                Time =  forecast.daily_units.time, 
                Temperature = forecast.daily_units.temperature_2m_max, 
                WindSpeed = forecast.daily_units.windspeed_10m_max, 
            },
            CurrentWeather = new WeatherDto
            {
                Temperature = forecast.current_weather.temperature,
                WindSpeed = forecast.current_weather.windspeed,
                WindDirection = forecast.current_weather.winddirection,
                Weather = (WmoWeatherCode) forecast.current_weather.weathercode,
            },
            Forecast = forecasts
        };

        return mappedForecast;
    }
}