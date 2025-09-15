using cleanArchitecture.Application.Features.Products.Commands.Add;
using cleanArchitecture.Application.Features.Products.Commands.Delete;
using cleanArchitecture.Application.Features.Products.Commands.Update;
using cleanArchitecture.Application.Features.Products.Queries.GetAll;
using cleanArchitecture.Application.Features.Products.Queries.GetById;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Mvc;
using cleanArchitecture.Domain.Filters;
using Router = cleanArchitecture.Domain.Routes.BaseRouter.Router;

namespace cleanArchitecture.presentation2.Controllers;

public class ProductController : BaseController
{
    [HttpPost(Router.ProductRouter.Add)]
    public async Task<IActionResult> Create(AddProductCommand productCommand)
    {
        var result = await mediator.Send(productCommand);
        return Result(result);
    }
    

    [HttpGet(Router.ProductRouter.GetAll)]
    public async Task<IActionResult> GetAll([FromQuery]GetAllProductsQuery request)
    {
        var result = await mediator.Send(request);
        return Result(result);
    }
    
    [HttpGet(Router.ProductRouter.GetById)]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await mediator.Send(new GetProductByIdQuery(id));
        return Result(result);
    }
    
    [HttpPut(Router.ProductRouter.Update)]
    public async Task<IActionResult> Update(Guid id, UpdateProductDto dto)
    {
        var command = new UpdateProductCommand(id, dto.Name, dto.Price, dto.CategoryId);
        var result = await mediator.Send(command);
        return Result(result);
    }
    
    [HttpDelete(Router.ProductRouter.Delete)]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await mediator.Send(new DeleteProductCommand(id));
        return Result(result);
    }
    
    
}