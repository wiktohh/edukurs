using Domain.Entities;
using Domain.Repositories;
using Domain.ValueObjects.SubmittedTask;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DAL.Repositories;

public class SubmittedTaskRepository : ISubmittedTaskRepository
{
    private readonly DataContext _context;

    public SubmittedTaskRepository(DataContext context)
    {
        _context = context;
    }

    public async Task<SubmittedTask?> GetSubmittedTaskAsync(ReportId id) => await _context.SubmittedReports.Include(x=>x.RepTask).SingleOrDefaultAsync(x=>x.Id == id);

    public async Task<IEnumerable<SubmittedTask>> GetSubmittedTasksAsync() => await _context.SubmittedReports.ToListAsync();
    public IQueryable<SubmittedTask> GetSubmittedTasksQueryable() => _context.SubmittedReports.AsQueryable();
    public async Task AddSubmittedTaskAsync(SubmittedTask submittedTask) => await _context.SubmittedReports.AddAsync(submittedTask);
    public void UpdateSubmittedTask(SubmittedTask submittedTask) => _context.SubmittedReports.Update(submittedTask);
    public void DeleteSubmittedTask(SubmittedTask submittedTask) => _context.SubmittedReports.Remove(submittedTask);
    public async Task<bool> SubmittedTaskExistsAsync(ReportId id) => await _context.SubmittedReports.AnyAsync(x => x.Id == id);
}