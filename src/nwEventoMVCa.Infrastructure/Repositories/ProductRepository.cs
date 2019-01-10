using nwEventoMVCa.Core.Domain;
using nwEventoMVCa.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace nwEventoMVCa.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private static readonly ISet<Product> _products = new HashSet<Product>()
        {
            new Product("Product1", 100),
            new Product("Product2", 100),
            new Product("Product3", 100),
            new Product("Product4", 100)
        };

        public Product Get(Guid id)
                => _products.SingleOrDefault(x => x.Id == id);

        public IEnumerable<Product> GetAll()
            => _products;

        public void Add(Product product)
            => _products.Add(product);

        //In memory - no need to update
        public void Update(Product product)
        {
        }
    }
}
