using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.DAL.Configuration;

public class TasksConfiguration : IEntityTypeConfiguration<SubmittedTask>
{
    public void Configure(EntityTypeBuilder<SubmittedTask> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .IsRequired();
        builder.Property(x => x.RepTaskId)
            .IsRequired();
        builder.Property(x => x.Status)
            .IsRequired();
        builder.HasOne(x => x.RepTask)
            .WithMany(x=>x.SubmittedTasks)
            .HasForeignKey(x => x.RepTaskId);
        builder.HasOne(x => x.User)
            .WithMany(x=>x.SubmittedTasks)
            .HasForeignKey(x => x.UserId);
    }
}