using authentication.Dtos;
using authentication.Models;

namespace authentication.Services.Interfaces;

public interface ICartService
{
    Task<Response<CartItemDto>> AddToCartAsync(int productId, int quantity, string userId);
    Task<Response<List<CartItemDto>>> GetCartItemsAsync(string userId);
}