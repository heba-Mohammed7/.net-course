
using cleanArchitecture.Application.Abstractions.Messaging;
namespace cleanArchitecture.Application.Features.Categories.Commands.Delete;
public record DeleteCategoryCommand(Guid Id) : ICommand<Guid>;
