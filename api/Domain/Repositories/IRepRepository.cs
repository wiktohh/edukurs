using Domain.Entities;
using Domain.ValueObjects.Repository;

namespace Domain.Repositories;

public interface IRepRepository
{
    IQueryable<Repository> GetAllRepositoriesAsync();
    IQueryable<UserRepository> GetAllUsersRepositoriesAsync();
    Task<Repository> GetRepositoryByIdAsync(RepositoryId Id);
    Task AddRepositoryAsync(Repository repository);
    Task AddUserToRepositoryAsync(UserRepository userRepository);
    void UpdateRepository(Repository repository);
    void RemoveRepository(Repository repository);
}