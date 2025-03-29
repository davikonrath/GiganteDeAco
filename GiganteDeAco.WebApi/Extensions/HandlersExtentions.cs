using GiganteDeAco.Application.Handlers;
using GiganteDeAco.Domain.Handlers;

namespace GiganteDeAco.WebApi.Extensions;

public static class HandlersExtensions
{
    public static IServiceCollection AddHandlers(this IServiceCollection services)
    {
        services.AddTransient<IRoboHandler, RoboHandler>();
        services.AddTransient<ICabecaHandler, CabecaHandler>();
        services.AddTransient<ICotoveloHandler, CotoveloHandler>();
        services.AddTransient<IPulsoHandler, PulsoHandler>();

        return services;
    }
}