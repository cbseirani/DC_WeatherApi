using WeatherApi.Common;

namespace WeatherApi.Dto;

public class WeatherDto
{
    public double Temperature { get; set; }
    
    public double MaxTemperature { get; set; }
    
    public double MinTemperature { get; set; }
    
    public double WindSpeed { get; set; }
    
    public double MaxWindSpeed { get; set; }
    
    public double WindDirection { get; set; }
    
    public DateTime? Time { get; set; }

    public DateTime? Sunrise { get; set; }

    public DateTime? Sunset { get; set; }

    public WmoWeatherCode Weather { get; set; }   
}