using Yeeter.Models;

namespace Yeeter.Business.CosmosRepository;

public class CosmosYeeterRepository : IYeeterRepository
{
    private readonly YeeterCosmosClient _client;
    public CosmosYeeterRepository(YeeterCosmosClient client)
    {
        _client = client;
    }

    public async Task<Yeet?> GetYeet(string id)
    {

        return await _client.GetKey<Yeet>(id, id);
    }

    public async Task<IEnumerable<Yeet>> GetYeets(int count)
    {
        var query = @$"
SELECT TOP {count} c.Yeets
FROM c
ORDER BY c.CreatedDate DESC
        ";

        return await _client.GetManyAsync<Yeet>(query);
    }

    public async Task<IEnumerable<Yeet>> GetYeetsByUserId(string id, int count)
    {
        var query = @$"
SELECT TOP {count} c.Yeets
FROM c
WHERE c.Id = @Id
ORDER BY c.CreatedDate DESC
        ";
        return await _client.GetManyAsync<Yeet>(query, new { id });
    }

    public async Task InitializeData()
    {
        throw new NotImplementedException();
    }
}