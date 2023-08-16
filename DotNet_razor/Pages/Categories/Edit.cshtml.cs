using DotNet_razor.Data;
using DotNet_razor.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace DotNet_razor.Pages.Categories
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        [BindProperty]
        public Category Category { get; set; }
        public EditModel(ApplicationDbContext db)
        {
            this._db = db;
        }
        public IActionResult OnGet(int? id)
        {
            if(id == null)
            {
                return NotFound();  
            }
            Category = _db.Categories.Find(id);
            if(Category == null)
            {
                return NotFound();
            }
            return Page();
        }
        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                _db.Categories.Update(Category);
                _db.SaveChanges();
                return RedirectToPage("Index", "Category");
            }
            return Page();
        }
    }
}
