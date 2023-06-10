using Flurl;
using Flurl.Http;
using WeatherApi.OpenMeteo.Models;

// open meteo documentation: https://open-meteo.com/en/docs
// sample request for openmeteo: https://api.open-meteo.com/v1/forecast?latitude=52.52&longitude=13.41&daily=weathercode,temperature_2m_max,temperature_2m_min,sunrise,sunset,windspeed_10m_max&current_weather=true&temperature_unit=fahrenheit&past_days=1&forecast_days=3timezone=America%2FNew_York

Console.WriteLine("Testing Open-Meteo");
var baseUrl = "https://api.open-meteo.com/v1/forecast";

try
{
    Console.WriteLine("Making a request...");
    var result = await baseUrl.SetQueryParams(new ForecastRequest
    {
        latitude = 52.52,
        longitude = 13.41,
        daily = "weathercode,temperature_2m_max,temperature_2m_min,sunrise,sunset,windspeed_10m_max",
        current_weather = true,
        temperature_unit = "fahrenheit",
        forecast_days = 7,
        timezone = "America/New_York"
    }).GetJsonAsync<ForecastResponse>();
    
    Console.WriteLine("{0}", result);
}
catch (Exception e)
{
    Console.WriteLine("Error: {0}", e.Message);
}

Console.WriteLine("Fin. Press Enter/Return to exit.");
Console.ReadLine();