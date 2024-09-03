using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Topic_8_25.DTOs;
using WepAPICore;
using WepAPICore.DTOs;
using WepAPICore.Models;

namespace Topic_8_25.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        public MyDbContext db;
        private readonly TokenGenerator _tokenGenerator;
        public UsersController(MyDbContext _db, TokenGenerator tokenGenerator)
        {
            _tokenGenerator = tokenGenerator;
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
            var users = db.Users.FirstOrDefault(p => p.UserId == User);
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

        //[HttpGet("OrderBy/")]
        //public IActionResult price()
        //{
        //    var product = db.Products.OrderBy(p => p.Price).Select(p => new
        //    {
        //        p.ProductId,
        //        p.ProductName,
        //        p.ProductImage,
        //        p.Price,
        //        Category = new
        //        {
        //            p.Category.CategoryId,
        //            p.Category.CategoryImage,
        //            p.Category.CategoryName
        //        }
        //    }).ToList();
        //    return Ok(product);
        //}

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

        [Route("gggggggggg")]
        [HttpPost]
        public IActionResult addUSer([FromForm] UserRequestDTO userDTO)
        {
            byte[] hash;
            byte[] salt;

            PasswordHasher.createPasswordHash(userDTO.Password, out hash, out salt);

            var user = new User
            {
                Username = userDTO.Username,
                Password = userDTO.Password,
                PasswordHash = hash,
                PasswordSalt = salt,
                Email = userDTO.Email

            };
            db.Users.Add(user);
            db.SaveChanges();
            return Ok(user);
        }


        //[HttpGet("Login/{email}/{password}")]
        //public IActionResult Login(string email, string password)
        //{

        //    var checkUser = db.Users.FirstOrDefault(u => u.Email == email);

        //    if (checkUser == null || !PasswordHasher.VerifyPasswordHash(password, checkUser.PasswordHash, checkUser.PasswordSalt))
        //    {
        //        return Unauthorized("Invalid username or password.");
        //    }


        //    return Ok(checkUser);
        //}

        [HttpPost("LogIn")]
        public IActionResult Login([FromForm] UserRequestDTO model)
        {
            var user = db.Users.FirstOrDefault(x => x.Email == model.Email);

            if (user == null || !PasswordHasher.VerifyPasswordHash(model.Password, user.PasswordHash, user.PasswordSalt)) 
            {
                return Unauthorized("Invalid username or password."); // 401 Unauthorized 
            }
            var roles = db.UserRoles.Where(x => x.UserId == user.UserId).Select(ur => ur.Role).ToList();
            var token = _tokenGenerator.GenerateToken(user.Username, roles);

            return Ok(new { Token = token });
        }
    }
}
