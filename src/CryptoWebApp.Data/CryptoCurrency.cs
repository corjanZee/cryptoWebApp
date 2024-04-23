using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CryptoWebApp.Data;

public class CryptoCurrency
{
    public CryptoCurrency(string code, string name, string description)
    {
        Id = ObjectId.GenerateNewId();
        Description = description;
        Code = !string.IsNullOrWhiteSpace(code) ? code : throw new ArgumentNullException(nameof(code));
        Name = !string.IsNullOrWhiteSpace(name) ? name : throw new ArgumentNullException(nameof(name));
    }

    [BsonId]
    public ObjectId Id { get; }
    public string Code { get; }
    public string Name { get; }
    public string Description { get; }
    
}