namespace FalastinShop.SIMS.ProductManagment;
public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Quantity { get; set; }
    public Price Price { get; set; }

    public Product() { }

    public Product(string name, int quantity, double itemPrice, Currency currency)
    {
        Name = name;
        Quantity = quantity;
        Price = new Price() { ItemPrice = itemPrice, Currency = currency };
    }
}
