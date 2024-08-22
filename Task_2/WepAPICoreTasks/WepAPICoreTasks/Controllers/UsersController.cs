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
        [HttpGet("{id:int}")]
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
                var user = db.Users.Where(c => c.UserId == id);
                return Ok(user);
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
                var user = db.Users.Where(c => c.Username == name);
                return Ok(user);
            }

          
        }


        [Route("DeleteAllId")]
        [HttpDelete("{id}")]
        public IActionResult delete(int id)
        {
            //var category = db.Categories.Include(p => p.Products).Where(c => c.CategoryId == id).ExecuteDelete();

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
                var user = db.Users.Remove(db.Users.FirstOrDefault(c => c.UserId == id));

                return Ok(user);    

            }

          
        }
    }
}
