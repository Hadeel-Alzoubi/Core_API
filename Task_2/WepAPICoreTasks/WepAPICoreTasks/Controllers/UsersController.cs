using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WepAPICoreTasks.Models;

namespace WepAPICoreTasks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        MyDbContext db;

        public UsersController(MyDbContext _db)
        {
            db = _db;
        }

        [Route("GetAllId")]
        [HttpGet]
        public IActionResult getAll()
        {
            var user = db.Users.ToList();
            return Ok(user);
        }

        [Route("GetById")]
        [HttpGet("GetDataById{id:int}")]
        public IActionResult get(int id)
        {

      
            if (id < 0)
            {
                return BadRequest();
            }

            var user = db.Users.Where(c => c.UserId == id);
            return Ok(user);

           
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
                var user = db.Users.Where(c => c.Username == name);
                return Ok(user);
            }

          
        }


        [Route("DeleteAllId")]
        [HttpDelete("{id}")]
        public IActionResult delete(int id)
        {
            //var category = db.Categories.Include(p => p.Products).Where(c => c.CategoryId == id).ExecuteDelete();

            if (id < 0)
            {
                return BadRequest();
            }

            var remove = db.Users.FirstOrDefault(c => c.UserId == id);
            if (remove == null)
            {
                return NotFound();
            }

            db.Users.Remove(remove);
            db.SaveChanges();
            return Ok();    

        }
    }
}
