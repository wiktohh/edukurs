using Domain.Entities;
using Domain.Repositories;
using Domain.ValueObjects.User;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DAL.Repositories;

public class AccountRepository : IAccountRepository
{
    private readonly DataContext _dbContext;

    public AccountRepository(DataContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<ICollection<User>> GetAllUsersAsync()
    {
        return await _dbContext.Users.ToListAsync();
    }

    public async Task<User> GetByIdAsync(UserId id)
    {
        return await _dbContext.Users.SingleOrDefaultAsync(x => x.Id == id);
    }

    public async Task<User> GetByEmailAsync(Email email)
    {
        return await _dbContext.Users.SingleOrDefaultAsync(x => x.Email == email);
    }

    public async Task AddAsync(User user)
    {
        await _dbContext.Users.AddAsync(user);
    }
}