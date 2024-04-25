using Application.DTO;
using Application.Exceptions;
using Domain.Repositories;
using MediatR;

namespace Application.Query.UserQueries.GetUser.GetUserById;

public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery,UserDto>
{
    private readonly IAccountRepository _accountRepository;

    public GetUserByIdQueryHandler(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }
    
    public async Task<UserDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await _accountRepository.GetByIdAsync(request.Id);
        if (user == null)
        {
            throw new NotFoundException("User with this id does not exist");
        }

        return user.AsDto();
    }
}