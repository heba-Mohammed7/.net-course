using cleanArchitecture.Application.Abstractions.Messaging;

namespace cleanArchitecture.Application.Features.Categories.Commands.Add;

public record AddCategoryCommand(string Name) : ICommand<Guid>;