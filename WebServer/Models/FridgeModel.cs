using System;
using System.Collections.Generic;

namespace WebServer.Models
{
    public class FridgeModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = String.Empty;
        public int? Year { get; set; }

        public virtual List<Fridge>? Fridges { get; set; }
    }
}
