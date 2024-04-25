using Domain.Entities;
using Domain.Repositories;
using Domain.ValueObjects.RepTask;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DAL.Repositories;

public class TaskRepository : ITaskRepository
{
    private readonly DataContext _context;

    public TaskRepository(DataContext context)
    {
        _context = context;
    }
    
    public async Task<RepTask?> GetRepTaskAsync(RepTaskId id) => await _context.Tasks.SingleOrDefaultAsync(x => x.Id == id);
    public async Task<IEnumerable<RepTask>> GetRepTasksAsync() => await _context.Tasks.ToListAsync();
    public IQueryable<RepTask> GetRepTasksQueryable()  => _context.Tasks.AsQueryable();
    public async Task AddRepTaskAsync(RepTask repTask) => await _context.Tasks.AddAsync(repTask);
    public void UpdateRepTask(RepTask repTask) => _context.Tasks.Update(repTask);
    public void DeleteRepTask(RepTask repTask) => _context.Tasks.Remove(repTask);
    public async Task<bool> RepTaskExistsAsync(RepTaskId id) => await _context.Tasks.AnyAsync(x => x.Id == id);
}