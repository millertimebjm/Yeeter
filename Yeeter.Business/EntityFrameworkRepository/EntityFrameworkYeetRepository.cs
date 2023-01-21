using Yeeter.Models;
using Yeeter.Common;

namespace Yeeter.Business.EntityFrameworkRepository;

public class EntityFrameworkYeetRepository : IYeetRepository
{
    private readonly YeeterDbContext _yeeterDbContext;
    public EntityFrameworkYeetRepository(YeeterDbContext yeeterDbContext)
    {
        _yeeterDbContext = yeeterDbContext;
    }

    public async Task<Yeet?> GetYeet(string id)
    {
        if (!IdGenerator.IsValidId(id))
            return null;

        return await _yeeterDbContext.Yeets.SingleOrDefaultAsync(_ => _.Id == id);
    }
    public async Task<IEnumerable<Yeet>> GetYeets(int count)
    {
        if (count < 1) count = 1;
        if (count > 20) count = 20;

        return await _yeeterDbContext
            .Yeets
            .OrderByDesc(_ => _.CreatedDate)
            .Take(count)
            .ToListAsync();
    }
}