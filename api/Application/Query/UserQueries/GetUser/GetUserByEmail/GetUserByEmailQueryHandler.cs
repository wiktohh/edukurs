using Application.DTO;
using Application.Exceptions;
using Domain.Repositories;
using MediatR;

namespace Application.Query.UserQueries.GetUser.GetUserByEmail;

internal class GetUserByEmailQueryHandler : IRequestHandler<GetUserByEmailQuery,UserDto>
{
    private readonly IAccountRepository _accountRepository;

    public GetUserByEmailQueryHandler(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }
    
    public async Task<UserDto> Handle(GetUserByEmailQuery request, CancellationToken cancellationToken)
    {
        var user = await _accountRepository.GetByEmailAsync(request.Email);
        if (user == null)
        {
            throw new NotFoundException("User with this email does not exist");
        }

        return user.AsDto();
    }
}