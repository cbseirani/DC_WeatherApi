namespace WeatherApi.OpenMeteo.Models;

// generated with https://json2csharp.com/
public class Daily
{
    public List<string> time { get; set; }
    
    public List<int> weathercode { get; set; }
    
    public List<double> temperature_2m_max { get; set; }
    
    public List<double> temperature_2m_min { get; set; }
    
    public List<string> sunrise { get; set; }
    
    public List<string> sunset { get; set; }
    
    public List<double> windspeed_10m_max { get; set; }      
}