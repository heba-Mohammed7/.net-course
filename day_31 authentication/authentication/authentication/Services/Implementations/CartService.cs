using System.Net;
using authentication.Data;
using authentication.Dtos;
using authentication.Models;
using authentication.Services.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace authentication.Services.Implementations;

public class CartService(ApplicationDbContext _context, IMapper _mapper) : ICartService
    {

        public async Task<Response<CartItemDto>> AddToCartAsync(int productId, int quantity, string userId)
        {
            var product = await _context.Products
                .Where(p => p.IsApproved)
                .FirstOrDefaultAsync(p => p.Id == productId);
            if (product == null)
            {
                return new Response<CartItemDto>
                {
                    Status = false,
                    Message = "Product not found or not approved",
                    StatusCode = HttpStatusCode.NotFound
                };
            }

            var existingItem = await _context.CartItems
                .FirstOrDefaultAsync(ci => ci.ProductId == productId && ci.UserId == userId);

            if (existingItem != null)
            {
                existingItem.Quantity += quantity;
            }
            else
            {
                var cartItem = new CartItem
                {
                    ProductId = productId,
                    UserId = userId,
                    Quantity = quantity
                };
                _context.CartItems.Add(cartItem);
            }

            await _context.SaveChangesAsync();

            var cartItemDto = await _context.CartItems
                .Include(ci => ci.Product)
                .Where(ci => ci.ProductId == productId && ci.UserId == userId)
                .Select(ci => _mapper.Map<CartItemDto>(ci))
                .FirstOrDefaultAsync();

            return new Response<CartItemDto>
            {
                Status = true,
                Data = cartItemDto,
                Message = "Product added to cart successfully",
                StatusCode = HttpStatusCode.Created
            };
        }

        public async Task<Response<List<CartItemDto>>> GetCartItemsAsync(string userId)
        {
            var cartItems = await _context.CartItems
                .Include(ci => ci.Product)
                .Where(ci => ci.UserId == userId && ci.Product.IsApproved)
                .ToListAsync();

            var cartItemDtos = _mapper.Map<List<CartItemDto>>(cartItems);
            return new Response<List<CartItemDto>>
            {
                Status = true,
                Data = cartItemDtos,
                Message = "Cart items retrieved successfully",
                StatusCode = HttpStatusCode.OK
            };
        }

       
    }