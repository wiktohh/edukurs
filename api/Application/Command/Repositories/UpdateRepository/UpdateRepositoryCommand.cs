using MediatR;

namespace Application.Command.Repositories.UpdateRepository;

public class UpdateRepositoryCommand : IRequest
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid SenderId { get; set; }
}