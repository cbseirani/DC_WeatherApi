using Moq;
using Serilog;
using WeatherApi.Dto;
using WeatherApi.OpenMeteo.Interfaces;
using WeatherApi.Repositories.Interfaces;
using WeatherApi.Services;
using Xunit;

namespace WeatherApi.Tests.Services;

public class WeatherServiceTests
{
    private readonly Mock<IWeatherRepository> _weatherRepository = new();
    private readonly Mock<IOpenMeteoClient> _openMeteoClient = new();
    private readonly Mock<ILogger> _logger = new();
    private static readonly Guid UserGuid = Guid.NewGuid();
    
    [Fact]
    public async Task<bool> GetWithPreviousLocations_Expected()
    {
        _weatherRepository
            .Setup(x => x.Get(It.IsAny<Guid>()))
            .ReturnsAsync(new List<ForecastDto>()
            {
                new ForecastDto()
                {
                    
                },
                new ForecastDto()
                {
                    
                }
            });
        
        _weatherRepository
            .Setup(x => x.Save(It.IsAny<Guid>(), It.IsAny<ForecastDto>()))
            .ReturnsAsync(new ForecastDto
            {

            });

        _openMeteoClient
            .Setup(x => x.RequestForecast(It.IsAny<CoordinatesDto>()))
            .ReturnsAsync(new ForecastDto
            {

            });
        

        var response = await CreateService().Get(UserGuid);
        
        Assert.NotNull(response);
        return true;
    }
    
    //getWithoutLocations
    
    //saveWithCoords
    //saveWithoutCoors
    
    //deleteWithKey
    //deleteWithoutKey

    private WeatherService CreateService()
    {
        return new WeatherService(_weatherRepository.Object, _openMeteoClient.Object, _logger.Object);
    }
    
}