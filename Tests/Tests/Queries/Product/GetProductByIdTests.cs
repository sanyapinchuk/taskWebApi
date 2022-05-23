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
    public class GetProductByIdTests : TestCommandBase
    {
        [Fact]
        public async Task GetProductById_Success()
        {
            //Arrange

            //Act

            //Assert
            Assert.NotNull(
                await repository.Product.GetProductByIdAsync(1));
        }

        [Fact]
        public async Task GetProductById_WrongId()
        {
            //Arrange

            //Act

            //Assert
            Assert.Null(
                await repository.Product.GetProductByIdAsync(30));
        }
    }
}
