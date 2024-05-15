using Application.Security;
using Domain.Entities;
using Infrastructure.DAL.Security;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DAL;

public class DataContext : DbContext
{
    private readonly IPasswordHasher<User> _passwordHasher;
    public DataContext(DbContextOptions<DataContext> options,IPasswordHasher<User> passwordHasher) : base(options)
    {
        _passwordHasher = passwordHasher;
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Repository> Repositories { get; set; }
    public DbSet<UserRepository> UserRepository { get; set; }
    public DbSet<Ticket> Tickets { get; set; }
    public DbSet<SubmittedTask> SubmittedReports { get; set; }
    public DbSet<RepTask> Tasks { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasData(new User(Guid.NewGuid(),"admin@gmail.com","admin","admin",_passwordHasher.HashPassword(null,"admin"),"Admin"));
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DataContext).Assembly);
    }
}