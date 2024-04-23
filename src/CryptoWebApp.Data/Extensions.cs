using CryptoWebApp.Messages;

namespace CryptoWebApp.Data;

internal static class Extensions
{
    internal static CryptoModel ToModel(this CryptoCurrency cryptoCurrency) =>
        new(cryptoCurrency.Code, cryptoCurrency.Name, cryptoCurrency.Description);
    
    internal static async Task<CryptoModel> ToModelAsync(this Task<CryptoCurrency> cryptoCurrency) => 
        (await cryptoCurrency)
        .ToModel();
}