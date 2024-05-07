using Domain.ValueObjects.RepTask;
using Domain.ValueObjects.SubmittedTask;
using Domain.ValueObjects.User;
using Title = Domain.ValueObjects.RepTask.Title;

namespace Domain.Entities;

public class SubmittedTask
{
    public ReportId Id { get; set; }
    public string Path { get; set; }
    public UserId UserId { get; set; }
    public virtual User User { get; set; }
    public RepTaskId RepTaskId { get; set; }
    public virtual RepTask RepTask { get; set; }
}