using Yeeter.Models;
using Yeeter.Common;
using Microsoft.EntityFrameworkCore;
using Faker;

namespace Yeeter.Business.EntityFrameworkRepository;

public class EntityFrameworkYeeterRepository : IYeeterRepository
{
    private readonly YeeterDbContext _yeeterDbContext;
    private readonly IYeeterConfiguration _yeeterConfiguration;
    public EntityFrameworkYeeterRepository(
        YeeterDbContext yeeterDbContext,
        IYeeterConfiguration yeeterConfiguration)
    {
        _yeeterDbContext = yeeterDbContext;
        _yeeterConfiguration = yeeterConfiguration;
    }

    public async Task<Yeet?> GetYeet(string id)
    {
        if (!IdGenerator.IsValidId(id))
            return null;

        return await _yeeterDbContext
            .Yeets
            .Include(_ => _.User)
            .SingleOrDefaultAsync(_ => _.Id == id);
    }

    public async Task<IEnumerable<Yeet>> GetYeets(int count)
    {
        await InitializeData();

        return await _yeeterDbContext
            .Yeets
            .AsNoTracking()
            .Include(_ => _.User)
            .OrderByDescending(_ => _.CreatedDate)
            .Take(count)
            .ToListAsync();
    }

    public async Task<User?> GetYeetsByUserId(string id, int count)
    {
        return await _yeeterDbContext
            .Users
            .Include(_ => _.Yeets
                .OrderByDescending(_ => _.CreatedDate)
                .Take(count))
            .SingleOrDefaultAsync(_ => _.Id == id);
    }

    public async Task InitializeData()
    {
        if (!_yeeterConfiguration.GetYeeterDataInitialized())
        {
            await InternalInitializeData();
            _yeeterConfiguration.SetYeeterDataInitialized(true);
        }
    }

    public async Task InternalInitializeData()
    {
        Console.WriteLine("Starting Initialize:");
        var random = new Random();
        for (int i = 0; i < 10; i++)
        {
            User user = new User(
                Faker.Name.FullName(),
                "@" + Faker.Name.Last() + Faker.Name.First())
            {
                CreatedDate = DateTime.UtcNow.AddSeconds(-random.Next(1, 60 * 60 * 24 * 365)),
            };
            await _yeeterDbContext.AddAsync(user);
            await _yeeterDbContext.SaveChangesAsync();
            Console.WriteLine(".");
            for (int j = 0; j < 10; j++)
            {
                // var yeets = new List<Yeet>();
                var yeet = new Yeet(string.Join(" ", Faker.Lorem.Sentences(3)), user.Id)
                {
                    CreatedDate = DateTime.UtcNow.AddSeconds(-random.Next(1, 60 * 60 * 24 * 365)),
                };
                await _yeeterDbContext.AddAsync(yeet);
                await _yeeterDbContext.SaveChangesAsync();
                // await _yeeterDbContext.AddRangeAsync(yeets);
                Console.Write(".");
            }
        }
        Console.WriteLine("Done Initialize");
    }
}