using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        [HttpGet]
        public IActionResult Get()
        {
            var products = db.Products.Include(p => p.Category).ToList();
            return Ok(products);
        }

        [HttpGet("GetById")]
        public IActionResult Get(int id)
        {
            var products = db.Products.Include(p => p.Category).FirstOrDefault(c => c.ProductId == id);
            return Ok(products);
        }
    }
}
