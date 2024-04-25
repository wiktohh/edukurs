using Domain.Entities;
using Domain.Repositories;
using Domain.ValueObjects.Repository;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DAL.Repositories;

public class RepRepository : IRepRepository
{
    private readonly DataContext _context;

    public RepRepository(DataContext context)
    {
        _context = context;
    }
    
    public IQueryable<Repository> GetAllRepositoriesAsync() => _context.Repositories.Include(x=>x.Users).ThenInclude(x=>x.User).AsQueryable();
    public IQueryable<UserRepository> GetAllUsersRepositoriesAsync()
    {
        return _context.UserRepository;
    }

    public async Task<Repository> GetRepositoryByIdAsync(RepositoryId Id)=> await _context.Repositories.Include(x=>x.Users).ThenInclude(x=>x.User).SingleOrDefaultAsync(x => x.Id == Id);

    public async Task AddRepositoryAsync(Repository repository) => await _context.Repositories.AddAsync(repository);
    public async Task AddUserToRepositoryAsync(UserRepository userRepository)
    {
        await _context.UserRepository.AddAsync(userRepository);
    }

    public void UpdateRepository(Repository repository)
    {
        _context.Repositories.Update(repository);
    }

    public void RemoveRepository(Repository repository)
    {
        _context.Repositories.Remove(repository);
    }
}