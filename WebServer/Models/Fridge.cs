using System;
using System.Collections.Generic;

namespace WebServer.Models
{
    public class Fridge
    {
        public int Id { get; set; }
        public string Name { get; set; } = String.Empty;    
        public string? Owner_name { get; set; }

        public virtual List<Fridge_Product>? Fridge_Products { get; set; } 

        public int FridgeModelId { get; set; }
        public virtual FridgeModel? FridgeModel { get; set; }
    }
}
