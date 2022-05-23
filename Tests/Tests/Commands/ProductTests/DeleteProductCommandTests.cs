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
    public class DeleteProductCommandTests : TestCommandBase
    {
        [Fact]
        public async Task DeleteProductCommand_Success()
        {
            //Arrange
            var Id = 22;
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
            dataContext.Products.Remove(product);
            dataContext.SaveChanges();
            //Assert
            Assert.Null(
                await dataContext.Products.SingleOrDefaultAsync(product =>
                product.Id == Id));
        }

    }
}
