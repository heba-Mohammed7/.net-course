namespace studentCourse.Controllers;

public class StudentController : BaseController
    {
        [HttpGet(Router.StudentRouter.GetAll)]
        public async Task<IActionResult> GetAll([FromQuery] GetAllStudentsDto studentdto)
        {
            var result = await mediator.Send(studentdto);
            return Result(result);
        }

        [HttpGet(Router.StudentRouter.GetById)]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await mediator.Send(new GetStudentById { Id = id });
            return Result(result);
        }

        [HttpPost(Router.StudentRouter.Add)]
        public async Task<IActionResult> Create(StudentDto studentDto)
        {
            var result = await mediator.Send(studentDto);
            return Result(result);
        }

        [HttpPut(Router.StudentRouter.Update)]
        public async Task<IActionResult> Update(int id, StudentDto studentDto)
        {
            var updateStudentDto = new UpdateStudentDto
            {
                Id = id,
                Name = studentDto.Name,
                Age = studentDto.Age
            };
            var result = await mediator.Send(updateStudentDto);
            return Result(result);
        }

        [HttpDelete(Router.StudentRouter.Delete)]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await mediator.Send(new DeleteStudentDto { Id = id });
            return Result(result);
        }

        [HttpPost(Router.StudentRouter.enroll + "/enroll/{courseId}")]
        public async Task<IActionResult> Enroll(int id, int courseId)
        {
            var result = await mediator.Send(new EnrollDto { StudentId = id, CourseId = courseId });
            return Result(result);
        }

        [HttpDelete(Router.StudentRouter.enroll + "/disenroll/{courseId}")]
        public async Task<IActionResult> Disenroll(int id, int courseId)
        {
            var result = await mediator.Send(new DisenrollDto { StudentId = id, CourseId = courseId });
            return Result(result);
        }
    }