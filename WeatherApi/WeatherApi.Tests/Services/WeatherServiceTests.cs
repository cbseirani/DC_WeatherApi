using Moq;
using Serilog;
using WeatherApi.Common;
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
        var time = DateTime.UtcNow.AddHours(-3);
        var forecastLocation1 = new ForecastDto
        {
            ForecastKey = "(52.52,14.14)",
            Location = new CoordinatesDto
            {
                Latitude = 52.52,
                Longitude = 14.14
            },
            CurrentWeather = new WeatherDto
            {
                Time = time
            }
        };
        var forecastLocation2 = new ForecastDto
        {
            ForecastKey = "(67.52,13.14)",
            Location = new CoordinatesDto
            {
                Latitude = 67.52,
                Longitude = 13.14
            },
            CurrentWeather = new WeatherDto
            {
                Time = time
            }
        };
        
        _weatherRepository
            .Setup(x => x.Get(It.IsAny<Guid>()))
            .ReturnsAsync(new List<ForecastDto>
            {
                forecastLocation1,
                forecastLocation2
            });
        
        _weatherRepository
            .Setup(x => x.Save(It.IsAny<Guid>(), It.IsAny<ForecastDto>()))
            .ReturnsAsync(new ForecastDto
            {
                ForecastKey = forecastLocation1.ForecastKey,
                Location = forecastLocation1.Location,
                CurrentWeather = new WeatherDto
                {
                    Time = DateTime.UtcNow
                }               
            });

        // doing this just for the coverage of this utility
        var (lat, lo) = ForecastUtilities.GetCoordinatesFromKey(forecastLocation1.ForecastKey);
        (lat, lo) = ForecastUtilities.GetCoordinatesFromKey(forecastLocation2.ForecastKey);
        
        _openMeteoClient
            .Setup(x => x.RequestForecast(It.IsAny<CoordinatesDto>()))
            .ReturnsAsync(new ForecastDto
            {
                ForecastKey = forecastLocation1.ForecastKey,
                Location = forecastLocation1.Location,
                CurrentWeather = new WeatherDto
                {
                    Time = DateTime.UtcNow
                }               
            });

        var response = (await CreateService().Get(UserGuid)).ToList();
        
        Assert.NotNull(response);
        Assert.NotNull(response[0]);
        Assert.NotNull(response[1]);
        Assert.NotEqual(time, response[0]?.CurrentWeather?.Time);
        Assert.NotEqual(time, response[1]?.CurrentWeather?.Time);
        return true;
    }
    
    [Fact]
    public async Task<bool> SaveLocation_Expected()
    {
        var coordinates = new CoordinatesDto()
        {
            Latitude = 52.52,
            Longitude = 14.14
        };
        
        _openMeteoClient
            .Setup(x => x.RequestForecast(It.IsAny<CoordinatesDto>()))
            .ReturnsAsync(new ForecastDto
            {
                ForecastKey = "(52.52,14.14)",
                Location = coordinates,
                CurrentWeather = new WeatherDto
                {
                    Time = DateTime.UtcNow
                }         
            });
        
        _weatherRepository
            .Setup(x => x.Save(It.IsAny<Guid>(), It.IsAny<ForecastDto>()))
            .ReturnsAsync(new ForecastDto
            {
                ForecastKey = "(52.52,14.14)",
                Location = coordinates,
                CurrentWeather = new WeatherDto
                {
                    Time = DateTime.UtcNow
                }         
            });
        
        var response = (await CreateService().Save(UserGuid, coordinates));
        
        Assert.NotNull(response);
        return true;
    }
    
    [Fact]
    public async Task<bool> DeleteLocation_Expected()
    {
        var coordinates = new CoordinatesDto()
        {
            Latitude = 52.52,
            Longitude = 14.14
        };
        
        _weatherRepository
            .Setup(x => x.Delete(It.IsAny<Guid>(), It.IsAny<string>()))
            .ReturnsAsync(true);
        
        var response = await CreateService().Delete(UserGuid, ForecastUtilities.GenerateCoordinatesKey(coordinates.Latitude, coordinates.Longitude));
        
        Assert.True(response);
        return true;
    }

    private WeatherService CreateService()
    {
        return new WeatherService(_weatherRepository.Object, _openMeteoClient.Object, _logger.Object);
    }
    
}