using AdvancedApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Data;
using Microsoft.EntityFrameworkCore;

namespace AdvancedApp.Controllers {

    public class MultiController : Controller {
        private AdvancedContext context;
        private ILogger<MultiController> logger;
        private IsolationLevel level = IsolationLevel.ReadUncommitted;

        public MultiController(AdvancedContext ctx, ILogger<MultiController> log) {
            context = ctx;
            logger = log;
        }

        public IActionResult Index() {
            context.Database.BeginTransaction(level);
            return View("EditAll", context.Employees);
        }

        [HttpPost]
        public IActionResult UpdateAll(Employee[] employees) {
            context.Database.BeginTransaction(level);
            context.UpdateRange(employees);

            Employee temp = new Employee {
                SSN = "00-00-0000",
                FirstName = "Temporary",
                FamilyName = "Row",
                Salary = 0
            };
            context.Add(temp);
            context.SaveChanges();
            System.Threading.Thread.Sleep(5000);
            context.Remove(temp);
            context.SaveChanges();
            if (context.Employees.Sum(e => e.Salary) < 1_000_000) {
                context.Database.CommitTransaction();
            } else {
                context.Database.RollbackTransaction();
                logger.LogError("Salary total exceeds limit");
            }
            return RedirectToAction(nameof(Index));
        }

        public string ReadTest() {
            decimal firstSum = context.Employees.Sum(e => e.Salary);
            System.Threading.Thread.Sleep(5000);
            decimal secondSum = context.Employees.Sum(e => e.Salary);
            return $"Repeatable read results - first: {firstSum}, "
                + $"second: {secondSum}";
        }
    }
}
