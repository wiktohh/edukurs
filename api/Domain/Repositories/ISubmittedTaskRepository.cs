using Domain.Entities;
using Domain.ValueObjects.SubmittedTask;

namespace Domain.Repositories;

public interface ISubmittedTaskRepository
{
    public Task<SubmittedTask?> GetSubmittedTaskAsync(ReportId id);
    public Task<IEnumerable<SubmittedTask>> GetSubmittedTasksAsync();
    public IQueryable<SubmittedTask> GetSubmittedTasksQueryable();
    public Task AddSubmittedTaskAsync(SubmittedTask submittedTask);
    public void UpdateSubmittedTask(SubmittedTask submittedTask);
    public void DeleteSubmittedTask(SubmittedTask submittedTask);
    public Task<bool> SubmittedTaskExistsAsync(ReportId id);
    
}