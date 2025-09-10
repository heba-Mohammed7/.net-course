namespace studentCourse.Controllers;

public class CourseController : BaseController
{

    [HttpGet(Router.CourseRouter.Main)]
    public async Task<IActionResult> All([FromQuery] string? name, [FromQuery] string? code)
    {
        var result = await mediator.Send(new GetAllCoursesDto { Name = name, Code = code });
        return Result(result);
    }

    [HttpGet(Router.CourseRouter.MainId)]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await mediator.Send(new GetCourseById { Id = id });
        return Result(result);
    }

    [HttpPost(Router.CourseRouter.Main)]
    public async Task<IActionResult> Create(CourseDto courseDto)
    {
        var result = await mediator.Send(courseDto);
        return Result(result);
    }

    [HttpPut(Router.CourseRouter.MainId)]
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

    [HttpDelete(Router.CourseRouter.MainId)]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await mediator.Send(new DeleteCourseDto { Id = id });
        return Result(result);
    }
}