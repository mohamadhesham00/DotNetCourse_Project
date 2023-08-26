using DotNet.DataAccess.Repository;
using DotNet.DataAccess.Repository.IRepository;
using DotNet.Models;
using DotNet.Models.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace DotNetCourse.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {

        private readonly IUnitOfWork _UnitOfWork;
        private readonly IWebHostEnvironment _WebHostEnvironment;
        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _UnitOfWork = unitOfWork;
            _WebHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            List<Product> products = _UnitOfWork.product.GetAll(IncludeProperties:"Category").ToList();
            return View(products);
        }

        public IActionResult Upsert(int? id)
        {
            ProductVM productVM = new ProductVM();
            // for showing list of categories that is available in the db for you to choose
            productVM.CategoryList = _UnitOfWork.category.GetAll().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            }) ;
            productVM.Product = new Product();
            if(id == 0 || id == null)
            {
                //create
                return View(productVM);

            }
            else
            {
                //update
                productVM.Product  = _UnitOfWork.product.Get(u=>u.Id == id);
                return View(productVM);
            }
        
        
        }

        [HttpPost]

        public IActionResult Upsert(ProductVM productVM, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _WebHostEnvironment.WebRootPath;
                if(file != null)
                {
                    string FileName = Guid.NewGuid().ToString()+ Path.GetExtension(file.FileName);
                    string ProductPath = Path.Combine(wwwRootPath, @"images\Product");

                    if (!string.IsNullOrEmpty(productVM.Product.ImageUrl))
                    {
                        var OldImagePath = wwwRootPath + productVM.Product.ImageUrl;
                        if(System.IO.File.Exists(OldImagePath))
                        {
                            System.IO.File.Delete(OldImagePath);
                        }
                    }
                    
                    string ImagePath = Path.Combine(ProductPath, FileName);
                    using (var fileStream = new FileStream(ImagePath,FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    productVM.Product.ImageUrl = @"\images\Product\" + FileName;
                }

                if(productVM.Product.Id == 0)
                {
                    _UnitOfWork.product.Add(productVM.Product);
                    TempData["success"] = "Product created successfully";

                }
                else { 
                    _UnitOfWork.product.update(productVM.Product);
                    TempData["success"] = "Product updated successfully";
                }
                _UnitOfWork.Save();
                return RedirectToAction("Index", "Product");
            }
            else
            {
                productVM.CategoryList = _UnitOfWork.category.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                });
                return View(productVM);
            }
        }
        //public IActionResult Edit(int? id)
        //{
        //    if (id == null || id == 0)
        //    {
        //        return NotFound();
        //    }
        //    Product product = _UnitOfWork.product.Get(u => u.Id == id);
        //    if (product == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(product);
        //}

        //[HttpPost]
        //public IActionResult Upsert(Product product)
        //{
        //    _UnitOfWork.product.update(product);
        //    _UnitOfWork.Save();
        //    TempData["success"] = "Product edited successfully";
        //    return RedirectToAction("Index", "Product");

        //}
        //public IActionResult Delete(int? id)
        //{
        //    if (id == null || id == 0)
        //    {
        //        return NotFound();
        //    }
        //    Product product = _UnitOfWork.product.Get(u => u.Id == id);
        //    if (product == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(product);
        //}

        //[HttpPost]
        //[ActionName("Delete")]
        //public IActionResult DeleteProduct(int? id)
        //{
        //    Product product = _UnitOfWork.product.Get(u => u.Id == id);
        //    if (product == null)
        //    {
        //        return NotFound();
        //    }
        //    _UnitOfWork.product.Remove(product);
        //    _UnitOfWork.Save();
        //    TempData["success"] = "Product deleted successfully";
        //    return RedirectToAction("Index", "Product");
        //}


        #region API CALLS

        [HttpGet]
        public IActionResult GetAll() {

            List<Product> products = _UnitOfWork.product.GetAll(IncludeProperties: "Category").ToList();
            return Json(new { data = products });
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var obj = _UnitOfWork.product.Get(u => u.Id == id);
            if (obj == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }
            if (!string.IsNullOrEmpty(obj.ImageUrl))
            {
                var OldImagePath = _WebHostEnvironment.WebRootPath + obj.ImageUrl;
                if (System.IO.File.Exists(OldImagePath))
                {
                    System.IO.File.Delete(OldImagePath);
                }
            }
            _UnitOfWork.product.Remove(obj);
            _UnitOfWork.Save();
            return Json(new { success = true, message = "Deleted successfully" });

        }





        #endregion

    }
}
