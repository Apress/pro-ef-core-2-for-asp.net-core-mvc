using ExistingDb.Models.Manual;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;

namespace ExistingDb.Controllers {

    public class ManualController : Controller {
        private ManualContext context;

        public ManualController(ManualContext ctx) => context = ctx;

        public IActionResult Index() {
            ViewBag.Styles = context.ShoeStyles.Include(s => s.Products);
            ViewBag.Widths = context.ShoeWidths.Include(s => s.Products);
            ViewBag.Categories = context.Categories
                .Include(c => c.Shoes).ThenInclude(j => j.Shoe);
            return View(context.Shoes.Include(s => s.Style)
                .Include(s => s.Width).Include(s => s.Categories)
                    .ThenInclude(j => j.Category));
        } 

        public IActionResult Edit(long id) {
            ViewBag.Styles = context.ShoeStyles;
            ViewBag.Widths = context.ShoeWidths;
            ViewBag.Categories = context.Categories;
            return View(context.Shoes.Include(s => s.Style)
                .Include(s => s.Campaign)
                .Include(s => s.Width).Include(s => s.Categories)
                .ThenInclude(j => j.Category).First(s => s.Id == id));
        }

        [HttpPost]
        public IActionResult Update(Shoe shoe, long[] newCategoryIds, 
                ShoeCategoryJunction[] oldJunctions) {

            IEnumerable<ShoeCategoryJunction> unchangedJunctions 
                = oldJunctions.Where(j => newCategoryIds.Contains(j.CategoryId));

            context.Set<ShoeCategoryJunction>()
                .RemoveRange(oldJunctions.Except(unchangedJunctions));

            shoe.Categories = newCategoryIds.Except(unchangedJunctions
                .Select(j => j.CategoryId))
                .Select(id => new ShoeCategoryJunction { 
                     ShoeId = shoe.Id, CategoryId = id 
                 }).ToList();

            context.Shoes.Update(shoe);
            context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
