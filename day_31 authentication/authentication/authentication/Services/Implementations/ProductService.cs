using System.Net;
using authentication.Data;
using authentication.Dtos;
using authentication.Models;
using authentication.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace authentication.Services.Implementations;

public class ProductService(ApplicationDbContext _context, IMapper _mapper, IFileUpload _fileUpload, UserManager<ApplicationUser> _userManager) : IProductService
    {
        

        public async Task<Response<ProductDto>> CreateProductAsync(CreateProductDto model, string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (!await _userManager.IsInRoleAsync(user, "ProductCreator"))
            {
                return new Response<ProductDto>
                {
                    Status = false,
                    Message = "User is not authorized to create products",
                    StatusCode = HttpStatusCode.Unauthorized
                };
            }

            var product = new Product
            {
                Name = model.Name,
                Description = model.Description,
                Price = model.Price,
                CreatorId = userId,
                CreatedAt = DateTime.UtcNow,
                IsApproved = false
            };

            if (model.Image != null)
            {
                product.ImagePath = await _fileUpload.UploadAsync(model.Image, "products", CancellationToken.None);
            }

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            var productDto = _mapper.Map<ProductDto>(product);
            return new Response<ProductDto>
            {
                Status = true,
                Data = productDto,
                Message = "Product created successfully, awaiting approval",
                StatusCode = HttpStatusCode.Created
            };
        }

        public async Task<Response<ProductDto>> ApproveProductAsync(int productId)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null)
            {
                return new Response<ProductDto>
                {
                    Status = false,
                    Message = "Product not found",
                    StatusCode = HttpStatusCode.NotFound
                };
            }

            product.IsApproved = true;
            await _context.SaveChangesAsync();

            var productDto = _mapper.Map<ProductDto>(product);
            return new Response<ProductDto>
            {
                Status = true,
                Data = productDto,
                Message = "Product approved successfully",
                StatusCode = HttpStatusCode.OK
            };
        }

        public async Task<Response<List<ProductDto>>> GetAllApprovedProductsAsync()
        {
            var products = await _context.Products
                .Where(p => p.IsApproved)
                .ToListAsync();
            var productDtos = _mapper.Map<List<ProductDto>>(products);
            return new Response<List<ProductDto>>
            {
                Status = true,
                Data = productDtos,
                Message = "Retrieved approved products successfully",
                StatusCode = HttpStatusCode.OK
            };
        }

        public async Task<Response<ProductDto>> GetProductByIdAsync(int id)
        {
            var product = await _context.Products
                .Where(p => p.IsApproved)
                .FirstOrDefaultAsync(p => p.Id == id);
            if (product == null)
            {
                return new Response<ProductDto>
                {
                    Status = false,
                    Message = "Product not found or not approved",
                    StatusCode = HttpStatusCode.NotFound
                };
            }

            var productDto = _mapper.Map<ProductDto>(product);
            return new Response<ProductDto>
            {
                Status = true,
                Data = productDto,
                Message = "Product retrieved successfully",
                StatusCode = HttpStatusCode.OK
            };
        }
    }