using cleanArchitecture.Domain.Responses;
using MediatR;

namespace cleanArchitecture.Application.Abstractions.Messaging;
public interface IQuery<TResponse> : IRequest<Response<TResponse>>;
