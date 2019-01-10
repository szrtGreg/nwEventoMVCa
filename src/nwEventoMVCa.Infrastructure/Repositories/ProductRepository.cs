﻿using nwEventoMVCa.Core.Domain;
using nwEventoMVCa.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace nwEventoMVCa.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private static readonly ISet<Product> _products = new HashSet<Product>();

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
