using cleanArchitecture.Application.Abstractions.Messaging;
using cleanArchitecture.Application.Features.Carts.Dtos;

namespace cleanArchitecture.Application.Features.Carts.Queries.GetCart;

public record GetCartQuery(string SessionId) : IQuery<CartDto>;
