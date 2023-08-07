using DotNetCourse.Data;
using DotNetCourse.Models;
using Microsoft.AspNetCore.Mvc;

namespace DotNetCourse.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;
        public CategoryController(ApplicationDbContext db)
        {
            this._db = db;
        }
        public IActionResult Index()
        {
            List<Category> ObjCategoryList = _db.Categories.ToList();
            return View(ObjCategoryList);
        }
    }
}
