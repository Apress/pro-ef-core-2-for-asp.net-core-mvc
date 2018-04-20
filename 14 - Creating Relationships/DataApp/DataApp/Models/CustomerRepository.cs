using System.Collections.Generic;

namespace DataApp.Models {

    public interface ICustomerRepository {

        IEnumerable<Customer> GetAllCustomers();

    }

    public class EFCustomerRepository : ICustomerRepository {
        private EFCustomerContext context;

        public EFCustomerRepository(EFCustomerContext ctx) {
            context = ctx;
        }

        public IEnumerable<Customer> GetAllCustomers() {
            return context.Customers;
        }
    }
}
