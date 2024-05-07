using Domain.Entities;
using Domain.ValueObjects.RepTask;
using Domain.ValueObjects.SubmittedTask;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.DAL.Configuration;

public class ReportsConfiguration : IEntityTypeConfiguration<SubmittedTask>
{
    public void Configure(EntityTypeBuilder<SubmittedTask> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .HasConversion(x => x.Value, x => new ReportId(x))
            .IsRequired();
        builder.Property(x => x.Path)
            .IsRequired();
        builder.Property(x => x.RepTaskId)
            .IsRequired();
        builder.HasOne(x => x.RepTask)
            .WithMany(x=>x.SubmittedTasks)
            .HasForeignKey(x => x.RepTaskId);
        builder.HasOne(x => x.User)
            .WithMany(x=>x.SubmittedTasks)
            .HasForeignKey(x => x.UserId);
    }
}