using ETrade.DAL.Abstract;
using ETrade.DAL.Concrete;
using ETrade.DAL.Context;
using ETrade.Entity.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ETrade.Core.Controllers
{
    public class ProductController : Controller
    {
        private readonly ETradeDbContext db;
        private readonly IProductDAL productDAL;
        private readonly ICategoryDAL categoryDAL;

        public ProductController(ETradeDbContext db, IProductDAL productDAL, ICategoryDAL categoryDAL)
        {
            this.db= db;
            this.productDAL = productDAL;
            this.categoryDAL = categoryDAL;
            
        }

        public IActionResult Index()
        {
            var products = db.Products.Include(p=>p.Category);
            return View(products);
        }
        public IActionResult Create()
            {
            ViewData["CategoryId"]=new SelectList(categoryDAL.GetAll(), "Id", "Name");
            return View();
        }

        [HttpPost]
        public IActionResult Create(Product product)
        {
            if (!ModelState.IsValid)
            {
                
                productDAL.Add(product);
                //return View("Index",productDAL.GetAll());
                return RedirectToAction("Index");
            }
            return View();
        }
        public IActionResult Edit(int id)
        {
            ViewData["CategoryId"] = new SelectList(categoryDAL.GetAll(), "Id", "Name");
            var product = productDAL.Get(id);
            return View(product);
        }
        [HttpPost]
        public IActionResult Edit(Product product)
        {
            if (!ModelState.IsValid)
            {

                productDAL.Update(product);
                //return View("Index",productDAL.GetAll());
                return RedirectToAction("Index");
            }
            return View();
        }
        public IActionResult Details(int id)
        {
            var product = productDAL.Get(id);
            product.Category = categoryDAL.Get(product.CategoryId);
            return View(product);   
        }
        public IActionResult Delete(int id)
        {
            var product = productDAL.Get(id);
            product.Category = categoryDAL.Get(product.CategoryId);
            return View(product);
        }

        [HttpPost]
        public IActionResult Delete(Product product)
        {
            productDAL.Delete(product);
            return RedirectToAction("Index");
        }
    }
}
