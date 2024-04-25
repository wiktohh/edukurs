using Domain.ValueObjects.RepTask;
using Domain.ValueObjects.User;

namespace Domain.Entities;

public class SubmittedTask
{
    public Guid Id { get; set; }
    public UserId UserId { get; set; }
    public virtual User User { get; set; }
    public RepTaskId RepTaskId { get; set; }
    public virtual RepTask RepTask { get; set; }
    public string Status { get; set; }
}