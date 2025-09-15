using cleanArchitecture.Domain.Responses;
using MediatR;

namespace cleanArchitecture.Application.Abstractions.Messaging;
public interface ICommand<TResponse> : IRequest<Response<TResponse>>;
