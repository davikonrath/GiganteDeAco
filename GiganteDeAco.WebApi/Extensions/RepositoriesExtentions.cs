using GiganteDeAco.Application.Handlers;
using GiganteDeAco.Domain.Handlers;
using GiganteDeAco.Domain.Repositories;
using GiganteDeAco.Infra.Repositories;

namespace GiganteDeAco.WebApi.Extensions;

public static class RepositoriesExtensions
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddTransient<IRoboRepository, RoboRepository>();

        return services;
    }
}