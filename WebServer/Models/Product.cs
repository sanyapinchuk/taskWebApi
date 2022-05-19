using System;
using System.Collections.Generic;

namespace WebServer.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int? Default_quantity { get; set; }

        public virtual List<Fridge_Product> Fridge_Products { get; set; }
    }
}
