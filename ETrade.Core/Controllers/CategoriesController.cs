using ETrade.DAL.Concrete;
using ETrade.Entity.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;

namespace ETrade.Core.Controllers
{
    public class CategoriesController : Controller
    {
        public async Task<IActionResult> Index()
        {
            var httpClient = new HttpClient();
            var responseMessage = await httpClient.GetAsync("http://localhost:14694/api/Categories");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonString = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<Category>>(jsonString);
                return View(values);
            }
            return View();
        }
        public IActionResult Create() => View();
        [HttpPost]
        public async Task<IActionResult> CreateAsync(Category category)
        {
            var httpClient = new HttpClient();
            var jsonString = JsonConvert.SerializeObject(category); // categoryi json formatına çevirdik.
            var stringContent = new StringContent(jsonString, Encoding.UTF8, "application/json");
            var responseMessage = await httpClient.PostAsync("http://localhost:14694/api/Categories",stringContent);
            if (responseMessage.IsSuccessStatusCode)
                return RedirectToAction("Index");
            return View();
        }
        public async Task<IActionResult> Edit(int id)
        {
            var httpClient = new HttpClient();
            var responseMessage = await httpClient.GetAsync($"http://localhost:5177/api/Categories/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonString = await responseMessage.Content.ReadAsStringAsync();
                var value = JsonConvert.DeserializeObject<Category>(jsonString);
                return View(value);
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Category category)
        {
            var httpClient = new HttpClient();
            var jsonString = JsonConvert.SerializeObject(category);
            var stringContent = new StringContent(jsonString, Encoding.UTF8, "application/json");
            var responseMessage = await httpClient.PutAsync("http://localhost:5177/api/Categories", stringContent);
            if (responseMessage.IsSuccessStatusCode)
                return RedirectToAction("Index");
            return View();
        }
        public async Task<IActionResult> Details(int id)
        {
            var httpClient = new HttpClient();
            var responseMessage = await httpClient.GetAsync($"http://localhost:5177/api/Categories/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonString = await responseMessage.Content.ReadAsStringAsync();
                var value = JsonConvert.DeserializeObject<Category>(jsonString);
                return View(value);
            }
            return View();
        }
        public async Task<IActionResult> Delete(int id)
        {
            var httpClient = new HttpClient();
            var responseMessage = await httpClient.GetAsync($"http://localhost:5177/api/Categories/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonString = await responseMessage.Content.ReadAsStringAsync();
                var value = JsonConvert.DeserializeObject<Category>(jsonString);
                return View(value);
            }
            return View();
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmedAsync(Category category)
        {
            var httpClient = new HttpClient();
            var responseMessage = await httpClient.DeleteAsync($"http://localhost:5177/api/Categories?id={category.Id}");
            if (responseMessage.IsSuccessStatusCode)
                return RedirectToAction("Index");
            return View();
        }
    }
}
