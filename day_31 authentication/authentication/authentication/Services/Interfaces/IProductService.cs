using authentication.Dtos;
using authentication.Models;

namespace authentication.Services.Interfaces;

public interface IProductService
{
    Task<Response<ProductDto>> CreateProductAsync(CreateProductDto model, string userId);
    Task<Response<ProductDto>> ApproveProductAsync(int productId);
    Task<Response<List<ProductDto>>> GetAllApprovedProductsAsync();
    Task<Response<ProductDto>> GetProductByIdAsync(int id);
}