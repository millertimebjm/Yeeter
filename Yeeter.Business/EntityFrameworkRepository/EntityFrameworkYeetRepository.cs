using Yeeter.Models;

namespace Yeeter.Business.EntityFrameworkRepository;

public class EntityFrameworkYeetRepository : IYeetRepository
{
    public EntityFrameworkYeetRepository()
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