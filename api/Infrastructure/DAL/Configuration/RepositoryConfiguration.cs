using Domain.Entities;
using Domain.ValueObjects.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.DAL.Configuration;

public class RepositoryConfiguration : IEntityTypeConfiguration<Repository>
{
    public void Configure(EntityTypeBuilder<Repository> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x=>x.Id)
            .HasConversion(x=>x.Value,x=>new RepositoryId(x));
        builder.Property(x => x.Name)
            .HasConversion(x => x.Value, x => new Name(x))
            .HasMaxLength(60)
            .IsRequired();
        builder.Property(x => x.OwnerId)
            .IsRequired();

    }
}