using DataApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace DataApp.Controllers {

    public class One2OneController : Controller {
        private EFDatabaseContext context;

        public One2OneController(EFDatabaseContext ctx) => context = ctx;

        public IActionResult Index () {
            return View(context.Set<ContactDetails>().Include(cd => cd.Supplier));
        }

        public IActionResult Create() => View("ContactEditor");

        public IActionResult Edit(long id) {
            ViewBag.Suppliers = context.Suppliers.Include(s => s.Contact);
            return View("ContactEditor",
                context.Set<ContactDetails>()
                    .Include(cd => cd.Supplier).First(cd => cd.Id == id));
        }

        [HttpPost]
        public IActionResult Update(ContactDetails details, 
                long? targetSupplierId, long[] spares) {
            if (details.Id == 0) {
                context.Add<ContactDetails>(details);
            } else {
                context.Update<ContactDetails>(details);
                if (targetSupplierId.HasValue) {
                    if (spares.Contains(targetSupplierId.Value)) {
                        details.SupplierId = targetSupplierId.Value;                
                    } else {
                        ContactDetails targetDetails = context.Set<ContactDetails>()
                            .FirstOrDefault(cd => cd.SupplierId == targetSupplierId);
                        targetDetails.SupplierId = null;
                        details.SupplierId = targetSupplierId.Value;
                        context.SaveChanges();
                    }
                }
            }
            context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }



    }
}
