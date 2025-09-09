namespace studentCourse.Features.Course.Query.Handler;

public class CourseQueryHandler(ICourseRepository _courseRepository, IMapper _mapper) :
    IRequestHandler<GetAllCoursesDto, Response>,
    IRequestHandler<GetCourseById, Response>
{
    

    public async Task<Response> Handle(GetAllCoursesDto request, CancellationToken cancellationToken)
    {
        try
        {
            var spec = new CourseSpecification(null);
            var query = await _courseRepository.GetQueryableAsync(spec); 
            var courses = await query.ToListAsync(cancellationToken);
            var courseDtos = _mapper.Map<List<CourseResponseDto>>(courses); 

            return new Response
            {
                StatusCode = HttpStatusCode.OK,
                Data = courseDtos,
                Message = "Courses retrieved successfully",
                Status = true
            };
        }
        catch (Exception ex)
        {
            return new Response
            {
                StatusCode = HttpStatusCode.InternalServerError,
                Data = null,
                Message = $"Error retrieving courses: {ex.Message}",
                Status = false
            };
        }
    }

    public async Task<Response> Handle(GetCourseById request, CancellationToken cancellationToken)
    {
        try
        {
            var spec = new CourseSpecification(new CourseResponseDto { });
            spec.AddCriteria(x => x.Id == request.Id);

            var query =await _courseRepository.GetQueryableAsync(spec);
            var course = await query.FirstOrDefaultAsync(cancellationToken);

            if (course == null)
            {
                return new Response
                {
                    StatusCode = HttpStatusCode.NotFound,
                    Message = $"Course with id {request.Id} not found",
                    Status = false
                };
            }

            var courseDto = _mapper.Map<CourseResponseDto>(course);

            return new Response
            {
                StatusCode = HttpStatusCode.OK,
                Data = courseDto,
                Message = "Course retrieved successfully",
                Status = true
            };
        }
        catch (Exception ex)
        {
            return new Response
            {
                StatusCode = HttpStatusCode.InternalServerError,
                Data = null,
                Message = $"Error retrieving course: {ex.Message}",
                Status = false
            };
        }
    }
}