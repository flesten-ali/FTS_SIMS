using FalastinShop.SIMS.PrintConfig;
using FalastinShop.SIMS.ProductManagment;
namespace FalastinShop.SIMS;

public class Inventory
{
    private List<Product> products;

    public Inventory()
    {
        products = new List<Product>();
    }

    public void AddProduct(Product product)
    {
        products.Add(product);
    }

    public void PrintAllProducts()
    {
        if (products.Count == 0)
        {
            Print.ConfigErrorConsole("There is no Products yet!");
            return;
        }

        foreach (var product in products)
        {
            Console.WriteLine($"* {product.Name} with Quantity {product.Quantity}. Price: {product.Price.ItemPrice} {product.Price.Currency}\n");
        }
    }
    public Product? FindByName(string name)
    {
        var res = products.FirstOrDefault(x => x.Name == name);

        return res;
    }

    public void PrintProduct(Product product)
    {
        Console.WriteLine($"* {product.Name} with Quantity {product.Quantity}. Price: {product.Price.ItemPrice} {product.Price.Currency}\n");
    }

    public void DeleteByName(string name)
    {
        var product = FindByName(name);
        if (product != null)
        {
            products.Remove(product);
        }
    }
}
