using System.Collections.Generic;

namespace SportsStore.Models {

    public interface IRepository {

        IEnumerable<Product> Products { get; }

        void AddProduct(Product product);
    }
}
