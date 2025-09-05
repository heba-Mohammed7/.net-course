using authentication.Dtos;
using authentication.Models;
using AutoMapper;

namespace authentication.Services.Implementations;

public class ImageUrlResolver(IHttpContextAccessor _httpContextAccessor) : IValueResolver<Product, ProductDto, string?>
{
    

    public string? Resolve(Product source, ProductDto destination, string? destMember, ResolutionContext context)
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