using ETrade.Core.Models;
using ETrade.DAL.Abstract;
using ETrade.Entity.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ETrade.Core.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductDAL productDAL;
        private readonly ICategoryDAL categoryDAL;

        public HomeController(ILogger<HomeController> logger, IProductDAL productDAL, ICategoryDAL categoryDAL)
        {
            _logger = logger;
            this.categoryDAL = categoryDAL;
            this.productDAL= productDAL;
        }

        public IActionResult Index()
        {
            return View(productDAL.GetAll(p=>p.IsHome));
        }
        public IActionResult List(int? id) //id = category id
        {
            ViewBag.Id = id;
            var products = productDAL.GetAll();
            if (id!=null)
                products = products.Where(p=>p.CategoryId == id).ToList();
            var model = new ListViewModel()
            {
                Categories = categoryDAL.GetAll(),
                Products = products
            };
            return View(model);
        }
        public IActionResult Details(int id)
        {
            return View(productDAL.Get(id));
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