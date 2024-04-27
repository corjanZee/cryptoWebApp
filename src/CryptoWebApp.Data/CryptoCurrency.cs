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

    public void Update(string code, string name, string description)
    {
        if (!string.IsNullOrWhiteSpace(code))
            Code = code;

        if (!string.IsNullOrWhiteSpace(name))
            Name = name;

        if (!string.IsNullOrWhiteSpace(description))
            Description = description;
    }
    
    public ObjectId Id { get; private set; }
    public string Code { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    
}