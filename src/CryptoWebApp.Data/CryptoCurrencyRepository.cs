using CryptoWebApp.Messages;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;

namespace CryptoWebApp.Data;

public class CryptoCurrencyRepository(CryptoDbContext cryptoDbContext) : ICryptoCurrencyRepository
{
    public async Task<IEnumerable<CryptoModel>> GetAllAsync(CancellationToken cancellationToken)
    {
        return (await cryptoDbContext.CryptoCurrencies.ToListAsync(cancellationToken: cancellationToken))
            .Select(x => x.ToModel());
    }

    public async Task<IEnumerable<CryptoModel>> GetAsync(CryptoModel cryptoModel, CancellationToken cancellationToken)
    {
        var value = await cryptoDbContext.CryptoCurrencies.Where(x =>
                x.Code == cryptoModel.Code || x.Name == cryptoModel.Name)
            .ToListAsync(cancellationToken);
        return value.Select(x => x.ToModel());
    }

    public async Task<CryptoModel> CreateAsync(CryptoModel cryptoModel, CancellationToken cancellationToken)
    {
        var values = await GetAsync(cryptoModel, cancellationToken);
        if (values.Any())
            throw new AlreadyExistException(cryptoModel);
        var result = await cryptoDbContext.CryptoCurrencies.AddAsync(
            new CryptoCurrency(
                cryptoModel.Code,
                cryptoModel.Name,
                cryptoModel.Description), cancellationToken);
        await cryptoDbContext.SaveChangesAsync(cancellationToken);
        return result.Entity.ToModel();
    }

    public Task<CryptoModel> Update(CryptoModel cryptoModel, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<CryptoModel?> Delete(string id, CancellationToken cancellationToken)
    {
        if (!ObjectId.TryParse(id, out var cryptoId))
            return default;
        var item = await cryptoDbContext.CryptoCurrencies.FirstOrDefaultAsync(x => x.Id == cryptoId, cancellationToken: cancellationToken);
        if (item == null)
            return default;

        cryptoDbContext.CryptoCurrencies.Remove(item);
        await cryptoDbContext.SaveChangesAsync(cancellationToken);
        return item.ToModel();
    }
}