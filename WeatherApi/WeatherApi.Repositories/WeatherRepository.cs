using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;
using WeatherApi.Dto;
using WeatherApi.Repositories.Interfaces;

namespace WeatherApi.Repositories;

public class WeatherRepository : IWeatherRepository
{
    private readonly CosmosClient _cosmosClient;
    private readonly string? _databaseId;
    private readonly string? _containerId;

    public WeatherRepository(IConfiguration configuration)
    {
        _databaseId = configuration["CosmosDbId"];
        _containerId = configuration["CosmosDbContainerId"];
        _cosmosClient = new CosmosClient(connectionString: configuration["CosmosDbConnString"]);
    }
    
    public async Task<IEnumerable<ForecastDto>> Get()
    {
        var container = await GetCosmosContainer();
        return container.GetItemLinqQueryable<ForecastDto>().ToList();
    }

    public async Task<ForecastDto> Save(ForecastDto forecast)
    {
        var container = await GetCosmosContainer();
        return await container.UpsertItemAsync(forecast, GeneratePartitionKey(forecast.ForecastKey));
    }

    public async Task<bool> Delete(string forecastKey)
    {
        var container = await GetCosmosContainer();
        await container.DeleteItemAsync<ForecastDto>(forecastKey, GeneratePartitionKey(forecastKey));
        return true;
    }

    private PartitionKey GeneratePartitionKey(string forecastKey)
    {
        return new PartitionKey(string.Concat(forecastKey.AsSpan(0, 2), forecastKey[^2..].Remove('.')));
    }
    
    private async Task<Container> GetCosmosContainer()
    {
        Database db = await _cosmosClient.CreateDatabaseIfNotExistsAsync(id: _databaseId);
        return db.CreateContainerIfNotExistsAsync(_containerId, "/PartitionKey").Result;
    }
}