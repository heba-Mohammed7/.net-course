using AutoMapper;
using Task.Dto;
using Task.Models;

namespace Task.Mapping;


public class ImageUrlResolver : IValueResolver<Employee, EmployeeDto, string?>
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ImageUrlResolver(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string? Resolve(Employee source, EmployeeDto destination, string? destMember, ResolutionContext context)
    {
        if (string.IsNullOrEmpty(source.ImagePath))
        {
            return null;
        }

        var request = _httpContextAccessor.HttpContext?.Request;
        if (request == null)
        {
            return source.ImagePath;
        }

        return $"{request.Scheme}://{request.Host}{source.ImagePath}";
    }
}