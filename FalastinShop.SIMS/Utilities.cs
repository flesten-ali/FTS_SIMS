using FalastinShop.Simple_Inventory_Management_System.InventoryManagment;
using FalastinShop.Simple_Inventory_Management_System.PrintConfig;
using FalastinShop.Simple_Inventory_Management_System.ProductManagment;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FalastinShop.Simple_Inventory_Management_System
{
    public class Utilities
    {
       private static Inventory inventory = new();
        public static void ShowChoicesMenue()
        {

            Console.WriteLine("Choose the action number: ");

            Console.WriteLine("1. Add a product\n2. View all products\n3. Edit a product\n4. Delete a product\n5. Search for a product\n6. Exit");


        }
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
                case "3":
                    EditProductChoice();
                    break;
                case "4":
                    DeleteproductChoice();
                    break;
                case "5":
                    Searchforproduct();
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

            var itemQuntity = EnterQuntity();

            var itemPrice = EnterPriceValue();

            var itemcurrency = EnterCurrency();

            var product = new Product(itemName, itemQuntity, itemPrice, itemcurrency);
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
        public static int EnterQuntity()
        {
            Console.WriteLine("Please enter Product Quntity");
            var qun = Console.ReadLine();
            int.TryParse(qun, out int itemQuntity);
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
            inventory.AddProduct(product);
            Print.ConfigSuccessConsole("Product added successfully!");

            StartSelection();
         }
        
      
        public static void ViewAllProductsChoice()
        {
             
            inventory.PrintAllProducts();
            StartSelection();
        }
        public static void EditProductChoice()
        {

            Console.WriteLine("Enter the product name that you want to edit ");
            string name = EnterName();
            var existProduct = inventory.FindByName(name);
            
            if (existProduct != null)
            {
                inventory.PrintProduct(existProduct);
                Console.WriteLine("Do you want to update Name ? y/n ?");
                var choice1 = Console.ReadLine();
                if(choice1 == "y")
                {
                    existProduct.Name = EnterName();
                }



                Console.WriteLine("Do you want to update Quntity ? y/n ?");
                choice1 = Console.ReadLine();
                if (choice1 == "y")
                {
                    existProduct.Quntity = EnterQuntity();
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

                Print.ConfigSuccessConsole($"Product updated successfully!");


            }
            else
            {
                Print.ConfigErrorConsole($"Product with name {name} doesnot exist!");
            }
            StartSelection();

        }
        public static void DeleteproductChoice()
        {
            Console.WriteLine("Enter name of product you want to delete");
            var name = EnterName();
            var exist = inventory.FindByName(name);
            if (exist != null)
            {
                inventory.DeleteByName(name);
                Print.ConfigSuccessConsole($"Product deleted successfully!");

            }
            else
            {
                Print.ConfigErrorConsole($"Product with name {name} doesnot exist!");
            }
            StartSelection();
        }

        public static void Searchforproduct()
        {
            Console.WriteLine("Enter name of product you want to search");
            var name = EnterName();
            var existProduct = inventory.FindByName(name);
            if (existProduct != null)
            {
                inventory.PrintProduct(existProduct);
                Print.ConfigSuccessConsole("Product Found!");
            }
            else
            {
                Print.ConfigErrorConsole($"Product with name {name} doesnot exist!");
            }
            StartSelection();
        }
        public static void Exit()
        {
            Print.ConfigSuccessConsole("Thanks!Press Enter to Exit");
        }

    }
}
