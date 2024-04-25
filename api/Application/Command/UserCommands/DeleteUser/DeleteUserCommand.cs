using MediatR;

namespace Application.Command.UserCommands.DeleteUser;

public class DeleteUserCommand : IRequest
{
    public Guid Id { get; set; }
}