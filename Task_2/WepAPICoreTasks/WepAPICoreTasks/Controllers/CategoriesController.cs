using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WepAPICoreTasks.Models;

namespace WepAPICoreTasks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        MyDbContext db;

        public CategoriesController(MyDbContext _db)
        {
            db = _db;
        }

        [Route("GetAll")]
        [HttpGet]
        public IActionResult getAll()
        {
            var categories = db.Categories.ToList();
            return Ok(categories);
        }

        [Route("GetById")]
        [HttpGet("GetDataById{id:int:min(5)}")]
        public IActionResult get(int id)
        {
         
            if (id < 0)
            {
                return BadRequest();
            }

            var category = db.Categories.Where(c => c.CategoryId == id);
            return Ok(category);

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
                var category = db.Categories.Where(c => c.CategoryName == name);
                return Ok(category);

            }

        }

        [Route("DeleteById")]
        [HttpDelete("{id}")]
        public IActionResult delete(int id)
        {
            //var category = db.Categories.Include(p => p.Products).Where(c => c.CategoryId == id).ExecuteDelete();


            if (id < 0)
            {
                return BadRequest();
            }

            var remove = db.Categories.FirstOrDefault(c => c.CategoryId == id);
            if (remove == null)
            {
                return NotFound(); 
            }
            db.Categories.Remove(remove);
            db.SaveChanges();
            return Ok();

           
        }
    }
}
