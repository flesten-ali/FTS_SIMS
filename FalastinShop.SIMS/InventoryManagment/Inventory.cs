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







    }
}
