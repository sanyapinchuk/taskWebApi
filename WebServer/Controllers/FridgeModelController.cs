using Microsoft.AspNetCore.Mvc;
using WebServer.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FridgeModelController : ControllerBase
    {
        private readonly DataContext dataContext;

        public FridgeModelController(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }


        // GET: api/<FridgeModelController>
        [HttpGet]
        public async Task<ActionResult<List<FridgeModel>>> Get()
        {
           return Ok(await dataContext.FridgeModels.ToListAsync());
        }

        // GET api/<FridgeModelController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<FridgeModelController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<FridgeModelController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<FridgeModelController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
