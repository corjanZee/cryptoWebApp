using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using MongoDB.EntityFrameworkCore.Extensions;

namespace CryptoWebApp.Data;

public class CryptoDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<CryptoCurrency> CryptoCurrencies { get; init; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<CryptoCurrency>().ToCollection("CryptoCurrencies");
    }
}