using cleanArchitecture.Application.Abstractions.Messaging;

namespace cleanArchitecture.Application.Features.Categories.Commands.Update;

public record UpdateCategoryCommand(Guid Id, string Name) : ICommand<string>;
