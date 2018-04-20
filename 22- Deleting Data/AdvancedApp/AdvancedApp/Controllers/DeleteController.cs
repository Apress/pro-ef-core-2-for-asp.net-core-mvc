using AdvancedApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;

namespace AdvancedApp.Controllers {

    public class DeleteController : Controller {
        private AdvancedContext context;

        public DeleteController(AdvancedContext ctx) => context = ctx;

        public IActionResult Index() {
            return View(context.Employees.Where(e => e.SoftDeleted)
                .Include(e => e.OtherIdentity).IgnoreQueryFilters());
        }

        [HttpPost]
        public IActionResult Restore(Employee employee) {
            context.Employees.IgnoreQueryFilters()
                .First(e => e.SSN == employee.SSN 
                    && e.FirstName == employee.FirstName 
                    && e.FamilyName == employee.FamilyName).SoftDeleted = false;
            context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult Delete(Employee e) {
            if (e.OtherIdentity != null) {
                context.Remove(e.OtherIdentity);
            }
            context.Employees.Remove(e);
            context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult DeleteAll() {
            IEnumerable<Employee> data = context.Employees
                .IgnoreQueryFilters()
                .Include(e => e.OtherIdentity)
                .Where(e => e.SoftDeleted).ToArray();
            context.RemoveRange(data.Select(e => e.OtherIdentity));
            context.RemoveRange(data);
            context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
