namespace WeatherApi.OpenMeteo.Models;

// generated with https://json2csharp.com/
public class CurrentWeather
{
    public double temperature { get; set; }
    
    public double windspeed { get; set; }
    
    public double winddirection { get; set; }
    
    public int weathercode { get; set; }
    
    public int is_day { get; set; }
    
    public string time { get; set; }   
}