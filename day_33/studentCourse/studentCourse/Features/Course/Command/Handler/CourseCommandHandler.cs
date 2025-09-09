namespace studentCourse.Features.Course.Command.Handler;


public class CourseCommandHandler(ICourseRepository _courseRepository, ILearnRepository _learnRepository, IMapper _mapper) :
    IRequestHandler<CourseDto, Response>,
    IRequestHandler<UpdateCourseDto, Response>,
    IRequestHandler<DeleteCourseDto, Response>
{
    

    public async Task<Response> Handle(CourseDto request, CancellationToken cancellationToken)
    {
        var course = _mapper.Map<CourseEntity>(request);
        await _courseRepository.Create(course);

        return new Response
        {
            StatusCode = HttpStatusCode.Created,
            Data = course.Id,
            Message = "Course created successfully",
            Status = true
        };
    }

    public async Task<Response> Handle(UpdateCourseDto request, CancellationToken cancellationToken)
    {
        var course = await _courseRepository.GetById(request.Id);

        if (course == null)
        {
            return new Response
            {
                StatusCode = HttpStatusCode.NotFound,
                Message = $"Course with id {request.Id} not found",
                Status = false,
            };
        }

        _mapper.Map(request, course);
        await _courseRepository.Update(course);

        return new Response
        {
            StatusCode = HttpStatusCode.OK,
            Data = course.Id,
            Message = "Course updated successfully",
            Status = true
        };
    }

    public async Task<Response> Handle(DeleteCourseDto request, CancellationToken cancellationToken)
    {
        var course = await _courseRepository.GetById(request.Id);
        if (course == null)
        {
            return new Response
            {
                StatusCode = HttpStatusCode.NotFound,
                Message = $"Course with id {request.Id} not found",
                Status = false,
            };
        }

        var learns = await _learnRepository.GetAll();
        var courseLearns = learns.Where(l => l.CourseId == request.Id).ToList();
        foreach (var learn in courseLearns)
        {
            await _learnRepository.RemoveAsync(learn);
        }

        await _courseRepository.DeleteAsync(course);

        return new Response
        {
            StatusCode = HttpStatusCode.OK,
            Message = "Course deleted successfully",
            Status = true
        };
    }
}
