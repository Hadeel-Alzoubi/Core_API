using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Topic_8_25.DTOs;
using Topic_8_25.Models;

namespace Topic_8_25.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        public MyDbContext db;

        public CategoriesController(MyDbContext _db)
        {
            db = _db;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var category = db.Categories.ToList();
            return Ok(category);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var category = db.Categories.Where(c => c.CategoryId == id);
            return Ok(category);
        }

        [HttpPost]
        public IActionResult AddCategory([FromForm] categoryRequestDTO category)
        {
            var uploadImageFolder = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");
            if (!Directory.Exists(uploadImageFolder))
            {
                Directory.CreateDirectory(uploadImageFolder);
            }
            var imageFile = Path.Combine(uploadImageFolder, category.CategoryImage.FileName);
            using (var stream = new FileStream(imageFile, FileMode.Create))
            {
                category.CategoryImage.CopyToAsync(stream);
            }
            var data = new Category
            {
                CategoryName = category.CategoryName,
                CategoryImage = category.CategoryImage.FileName,
            };

            db.Categories.Add(data);
            db.SaveChanges();
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateCategory(int id, [FromForm] categoryRequestDTO category)
        {
            var c = db.Categories.FirstOrDefault(c => c.CategoryId == id);

            var uploadImageFolder = Path.Combine(Directory.GetCurrentDirectory(),"Uploads");
            if (!Directory.Exists(uploadImageFolder))
            {
                Directory.CreateDirectory(uploadImageFolder);
            }
            var imageFile = Path.Combine(uploadImageFolder, category.CategoryImage.FileName);
            using(var stream = new FileStream(imageFile,FileMode.Create))
            {
                category.CategoryImage.CopyToAsync(stream);
            }
            
            c.CategoryName = category.CategoryName ?? c.CategoryName;
            c.CategoryImage = category.CategoryImage.FileName ?? c.CategoryImage;
            
            db.SaveChanges();
            return Ok();
        }

        [HttpDelete("id")]
        public IActionResult Delete(int id)
        {
            if (id != null)
            {
                var category = db.Categories.FirstOrDefault(c => c.CategoryId == id);

                db.Categories.Remove(category);
                db.SaveChanges();
            }
            return Ok();

        }

    }
}
