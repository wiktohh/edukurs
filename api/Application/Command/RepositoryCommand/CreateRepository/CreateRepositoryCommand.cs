using MediatR;

namespace Application.Command.RepositoryCommand.CreateRepository;

public class CreateRepositoryCommand : IRequest
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid OwnerId { get; set; }
}