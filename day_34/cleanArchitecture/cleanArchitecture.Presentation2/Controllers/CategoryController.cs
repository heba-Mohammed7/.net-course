
using cleanArchitecture.Application.Features.Categories.Commands.Add;
using cleanArchitecture.Application.Features.Categories.Commands.Delete;
using cleanArchitecture.Application.Features.Categories.Commands.Update;
using cleanArchitecture.Application.Features.Categories.Queries.GetAll;
using cleanArchitecture.Application.Features.Categories.Queries.GetById;
using cleanArchitecture.Domain.Routes.BaseRouter;
using Microsoft.AspNetCore.Mvc;

namespace cleanArchitecture.presentation2.Controllers;

public class CategoryController : BaseController
{
    [HttpPost(Router.CategoryRouter.Add)]
    public async Task<IActionResult> Create(AddCategoryCommand productCommand)
    {
        var result = await mediator.Send(productCommand);
        return Result(result);
    }
    [HttpGet(Router.CategoryRouter.GetAll)]
    public async Task<IActionResult> GetAll([FromQuery]GetAllCategoriesQuery request)
    {
        var result = await mediator.Send(request);
        return Result(result);
    }
    [HttpGet(Router.CategoryRouter.GetById)]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await mediator.Send(new GetCategoryByIdQuery(id));
        return Result(result);
    }
    
    [HttpPut(Router.CategoryRouter.Update)]
    public async Task<IActionResult> Update(Guid id, UpdateCategoryDto dto)
    {
        var command = new UpdateCategoryCommand(id, dto.Name);
        var result = await mediator.Send(command);
        return Result(result);
    }
    
    [HttpDelete(Router.CategoryRouter.Delete)]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await mediator.Send(new DeleteCategoryCommand(id));
        return Result(result);
    }
}