using Domain.ValueObjects.Repository;
using Domain.ValueObjects.Ticket;
using Domain.ValueObjects.User;

namespace Domain.Entities;

public class Ticket
{
    public TicketId Id { get; set; }
    public UserId UserId { get; set; }
    public User User { get; set; }
    public RepositoryId RepositoryId { get; set; }
    public Repository Repository { get; set; }
    public Status Status { get; set; }
}