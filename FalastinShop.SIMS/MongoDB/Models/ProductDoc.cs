using FalastinShop.SIMS.ProductManagment;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace FalastinShop.SIMS.MongoDB.Models;

public class ProductDoc
{
    [BsonId]
    public ObjectId Id { get; set; }

    public string Name {  get; set; }
    public int Quantity { get; set; }   
    public double Price {  get; set; }

    public ObjectId CurrencyId {  get; set; }
}
