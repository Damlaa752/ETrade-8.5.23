using ETrade.DAL.Abstract;
using ETrade.DAL.Context;
using ETrade.Entity.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ETrade.Core.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryDAL categoryDAL;

        public CategoryController(ICategoryDAL categoryDAL)
        {
            this.categoryDAL = categoryDAL;
        }
        //Get
        public IActionResult Index()=> View(categoryDAL.GetAll());
        public IActionResult Create() => View();
        [HttpPost]
        public IActionResult Create(Category category) 
        {
            if (ModelState.IsValid)
            {
                categoryDAL.Add(category);
                return RedirectToAction("Index");
            }
          return View(category);
        }
        public ActionResult Edit(int id) => View(categoryDAL.Get(id));
        [HttpPost]
        public IActionResult Edit(Category category)
        {
            var model = categoryDAL.Get(category.Id);
            model.Name = category.Name;
            model.Description=category.Description;
            if (ModelState.IsValid)
            {
                categoryDAL.Update(category);
                return RedirectToAction("Index");
            }
            return View(category);
        }
        public IActionResult Details(int id)=> View(categoryDAL.Get(id));
        public IActionResult Delete(int id)=> View(categoryDAL.Get(id));
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(Category category)
        {
            categoryDAL.Delete(category);
            return RedirectToAction ("Index");
                
        }
    }
}
