using Domain.Entities;

namespace Application.DTO;

public static class Extensions
{
    public static UserDto AsDto(this User user)
    {
        return new UserDto(user.FirstName, user.LastName, user.Email, user.Role);
    }
    
    public static RepositoryDto AsDto(this Repository repository)
    {
        var users = repository.Users.Select(user => user.User.AsDto()).ToList();
        return new RepositoryDto(repository.Id, repository.Name, repository.OwnerId,users);
    }
    
    public static TicketDto AsDto(this Ticket ticket)
    {
        return new TicketDto(ticket.RepositoryId, ticket.UserId,ticket.Status);
    }
}