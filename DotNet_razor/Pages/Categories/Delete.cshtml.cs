using DotNet_razor.Data;
using DotNet_razor.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DotNet_razor.Pages.Categories
{
    public class DeleteModel : PageModel
    {
        public readonly ApplicationDbContext _db;
        [BindProperty]
        public Category Category { get; set; }
        public DeleteModel(ApplicationDbContext db)
        {
            _db = db;   
        }
        public IActionResult OnGet(int? id)
        {
            if(id == null||id == 0)
            {
                return NotFound();
            }
            Category = _db.Categories.Find(id);
            if(Category == null) {
                return NotFound();   
            }
            return Page();
        }
        public IActionResult OnPost()
        {
            if(Category == null)
            {
                return NotFound(ModelState);
            }
            _db.Categories.Remove(Category);
            _db.SaveChanges();
            return RedirectToPage("Index");
        }
    }
}
