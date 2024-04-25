using MediatR;

namespace Application.Command.Repositories.CreateRepository;

public class CreateRepositoryCommand : IRequest
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid OwnerId { get; set; }
}