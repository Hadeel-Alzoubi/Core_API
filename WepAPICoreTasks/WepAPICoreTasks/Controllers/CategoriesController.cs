﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WepAPICoreTasks.Models;

namespace WepAPICoreTasks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        MyDbContext db;
        public CategoriesController(MyDbContext _db)
        {
            db = _db;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var all = db.Categories.ToList();
            return Ok(all);
        }

        [HttpGet("ById")]
        public IActionResult Get(int id)
        {
            var byId = db.Categories.Find(id);
            return Ok(byId);
        }
    }
}
