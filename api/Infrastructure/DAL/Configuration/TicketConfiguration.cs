using Domain.Entities;
using Domain.ValueObjects.Ticket;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.DAL.Configuration;

public class TicketConfiguration : IEntityTypeConfiguration<Ticket>
{
    public void Configure(EntityTypeBuilder<Ticket> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .HasConversion(x => x.Value, x => new TicketId(x));
        builder.Property(x => x.UserId)
            .IsRequired();
        builder.Property(x => x.RepositoryId)
            .IsRequired();
        builder.Property(x => x.Status)
            .HasConversion(x => x.Value, x => new Status(x))
            .IsRequired();
        builder.HasOne(x => x.User)
            .WithMany(x => x.Tickets)
            .HasForeignKey(x => x.UserId);
        builder.HasOne(x => x.Repository)
            .WithMany(x => x.Tickets)
            .HasForeignKey(x => x.RepositoryId);
    }
}