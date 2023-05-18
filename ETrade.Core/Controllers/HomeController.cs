﻿using ETrade.Core.Models;
using ETrade.DAL.Abstract;
using ETrade.Entity.Models.Entities;
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
        static string search;
        public IActionResult Search(string query, int id)
        {
            if (query != null)
                search = query;
            var model = new ListViewModel();
            var products = new List<Product>();
            try
            {
                products = productDAL.GetAll(p => p.Name.Contains(search) || p.Description.Contains(search));
                if (query != null)
                {
                    if (id != 0)
                        products = products.Where(p => p.CategoryId == id).ToList();

                }
                else
                {
                    if (id != 0)
                        products = products.Where(p => p.CategoryId == id).ToList();
                }
                model = new ListViewModel()
                {
                    Categories = categoryDAL.GetAll(),
                    Products = products
                };

            }
            catch (Exception)
            {

                id = 0;
                model = new ListViewModel()
                {
                    Categories = categoryDAL.GetAll(),
                    Products = products
                };
            }

            ViewBag.Id = id;

            return View(model);
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