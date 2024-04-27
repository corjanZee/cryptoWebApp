using System.Linq.Expressions;
using CryptoWebApp.Messages;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;

namespace CryptoWebApp.Data;

public class CryptoCurrencyRepository(CryptoDbContext cryptoDbContext) : ICryptoCurrencyRepository
{
    public async Task<IEnumerable<CryptoModel>> GetAllAsync(CancellationToken cancellationToken) =>
        (await cryptoDbContext.CryptoCurrencies.ToListAsync(cancellationToken: cancellationToken))
        .Select(x => x.ToModel());

    public Task<IEnumerable<CryptoModel>> GetAsync(string code, string name, CancellationToken cancellationToken)
    {
        if (!string.IsNullOrWhiteSpace(code) && !string.IsNullOrWhiteSpace(name))
        {
            return SearchAsync(x => x.Code == code || x.Name == name, cancellationToken);
        }

        if (!string.IsNullOrWhiteSpace(code))
        {
            return SearchAsync(x => x.Code == code, cancellationToken);
        }
        
        if (!string.IsNullOrWhiteSpace(name))
        {
            return SearchAsync(x => x.Name == name, cancellationToken);
        }

        throw new InvalidOperationException("Both the code and name cannot be null for searching");
    }

    public async Task<CryptoModel> CreateAsync(CryptoModel cryptoModel, CancellationToken cancellationToken)
    {
        var values = await GetAsync(cryptoModel.Code, cryptoModel.Name, cancellationToken);
        if (values.Any())
            throw new AlreadyExistException(cryptoModel);
        var result = await cryptoDbContext.CryptoCurrencies.AddAsync(
            new CryptoCurrency(
                cryptoModel.Code,
                cryptoModel.Name,
                cryptoModel.Description), 
            cancellationToken);
        await cryptoDbContext.SaveChangesAsync(cancellationToken);
        return result
            .Entity
            .ToModel();
    }

    public async Task<CryptoModel?> Update(string id, CryptoModel cryptoModel, CancellationToken cancellationToken)
    {
        if (!ObjectId.TryParse(id, out var cryptoId))
            return default;

        var item = await cryptoDbContext
            .CryptoCurrencies
            .FirstOrDefaultAsync(x => x.Id == cryptoId, cancellationToken);

        if (item == null)
            return default;

        if (IsTheSame(cryptoModel, item))
            return item.ToModel();

        item.Update(cryptoModel.Code, cryptoModel.Name, cryptoModel.Description);
        await cryptoDbContext.SaveChangesAsync(cancellationToken);
        return item.ToModel();
    }

    public async Task<CryptoModel?> Delete(string id, CancellationToken cancellationToken)
    {
        if (!ObjectId.TryParse(id, out var cryptoId))
            return default;
        
        var item = await cryptoDbContext
            .CryptoCurrencies
            .FirstOrDefaultAsync(x => x.Id == cryptoId, cancellationToken: cancellationToken);
        if (item == null)
            return default;

        cryptoDbContext
            .CryptoCurrencies
            .Remove(item);
        await cryptoDbContext.SaveChangesAsync(cancellationToken);
        return item.ToModel();
    }

    private static bool IsTheSame(CryptoModel cryptoModel, CryptoCurrency cryptoCurrency) =>
        cryptoModel.Code == cryptoCurrency.Code
        && cryptoModel.Name == cryptoCurrency.Name
        && cryptoModel.Description == cryptoCurrency.Description;

    private async Task<IEnumerable<CryptoModel>> SearchAsync(Expression<Func<CryptoCurrency, bool>> whereExpression, CancellationToken cancellationToken)
    {
        var items = await cryptoDbContext
            .CryptoCurrencies
            .Where(whereExpression)
            .ToListAsync(cancellationToken);

        return items.Select(x => x.ToModel());
    }
}