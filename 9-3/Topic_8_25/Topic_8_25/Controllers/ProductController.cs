using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Topic_8_25.DTOs;
using WepAPICore.Models;

namespace Topic_8_25.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        public MyDbContext db;

        public ProductController(MyDbContext _db)
        {
            db = _db;
        }


        [HttpGet]
        [Authorize]

        public IActionResult Get()
        {
            var product = db.Products.ToList();
            return Ok(product);
        }

        [HttpGet("getProduct/{product}")]
        public IActionResult GetBy(int product)
        {
            var products = db.Products.Where(p => p.ProductId == product);
            return Ok(products);
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

        //Sort Data
        [HttpGet("OrderBy/")]
        public IActionResult price()
        {
            var product = db.Products.OrderBy(p => p.Price).Select(p => new
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
        public IActionResult AddProduct([FromForm] productRequestDTO product)
        {


            var uploadImageFolder = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");
            if (!Directory.Exists(uploadImageFolder))
            {
                Directory.CreateDirectory(uploadImageFolder);
            }
            var imageFile = Path.Combine(uploadImageFolder, product.ProductImage.FileName);
            using (var stream = new FileStream(imageFile, FileMode.Create))
            {
                product.ProductImage.CopyToAsync(stream);
            }

            var data = new Product
            {
                ProductName = product.ProductName,
                ProductImage = product.ProductImage.FileName,
                Price = product.Price,
                CategoryId = product.CategoryId,
                Description = product.Description,
            };

            db.Products.Add(data);
            db.SaveChanges();
            return Ok();
        }


        [HttpPut("{id}")]
        public IActionResult updateProduct(int id, [FromForm] productRequestDTO product)
        {
            var pro = db.Products.FirstOrDefault(p => p.ProductId == id);

            var uploadImageFolder = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");
            if (!Directory.Exists(uploadImageFolder))
            {
                Directory.CreateDirectory(uploadImageFolder);
            }
            var imageFile = Path.Combine(uploadImageFolder, product.ProductImage.FileName);
            using (var stream = new FileStream(imageFile, FileMode.Create))
            {
                product.ProductImage.CopyToAsync(stream);
            }

            pro.ProductName = product.ProductName;
            pro.Price = product.Price;
            pro.ProductImage = product.ProductImage.FileName;

            db.SaveChanges();
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult delete(int id)
        {

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


        [HttpGet("SortByName")]
        public IActionResult get()
        {
            var pro = db.Products.OrderByDescending(n => n.ProductName).ToList().TakeLast(5); 
            return Ok(pro);
        }

    }
}
