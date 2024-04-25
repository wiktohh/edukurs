using Domain.Repositories;
using Infrastructure.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.DAL;

public static class Extensions
{
    private static string OptionsSectionName = "Postgres";
    public static IServiceCollection AddPostgres(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IAccountRepository, AccountRepository>();
        services.AddScoped<IRepRepository, RepRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<ITicketRepository, TicketRepository>();
        services.AddScoped<ISubmittedTaskRepository, SubmittedTaskRepository>();
        services.AddScoped<ITaskRepository, TaskRepository>();
        services.Configure<PostgresOptions>(configuration.GetRequiredSection(OptionsSectionName));
        var postgresOptions = configuration.GetConfiguration<PostgresOptions>(OptionsSectionName);
        services.AddDbContext<DataContext>(options =>
        {
            options.UseNpgsql(postgresOptions.ConnectionString);
        });
        return services;
    }
}
