using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FalastinShop.Simple_Inventory_Management_System.ProductManagment
{
    public class Product
    {
        public Guid Id { get; }
        public string Name { get;   set; } 
        public int Quntity { get;   set; }  
        public Price Price { get;   set; }
 

         public Product(string name, int quntity,double itemPrice ,  Currency currency)
         {
            Name = name;
            Quntity = quntity;
            Price = new Price() { ItemPrice  = itemPrice, Currency = currency};
            Id =Guid.NewGuid();
         }





    }
}
