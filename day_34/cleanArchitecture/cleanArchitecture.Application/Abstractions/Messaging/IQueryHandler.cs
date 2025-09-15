using cleanArchitecture.Domain.Responses;
using MediatR;

namespace cleanArchitecture.Application.Abstractions.Messaging;

public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, Response<TResponse>>
    where TQuery : IQuery<TResponse>;
