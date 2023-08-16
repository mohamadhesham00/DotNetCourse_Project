using DotNet_razor.Data;
using DotNet_razor.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DotNet_razor.Pages.Categories
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public List<Category> ObjCategoryList ;
        public IndexModel(ApplicationDbContext db)
        {
            this._db = db;
        }
        public void OnGet()
        {
            ObjCategoryList = _db.Categories.ToList();
            
        }
    }
}
