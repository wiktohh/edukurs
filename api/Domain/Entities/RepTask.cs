using Domain.ValueObjects.Repository;
using Domain.ValueObjects.RepTask;

namespace Domain.Entities;

public class RepTask
{
    public RepTaskId Id { get; set; }
    public Title Title { get; set; }
    public Description Description { get; set; }
    public RepositoryId RepositoryId { get; set; }
    public virtual Repository Repository { get; set; }
    public Deadline Deadline { get; set; }
    public virtual ICollection<SubmittedTask> SubmittedTasks { get; set; }
}

//id,title description repositoryId deadline