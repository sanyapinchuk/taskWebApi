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
    public class CreateProductCommandTests : TestCommandBase
    {
        [Fact]
        public async Task CreateProductCommand_Success()
        {
            //Arrange
            var Id = 20;
            var Name = "Грибы";
            var Default_Quantity = 10;

            var product = new Product
            {
                Id = Id,
                Name = Name,
                Default_quantity = Default_Quantity
            };

            //Act

            dataContext.Products.Add(product);
            dataContext.SaveChanges();
            //Assert
            Assert.NotNull(
                await dataContext.Products.SingleOrDefaultAsync(product=>
                product.Id == Id && product.Name == Name &&
                product.Default_quantity == Default_Quantity
                ));
        }
    }
}
