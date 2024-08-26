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
        [HttpGet("GetDataById{id:int:max(10)}")]
        public IActionResult get(int id)
        {
        
            if (id < 0)
            {
                return BadRequest();
            }

            var product = db.Products.Where(c => c.ProductId == id);
            return Ok(product);

          
        }

        [Route("GetByName")]
        [HttpGet("getname{name}")]
        public IActionResult getname(string name)
        {
            if (name == null)
            {
                return BadRequest();
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

            if (id < 0)
            {
                return BadRequest();
            }

            var remove = db.Products.FirstOrDefault(c => c.ProductId == id);
            if (remove == null)
            {
                return NotFound();
            }
            db.Products.Remove(remove);
            db.SaveChanges();
            return Ok();
          
        }
    }
}
