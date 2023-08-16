
using DotNet.DataAccess.Data;
using DotNet.DataAccess.Repository.IRepository;
using DotNet.Models;
using Microsoft.AspNetCore.Mvc;

namespace DotNetCourse.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _UnitOfWork;
        public CategoryController(IUnitOfWork UnitOfWork)
        {
            _UnitOfWork = UnitOfWork;
        }
        public IActionResult Index()
        {
            List<Category> ObjCategoryList = _UnitOfWork.category.GetAll().ToList();
            return View(ObjCategoryList);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category obj)
        {
            if (ModelState.IsValid)
            {
                _UnitOfWork.category.Add(obj);
                _UnitOfWork.Save();
                TempData["success"] = "category created successfully";
                return RedirectToAction("Index", "Category");
            }
            return View();
        }
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category categoryfromdb = _UnitOfWork.category.Get(u => u.Id == id);
            if (categoryfromdb == null)
            {
                return NotFound();
            }
            return View(categoryfromdb);
        }

        [HttpPost]
        public IActionResult Edit(Category obj)
        {
            if (ModelState.IsValid)
            {
                _UnitOfWork.category.Update(obj);
                _UnitOfWork.Save();
                TempData["success"] = "category edited successfully";
                return RedirectToAction("Index", "Category");
            }
            return View();
        }
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category categoryfromdb = _UnitOfWork.category.Get(u => u.Id == id);
            if (categoryfromdb == null)
            {
                return NotFound();
            }
            return View(categoryfromdb);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteCategory(int? id)
        {
            Category categoryfromdb = _UnitOfWork.category.Get(u => u.Id == id);
            if (categoryfromdb == null)
            {
                return NotFound();
            }
            _UnitOfWork.category.Remove(categoryfromdb);
            _UnitOfWork.Save();
            TempData["success"] = "category deleted successfully";
            return RedirectToAction("Index", "Category");
        }
    }
}
