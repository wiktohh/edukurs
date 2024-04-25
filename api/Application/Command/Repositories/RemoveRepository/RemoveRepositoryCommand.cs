using MediatR;

namespace Application.Command.Repositories.RemoveRepository;

public class RemoveRepositoryCommand : IRequest
{
    public Guid Id { get; set; }
    public Guid SenderId { get; set; }
}