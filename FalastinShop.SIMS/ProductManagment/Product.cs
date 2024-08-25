namespace FalastinShop.SIMS.ProductManagment;
public class Product
     {
        public Guid Id { get; }
        public string Name { get;   set; } 
        public int Quantity { get;   set; }  
        public Price Price { get;   set; }
 

         public Product(string name, int quantity, double itemPrice ,  Currency currency)
         {
            Id = Guid.NewGuid();
            Name = name;
            Quantity = quantity;
            Price = new Price() { ItemPrice  = itemPrice, Currency = currency};
           
         }
    }
 