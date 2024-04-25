using Domain.Entities;
using Domain.ValueObjects.RepTask;

namespace Domain.Repositories;

public interface ITaskRepository
{
    public Task<RepTask?> GetRepTaskAsync(RepTaskId id);
    public Task<IEnumerable<RepTask>> GetRepTasksAsync();
    public IQueryable<RepTask> GetRepTasksQueryable();
    public Task AddRepTaskAsync(RepTask repTask);
    public void UpdateRepTask(RepTask repTask);
    public void DeleteRepTask(RepTask repTask);
    public Task<bool> RepTaskExistsAsync(RepTaskId id);
}