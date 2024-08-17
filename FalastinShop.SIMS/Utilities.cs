using FalastinShop.Simple_Inventory_Management_System.InventoryManagment;
using FalastinShop.Simple_Inventory_Management_System.ProductManagment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FalastinShop.Simple_Inventory_Management_System
{
    public class Utilities
    {
       private static Inventory inventory = new();

        public static void StartSelection()
        {
             
            ShowChoicesMenue();
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
                default:
                    Console.WriteLine("Please enter valid selection!");
                    StartSelection();
                    break;

            }
             
        }
        public static void ShowChoicesMenue()
        {
            
            Console.WriteLine("Choose the action number: ");

            Console.WriteLine("1. Add a product\n2. View all products\n3. Edit a product\n4. Delete a product\n5. Search for a product\n6. Exit");


        }

        public static void AddProductChoice()
        {
             
            var itemName = "";
            do
            {
                Console.WriteLine("Please enter Product name");
                itemName = Console.ReadLine() ?? "";

            } while (itemName == "");
            
            
            Console.WriteLine("Please enter Product Quntity");
            var qun =  Console.ReadLine();
            int.TryParse(qun, out int itemQuntity);

            Console.WriteLine("Please enter Product price");
            var price = Console.ReadLine();
            double.TryParse(price, out double itemPrice);


            Console.WriteLine("Please enter price currency Dollar, Euro or Bound");
            var currency = Console.ReadLine();
            Enum.TryParse(currency , out Currency itemcurrency);
            
            
            var product  = new Product(itemName,itemQuntity,itemPrice,itemcurrency);
            inventory.AddProduct(product);
 
            StartSelection();
         }


        public static void ViewAllProductsChoice()
        {
             
            inventory.PrintAllProducts();
            StartSelection();
        }

    }
}
