using System.Text;
using Application.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Auth;

public static class Extensions
{
    private const string SectionName = "auth";

    public static IServiceCollection AddAuth(this IServiceCollection services, IConfiguration configuration)
    {
        var options = configuration.GetConfiguration<AuthOptions>(SectionName);
        services.Configure<AuthOptions>(configuration.GetRequiredSection(SectionName))
            .AddSingleton<IAuthenticator,Authenticator>()
            .AddSingleton<ITokenStorage,HttpContextTokenStorage>()
            .AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(opt =>
            {
                opt.Audience = options.Audience;
                opt.IncludeErrorDetails = true;
                opt.TokenValidationParameters = new()
                {
                    ValidIssuer = options.Issuer,
                    ClockSkew = TimeSpan.Zero,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.SigningKey))
                };
            });
        return services;
    }
}