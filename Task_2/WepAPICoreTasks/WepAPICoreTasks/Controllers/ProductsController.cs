using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WepAPICoreTasks.Models;

namespace WepAPICoreTasks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        MyDbContext db;
        public ProductsController(MyDbContext _db)
        {
            db = _db;
        }

        [Route("GetAll")]
        [HttpGet]
        public IActionResult getAll()
        {
            var product = db.Products.ToList();
            return Ok(product);
        }

        [Route("GetById")]
        [HttpGet("{id:int:max(10)}")]
        public IActionResult get(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            if (id < 0)
            {
                return BadRequest();
            }
            else
            {
                var product = db.Products.Where(c => c.ProductId == id);
                return Ok(product);

            }

          
        }

        [Route("GetByName")]
        [HttpGet("{name}")]
        public IActionResult get(string name)
        {
            if (name == null)
            {
                return NotFound();
            }
         
            else
            {
                var product = db.Products.Where(c => c.ProductName == name);
                return Ok(product);
            }

         
        }

        [Route("DeleteById")]
        [HttpDelete("{id}")]
        public IActionResult delete(int id)
        {
            //var product = db.Products.Include(p => p.Categories).Where(c => c.ProductId == id).ExecuteDelete();

            if (id == null)
            {
                return NotFound();
            }
            if (id < 0)
            {
                return BadRequest();
            }
            else
            {
                var product = db.Products.Remove(db.Products.FirstOrDefault(c => c.ProductId == id));

                return Ok(product);

            }

          
        }
    }
}
