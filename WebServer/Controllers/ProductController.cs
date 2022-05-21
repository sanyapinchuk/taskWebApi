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
        [Route("getAll")]
        
        public async Task<ActionResult<List<Product>>> GetAllProducts()
        {
            return Ok(await repository.Product.GetAllAsync());
        }
        
        /*[HttpPut]
        [Route("/kal/{id}")]
        public void Put2(int id)
        {

        }*/
        
        [HttpPut]
        [Route("changeProduct/{id}")]                
        public async Task<HttpResponseMessage> Put(int id, string? name, int? default_quantity)
        {
            var prdct = await repository.Product.GetProductByIdAsync(id);
            if(prdct!=null)
            {
                var product = new Product();
                product.Id = id;
                if(name!=null)
                    product.Name = name;
                product.Name = prdct.Name;
                if (default_quantity != null)
                    product.Default_quantity = default_quantity;
                else
                    product.Default_quantity = prdct.Default_quantity;
                repository.Product.UpdateAsync(product);

                repository.Save();
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            return new HttpResponseMessage(HttpStatusCode.NotFound);

        }
        
    }
}
