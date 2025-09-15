using Ardalis.Specification;
using cleanArchitecture.Domain.Models.Categories;

namespace cleanArchitecture.Application.Features.Categories.Specifications;

public class CategoriesSpec : Specification<Category>
{
    public CategoriesSpec(string? name, int pageSize, int pageNumber)
    {
        if (name != null)
            Query.Where(x=>x.Name.Contains(name));
        Query.Skip(pageSize * (pageNumber - 1));
        Query.Take(pageSize);
    }
}