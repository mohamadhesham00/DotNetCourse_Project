using DotNet.DataAccess.Data;
using DotNet.DataAccess.Repository.IRepository;
using DotNet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNet.DataAccess.Repository
{
    public class ProductRepository : Repository<Product> , IProductRepository
    {
        private readonly ApplicationDbContext _db;
        
        public ProductRepository (ApplicationDbContext db) : base (db)
        {
            _db = db;
        }
        public void update(Product product)
        {
           var objfromdb = _db.Products.FirstOrDefault(u => u.Id == product.Id);
            if (objfromdb != null) { 
                objfromdb.Title = product.Title;
                objfromdb.ISBN = product.ISBN;
                objfromdb.Description = product.Description;
                objfromdb.Price = product.Price;
                objfromdb.Price50 = product.Price50;
                objfromdb.Price100 = product.Price100;
                objfromdb.Author = product.Author;
                objfromdb.CategoryId = product.CategoryId;
                objfromdb.ListPrice = product.ListPrice;
                if(product.ImageUrl != null)
                {
                    objfromdb.ImageUrl = product.ImageUrl;
                }
                
            }

        }
    }
}
