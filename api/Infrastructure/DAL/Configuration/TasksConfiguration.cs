using Domain.Entities;
using Domain.ValueObjects.RepTask;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.DAL.Configuration;

public class TasksConfiguration : IEntityTypeConfiguration<RepTask>
{
    public void Configure(EntityTypeBuilder<RepTask> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .HasConversion(x => x.Value, x => new RepTaskId(x))
            .IsRequired();
        builder.Property(x => x.Description)
            .HasConversion(x =>x.Value, x => new Description(x))
            .IsRequired();
        builder.Property(x => x.Title)
            .HasConversion(x => x.Value, x => new Title(x))
            .IsRequired();
        builder.Property(x => x.Deadline)
            .HasConversion(x => x.Value, x => new Deadline(x))
            .IsRequired();
        builder.HasOne(x => x.Repository)
            .WithMany(x => x.RepTasks)
            .HasForeignKey(x => x.RepositoryId);
        builder.HasMany(x => x.SubmittedTasks)
            .WithOne(x => x.RepTask)
            .HasForeignKey(x => x.RepTaskId);
    }
}