using cleanArchitecture.Application.Abstractions.Repositories;
using cleanArchitecture.Domain.Models.Categories;
using cleanArchitecture.Domain.Models.Products;
using FluentValidation;

namespace cleanArchitecture.Application.Features.Products.Commands.Update;

public class UpdateProductValidator : AbstractValidator<UpdateProductCommand>
{
    private readonly IReadRepository<Category> _categoryRepository;

    public UpdateProductValidator(IReadRepository<Category> categoryRepository)
    {
        _categoryRepository = categoryRepository;

        RuleFor(p => p.Id)
            .NotEmpty().WithMessage("Product ID is required.");

        RuleFor(p => p.Name)
            .NotEmpty().WithMessage("Product name is required.")
            .MaximumLength(ProductConstants.ProductNameMaxLengthValue).WithMessage(ProductConstants.ProductNameMaxLengthMessage);

        RuleFor(p => p.CategoryId)
            .NotEmpty()
            .WithMessage("Category ID is required.");

        RuleFor(p => p.Price)
            .GreaterThan(0).WithMessage("Price must be greater than 0.");
    }
}
