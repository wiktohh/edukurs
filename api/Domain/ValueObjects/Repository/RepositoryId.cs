using Domain.Exceptions;

namespace Domain.ValueObjects.Repository;

public record RepositoryId
{
    public Guid Value { get; }
    
    public RepositoryId(Guid value)
    {
        if (value == Guid.Empty)
        {
            throw new InvalidRepositoryIdException();
        }
        Value = value;
    }
    
    public static implicit operator RepositoryId(Guid value) => new(value);
    public static implicit operator Guid(RepositoryId value) => value.Value;
    public override string ToString() => Value.ToString();
}