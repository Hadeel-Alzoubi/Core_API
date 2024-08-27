using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Topic_8_25.DTOs;
using Topic_8_25.Models;

namespace Topic_8_25.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        public MyDbContext db;

        public UsersController(MyDbContext _db)
        {
            db = _db;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var User = db.Users.ToList();
            return Ok(User);
        }

        [HttpGet("getUser/{User}")]
        public IActionResult GetBy(int User)
        {
            var users = db.Users.Where(p => p.UserId == User);
            return Ok(users);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            //var product = db.Products.Where(p => p.ProductName == name).ToList();
            //return Ok(product);
            var product = db.Products.Where(p => p.Category.CategoryId == id).Select(p => new
            {
                p.ProductId,
                p.ProductName,
                p.ProductImage,
                p.Price,
                Category = new
                {
                    p.Category.CategoryId,
                    p.Category.CategoryImage,
                    p.Category.CategoryName
                }
            }).ToList();
            return Ok(product);
        }


        [HttpPost]
        public IActionResult AddProduct([FromForm] UserRequestDTO user)
        {

            var data = new User
            {
                Username = user.Username,
                Password = user.Password,
                Email = user.Email,
            };

            db.Users.Add(data);
            db.SaveChanges();
            return Ok();
        }


        [HttpPut("{id}")]
        public IActionResult updateProduct(int id, [FromForm] UserRequestDTO user)
        {
            var use = db.Users.FirstOrDefault(p => p.UserId == id);

            use.Username = user.Username;
            use.Password = user.Password;
            use.Email = user.Email;

            db.SaveChanges();
            return Ok();
        }

    }
}
