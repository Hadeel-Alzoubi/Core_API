using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WepAPICoreTasks.Models;

namespace WepAPICoreTasks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        MyDbContext db;
        public OrdersController(MyDbContext _db)
        {
            db = _db;
        }

        [Route("GetAll")]
        [HttpGet]
        public IActionResult getAll()
        {
            var order = db.Orders.ToList();
            return Ok(order);
        }

        [Route("GetById")]
        [HttpGet("GetDataById{id:int}")]
        public IActionResult get(int id)
        {

            if (id < 0)
            {
                return BadRequest();
            }
           
                var order = db.Orders.Where(c => c.OrderId == id);
                return Ok(order);

         
        }

        [Route("GetByName")]
        [HttpGet("getname{name}")]
        public IActionResult getname(int name)
        {
            if (name == null)
            {
                return BadRequest();
            }
            else
            {
                var order = db.Orders.Where(c => c.OrderId == name);
                return Ok(order);
            }

        
        }

        [Route("DeleteById")]
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
                var remove = db.Orders.FirstOrDefault(c => c.OrderId == id);
                if (remove == null)
                {
                    return NotFound();
                }
                db.Orders.Remove(remove);
                db.SaveChanges();
                return Ok();
            }

         
        }
    }
}
