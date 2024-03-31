using Domain.Entities;
using Domain.ValueObjects.Repository;

namespace Domain.Repositories;

public interface IRepRepository
{
    IQueryable<Repository> GetAllRepositoriesAsync();
    Task<Repository> GetRepositoryByIdAsync(RepositoryId Id);
    Task AddRepositoryAsync(Repository repository);
    Task AddUserToRepositoryAsync(UserRepository userRepository);
}