namespace Application.DTO.Request;

public record SendTicketRequest(Guid RepositoryId);
public record SendTicketResponse(string Status);