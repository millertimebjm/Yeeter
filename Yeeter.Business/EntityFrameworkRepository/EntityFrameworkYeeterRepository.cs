using Yeeter.Models;
using Yeeter.Common;
using Microsoft.EntityFrameworkCore;

namespace Yeeter.Business.EntityFrameworkRepository;

public class EntityFrameworkYeeterRepository : IYeeterRepository
{
    private readonly YeeterDbContext _yeeterDbContext;
    public EntityFrameworkYeeterRepository(YeeterDbContext yeeterDbContext)
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
            .OrderByDescending(_ => _.CreatedDate)
            .Take(count)
            .ToListAsync();
    }

    public async Task<IEnumerable<Yeet>> GetYeetsByUserId(string id, int count)
    {
        if (count < 1) count = 1;
        if (count > 20) count = 20;

        return await _yeeterDbContext
            .Yeets
            .Where(_ => _.UserId == id)
            .OrderByDescending(_ => _.CreatedDate)
            .Take(count)
            .ToListAsync();
    }
}