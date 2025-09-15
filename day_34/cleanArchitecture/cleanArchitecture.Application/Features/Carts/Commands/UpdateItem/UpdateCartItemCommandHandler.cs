using cleanArchitecture.Application.Abstractions.Messaging;
using cleanArchitecture.Application.Abstractions.Repositories;
using cleanArchitecture.Application.Features.Carts.Specifications;
using cleanArchitecture.Domain.Models.Carts;
using cleanArchitecture.Domain.Responses;

namespace cleanArchitecture.Application.Features.Carts.Commands.UpdateItem;

public class UpdateCartItemCommandHandler(
    IRepository<Cart> cartRepository,
    IRepository<CartItem> cartItemRepository) : ICommandHandler<UpdateCartItemCommand, string>
{
    public async Task<Response<string>> Handle(UpdateCartItemCommand request, CancellationToken cancellationToken)
    {
        var cartItem = await cartItemRepository.GetByIdAsync(request.ItemId, cancellationToken);
        if (cartItem is null)
        {
            return Response<string>.NotFound("Cart item not found.");
        }

        if (request.Quantity <= 0)
        {
            return Response<string>.Failure("Quantity must be greater than zero.");
        }

        cartItem.Quantity = request.Quantity;
        cartItem.TotalPrice = cartItem.UnitPrice * request.Quantity;
        cartItem.UpdatedAt = DateTime.UtcNow;
        await cartItemRepository.UpdateAsync(cartItem, cancellationToken);

        var cart = await cartRepository.GetByIdAsync(cartItem.CartId, cancellationToken);
        if (cart != null)
        {
            var cartItems = await cartItemRepository.ListAsync(new CartItemsByCartIdSpec(cart.Id), cancellationToken);
            cart.TotalAmount = cartItems.Sum(ci => ci.TotalPrice);
            cart.ItemCount = cartItems.Sum(ci => ci.Quantity);
            cart.UpdatedAt = DateTime.UtcNow;
            await cartRepository.UpdateAsync(cart, cancellationToken);
        }

        return Response<string>.Success("Cart item updated successfully");
    }
}
