using Application.DTO;
using Domain.Repositories;
using MediatR;

namespace Application.Query.UserQueries.GetUser.GetAllUsers;

internal class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery,ICollection<UserDto>>
{
    private readonly IAccountRepository _repository;

    public GetAllUsersQueryHandler(IAccountRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<ICollection<UserDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        var users = await _repository.GetAllUsersAsync();
        return users.Select(x => x.AsDto()).ToList();
    }
}