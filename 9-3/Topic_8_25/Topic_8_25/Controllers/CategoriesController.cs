using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Topic_8_25.DTOs;

using WepAPICore.Models;

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

        [HttpDelete("delete")]
        public IActionResult delete(int id)
        {
            if (id < 0)
            {
                return BadRequest();
            }

            var remove = db.Categories.FirstOrDefault(c => c.CategoryId == id);
            if (remove == null)
            {
                return NotFound();
            }
            db.Categories.Remove(remove);
            db.SaveChanges();
            return Ok();


        }

    }
}
