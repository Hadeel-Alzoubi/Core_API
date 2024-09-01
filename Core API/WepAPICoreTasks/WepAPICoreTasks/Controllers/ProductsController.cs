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

        //[HttpGet("GetById")]
        //public IActionResult Get(int id)
        //{

        //    var products = db.Products.Include(p => p.Category).FirstOrDefault(c => c.ProductId == id);
        //    return Ok(products);
        //}

        [HttpGet("GetById , p {id}")]
        public IActionResult Get(int id, int p = 100)
        {
            var products = db.Products.Include(p => p.Category).Where(c => c.CategoryId == id && c.Price > p);
            int count = products.Count();
            return Ok(count);
        }

        [HttpGet("api/Products {name:alpha}")]
        public IActionResult Get(string name)
        {
            var products = db.Products.Include(p => p.Category).Where(c => c.ProductName == name);
            return Ok(products);
        }

        [HttpDelete("{removeID}")]
        public IActionResult Gett(int removeID)
        {
            var products = db.Products.Include(p => p.Category).Where(c => c.ProductId == removeID).ExecuteDelete();

            //var products = db.Products.Remove(db.Products.FirstOrDefault(c => c.ProductId == removeID));
            return Ok(products);
        }


    }
}
