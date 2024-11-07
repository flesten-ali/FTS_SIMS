using FalastinShop.SIMS.DB;
using FalastinShop.SIMS.PrintConfig;
using FalastinShop.SIMS.ProductManagment;
namespace FalastinShop.SIMS;

public class Utilities
{
    private static Inventory _inventory = new();
    private static readonly IAppDB _appDB = new AppDB();

    public static void ShowChoicesMenu()
    {
        Console.WriteLine("Choose the action number: ");
        Console.WriteLine("""
                1. Add a product\n
                2. View all products\n
                3. Edit a product\n
                4. Delete a product\n
                5. Search for a product\n
                6. Exit
                """);
    }

    public static void StartSelection()
    {
        ShowChoicesMenu();
        string? actionNumber = Console.ReadLine();
        Console.Clear();

        switch (actionNumber)
        {
            case "1":
                AddProductChoice();
                break;
            case "2":
                ViewAllProductsChoice();
                break;
            case "3":
                EditProductChoice();
                break;
            case "4":
                DeleteProductChoice();
                break;
            case "5":
                SearchForProduct();
                break;
            case "6":
                Exit();
                break;
            default:
                Console.WriteLine("Please enter valid selection!");
                StartSelection();
                break;
        }
    }

    public static Product EnterProductInfo()
    {
        var itemName = EnterName();

        var itemQuantity = EnterQuantity();

        var itemPrice = EnterPriceValue();

        var itemCurrency = EnterCurrency();

        var product = new Product(itemName, itemQuantity, itemPrice, itemCurrency);
        return product;
    }

    public static string EnterName()
    {
        var itemName = "";
        do
        {
            Console.WriteLine("Please enter Product name");
            itemName = Console.ReadLine() ?? "";
        } while (itemName == "");
        return itemName;
    }

    public static int EnterQuantity()
    {
        Console.WriteLine("Please enter Product Quntity");
        var qun = Console.ReadLine();
        int.TryParse(qun, out var itemQuntity);
        return itemQuntity;
    }

    public static double EnterPriceValue()
    {
        Console.WriteLine("Please enter Product price");
        var price = Console.ReadLine();
        double.TryParse(price, out double itemPrice);
        return itemPrice;
    }

    public static Currency EnterCurrency()
    {
        Console.WriteLine("Please enter price currency Dollar, Euro or Bound");
        var currency = Console.ReadLine();
        Enum.TryParse(currency, out Currency itemcurrency);
        return itemcurrency;
    }

    public static void AddProductChoice()
    {
        var product = EnterProductInfo();
        _inventory.AddProduct(product);
        _appDB.SaveProduct(product);
        Print.ConfigSuccessConsole("Product added successfully!");

        StartSelection();
    }

    public static void ViewAllProductsChoice()
    {
        _inventory.PrintAllProducts();
        StartSelection();
    }

    public static void EditProductChoice()
    {
        Console.WriteLine("Enter the product name that you want to edit ");
        string name = EnterName();
        var existProduct = _inventory.FindByName(name);

        if (existProduct != null)
        {
            _inventory.PrintProduct(existProduct);
            Console.WriteLine("Do you want to update Name ? y/n ?");
            var choice1 = Console.ReadLine();
         
            if (choice1 == "y")
            {
                existProduct.Name = EnterName();
            }

            Console.WriteLine("Do you want to update Quantity ? y/n ?");
            choice1 = Console.ReadLine();
            if (choice1 == "y")
            {
                existProduct.Quantity = EnterQuantity();
            }

            Console.WriteLine("Do you want to update Price Value ? y/n ?");
            choice1 = Console.ReadLine();

            if (choice1 == "y")
            {
                existProduct.Price.ItemPrice = EnterPriceValue();
            }

            Console.WriteLine("Do you want to update Price Currency ? y/n ?");
            choice1 = Console.ReadLine();

            if (choice1 == "y")
            {
                existProduct.Price.Currency = EnterCurrency();
            }
            _appDB.UpdateProduct(name, existProduct);
            Print.ConfigSuccessConsole($"Product updated successfully!");
        }
        else
        {
            Print.ConfigErrorConsole($"Product with name {name} does not exist!");
        }
        StartSelection();
    }

    public static void DeleteProductChoice()
    {
        Console.WriteLine("Enter name of product you want to delete");
        var name = EnterName();
        var exist = _inventory.FindByName(name);
        if (exist != null)
        {
            _inventory.DeleteByName(name);
            _appDB.DeleteProduct(name);  
            Print.ConfigSuccessConsole($"Product deleted successfully!");
        }
        else
        {
            Print.ConfigErrorConsole($"Product with name {name} does not exist!");
        }
        StartSelection();
    }

    public static void SearchForProduct()
    {
        Console.WriteLine("Enter name of product you want to search");
        var name = EnterName();
        var existProduct = _inventory.FindByName(name);
        if (existProduct != null)
        {
            _inventory.PrintProduct(existProduct);
            Print.ConfigSuccessConsole("Product Found!");
        }
        else
        {
            Print.ConfigErrorConsole($"Product with name {name} does not exist!");
        }
        StartSelection();
    }

    public static void Exit()
    {
        Print.ConfigSuccessConsole("Thanks!Press Enter to Exit");
    }
}

