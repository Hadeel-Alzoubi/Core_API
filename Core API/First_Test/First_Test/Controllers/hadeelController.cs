using First_Test.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace First_Test.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class hadeelController : ControllerBase
    {
        private readonly MyDbContext _db;

        public hadeelController(MyDbContext db)
        {
            _db = db;
        }

        //[HttpGet]
        //public IActionResult Get(int id ,string n)
        //{
        //    var c = _db.Categories.Where(a => a.CategoryId == id &&  a.Name == n).ToList();

        //    return Ok(c);
        //} 

        //[HttpGet]
        //public IActionResult Get(int n)
        //{
        //    var c = _db.Categories.Where(a => a.CategoryId == n).ToList();

        //    return Ok(c);
        //}

        //[HttpGet]
        //public IActionResult Get(int n)
        //{
        //    var c = _db.Categories.Where(a => a.CategoryId == n).FirstOrDefault();

        //    return Ok(c);
        //}   
        
        //[HttpGet]
        //public IActionResult Get(int n)
        //{
        //    var c = _db.Categories.FirstOrDefault(a => a.CategoryId == n);

        //    return Ok(c);
        //}

    }
}
