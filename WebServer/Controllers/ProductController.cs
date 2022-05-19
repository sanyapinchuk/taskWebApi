using Microsoft.AspNetCore.Mvc;
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
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ProductController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
