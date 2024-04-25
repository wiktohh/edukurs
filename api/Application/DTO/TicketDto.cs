namespace Application.DTO;

public record TicketDto(Guid Id,Guid RepositoryId, Guid UserId,string Status);