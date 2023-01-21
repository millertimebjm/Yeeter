using Yeeter.Models;

namespace Yeeter.Business;

public interface IYeetRepository
{
    Task<Yeet?> GetYeet(string id);
    Task<IEnumerable<Yeet>> GetYeets(int count);
}