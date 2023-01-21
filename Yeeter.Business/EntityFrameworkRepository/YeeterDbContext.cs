using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;
using Microsoft.EntityFrameworkCore.SqlServer;
using Yeeter.Models;

namespace Yeeter.Business.EntityFrameworkRepository;

public class YeeterDbContext : DbContext
{
    public readonly string _sqlServerConnectionString;
    public readonly string _inMemoryConnectionString;
    public YeeterDbContext(
        string sqlServerConnectionString,
        string inMemoryConnectionString) : base()
    {
        _sqlServerConnectionString = sqlServerConnectionString;
        _inMemoryConnectionString = inMemoryConnectionString;
    }

    public override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            if (!string.IsNullOrWhiteSpace(_sqlServerConnectionString))
                optionsBuilder.UseSqlServer(_sqlServerConnectionString);
            if (!string.IsNullOrWhiteSpace(_inMemoryConnectionString))
                optionsBuilder.UsInMemory(_inMemoryConnectionString);
            else
                throw new NotImplementedException();
        }
    }

    public DbSet<Yeet> Yeets { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //Configure default schema
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Yeet>().ToTable("Yeet");
        modelBuilder.Entity<User>().ToTable("User");
    }
}