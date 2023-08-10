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
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category obj)
        {
            if(ModelState.IsValid)
            {
                _db.Categories.Add(obj);
                _db.SaveChanges();
                TempData["success"] = "category created successfully";
                return RedirectToAction("Index", "Category");
            }
            return View();
        }
        public IActionResult Edit(int? id)
        {
            if(id == null||id == 0)
            {
                return NotFound();
            }
            Category categoryfromdb = _db.Categories.Find(id);
            if(categoryfromdb == null) {
                return NotFound();
            }
            return View(categoryfromdb);
        }

        [HttpPost]
        public IActionResult Edit(Category obj)
        {
            if(ModelState.IsValid)
            {
                _db.Categories.Update(obj);
                _db.SaveChanges();
                TempData["success"] = "category edited successfully";
                return RedirectToAction("Index", "Category");
            }
            return View();
        }
        public IActionResult Delete (int? id)
        {
            if(id == null||id == 0)
            {
                return NotFound();
            }
            Category categoryfromdb = _db.Categories.Find(id);
            if(categoryfromdb == null) {
                return NotFound();
            }
            return View(categoryfromdb);
        }

        [HttpPost,ActionName("Delete")]
        public IActionResult DeleteCategory(int? id)
        {
            Category categoryfromdb= _db.Categories.Find(id);
            if(categoryfromdb == null)
            {
                return NotFound();
            }
            _db.Categories.Remove(categoryfromdb);
            _db.SaveChanges();
            TempData["success"] = "category deleted successfully";
            return RedirectToAction("Index", "Category");
        }
    }
}
