namespace Application.DTO;

public record TicketDto(Guid Id,Guid RepositoryId, Guid UserId,string FirstName,string LastName,string Email,string Status);