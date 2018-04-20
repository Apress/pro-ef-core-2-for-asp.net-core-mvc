using AdvancedApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace AdvancedApp.Controllers {

    public class QueryController : Controller {
        private AdvancedContext context;

        public QueryController(AdvancedContext ctx) => context = ctx;

        public IActionResult ServerEval() {
            return View("Query", context.Employees.Where(e => e.Salary > 150_000));
        }

        public IActionResult ClientEval() {
            return View("Query", context.Employees.Where(e => IsHighEarner(e)));
        }

        private bool IsHighEarner(Employee e) {
            return e.Salary > 150_000;
        }
    }
}
