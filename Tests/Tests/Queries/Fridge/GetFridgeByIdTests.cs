using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tests.Common;
using Xunit;

namespace Tests.Queries
{
    public class GetFridgeByIdTests: TestCommandBase
    {

        [Fact]
        public async Task GetFridgeById_Success()
        {
            //Arrange

            //Act

            //Assert
            Assert.NotNull(
                await repository.Fridge.GetFridgeByIdAsync(1));
        }

        [Fact]
        public async Task GetFridgeById_WrongId()
        {
            //Arrange

            //Act

            //Assert
            Assert.Null(
                await repository.Fridge.GetFridgeByIdAsync(30));
        }
    }
}
