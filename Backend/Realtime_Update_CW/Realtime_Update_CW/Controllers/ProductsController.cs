using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Realtime_Update_CW.Repository;

namespace Realtime_Update_CW.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _repository;

        public ProductsController(IProductRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public JsonResult GetProducts()
        {
            return new JsonResult(_repository.GetAllProducts());
        }
    }
}
