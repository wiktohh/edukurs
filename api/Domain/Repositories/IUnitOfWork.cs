namespace Domain.Repositories;

public interface IUnitOfWork
{
    Task<bool>  SaveChangesAsync(CancellationToken cancellationToken = default);
}