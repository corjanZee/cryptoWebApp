namespace CryptoWebApp.Messages;

public interface ICryptoCurrencyRepository
{
    Task<IEnumerable<CryptoModel>> GetAllAsync(CancellationToken cancellationToken);
    Task<IEnumerable<CryptoModel>> GetAsync(CryptoModel cryptoModel, CancellationToken cancellationToken);
    Task<CryptoModel> CreateAsync(CryptoModel cryptoModel, CancellationToken cancellationToken);
    Task<CryptoModel> Update(CryptoModel cryptoModel, CancellationToken cancellationToken);
}