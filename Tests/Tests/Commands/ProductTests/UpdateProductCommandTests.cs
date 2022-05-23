using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tests.Common;
using Xunit;
using WebServer.Data;
using Microsoft.EntityFrameworkCore;
using WebServer.Models;

namespace Tests.Commands.FridgeTests
{
    public class UpdateProductCommandTests : TestCommandBase
    {
        [Fact]
        public async Task UpdateProductCommand_Success()
        {
            //Arrange
            var Id = 1;
            var Name = "Творог";
            var Default_Quantity = 5;

            var product2 = new Product
            {
                Id = Id,
                Name = Name,
                Default_quantity = Default_Quantity
            };


            // repository.Product.UpdateAsync(product2);
            var pr = await repository.Product.GetProductByIdAsync(Id);
            if (pr == null)
                throw new Xunit.Sdk.NullException(pr);
            pr.Default_quantity = Default_Quantity;
            pr.Name = Name;
            repository.Save();
           //repository.Save();
           //dataContext.SaveChanges();
           //Assert
            Assert.NotNull(
                await dataContext.Products.SingleOrDefaultAsync(product =>
                product.Id == Id && product.Name == Name &&
                product.Default_quantity == Default_Quantity
                ));
        }
    }
}
