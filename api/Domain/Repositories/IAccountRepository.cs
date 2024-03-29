﻿using Domain.Entities;
using Domain.ValueObjects.User;

namespace Domain.Repositories;

public interface IAccountRepository
{
        Task<ICollection<User>> GetAllUsersAsync();
        Task<User> GetByIdAsync(UserId id);
        Task<User> GetByEmailAsync(Email email);
        Task AddAsync(User user);
}