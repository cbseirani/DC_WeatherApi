using Moq;
using WeatherApi.Controllers;
using WeatherApi.Dto;
using WeatherApi.Services.Interfaces;
using Xunit;

namespace WeatherApi.Tests.Controllers;

public class WeatherControllerTests
{
    private readonly Mock<IWeatherService> _weatherService = new();
    
    [Fact]
    public async Task<bool> GetForecast_Expected()
    {
        _weatherService
            .Setup(x => x.Save(It.IsAny<Guid>(), It.IsAny<CoordinatesDto>()))
            .ReturnsAsync(new ForecastDto());

        var response = await CreateController().Get(52.52, 13.41);
        
        Assert.NotNull(response);
        return true;
    }

    [Fact]
    public async Task<bool> GetPreviousForecasts_Expected()
    {
        _weatherService
            .Setup(x => x.Get(It.IsAny<Guid>()))
            .ReturnsAsync(new List<ForecastDto>());

        var response = await CreateController().Get();
        
        Assert.NotNull(response);
        return true;
    }

    [Fact]
    public async Task<bool> SaveLocationForecast_Expected()
    {
        _weatherService
            .Setup(x => x.Save(It.IsAny<Guid>(),It.IsAny<CoordinatesDto>()))
            .ReturnsAsync(new ForecastDto());

        var response = await CreateController().Get();
        
        Assert.NotNull(response);
        return true;
    }

    [Fact]
    public async Task<bool> DeleteLocationForecast_Expected()
    {
        _weatherService
            .Setup(x => x.Delete(It.IsAny<Guid>(), It.IsAny<string>()))
            .ReturnsAsync(true);

        var response = await CreateController().Get();
        
        Assert.NotNull(response);
        return true;
    }

    private WeatherController CreateController()
    {
        return new WeatherController(_weatherService.Object);
    }
}