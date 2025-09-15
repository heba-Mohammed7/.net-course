
using cleanArchitecture.Application.Abstractions.Repositories;
using cleanArchitecture.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using cleanArchitecture.Infrastructure.Repositories;

namespace cleanArchitecture.Infrastructure.Dependencies;

public static class RepositoryDependency
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped(typeof(IReadRepository<>), typeof(ReadRepository<>));

        return services;
    }
}
