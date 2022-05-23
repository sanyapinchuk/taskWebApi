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
    public class DeleeteFridgeCommandTests : TestCommandBase
    {
        [Fact]
        public async Task DeleteFridgeCommand_Success()
        {
            //Arrange
            var Id = 20;
            var Name = "LG";
            var FridgeModelId = 1;

            var fridge = new Fridge();
            {
                fridge.Id = Id;
                fridge.Name = Name;
                fridge.FridgeModelId = FridgeModelId;
            }

            //Act

            dataContext.Fridges.Add(fridge);
            dataContext.SaveChanges();
            dataContext.Fridges.Remove(fridge);
            dataContext.SaveChanges();
            //Assert
            Assert.Null(
                await dataContext.Fridges.SingleOrDefaultAsync(frd =>
                frd.Id == Id));
        }
    }
}
