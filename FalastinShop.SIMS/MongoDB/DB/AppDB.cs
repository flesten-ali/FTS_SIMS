using FalastinShop.SIMS.MongoDB.Models;
using FalastinShop.SIMS.ProductManagment;
using MongoDB.Bson;
using MongoDB.Driver;

namespace FalastinShop.SIMS.MongoDB.DB
{
    public class AppDB
    {
        public AppDB()
        {
            _client = new MongoClient(connectionUri);
            _database = _client.GetDatabase("Inventory");
        }

        const string connectionUri = "mongodb+srv://Falastin:password@cluster0.vpt1l.mongodb.net/?retryWrites=true&w=majority&appName=Cluster0";
        private readonly MongoClient _client;
        private readonly IMongoDatabase _database;

        public void SaveProduct(Product product)
        {
            var productCollection = _database.GetCollection<ProductDoc>("Products");

            var currencyCode = product.Price.Currency.ToString();
            var currencyId = FindCurrency(currencyCode);
            currencyId ??= InsertCurrency(currencyCode);

            InsertProduct(product, currencyId);
        }

        public void UpdateProduct(string name  , Product product)
        {
            var productCollection = _database.GetCollection<ProductDoc>("Products");

            var dbProduct = productCollection.Find(x=>x.Name == name).FirstOrDefault();

            if (dbProduct is null)
            {
                Console.WriteLine("Product Not Found!");
                return;
            }

            var currencyCode = product.Price.Currency.ToString();
            var currencyId = FindCurrency(currencyCode);
            currencyId ??= InsertCurrency(currencyCode);

            var filter = Builders<ProductDoc>.Filter.Eq(x => x.Name, name);
            var update = Builders<ProductDoc>.Update
                                               .Set(x => x.Name, product.Name)
                                               .Set(x => x.Quantity, product.Quantity)
                                               .Set(x => x.Price, product.Price.ItemPrice)
                                               .Set(x => x.CurrencyId, currencyId);

          var res =   productCollection.UpdateOne(filter, update);
            if(res.MatchedCount > 0)
                Console.WriteLine("Product updated successfully!");
            else
                Console.WriteLine("Update Faild!");
        }

        private void InsertProduct(Product product, ObjectId? currencyId)
        {
            var productCollection = _database.GetCollection<ProductDoc>("Products");

            var newProduct = new ProductDoc
            {
                Name = product.Name,
                Quantity = product.Quantity,
                Price = product.Price.ItemPrice,
                CurrencyId = (ObjectId)currencyId!,
            };
            productCollection.InsertOne(newProduct);
        }

        private ObjectId InsertCurrency(string currencyCode)
        {
            var currencyCollection = _database.GetCollection<CurrencyDoc>("Currencies");

            var currency = new CurrencyDoc
            {
                CurrencyCode = currencyCode,
            };
            currencyCollection.InsertOne(currency);
            return currency.CurrencyId;
        }

        public ObjectId? FindCurrency(string code)
        {
            var currencyCollection = _database.GetCollection<CurrencyDoc>("Currencies");

            var currency = currencyCollection.Find(x => x.CurrencyCode == code).FirstOrDefault();
            if (currency != null)
                return currency.CurrencyId;
            return null;
        }

        public void DeleteProduct(string name)
        {
            var productCollection = _database.GetCollection<ProductDoc>("Products");

            var dbProduct  = productCollection.Find(x => x.Name == name).FirstOrDefault();
            if(dbProduct ==  null)
            {
                Console.WriteLine("Product Not Found!");
                return;
            }
            var filter = Builders<ProductDoc>.Filter.Eq(x => x.Name, name);
            productCollection.DeleteOne(filter); 
        }
    }
}
