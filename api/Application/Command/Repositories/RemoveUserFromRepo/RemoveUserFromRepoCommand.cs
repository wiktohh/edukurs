using MediatR;

namespace Application.Command.Repositories.RemoveUserFromRepo;

public class RemoveUserFromRepoCommand : IRequest
{
    public Guid UserId { get; set; }   
    public Guid RepositoryId { get; set; }
    public Guid SenderId { get; set; }
}