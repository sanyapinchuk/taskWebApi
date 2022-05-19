using Microsoft.AspNetCore.Mvc;
using System.Net;
using WebServer.Data.Interfaces;
using WebServer.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IRepositoryManager repository;

        public ProductController(IRepositoryManager repository )
        {
            this.repository = repository;
        }


        // GET: api/<ProductController>
        [HttpGet]
        public async Task<ActionResult<List<Product>>> Get()
        {

            return Ok(await repository.Product.GetAllAsync());
        }

        // GET api/<ProductController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ProductController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ProductController>/5
        [HttpPut("{id},{name},{default_quantity}")]
        public HttpResponseMessage Put(int id, string name, int default_quantity)
        {
            var product = new Product();
            product.Id = id;
            product.Name = name;
            product.Default_quantity = default_quantity;
            repository.Product.UpdateAsync(product);

            repository.Save();
            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        // DELETE api/<ProductController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
