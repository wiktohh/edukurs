using Application.DTO;
using MediatR;

namespace Application.Query.UserQueries.GetUser;

public class GetUserByIdQuery : IRequest<UserDto>
{
   public Guid Id { get; set; }
}