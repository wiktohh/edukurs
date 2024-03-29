using Infrastructure.Auth;
using Infrastructure.DAL;
using Infrastructure.DAL.Security;
using Infrastructure.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class Extensions
{
    
    public static WebApplication UseInfrastructure(this WebApplication app)
    {
        app.UseMiddleware<ExceptionMiddleware>();
        return app;
    }
    
    public static IServiceCollection AddInfrastructure(this IServiceCollection service, IConfiguration configuration)
    {
        service.AddPostgres(configuration);
        service.AddSecurity();
        service.AddAuth(configuration);
        service.AddTransient<ExceptionMiddleware>();
        service.AddHttpContextAccessor();
        return service;
    }
    
    public static T GetConfiguration<T>(this IConfiguration configuration, string section) where T : new()
    {
        var config = new T();
        configuration.GetSection(section).Bind(config);
        return config;
    }
}