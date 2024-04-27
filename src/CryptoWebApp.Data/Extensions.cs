using CryptoWebApp.Messages;

namespace CryptoWebApp.Data;

internal static class Extensions
{
    internal static CryptoModel ToModel(this CryptoCurrency cryptoCurrency) =>
        new(cryptoCurrency.Id.ToString(), cryptoCurrency.Code, cryptoCurrency.Name, cryptoCurrency.Description);
}