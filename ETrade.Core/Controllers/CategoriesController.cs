using ETrade.Entity.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ETrade.Core.Controllers
{
    public class CategoriesController : Controller
    {
        public async Task<IActionResult> Index()
        {
            var httpClient = new HttpClient();
            var responseBody = await httpClient.GetAsync("http://localhost:14694/api/Categories");
            var jsonString = await responseBody.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<Category>>(jsonString);
            return View(values);
        }
    }
}
