using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using nwEventoMVCa.Core.Domain;
using nwEventoMVCa.Core.DTO;
using nwEventoMVCa.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace nwEventoMVCa.Core.Services
{
    public class CartService : ICartService
    {
        private readonly IUserRepository _userRepository;
        private readonly IProductRepository _productRepository;
        private readonly ICartManager _cartManager;
        private readonly IMapper _mapper;

        public CartService(IUserRepository userRepository,
            IProductRepository productRepository,
            ICartManager cartManager,
            IMapper mapper)
        {
            _userRepository = userRepository;
            _productRepository = productRepository;
            _cartManager = cartManager;
            _mapper = mapper;
        }

        public CartDto Get(Guid userId)
        {
            var cart = _cartManager.Get(userId);

            return cart == null ? null : _mapper.Map<CartDto>(cart);
        }

        public void AddProduct(Guid userId, Guid productId)
        {
            var cart = GetCartOrFail(userId);
            var product = _productRepository.Get(productId);
            if (product == null)
            {
                throw new Exception("Product was not found");
            }
            cart.AddProduct(product);
            _cartManager.Set(userId, cart);
        }

        public void Delete(Guid userId)
        {
            GetCartOrFail(userId);
            _cartManager.Delete(userId);
        }

        public void Clear(Guid userId)
        {
            var cart = GetCartOrFail(userId);
            cart.Clear();
            _cartManager.Set(userId, cart);
        }

        public void Create(Guid userId)
        {
            var cart = _cartManager.Get(userId);
            if (cart != null)
            {
                throw new Exception($"Cart already exists for user with id: '{userId}'.");
            }
            _cartManager.Set(userId, new Cart());
        }

        public void DeleteProduct(Guid userId, Guid productId)
        {
            throw new Exception();
        }

        private Cart GetCartOrFail(Guid userId)
        {
            var cart = _cartManager.Get(userId);
            if (cart == null)
            {
                throw new Exception($"Cart was not found for user with id: '{userId}'.");
            }
            return cart;
        }
    }
}
