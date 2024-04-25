using Domain.Repositories;

namespace Infrastructure.DAL.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly DataContext _dbContext;

    public UnitOfWork(DataContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<bool> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var i = await _dbContext.SaveChangesAsync();
        return i > 0;
    }
}