using Domain.ValueObjects.Repository;

namespace Domain.Entities;

public class Repository
{
    public RepositoryId Id { get; set; }
    public Name Name { get; set; }
    public ICollection<UserRepository> Users { get; set; } = new List<UserRepository>();
    public Guid OwnerId { get; set; } 
    
    public Repository(RepositoryId id, Name name, Guid ownerId)
    {
        Id = id;
        Name = name;
        OwnerId = ownerId;
    }
}