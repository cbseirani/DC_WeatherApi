namespace WeatherApi.OpenMeteo.Models;

public class ForecastRequest
{
    public double latitude { get; set; }
    
    public double longitude { get; set; }
    
    public string? daily { get; set; }
    
    public bool current_weather { get; set; }
    
    public string? temperature_unit { get; set; }
    
    public int forecast_days { get; set; }
    
    public string? timezone { get; set; }
}