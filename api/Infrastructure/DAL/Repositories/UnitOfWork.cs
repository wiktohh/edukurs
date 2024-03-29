using Domain.Repositories;

namespace Infrastructure.DAL.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly DataContext _dbContext;

    public UnitOfWork(DataContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _dbContext.SaveChangesAsync();
    }
}