using FluentValidation;

namespace cleanArchitecture.Application.Features.Categories.Queries.GetById;

public class GetCategoryByIdValidator : AbstractValidator<GetCategoryByIdQuery>
{
    public GetCategoryByIdValidator()
    {
         RuleFor(x => x.Id).NotEmpty();
    }
}