using System;

namespace WebServer.Models
{
    public class Fridge_Product
    {
        public int Id { get; set; }

        public int FridgeId { get; set; }
        public virtual Fridge? Fridge { get; set; }

        public int ProductId { get; set; }  
        public virtual Product? Product { get; set; }

        public int Quantity { get; set; } 
    }
}
