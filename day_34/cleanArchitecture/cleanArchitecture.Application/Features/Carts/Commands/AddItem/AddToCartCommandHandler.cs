using AutoMapper;
using cleanArchitecture.Application.Abstractions.Messaging;
using cleanArchitecture.Application.Abstractions.Repositories;
using cleanArchitecture.Application.Features.Carts.Specifications;
using cleanArchitecture.Domain.Models.Carts;
using cleanArchitecture.Domain.Models.Products;
using cleanArchitecture.Domain.Responses;

namespace cleanArchitecture.Application.Features.Carts.Commands.AddItem;

public class AddToCartCommandHandler(
    IMapper mapper,
    IRepository<Cart> cartRepository,
    IRepository<CartItem> cartItemRepository,
    IReadRepository<Product> productRepository) : ICommandHandler<AddToCartCommand, string>
{
    public async Task<Response<string>> Handle(AddToCartCommand request, CancellationToken cancellationToken)
    {
        var product = await productRepository.GetByIdAsync(request.ProductId, cancellationToken);
        if (product is null)
        {
            return Response<string>.NotFound("Product not found.");
        }

        var cart = await cartRepository.FirstOrDefaultAsync(new CartBySessionSpec(request.SessionId), cancellationToken);
        if (cart is null)
        {
            cart = mapper.Map<Cart>(request);
            await cartRepository.AddAsync(cart, cancellationToken);
        }

        var existingItem = await cartItemRepository.FirstOrDefaultAsync(
            new CartItemByCartAndProductSpec(cart.Id, request.ProductId), 
            cancellationToken);

        if (existingItem != null)
        {
            existingItem.Quantity += request.Quantity;
            existingItem.TotalPrice = existingItem.Quantity * existingItem.UnitPrice;
            existingItem.UpdatedAt = DateTime.UtcNow;
            await cartItemRepository.UpdateAsync(existingItem, cancellationToken);
        }
        else
        {
            var cartItem = mapper.Map<CartItem>(request);
            cartItem.CartId = cart.Id;
            cartItem.UnitPrice = product.Price;
            cartItem.TotalPrice = product.Price * request.Quantity;
            await cartItemRepository.AddAsync(cartItem, cancellationToken);
        }

        var cartItems = await cartItemRepository.ListAsync(new CartItemsByCartIdSpec(cart.Id), cancellationToken);
        cart.TotalAmount = cartItems.Sum(ci => ci.TotalPrice);
        cart.ItemCount = cartItems.Sum(ci => ci.Quantity);
        cart.UpdatedAt = DateTime.UtcNow;
        await cartRepository.UpdateAsync(cart, cancellationToken);

        return Response<string>.Success("Item added to cart successfully");
    }
}
