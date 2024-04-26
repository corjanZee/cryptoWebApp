using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using MongoDB.EntityFrameworkCore.Extensions;

namespace CryptoWebApp.Data;

public class CryptoDbContext(DbContextOptions<CryptoDbContext> options) : DbContext(options)
{
    public DbSet<CryptoCurrency> CryptoCurrencies { get; init; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<CryptoCurrency>()
            .ToCollection("cryptoCurrencies")
            .HasKey(x => x.Id);
    }
    
    public static CryptoDbContext Create(IMongoDatabase database) =>
        new(new DbContextOptionsBuilder<CryptoDbContext>()
            .UseMongoDB(database.Client, database.DatabaseNamespace.DatabaseName)
            .Options);
}