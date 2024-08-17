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
        private  List<Product> products;  

        public Inventory() { 
           products = new List<Product>();
        }


        public void AddProduct(Product product)
        {
            products.Add(product);
         }


        public void PrintAllProducts()
        {

            if ( products.Count ==0 ) { Print.ConfigErrorConsole("There is no Products yet!");return; }

            
            foreach (Product product in products)
            {
                Console.WriteLine($"* {product.Name} with Qunitity {product.Quntity}. Price: {product.Price.ItemPrice} {product.Price.Currency}\n");

            }

        }

        public Product? FindByName(string name)
        {
            var res = products.Where(x => x.Name == name).FirstOrDefault();
            
            return res   ;
        }
        public void PrintProduct(Product product)
        {
            
            Console.WriteLine($"* {product.Name} with Qunitity {product.Quntity}. Price: {product.Price.ItemPrice} {product.Price.Currency}\n");

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
}
