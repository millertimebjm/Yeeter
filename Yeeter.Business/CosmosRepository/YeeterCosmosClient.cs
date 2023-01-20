using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Fluent;

namespace Yeeter.Business.CosmosRepository;

public class YeeterCosmosClient
{
    private readonly string _connectionString;
    public YeeterCosmosClient(string connectionString)
    {
        _connectionString = connectionString;
    }

    private CosmosClient _client = null;
    private CosmosClient Client
    {
        get
        {
            if (_client is null)
            {
                CosmosSerializationOptions serializerOptions = new()
                {
                    PropertyNamingPolicy = CosmosPropertyNamingPolicy.CamelCase
                };

                _client = new CosmosClientBuilder(_connectionString)
                    .WithSerializerOptions(serializerOptions)
                    .Build();
            }
            return _client;
        }
    }
    private Database _database = null;
    private Database Database
    {
        get
        {
            if (_database is null)
            {
                _database = Client.GetDatabase("YeeterDatabase");
            }
            return _database;
        }
    }
    private Container _container = null;
    private Container Container
    {
        get
        {
            if (_container is null)
            {
                _container = Database.GetContainer("YeeterContainer");
            }
            return _container;
        }
    }

    public async Task<IEnumerable<T>> GetManyAsync<T>(string query)
    {
        var items = new List<T>();

        var iterator = Container.GetItemQueryIterator<T>(query);
        while (iterator.HasMoreResults)
        {
            var response = await iterator.ReadNextAsync();
            items.AddRange(response.Resource);
        }
        return items;
    }

    public async Task<T> GetKey<T>(string id, string key)
    {
        var readKey = new PartitionKey(id);
        ItemResponse<T> readResponse = await _container.ReadItemAsync<T>(
            id: id,
            partitionKey: readKey
        );
        return readResponse.Resource;
    }
}