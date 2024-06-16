using Domain.ValueObjects.User;

namespace Domain.Entities;

public class User
{
    public UserId Id { get; private set; }
    public Email Email { get; private set; }
    public FirstName FirstName { get; private set; }
    public LastName LastName  { get; private set; }
    public Password Password { get; private set; }
    public Role Role { get; private set; }
    public ICollection<UserRepository> Repositories { get; set; } = new List<UserRepository>();
    public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
    public virtual ICollection<SubmittedTask> SubmittedTasks { get; set; } = new List<SubmittedTask>();

    public User(UserId id, Email email, FirstName firstName, LastName lastName, Password password, Role role)
    {
        Id = id;
        Email = email;
        FirstName = firstName;
        LastName = lastName;
        Password = password;
        Role = role;
    }

    public void UpdateRole(Role role)
    {
        Role = role;
    }
}