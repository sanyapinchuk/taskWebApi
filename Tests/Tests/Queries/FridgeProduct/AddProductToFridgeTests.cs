using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tests.Common;
using WebServer.Data;
using WebServer.Models;
using Xunit;
using Tests.Comparers;
namespace Tests.Queries.FridgeProduct
{
    public class AddProductToFridgeTests : TestCommandBase
    {
        FridgeComparer comparer = new FridgeComparer();
        [Fact]
        public async Task AddProductToFridgeWithoutExistProductWithCount_Success()
        {
            //Act
            int id = 7; //new ID
            int idProduct = 4;
            int idFridge = 1;
            int count = 2;
            var fridge_product = new Fridge_Product
            
            {
                Id = id,
                ProductId = idProduct,
                FridgeId = idFridge,
                Quantity = count
            };
            
            //Action
            await Task.Run(() => repository.FridgeProduct
            .AddNewProductAsync(idFridge, idProduct, count));
            repository.Save();
            var fr_pr = await repository.FridgeProduct.GetFridgeProductAsync(
                    idFridge, idProduct);
            //Asssert

            if (fr_pr == null)
                throw new Xunit.Sdk.NotNullException();
            
            Assert.Equal<Fridge_Product>(fr_pr, fridge_product, comparer);

        }

        [Fact]
        public async Task AddProductToFridgeWithoutExistProductWithoutCount_Success()
        {
            //Act
            int id = 7; //new ID
            int idProduct = 4;
            int idFridge = 1;
            int count = 4; //default quantity
            var fridge_product = new Fridge_Product

            {
                Id = id,
                ProductId = idProduct,
                FridgeId = idFridge,
                Quantity = count
            };

            //Action
            await Task.Run(() => repository.FridgeProduct
            .AddNewProductAsync(idFridge, idProduct, null));
            repository.Save();
            var fr_pr = await repository.FridgeProduct.GetFridgeProductAsync(
                    idFridge, idProduct);
            //Asssert

            if (fr_pr == null)
                throw new Xunit.Sdk.NotNullException();

            Assert.Equal<Fridge_Product>(fr_pr, fridge_product, comparer);

        }

        [Fact]
        public async Task AddProductToFridgeWithExistProductWithCount_Success()
        {
            //Act
            int id = 1;
            int idProduct = 1;
            int idFridge = 1;
            int count = 6;
            int countToAdd = 2;
            var fridge_product = new Fridge_Product
            {
                Id = id,
                ProductId = idProduct,
                FridgeId = idFridge,
                Quantity = count
            };
            //Action
            await Task.Run(() => repository.FridgeProduct
            .AddNewProductAsync(idFridge, idProduct, countToAdd));
            repository.Save();
            var fr_pr = await repository.FridgeProduct.GetFridgeProductAsync(
                    idFridge, idProduct);
            //Asssert

            if (fr_pr == null)
                throw new Xunit.Sdk.NotNullException();

            Assert.Equal<Fridge_Product>(fr_pr, fridge_product, comparer);

        }

        [Fact]
        public async Task AddProductToFridgeWithExistProductWithoutCount_Success()
        {
            //Act
            int id = 5;
            int idProduct = 1;
            int idFridge = 3;
            int count = 2;
            var fridge_product = new Fridge_Product
            {
                Id = id,
                ProductId = idProduct,
                FridgeId = idFridge,
                Quantity = count
            };
            //Action
            await Task.Run(() => repository.FridgeProduct
            .AddNewProductAsync(idFridge, idProduct, null));
            repository.Save();
            var fr_pr = await repository.FridgeProduct.GetFridgeProductAsync(
                    idFridge, idProduct);
            //Asssert

            if (fr_pr == null)
                throw new Xunit.Sdk.NotNullException();

            Assert.Equal<Fridge_Product>(fr_pr, fridge_product, comparer);

        }
    }
}
