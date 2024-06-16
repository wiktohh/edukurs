using Domain.Entities;
using Domain.ValueObjects.User;

namespace Domain.Repositories;

public interface IAccountRepository
{
        Task<ICollection<User>> GetAllUsersAsync();
        Task<User> GetByIdAsync(UserId id);
        Task<User> GetByEmailAsync(Email email);
        Task<bool> isEmailUniqueAsync(Email email);
        Task AddAsync(User user);
        void UpdateAsync(User user);
        void DeleteAsync(User user);
        void RemoveUserFromRepository(UserRepository userRepository);
}