using MediatR;

namespace Application.Command.UserCommands.UpdateUser;

public class UpdateUserCommand : IRequest
{
    public Guid Id { get; set; }
    public string Role { get; set; }
}