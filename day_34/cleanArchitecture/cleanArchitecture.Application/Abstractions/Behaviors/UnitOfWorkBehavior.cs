using System.Transactions;
using MediatR;
using cleanArchitecture.Application.Abstractions.Messaging;

namespace cleanArchitecture.Application.Abstractions.Behaviors;

public class UnitOfWorkBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (!typeof(TRequest).IsAssignableTo(typeof(IBaseCommand)))
        {
            return await next();
        }

        using var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
        TResponse response = await next();
        transactionScope.Complete();
        return response;
    }
}
