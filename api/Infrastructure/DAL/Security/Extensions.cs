using Application.Security;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.DAL.Security;

public static class Extensions
{
    public static IServiceCollection AddSecurity(this IServiceCollection services)
    {
        services
            .AddSingleton<IPasswordHasher<User>, PasswordHasher<User>>()
            .AddSingleton<IPasswordManager, PasswordManager>();

        return services;
    }
}