using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebServer.Models;
namespace Tests.Comparers
{
    public class FridgeComparer : IEqualityComparer<Fridge_Product>
    {
        public bool Equals(Fridge_Product? x, Fridge_Product? y)
        {
            if (x == null && y == null)
                return true;
            if (x == null || y == null)
                return false;
            return
                x.Id == y.Id &&
                x.Quantity == y.Quantity &&
                x.FridgeId == y.FridgeId &&
                x.ProductId == y.ProductId;
        }

        public int GetHashCode([DisallowNull] Fridge_Product obj)
        {
            return 0;
        }
    }
}
