using Application.DTO;

namespace Application.Security;

public interface IAuthenticator
{
    JwtDto CreateToken(Guid userId,string role);
}