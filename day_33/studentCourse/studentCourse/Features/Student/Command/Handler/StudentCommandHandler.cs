namespace studentCourse.Features.Student.Command.Handler;

public class StudentCommandHandler(IStudentRepository _studentRepository, ILearnRepository _learnRepository, IMapper _mapper) :
        IRequestHandler<StudentDto, Response>,
        IRequestHandler<UpdateStudentDto, Response>,
        IRequestHandler<DeleteStudentDto, Response>
    {
        

        public async Task<Response> Handle(StudentDto request, CancellationToken cancellationToken)
        {
            var student = _mapper.Map<StudentEntity>(request);
            await _studentRepository.Create(student);

            return new Response
            {
                StatusCode = HttpStatusCode.Created,
                Data = student.Id,
                Message = "Student created successfully",
                Status = true
            };
        }

        public async Task<Response> Handle(UpdateStudentDto request, CancellationToken cancellationToken)
        {
            var student = await _studentRepository.GetById(request.Id);

            if (student == null)
            {
                return new Response
                {
                    StatusCode = HttpStatusCode.NotFound,
                    Message = $"Student with id {request.Id} not found",
                    Status = false,
                };
            }

            _mapper.Map(request, student);
            await _studentRepository.Update(student);

            return new Response
            {
                StatusCode = HttpStatusCode.OK,
                Data = student.Id,
                Message = "Student updated successfully",
                Status = true
            };
        }

        public async Task<Response> Handle(DeleteStudentDto request, CancellationToken cancellationToken)
        {
            var student = await _studentRepository.GetById(request.Id);
            if (student == null)
            {
                return new Response
                {
                    StatusCode = HttpStatusCode.NotFound,
                    Message = $"Student with id {request.Id} not found",
                    Status = false,
                };
            }

            var learns = await _learnRepository.GetAll(); 
            var studentLearns = learns.Where(l => l.StudentId == request.Id).ToList();
            foreach (var learn in studentLearns)
            {
                await _learnRepository.RemoveAsync(learn);
            }

            await _studentRepository.DeleteAsync(student); 

            return new Response
            {
                StatusCode = HttpStatusCode.OK,
                Message = "Student deleted successfully",
                Status = true
            };
        }
    }