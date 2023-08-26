using DotNet.DataAccess.Repository;
using DotNet.DataAccess.Repository.IRepository;
using DotNet.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DotNetCourse.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            IEnumerable<Product> ProductList = _unitOfWork.product.GetAll(IncludeProperties:"Category");
            return View(ProductList);
        }
        public IActionResult Details(int id)
        {
            Product Product = _unitOfWork.product.Get(u=>u.Id==id,IncludeProperties:"Category");
            return View(Product);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}