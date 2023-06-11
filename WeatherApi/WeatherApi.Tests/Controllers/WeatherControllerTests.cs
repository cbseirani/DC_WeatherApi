using Moq;
using WeatherApi.Common;
using WeatherApi.Controllers;
using WeatherApi.Dto;
using WeatherApi.Services.Interfaces;
using Xunit;

namespace WeatherApi.Tests.Controllers;

public class WeatherControllerTests
{
    private readonly Mock<IWeatherService> _weatherService = new();
    private static readonly Guid UserGuid = Guid.NewGuid();
    
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

        var coordinates = new CoordinatesDto { Latitude = 52.52, Longitude = 14.14 };
        var response = await CreateController().Save(coordinates);
        
        Assert.NotNull(response);
        return true;
    }

    [Fact]
    public async Task<bool> DeleteLocationForecast_Expected()
    {
        _weatherService
            .Setup(x => x.Delete(It.IsAny<Guid>(), It.IsAny<string>()))
            .ReturnsAsync(true);

        var key = ForecastUtilities.GenerateCoordinatesKey(52.52, 14.14);
        var response = await CreateController().Delete(key);
        
        Assert.True(response);
        return true;
    }

    private WeatherController CreateController()
    {
        return new WeatherController(_weatherService.Object);
    }
}