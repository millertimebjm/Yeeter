using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;
using Microsoft.EntityFrameworkCore.SqlServer;
using Yeeter.Models;

namespace Yeeter.Business.EntityFrameworkRepository;

public class YeeterDbContext : DbContext
{
    public readonly IYeeterConfiguration _yeeterConfiguration;
    public YeeterDbContext(
        IYeeterConfiguration yeeterConfiguration) : base()
    {
        _yeeterConfiguration = yeeterConfiguration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            if (!string.IsNullOrWhiteSpace(_yeeterConfiguration.GetYeeterSqlServerConnectionString()))
                optionsBuilder.UseSqlServer(_yeeterConfiguration.GetYeeterSqlServerConnectionString());
            else if (!string.IsNullOrWhiteSpace(_yeeterConfiguration.GetYeeterInMemoryDatabaseConnectionString()))
                optionsBuilder.UseInMemoryDatabase(_yeeterConfiguration.GetYeeterInMemoryDatabaseConnectionString());
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

        modelBuilder.Entity<Yeet>()
            .ToTable("Yeet");
        modelBuilder.Entity<User>()
            .ToTable("User");
        // .HasMany(_ => _.Yeets)
        // .WithOne(_ => _.User)
        // .HasForeignKey(_ => _.UserId);
    }
}