namespace CryptoWebApp.Messages;

public interface ICryptoCurrencyRepository
{
    Task<IEnumerable<CryptoModel>> GetAllAsync(CancellationToken cancellationToken);
    Task<IEnumerable<CryptoModel>> GetAsync(string code, string name, CancellationToken cancellationToken);
    Task<CryptoModel> CreateAsync(CryptoModel cryptoModel, CancellationToken cancellationToken);
    Task<CryptoModel?> Update(string id, CryptoModel cryptoModel, CancellationToken cancellationToken);
    Task<CryptoModel?> Delete(string id, CancellationToken cancellationToken);
}