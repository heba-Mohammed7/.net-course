using Microsoft.EntityFrameworkCore;
using MediatR;
using AutoMapper;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using studentCourse.Repositories.Interfaces;
using studentCourse.Specifications;
using studentCourse.Global;
using studentCourse.Features.Student.Command.Models;

namespace studentCourse.Features.Student.Query.Handler;

public class StudentQueryHandler(IStudentRepository _studentRepository, IMapper _mapper) :
    IRequestHandler<GetAllStudentsDto, Response>,
    IRequestHandler<GetStudentById, Response>
{
    

    public async Task<Response> Handle(GetAllStudentsDto request, CancellationToken cancellationToken)
    {
        try
        {
            var filterDto = new StudentResponseDto
            {
                Name = request.Name,
                Age = request.Age ?? 0
            };
            
            var spec = new StudentSpecification(filterDto);
            var query =await _studentRepository.GetQueryableAsync(spec);
            var students = await query.ToListAsync(cancellationToken);
            var studentDtos = _mapper.Map<List<StudentResponseDto>>(students);

            return new Response
            {
                StatusCode = HttpStatusCode.OK,
                Data = studentDtos,
                Message = "Students retrieved successfully",
                Status = true
            };
        }
        catch (Exception ex)
        {
            return new Response
            {
                StatusCode = HttpStatusCode.InternalServerError,
                Data = null,
                Message = $"Error retrieving students: {ex.Message}",
                Status = false
            };
        }
    }

    public async Task<Response> Handle(GetStudentById request, CancellationToken cancellationToken)
    {
        try
        {
            var spec = new StudentSpecification(new StudentResponseDto { });
            spec.AddCriteria(x => x.Id == request.Id);

            var query =await _studentRepository.GetQueryableAsync(spec);
            var student = await query.FirstOrDefaultAsync(cancellationToken);

            if (student == null)
            {
                return new Response
                {
                    StatusCode = HttpStatusCode.NotFound,
                    Message = $"Student with id {request.Id} not found",
                    Status = false
                };
            }

            var studentDto = _mapper.Map<StudentResponseDto>(student);

            return new Response
            {
                StatusCode = HttpStatusCode.OK,
                Data = studentDto,
                Message = "Student retrieved successfully",
                Status = true
            };
        }
        catch (Exception ex)
        {
            return new Response
            {
                StatusCode = HttpStatusCode.InternalServerError,
                Data = null,
                Message = $"Error retrieving student: {ex.Message}",
                Status = false
            };
        }
    }
}