using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FalastinShop.Simple_Inventory_Management_System.ProductManagment
{
    public class Product
    {
        private Guid id;
        public string Name { get; private set; } 
        public int Quntity { get; private set; }  
        public Price Price { get; private set; }
 

         public Product(string name, int quntity,double itemPrice ,  Currency currency)
         {
            Name = name;
            Quntity = quntity;
            Price = new Price() { ItemPrice  = itemPrice, Currency = currency};
            id =Guid.NewGuid();
         }





    }
}
