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
        private readonly IMemoryCache _cache;
        private readonly IMapper _mapper;

        public CartService(IUserRepository userRepository,
            IProductRepository productRepository,
            IMemoryCache cache,
            IMapper mapper)
        {
            _userRepository = userRepository;
            _productRepository = productRepository;
            _cache = cache;
            _mapper = mapper;
        }

        public CartDto Get(Guid userId)
        {
            var cart = GetCart(userId);

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
            SetCart(userId, cart);
        }

        public void Delete(Guid userId)
        {
            GetCartOrFail(userId);
            _cache.Remove(GetCartKey(userId));
        }

        public void Clear(Guid userId)
        {
            var cart = GetCartOrFail(userId);
            cart.Clear();
            SetCart(userId, cart);
        }

        public void Create(Guid userId)
        {
            var cart = GetCart(userId);
            if (cart != null)
            {
                throw new Exception($"Cart already exists for user with id: '{userId}'.");
            }
            SetCart(userId, new Cart());
        }

        public void DeleteProduct(Guid userId, Guid productId)
        {
            GetCartOrFail(userId);
            _cache.Remove(GetCartKey(userId));
        }

        private Cart GetCartOrFail(Guid userId)
        {
            var cart = GetCart(userId);
            if (cart == null)
            {
                throw new Exception($"Cart was not found for user with id: '{userId}'.");
            }
            return cart;
        }

        private Cart GetCart(Guid userId) => _cache.Get<Cart>(GetCartKey(userId));

        private void SetCart(Guid userId, Cart cart) => _cache.Set(GetCartKey(userId), cart);

        private string GetCartKey(Guid userId) => $"{userId}:cart";
    }
}
