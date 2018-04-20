using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.DependencyInjection;

namespace DataApp.Models {
    public class EFCustomerContext : DbContext {

        public EFCustomerContext(DbContextOptions<EFCustomerContext> opts)
            : base(opts) { }

        public DbSet<Customer> Customers { get; set; }
    }
}
