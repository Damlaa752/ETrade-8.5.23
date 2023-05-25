using ETrade.DAL.Abstract;
using ETrade.Entity.Models.Entities;
using ETrade.Entity.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ETrade.Core.Controllers
{
    // [Authorize(Roles = "Admin")]
    public class OrderController : Controller
    {

        private readonly IOrderDAL _orderDAL;

        public OrderController(IOrderDAL orderDAL)
        {
            _orderDAL = orderDAL;
        }

        public IActionResult Index()
        {
            return View(_orderDAL.GetAll());
        }
        public IActionResult Details(int id)
        {
            var order = _orderDAL.Get(id);
            //orderlinedal yazılmalı
            return View(order);
        }
        public IActionResult Delete(int id)
        {
            var order = _orderDAL.Get(id);
            _orderDAL.Delete(order);
            return RedirectToAction("Index");
        }
        public IActionResult Edit (int id)
        {
            var order = _orderDAL.Get(id);
            bool orderState = false;
            if (order.OrderState ==OrderState.Waiting)
                orderState = false;
            else
                orderState = true;
            var model = new OrderStateViewModel()
            {
                OrderId = order.Id,
                IsCompleted = false
            };
            return View(model);
        }
        [HttpPost]
        public IActionResult Edit(OrderStateViewModel model)
        {
            var order = _orderDAL.Get(model.OrderId);
            if (model.IsCompleted)
                order.OrderState = OrderState.Completed;
            else 
                order.OrderState = OrderState.Waiting;
            _orderDAL.Update(order);
            return RedirectToAction("Index");
        }
    }
}
