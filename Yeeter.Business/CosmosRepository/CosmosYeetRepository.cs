using Yeeter.Models;

namespace Yeeter.Business.CosmosRepository;

public class CosmosYeetRepository : IYeetRepository
{
    private readonly YeeterCosmosClient _client;
    public CosmosYeetRepository(YeeterCosmosClient client)
    {
        _client = client;
    }

    public async Task<Yeet> GetYeet(string id)
    {

        return await _client.GetKey<Yeet>(id, id);
    }

    public async Task<IEnumerable<Yeet>> GetYeets(int count)
    {
        var query = @$"
SELECT TOP {count} *
FROM c
ORDER BY c.CreatedDate DESC
        ";

        return await _client.GetManyAsync<Yeet>(query);
    }
}