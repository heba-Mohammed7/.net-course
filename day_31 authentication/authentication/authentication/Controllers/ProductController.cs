

using System.Net;
using System.Security.Claims;
using authentication.Controllers.Base;
using authentication.Dtos;
using authentication.Models;
using authentication.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace authentication.Controllers;
[Route("api/[controller]")]
    [ApiController]
    public class ProductController(IProductService _productService, ICartService _cartService) : BaseController
    {

        [Authorize(Roles = "ProductCreator")]
        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateProduct([FromForm] CreateProductDto model)
        {
            if (!ModelState.IsValid)
            {
                return Result(new Response<object>
                {
                    Data = ModelState,
                    StatusCode = HttpStatusCode.BadRequest
                });
            }

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var result = await _productService.CreateProductAsync(model, userId);
            return Result(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut]
        [Route("approve/{id}")]
        public async Task<IActionResult> ApproveProduct(int id)
        {
            var result = await _productService.ApproveProductAsync(id);
            return Result(result);
        }

        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> GetAllApprovedProducts()
        {
            var result = await _productService.GetAllApprovedProductsAsync();
            return Result(result);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var result = await _productService.GetProductByIdAsync(id);
            return Result(result);
        }

        [Authorize(Roles = "User")]
        [HttpPost]
        [Route("cart/add")]
        public async Task<IActionResult> AddToCart([FromBody] AddToCartDto model)
        {
            if (!ModelState.IsValid)
            {
                return Result(new Response<object>
                {
                    Data = ModelState,
                    StatusCode = HttpStatusCode.BadRequest
                });
            }

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var result = await _cartService.AddToCartAsync(model.ProductId, model.Quantity, userId);
            return Result(result);
        }

        [Authorize(Roles = "User")]
        [HttpGet]
        [Route("cart")]
        public async Task<IActionResult> GetCart()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var result = await _cartService.GetCartItemsAsync(userId);
            return Result(result);
        }

        
        
    }