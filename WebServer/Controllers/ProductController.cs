using Microsoft.AspNetCore.Mvc;
using System.Net;
using WebServer.Data.Interfaces;
using WebServer.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebServer.Controllers
{
    [Route("api/Product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IRepositoryManager repository;

        public ProductController(IRepositoryManager repository )
        {
            this.repository = repository;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            return  Ok(await repository.Product.GetProductByIdAsync(id));
        }

        [HttpGet]
        [Route("getAll")]        
        public async Task<ActionResult<List<Product>>> GetAllProducts()
        {
            return Ok(await repository.Product.GetAllAsync());
        }
        
       
        [HttpPut]
        [Route("Edit/{id}")]                
        public async Task<ActionResult> Put(int id, [FromBody] Product product)
        {
            var prdctDb = await repository.Product.GetProductByIdAsync(id);
            if(prdctDb!=null)
            {
                product.Id = prdctDb.Id;
                repository.Product.UpdateAsync(product);
                repository.Save();

                return StatusCode(200);
            }
            return StatusCode(404);

        }
        
    }
}
