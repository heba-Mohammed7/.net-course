using cleanArchitecture.Application.Abstractions.Messaging;
using cleanArchitecture.Application.Abstractions.Repositories;
using cleanArchitecture.Application.Features.Carts.Specifications;
using cleanArchitecture.Domain.Models.Carts;
using cleanArchitecture.Domain.Responses;

namespace cleanArchitecture.Application.Features.Carts.Commands.ClearCart;

public class ClearCartCommandHandler(
    IRepository<Cart> cartRepository,
    IRepository<CartItem> cartItemRepository) : ICommandHandler<ClearCartCommand, string>
{
    public async Task<Response<string>> Handle(ClearCartCommand request, CancellationToken cancellationToken)
    {
        var cart = await cartRepository.FirstOrDefaultAsync(new CartBySessionSpec(request.SessionId), cancellationToken);
        if (cart is null)
        {
            return Response<string>.NotFound("Cart not found.");
        }

        var cartItems = await cartItemRepository.ListAsync(new CartItemsByCartIdSpec(cart.Id), cancellationToken);
        foreach (var item in cartItems)
        {
            await cartItemRepository.DeleteAsync(item, cancellationToken);
        }

        cart.TotalAmount = 0;
        cart.ItemCount = 0;
        cart.UpdatedAt = DateTime.UtcNow;
        await cartRepository.UpdateAsync(cart, cancellationToken);

        return Response<string>.Success("Cart cleared successfully");
    }
}
