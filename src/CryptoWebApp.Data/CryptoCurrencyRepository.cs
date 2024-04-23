using CryptoWebApp.Messages;
using Microsoft.EntityFrameworkCore;

namespace CryptoWebApp.Data;

public class CryptoCurrencyRepository(CryptoDbContext cryptoDbContext) : ICryptoCurrencyRepository
{
    private readonly CryptoDbContext _cryptoDbContext = cryptoDbContext;

    public async Task<IEnumerable<CryptoModel>> GetAllAsync(CancellationToken cancellationToken)
    {
        return (await _cryptoDbContext.CryptoCurrencies.ToListAsync(cancellationToken: cancellationToken))
            .Select(x => x.ToModel());
    }

    public async Task<IEnumerable<CryptoModel>> GetAsync(CryptoModel cryptoModel, CancellationToken cancellationToken)
    {
        var value = await _cryptoDbContext.CryptoCurrencies.Where(x =>
                x.Code.Equals(cryptoModel.Code, StringComparison.InvariantCulture) || x.Name.Equals(cryptoModel.Name))
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
                cryptoModel.Description), 
            cancellationToken);
        return result.Entity.ToModel();
    }

    public Task<CryptoModel> Update(CryptoModel cryptoModel, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}