using ETrade.Core.Models.Helper;
using ETrade.DAL.Abstract;
using ETrade.DAL.Concrete;
using ETrade.DAL.Context;
using ETrade.Entity.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ETrade.Core.Controllers
{
  //  [Authorize]
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
        //[Authorize(Roles ="Admin,Create")]
        public IActionResult Create()
            {
            ViewData["CategoryId"]=new SelectList(categoryDAL.GetAll(), "Id", "Name");
            return View();
        }

        [HttpPost]
        public IActionResult Create(Product product, [Bind("Image")]IFormFile image)
        {
            if (!ModelState.IsValid)
            {
                if (image!=null)
                {
                    product.Image = FileHelper.Add(image);
                }
                productDAL.Add(product);
                //return View("Index",productDAL.GetAll());
                return RedirectToAction("Index");
            }
            return View();
        }
       // [Authorize(Roles = "Admin,Edit")]
        public IActionResult Edit(int id)
        {
            ViewData["CategoryId"] = new SelectList(categoryDAL.GetAll(), "Id", "Name");
            var product = productDAL.Get(id);
            
            return View(product);
        }
        [HttpPost]
        public IActionResult Edit(Product product, [Bind("Image")] IFormFile image)
        {
            if (!ModelState.IsValid)
            {
                if (image!=null)
                {
                    product.Image = FileHelper.Update(product.Image, image);
                }
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
        //[Authorize(Roles = "Admin,Delete")]
        public IActionResult Delete(int id)
        {
            var product = productDAL.Get(id);
            product.Category = categoryDAL.Get(product.CategoryId);
            return View(product);
        }

        [HttpPost]
        public IActionResult Delete(Product product)
        {
            FileHelper.Delete(product.Image);
            productDAL.Delete(product);
            return RedirectToAction("Index");
        }
    }
}
