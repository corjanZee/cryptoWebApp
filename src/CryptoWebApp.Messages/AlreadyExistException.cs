namespace CryptoWebApp.Messages;

public class AlreadyExistException(CryptoModel cryptoModel) : Exception
{
    public CryptoModel CyCryptoModel { get; } = cryptoModel;
}