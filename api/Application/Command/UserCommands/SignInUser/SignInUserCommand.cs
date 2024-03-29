using MediatR;

namespace Application.Command.UserCommands.SignInUser;

public class SignInUserCommand : IRequest
{
    public string Email { get; set; }
    public string Password { get; set; }
}