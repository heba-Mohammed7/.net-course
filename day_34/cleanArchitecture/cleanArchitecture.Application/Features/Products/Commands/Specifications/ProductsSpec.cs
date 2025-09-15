using Ardalis.Specification;
using cleanArchitecture.Domain.Models.Products;

namespace cleanArchitecture.Application.Features.Products.Specifications;

public class ProductsSpec : Specification<Product>
{
    public ProductsSpec(string? name, decimal? minPrice, decimal? maxPrice, int pageSize, int pageNumber)
    {
        if (name != null)
            Query.Where(x => x.Name.Contains(name));
            
        if (minPrice.HasValue)
            Query.Where(x => x.Price >= minPrice.Value);
            
        if (maxPrice.HasValue)
            Query.Where(x => x.Price <= maxPrice.Value);
            
        Query.Skip(pageSize * (pageNumber - 1));
        Query.Take(pageSize);
    }
}