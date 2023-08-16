using DotNet_razor.Data;
using DotNet_razor.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DotNet_razor.Pages.Categories
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        [BindProperty]
        public Category Category { get; set; }
        public CreateModel(ApplicationDbContext db)
        {
            this._db = db;
        }
        public void OnGet()
        {
            
        }
        public IActionResult OnPost() {

            if (ModelState.IsValid)
            {
                _db.Categories.Add(Category);
                _db.SaveChanges();
                TempData["success"] = "category created successfully";
                return RedirectToPage("Index", "Category");
            }
            return Page();
        }
    }
}
