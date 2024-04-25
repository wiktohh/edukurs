using Domain.Entities;

namespace Domain.Repositories;

public interface ISubmittedTaskRepository
{
    public Task<SubmittedTask?> GetSubmittedTaskAsync(Guid id);
    public Task<IEnumerable<SubmittedTask>> GetSubmittedTasksAsync();
    public IQueryable<SubmittedTask> GetSubmittedTasksQueryable();
    public Task AddSubmittedTaskAsync(SubmittedTask submittedTask);
    public void UpdateSubmittedTask(SubmittedTask submittedTask);
    public void DeleteSubmittedTask(SubmittedTask submittedTask);
    public Task<bool> SubmittedTaskExistsAsync(Guid id);
    
}