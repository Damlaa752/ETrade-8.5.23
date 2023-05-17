using ETrade.Core.Models.Helper;
using ETrade.DAL.Abstract;
using ETrade.Entity.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ETrade.Core.Controllers
{
    public class CartController : Controller
    {
        private readonly IProductDAL _productDAL;
        private readonly IOrderDAL _orderDAL;

        public CartController(IProductDAL productDAL, IOrderDAL orderDAL)
        {
            _productDAL = productDAL;
            _orderDAL = orderDAL;
        }
        public IActionResult Index()
        {
            var cart = SessionHelper.GetFromJson<List<CartItem>>(HttpContext.Session, "cart");
            if (cart != null)
            {
                ViewBag.Total = cart.Sum(i => i.Product.Price * i.Quatity).ToString("c");
                SessionHelper.Count = cart.Count;
                //SessionHelper.Count = cart.Sum(i=>i.Quatity);
            }
            return View(cart);
        }
        [HttpPost]
        public IActionResult Buy(int id, string quantity)
        {
            if (SessionHelper.GetFromJson<List<CartItem>>(HttpContext.Session, "cart") == null)
            {
                var cart = new List<CartItem>();
                cart.Add(new CartItem()
                {
                    Product = _productDAL.Get(id),
                    Quatity = Convert.ToInt32(quantity)
                });
                SessionHelper.SetAsJson(HttpContext.Session, "cart", cart);
            }
            else
            {
                var cart = SessionHelper.GetFromJson<List<CartItem>>(HttpContext.Session, "cart");
                int index = isIndex(cart, id);
                if (index == -1)
                {
                    cart.Add(new CartItem()
                    {
                        Product = _productDAL.Get(id),
                        Quatity = Convert.ToInt32(quantity)
                    });
                }
                else
                    cart[index].Quatity += Convert.ToInt32(quantity);
                SessionHelper.SetAsJson(HttpContext.Session, "cart", cart);
            }
            return RedirectToAction("Index");
        }
        private int isIndex(List<CartItem> cart, int id)
        {
            for (int i = 0; i < cart.Count; i++)
            {
                if (cart[i].Product.Id.Equals(id))
                    return i;
            }
            return -1;
        }
    }
}
