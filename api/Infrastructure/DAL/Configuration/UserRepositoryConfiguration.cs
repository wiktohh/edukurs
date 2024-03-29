using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.DAL.Configuration;

public class UserRepositoryConfiguration : IEntityTypeConfiguration<UserRepository>
{
    public void Configure(EntityTypeBuilder<UserRepository> builder)
    {
        builder.HasKey(x => new {x.UserId, x.RepositoryId});
        builder.HasOne(x => x.User)
            .WithMany(x => x.Repositories)
            .HasForeignKey(x => x.UserId);
        builder.HasOne(x => x.Repository)
            .WithMany(x => x.Users)
            .HasForeignKey(x => x.RepositoryId);
    }
}