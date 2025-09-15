
using cleanArchitecture.Application.Abstractions.Messaging;
using cleanArchitecture.Domain.Responses;
using MediatR;

namespace cleanArchitecture.Application.Abstractions.Messaging;
public interface ICommandHandler<in TCommand, TResponse>
    : IRequestHandler<TCommand, Response<TResponse>>
    where TCommand : ICommand<TResponse>;
