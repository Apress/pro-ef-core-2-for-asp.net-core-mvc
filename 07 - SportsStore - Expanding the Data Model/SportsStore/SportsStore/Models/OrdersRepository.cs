using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace SportsStore.Models {

    public class OrdersRepository : IOrdersRepository {
        private DataContext context;

        public OrdersRepository(DataContext ctx) => context = ctx;

        public IEnumerable<Order> Orders => context.Orders
            .Include(o => o.Lines).ThenInclude(l => l.Product);

        public Order GetOrder(long key) => context.Orders
            .Include(o => o.Lines).First(o => o.Id == key);

        public void AddOrder(Order order) {
            context.Orders.Add(order);
            context.SaveChanges();
        }
        
        public void UpdateOrder(Order order) {
            context.Orders.Update(order);
            context.SaveChanges();
        }

        public void DeleteOrder(Order order) {
            context.Orders.Remove(order);
            context.SaveChanges();
        } 
    }
}
