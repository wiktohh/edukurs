namespace Domain.Entities;

public class User
{
    public Guid Id { get; private set; }
    public string FirstName { get; private set; }
    public string LastName  { get; private set; }
    public string Password { get; private set; }
    public string Role { get; private set; }
}