namespace Domain.Entities;

public class RepTask
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public Guid RepositoryId { get; set; }
    public virtual Repository Repository { get; set; }
    public DateTime Deadline { get; set; }
    public virtual ICollection<SubmittedTask> SubmittedTasks { get; set; }
}