using AutoMapper;
using nwEventoMVCa.Core.Domain;
using nwEventoMVCa.Core.DTO;
using nwEventoMVCa.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace nwEventoMVCa.Core.Services
{
    public class ProductService : IProductService
    {

        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository productRepository,
                IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public IEnumerable<ProductDto> GetAll()
        {
            var productsDto = _productRepository
                .GetAll()
                .Select(p => _mapper.Map<ProductDto>(p));

            return productsDto;
        }

        public ProductDto Get(Guid id)
        {
            var product = _productRepository.Get(id);

            return product == null ? null : _mapper.Map<ProductDto>(product);
        }

        public void Add(string name)
        {
            var product = new Product(name);
            _productRepository.Add(product);
        }

        public void Update(ProductDto product)
        {
            var existingProduct = _productRepository.Get(product.Id);
            if (existingProduct == null)
            {
                throw new Exception($"Product was not found, id: '{product.Id}'.");
            }
            existingProduct.SetName(product.Name);
            _productRepository.Update(existingProduct);
        }
    }
}
