using Yeeter.Models;

namespace Yeeter.Business.CosmosRepository;

public class EfYeetRepository : IYeetRepository
{
    public EfYeetRepository()
    {

    }

    public async Task<Yeet> GetYeet(string id)
    {
        throw new NotImplementedException();
    }
    public async Task<IEnumerable<Yeet>> GetYeets(int count)
    {
        throw new NotImplementedException();
    }
}