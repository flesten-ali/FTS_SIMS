using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace FalastinShop.SIMS.MongoDB.Models;

public class CurrencyDoc
{
    [BsonId]
    public ObjectId CurrencyId {  get; set; }
  
    public string CurrencyCode { get; set; }
}
