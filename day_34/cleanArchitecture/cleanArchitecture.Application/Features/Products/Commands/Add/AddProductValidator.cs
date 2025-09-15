using cleanArchitecture.Application.Abstractions.Repositories;
using cleanArchitecture.Domain.Models;
using cleanArchitecture.Domain.Models.Categories;
using cleanArchitecture.Domain.Models.Products;
using FluentValidation;

namespace cleanArchitecture.Application.Features.Products.Commands.Add;

public class AddProductValidator : AbstractValidator<AddProductCommand>
{
    private readonly IReadRepository<Category> _categoryRepository;
    public AddProductValidator(IReadRepository<Category> categoryRepository)
    {
        _categoryRepository = categoryRepository;
        RuleFor(p => p.Name)
            .NotEmpty().WithMessage("Products name is required.")
            .MaximumLength(ProductConstants.ProductNameMaxLengthValue).WithMessage(ProductConstants.ProductNameMaxLengthMessage);

        RuleFor(p => p.CategoryId)
            .NotEmpty()
            .WithMessage("Categories ID is required.");

    }
}