using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;
using System.Collections.Generic;
using System.Linq;

namespace SportsStore.Controllers {

    public class OrdersController : Controller {
        private IRepository productRepository;
        private IOrdersRepository ordersRepository;

        public OrdersController(IRepository productRepo, 
                IOrdersRepository orderRepo) {
            productRepository = productRepo;
            ordersRepository = orderRepo;
        }

        public IActionResult Index() => View(ordersRepository.Orders);
        
        public IActionResult EditOrder(long id) {
            var products = productRepository.Products;
            Order order = id == 0 ? new Order() : ordersRepository.GetOrder(id);
            IDictionary<long, OrderLine> linesMap 
                = order.Lines?.ToDictionary(l => l.ProductId) 
                ?? new Dictionary<long, OrderLine>();
            ViewBag.Lines = products.Select(p => linesMap.ContainsKey(p.Id) 
                ? linesMap[p.Id] 
                 : new OrderLine { Product = p, ProductId = p.Id, Quantity = 0 });
            return View(order);
        }

        [HttpPost]
        public IActionResult AddOrUpdateOrder(Order order) {
            order.Lines = order.Lines
                .Where(l => l.Id > 0 || (l.Id == 0 && l.Quantity > 0)).ToArray();
            if (order.Id == 0) {
                ordersRepository.AddOrder(order);
            } else {
                ordersRepository.UpdateOrder(order);
            }
            return RedirectToAction(nameof(Index));
        }
        
        [HttpPost]
        public IActionResult DeleteOrder(Order order) {
            ordersRepository.DeleteOrder(order);
            return RedirectToAction(nameof(Index));
        }
    }
}
