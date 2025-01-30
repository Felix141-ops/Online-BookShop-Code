using Microsoft.AspNetCore.Mvc;
using Online_BookShop.DataAccess.Repository;
using Online_BookShop.DataAccess.Repository.IRepository;
using Online_BookShop.Models;
using Online_BookShop.Models.Models;
using System.Diagnostics;

namespace Online_BookShop.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        //unitofwork using dependency injection to retrieve products from the data base. 
        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)//injecting unitofwork
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            IEnumerable<Product> productList = _unitOfWork.Product.GetAll(includeProperties: "Category");
            return View(productList);
        }

        public IActionResult Details(int productId)
        {
            Product product = _unitOfWork.Product.Get( u=>u.ProductId== productId, includeProperties: "Category");//u=>u.productId == id is called a link operator 
            return View(product);
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
