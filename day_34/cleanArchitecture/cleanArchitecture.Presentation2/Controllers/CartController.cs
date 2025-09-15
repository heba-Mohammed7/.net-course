using cleanArchitecture.Application.Features.Carts.Commands.AddItem;
using cleanArchitecture.Application.Features.Carts.Commands.UpdateItem;
using cleanArchitecture.Application.Features.Carts.Commands.RemoveItem;
using cleanArchitecture.Application.Features.Carts.Commands.ClearCart;
using cleanArchitecture.Application.Features.Carts.Queries.GetCart;
using cleanArchitecture.Application.Features.Carts.Dtos;
using cleanArchitecture.Domain.Routes.BaseRouter;
using Microsoft.AspNetCore.Mvc;

namespace cleanArchitecture.presentation2.Controllers;

public class CartController : BaseController
{
    [HttpPost(Router.CartRouter.Add)]
    public async Task<IActionResult> AddToCart([FromHeader(Name = "Session-Id")] string sessionId, AddToCartDto request)
    {
        if (string.IsNullOrEmpty(sessionId))
        {
            sessionId = Guid.NewGuid().ToString();
        }
        
        var command = new AddToCartCommand(sessionId, request.ProductId, request.Quantity);
        var result = await mediator.Send(command);
        return Result(result);
    }

    [HttpGet(Router.CartRouter.GetCart)]
    public async Task<IActionResult> GetCart(string sessionId)
    {
        var query = new GetCartQuery(sessionId);
        var result = await mediator.Send(query);
        return Result(result);
    }

    [HttpPut(Router.CartRouter.Update)]
    public async Task<IActionResult> UpdateCartItem(Guid itemId, UpdateCartItemDto request)
    {
        var command = new UpdateCartItemCommand(itemId, request.Quantity);
        var result = await mediator.Send(command);
        return Result(result);
    }

    [HttpDelete(Router.CartRouter.Remove)]
    public async Task<IActionResult> RemoveCartItem(Guid itemId)
    {
        var command = new RemoveCartItemCommand(itemId);
        var result = await mediator.Send(command);
        return Result(result);
    }

    [HttpDelete(Router.CartRouter.ClearCart)]
    public async Task<IActionResult> ClearCart(string sessionId)
    {
        var command = new ClearCartCommand(sessionId);
        var result = await mediator.Send(command);
        return Result(result);
    }
}
