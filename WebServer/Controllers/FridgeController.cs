using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using System.Runtime.InteropServices;
using System.Text.Json;
using WebServer.Data.Interfaces;
using WebServer.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FridgeController : ControllerBase
    {
        private readonly IRepositoryManager repository;
        /*public FridgeController(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }*/

         public FridgeController(IRepositoryManager repository)
         {
             this.repository = repository;
         }

        // GET: api/<FridgeController>
        [HttpGet]
        public async Task<ActionResult<List<Fridge>>> Get()
        {
            return Ok(await repository.Fridge.GetAllAsync());
        }

        // GET api/<FridgeController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<List<Product>>> Get(int id)
        {           
            var a = await repository.Fridge.GetAllProductInFridge(id);

            //var products = new List<{string Name, int } >();
            var json = JsonConvert.SerializeObject(a, Formatting.Indented);
            return  Ok(json);
        }

        // POST api/<FridgeController>
        [HttpPost("{idFridge},{idProduct}")]
        public async Task<HttpResponseMessage> Post(int idFridge, int idProduct, [Optional] int? count)
        {
            var fridge = await repository.Fridge.GetFridgeByIdAsync(idFridge);

            if(fridge == null)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }    

            var product = await repository.Product.GetProductByIdAsync(idProduct);
            if(product == null)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
            if(count!= null&& count <= 0)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }

            repository.FridgeProduct.AddNewProductAsync(idFridge, idProduct, count);
            repository.Save();
            
            return new HttpResponseMessage(HttpStatusCode.Created);

        }

        // PUT api/<FridgeController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<FridgeController>/5
        [HttpDelete("{idFridge},{idProduct}")]
        public async Task<HttpResponseMessage> Delete(int idFridge, int idProduct, bool deleteAll=false)
        {
            var fridge = await repository.Fridge.GetFridgeByIdAsync(idFridge);

            if (fridge == null)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }

            var product = await repository.Product.GetProductByIdAsync(idProduct);
            if (product == null)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
            var resultDel = repository.FridgeProduct.DeleteProductAsync(idFridge, idProduct, deleteAll).Result;
            if(resultDel)
            {
                return new HttpResponseMessage(HttpStatusCode.OK);
                repository.Save();
            }
            else
            {               
                return new HttpResponseMessage(HttpStatusCode.Gone);
            }
        }
    }
}
