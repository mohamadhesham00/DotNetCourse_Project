using DotNet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNet.DataAccess.Repository.IRepository
{
    public interface IProductRepository : IRepository<Product>
    {
        public void update(Product product);
    }
}
