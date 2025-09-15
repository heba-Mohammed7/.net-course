namespace studentCourse.Controllers;

public class CourseController : BaseController
{

    [HttpGet(Router.CourseRouter.GetAll)]
    public async Task<IActionResult> GetAll([FromQuery] GetAllCoursesDto Coursedto)
    {
        var result = await mediator.Send(Coursedto);
        return Result(result);
    }

    [HttpGet(Router.CourseRouter.GetById)]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await mediator.Send(new GetCourseById { Id = id });
        return Result(result);
    }

    [HttpPost(Router.CourseRouter.Add)]
    public async Task<IActionResult> Create(CourseDto courseDto)
    {
        var result = await mediator.Send(courseDto);
        return Result(result);
    }

    [HttpPut(Router.CourseRouter.Update)]
    public async Task<IActionResult> Update(int id, CourseDto courseDto)
    {
        var updateCourseDto = new UpdateCourseDto
        {
            Id = id,
            Cname = courseDto.Cname,
            Code = courseDto.Code,
            Hours = courseDto.Hours
        };
        var result = await mediator.Send(updateCourseDto);
        return Result(result);
    }

    [HttpDelete(Router.CourseRouter.Delete)]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await mediator.Send(new DeleteCourseDto { Id = id });
        return Result(result);
    }
}