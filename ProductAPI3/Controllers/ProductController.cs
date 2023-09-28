using Microsoft.AspNetCore.Mvc;
using ProductAPI3.Models;
using ProductAPI3.Repository;

namespace ProductAPI3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : Controller
    {
        private readonly IProductRepo repo;
        
        public ProductController(IProductRepo repo) {  
            this.repo = repo; 
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult> GetAll() {
            var _list = await this.repo.GetAll();
            if( _list != null )
            {
                return Ok(_list);
            }
            else
            {
                return NotFound();  
            }
        }


        [HttpGet("GetById/{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            var _list = await this.repo.GetById(id);
            if (_list != null)
            {
                return Ok(_list);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost("Create")]
        public async Task<ActionResult> Create(ProductModel product)
        {
            var result = await this.repo.Create(product);
            return Ok(result);
        }

        [HttpPut("Update/{id}")]
        public async Task<ActionResult> Update(int id, ProductModel product)
        {
            var result = await this.repo.Update(product, id);
            return Ok(result);
        }

        [HttpDelete("Remove/{id}")]
        public async Task<ActionResult> Remove(int id)
        {
            var _result = await this.repo.Remove(id);
            return Ok(_result);
        }
    }
}
