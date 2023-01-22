using Yeeter.Models;

namespace Yeeter.Business;

public interface IYeeterRepository
{
    Task<Yeet?> GetYeet(string id);
    Task<IEnumerable<Yeet>> GetYeets(int count);
    Task<IEnumerable<Yeet>> GetYeetsByUserId(string userId, int count);
    Task InitializeData();
}