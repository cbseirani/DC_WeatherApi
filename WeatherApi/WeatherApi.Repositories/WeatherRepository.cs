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
    
    public async Task<IEnumerable<ForecastDto>> Get(Guid userGuid)
    {
        var container = await GetCosmosContainer();
        return container
            .GetItemLinqQueryable<ForecastDto>(allowSynchronousQueryExecution: true)
            .Where(x => x.PartitionKey == GeneratePartitionKey(userGuid));
    }

    public async Task<ForecastDto> Save(Guid userGuid, ForecastDto forecast)
    {
        var container = await GetCosmosContainer();
        forecast.PartitionKey = GeneratePartitionKey(userGuid);
        return await container.UpsertItemAsync(forecast, new PartitionKey(forecast.PartitionKey));
    }

    public async Task<bool> Delete(Guid userGuid, string forecastKey)
    {
        var container = await GetCosmosContainer();
        await container.DeleteItemAsync<ForecastDto>(forecastKey, new PartitionKey(GeneratePartitionKey(userGuid)));
        return true;
    }

    private static string GeneratePartitionKey(Guid userGuid)
    {
        return $"/{userGuid.ToString()}";
    }
    
    private async Task<Container> GetCosmosContainer()
    {
        Database db = await _cosmosClient.CreateDatabaseIfNotExistsAsync(id: _databaseId);
        return db.CreateContainerIfNotExistsAsync(_containerId, "/PartitionKey").Result;
    }
}