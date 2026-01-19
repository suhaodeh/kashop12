using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KASHOP12.DAL.Data.DTO.Request;
using KASHOP12.DAL.Data.DTO.Response;
using KASHOP12.DAL.Models;
using KASHOP12.DAL.Repository;
using Mapster;

namespace KASHOP12.BLL.Service
{
    public class CartService : ICartService
    {
        private readonly IProductRepository _productRepository;
        private readonly ICartRepository _cartRepository;

        public CartService(IProductRepository productRepository,ICartRepository cartRepository)
        {
            _productRepository = productRepository;
            _cartRepository = cartRepository;
        }

        public async Task<BaseResponse> AddToCartAsync(string userId, AddToCartRequest request)
        {
            var product = await _productRepository.FindByIdAsync(request.ProductId);
            if (product == null)
            {
                return new BaseResponse
                {
                    Success = false,
                    Message = "product not found"
                };
            }

            if(product.Quantity < request.Count)
            {
                return new BaseResponse
                {
                    Success = false,
                    Message = "Not enough stock"
                };

            }
            var cartItem = await _cartRepository.GetCartItemAsync(userId, request.ProductId);
            if(cartItem is not null)
            {
                cartItem.Count += request.Count;
                await _cartRepository.UpdateAsync(cartItem);
            }
            else
            {
                var cart = request.Adapt<Cart>();
                cart.UserId = userId;

                await _cartRepository.CreateAsync(cart);
            }
        
            return new BaseResponse
            {
                Success = true,
                Message = "Product added successfully"
            };
        }

        public async Task<CartSummaryResponse> GetUserCartAsync(string userId)
        {
            var cartItems = await _cartRepository.GetUserCartAsync(userId);

            var response = cartItems.Adapt<CartResponse>();

            var items = cartItems.Select(c => new CartResponse
            {
                ProductId = c.ProductId,
              
                Count = c.Count,
                Price = c.Product.Price
            }).ToList();
            return new CartSummaryResponse
            {
                Items=items,
            };
        }

        public async Task<BaseResponse> ClearCartAsync(string userId)
        {
            await _cartRepository.ClearCartAsync(userId);
            return new BaseResponse
            {
                Success = true,
                Message = "Cart Clear Successfully"
            };
        }
    }
}
