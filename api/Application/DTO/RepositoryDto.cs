using Domain.Entities;

namespace Application.DTO;

public record RepositoryDto(Guid Id, string Name, Guid OwnerId, ICollection<UserDto> Users);