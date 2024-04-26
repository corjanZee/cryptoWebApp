using CryptoWebApp.Data;
using CryptoWebApp.Messages;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetSection("DatabaseConnection:DbConString").Value;

if (connectionString == null)
{
    Console.WriteLine("DbConnection is not set in the appsettings.json");
    Environment.Exit(0);
}

var client = new MongoClient(connectionString);
var db = client.GetDatabase("crypto_db");
builder.Services.AddDbContext<CryptoDbContext>(options => options.UseMongoDB(db.Client, db.DatabaseNamespace.DatabaseName));
builder.Services.AddControllers();
builder.Services.AddScoped<ICryptoCurrencyRepository, CryptoCurrencyRepository>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

app.MapControllers();

app.Run();