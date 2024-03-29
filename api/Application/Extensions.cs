using System.Reflection.Metadata;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class Extensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg=>cfg.RegisterServicesFromAssemblies(AssemblyReference.Assembly));
        return services;
    }
}