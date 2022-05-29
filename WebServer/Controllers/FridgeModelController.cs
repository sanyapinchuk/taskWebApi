using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebServer.Data.Interfaces;
using WebServer.Models;

namespace WebServer.Controllers
{
    [Route("api/FridgeModel")]
    [ApiController]
    public class FridgeModelController : ControllerBase
    {
        private readonly IRepositoryManager repository;
        public FridgeModelController(IRepositoryManager repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        [Route("getAll")]
        public async Task<ActionResult<List<FridgeModel>>> GetAllModels()
        {
            return Ok(await repository.FridgeModel.GetAllAsync());
        }
    }
}
