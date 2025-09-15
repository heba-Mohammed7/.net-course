using AutoMapper;
using cleanArchitecture.Application.Abstractions.Messaging;
using cleanArchitecture.Application.Abstractions.Repositories;
using cleanArchitecture.Application.Features.Carts.Dtos;
using cleanArchitecture.Application.Features.Carts.Specifications;
using cleanArchitecture.Domain.Models.Carts;
using cleanArchitecture.Domain.Responses;

namespace cleanArchitecture.Application.Features.Carts.Queries.GetCart;

public class GetCartQueryHandler(
    IMapper mapper,
    IReadRepository<Cart> cartRepository,
    IReadRepository<CartItem> cartItemRepository) : IQueryHandler<GetCartQuery, CartDto>
{
    public async Task<Response<CartDto>> Handle(GetCartQuery request, CancellationToken cancellationToken)
    {
        var cart = await cartRepository.FirstOrDefaultAsync(new CartBySessionSpec(request.SessionId), cancellationToken);
        if (cart is null)
        {
            return Response<CartDto>.Success(new CartDto
            {
                Id = Guid.Empty,
                SessionId = request.SessionId,
                TotalAmount = 0,
                ItemCount = 0,
                Items = new List<CartItemDto>()
            });
        }

        var cartItems = await cartItemRepository.ListAsync(new CartItemsByCartIdSpec(cart.Id, true), cancellationToken);
        
        var cartDto = mapper.Map<CartDto>(cart);
        cartDto.Items = mapper.Map<List<CartItemDto>>(cartItems);

        return Response<CartDto>.Success(cartDto);
    }
}
