using WeatherApi.OpenMeteo;
using WeatherApi.OpenMeteo.Models;
using Xunit;

namespace WeatherApi.Tests.OpenMeteo;

public class OpenMeteoUtilitiesTests
{
    private const int NumForecastDays = 7;
    private const string TimeZone = "America/New_York"; 
    
    [Fact]
    public async Task<bool> MapForecast_Expected()
    {
        var forecast = new ForecastResponse();
        
        var result = OpenMeteoUtilities.MapForecast(forecast, NumForecastDays, TimeZone);
        
        Assert.NotNull(result);
        return true;
    }
}