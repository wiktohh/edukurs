using Domain.Entities;
using Domain.ValueObjects.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.DAL.Configuration;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .HasConversion(x => x.Value, x => new UserId(x));
        builder.Property(x=>x.FirstName)
            .HasConversion(x=>x.Value,x=>new FirstName(x))
            .HasMaxLength(60)
            .IsRequired();
        builder.Property(x=>x.LastName).HasConversion(x=>x.Value,x=>new LastName(x))
            .HasMaxLength(60)
            .IsRequired();
        builder.Property(x=>x.Password).HasConversion(x=>x.Value,x=>new Password(x))
            .HasMaxLength(250)
            .IsRequired();
        builder.Property(x=>x.Role).HasConversion(x=>x.Value,x=>new Role(x))
            .HasConversion(x => x.Value, x => new Role(x))
            .IsRequired()
            .HasMaxLength(30);
        builder.Property(x => x.Email).HasConversion(x => x.Value, x => new Email(x))
            .IsRequired()
            .HasMaxLength(50);
    }
}