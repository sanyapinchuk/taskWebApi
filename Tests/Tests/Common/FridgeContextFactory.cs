using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebServer.Data;
using WebServer.Data.Interfaces;
using WebServer.Models;

namespace Tests.Common
{
    public class FridgeContextFactory
    {
        public static DataContext Create()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            var context = new DataContext(options);
            context.Database.EnsureCreated();


            context.FridgeModels.AddRange(
                new FridgeModel
                {
                    Id = 1,
                    Name = "SM-213"
                },
                new FridgeModel
                {
                    Id = 2,
                    Name = "TB -112"
                }
                );

            context.Fridges.AddRange(
                new Fridge
                {
                    Id = 1,
                    Name = "Bosh",
                    Owner_name ="Максим",
                    FridgeModelId = 1,
                },
                new Fridge
                {
                    Id = 2,
                    Name = "Atlant",
                    Owner_name = "Олег",
                    FridgeModelId = 1,
                },
                new Fridge
                {
                    Id = 3,
                    Name = "Sony",
                    Owner_name = "Ольга",
                    FridgeModelId = 2,
                }
            );

            context.Products.AddRange(

                new Product
                {
                    Id = 1,
                    Name = "Творог",
                    Default_quantity = 1
                },
                new Product
                {
                    Id = 2,
                    Name = "Мясо",
                    Default_quantity = 2
                },
                new Product
                {
                    Id = 3,
                    Name = "Молоко",
                    Default_quantity = 1
                },
                new Product
                {
                    Id = 4,
                    Name = "Сырок",
                    Default_quantity = 4
                }
                );

            context.Fridges_Products.AddRange(

                new Fridge_Product
                {
                    Id = 1,
                    FridgeId = 1,
                    ProductId = 1,
                    Quantity = 4
                },
                new Fridge_Product
                {
                    Id = 2,
                    FridgeId = 1,
                    ProductId = 2,
                    Quantity = 2
                },
                new Fridge_Product
                {
                    Id = 3,
                    FridgeId = 2,
                    ProductId = 1,
                    Quantity = 2
                },
                new Fridge_Product
                {
                    Id = 4,
                    FridgeId = 2,
                    ProductId = 3,
                    Quantity = 1
                },
                new Fridge_Product
                {
                    Id = 5,
                    FridgeId = 3,
                    ProductId = 1,
                    Quantity = 1
                },
                new Fridge_Product
                {
                    Id = 6,
                    FridgeId = 3,
                    ProductId = 4,
                    Quantity = 5
                });


            context.SaveChanges();
            return context;
        }

        public static void Destroy(DataContext datacontext)
        {
            datacontext.Database.EnsureDeleted();
            datacontext.Dispose();
        }
    }
}

