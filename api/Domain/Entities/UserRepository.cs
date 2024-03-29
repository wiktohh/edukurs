using Domain.ValueObjects.Repository;
using Domain.ValueObjects.User;

namespace Domain.Entities;

public class UserRepository
{
    public RepositoryId RepositoryId { get; set; }
    public Repository Repository { get; set; }  
    public UserId UserId { get; set; }
    public User User { get; set; }
    public bool IsOwner { get; set; }
    
    public UserRepository(RepositoryId repositoryId, UserId userId, bool isOwner)
    {
        RepositoryId = repositoryId;
        UserId = userId;
        IsOwner = isOwner;
    }
}