using Microsoft.AspNetCore.Mvc;
using DataApp.Models;
using System.Linq;

namespace DataApp.Controllers {
    public class HomeController : Controller {
        private IDataRepository repository;

        public HomeController(IDataRepository repo) {
            repository = repo;
        }

        public IActionResult Index(string category = null, 
                decimal? price = null, bool includeRelated = true) {
            var products = repository
                .GetFilteredProducts(category, price, includeRelated);
            ViewBag.category = category;
            ViewBag.price = price;
            ViewBag.includeRelated = includeRelated;
            return View(products);
        }

        public IActionResult Create() {
            ViewBag.CreateMode = true;
            return View("Editor", new Product());
        }

        [HttpPost]
        public IActionResult Create(Product product) {
            repository.CreateProduct(product);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(long id) {
            ViewBag.CreateMode = false;
            return View("Editor", repository.GetProduct(id));
        }

        [HttpPost]
        public IActionResult Edit(Product product, Product original) {
            repository.UpdateProduct(product, original);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult Delete(long id) {
            repository.DeleteProduct(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
