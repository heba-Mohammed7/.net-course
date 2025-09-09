namespace studentCourse.Features.Learn.Command.Handler;

public class LearnCommandHandler(ILearnRepository _learnRepository, IStudentRepository _studentRepository, ICourseRepository _courseRepository) :
        IRequestHandler<EnrollDto, Response>,
        IRequestHandler<DisenrollDto, Response>
    {
        

        public async Task<Response> Handle(EnrollDto request, CancellationToken cancellationToken)
        {
            if (await _learnRepository.ExistsAsync(request.StudentId, request.CourseId))
            {
                return new Response { StatusCode = HttpStatusCode.BadRequest, Message = "Enrollment already exists", Status = false };
            }

            var student = await _studentRepository.GetById(request.StudentId);
            var course = await _courseRepository.GetById(request.CourseId);

            if (student == null || course == null)
            {
                return new Response { StatusCode = HttpStatusCode.NotFound, Message = "Student or Course not found", Status = false };
            }

            var learn = new LearnEntity { StudentId = request.StudentId, CourseId = request.CourseId };
            await _learnRepository.AddAsync(learn);

            return new Response
            {
                StatusCode = HttpStatusCode.Created,
                Data = learn,
                Message = "Student enrolled successfully",
                Status = true
            };
        }

        public async Task<Response> Handle(DisenrollDto request, CancellationToken cancellationToken)
        {
            if (!await _learnRepository.ExistsAsync(request.StudentId, request.CourseId))
            {
                return new Response { StatusCode = HttpStatusCode.NotFound, Message = "Enrollment not found", Status = false };
            }

            var learn = new LearnEntity { StudentId = request.StudentId, CourseId = request.CourseId };
            await _learnRepository.RemoveAsync(learn);

            return new Response
            {
                StatusCode = HttpStatusCode.OK,
                Message = "Student disenrolled successfully",
                Status = true
            };
        }
    }