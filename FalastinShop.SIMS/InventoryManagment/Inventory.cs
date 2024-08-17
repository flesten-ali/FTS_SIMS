using FalastinShop.Simple_Inventory_Management_System.PrintConfig;
using FalastinShop.Simple_Inventory_Management_System.ProductManagment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FalastinShop.Simple_Inventory_Management_System.InventoryManagment
{
    public class Inventory
    {
        private static List<Product> products;  

        public Inventory() { 
           products = new List<Product>();
        }


        public void AddProduct(Product product)
        {
            products.Add(product);
            Print.ConfigSuccessConsole("Product added successfully!");
        }


        public void PrintAllProducts()
        {

            if ( products.Count ==0 ) { Print.ConfigErrorConsole("There is no Products yet!");return; }

            
            foreach (Product product in products)
            {
                Console.WriteLine($"* {product.Name} with Qunitity {product.Quntity}. Price: {product.Price.ItemPrice} {product.Price.Currency}\n");

            }

        }


    }
}
