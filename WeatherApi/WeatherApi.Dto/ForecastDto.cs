using Newtonsoft.Json;

namespace WeatherApi.Dto;

public class ForecastDto
{
    [JsonProperty(PropertyName = "id")] // needed for cosmosdb
    public string ForecastKey { get; set; } = string.Empty;
    
    public CoordinatesDto Location { get; set; }
    
    public string? TimeZone { get; set; }

    public double Elevation { get; set; }
    
    public UnitDto? Units { get; set; }

    public WeatherDto? CurrentWeather { get; set; }
    
    public IEnumerable<WeatherDto>? Forecast { get; set; }

    public string? PartitionKey { get; set; }
}