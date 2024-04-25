namespace Application.DTO.Request;

public record AddTaskRequest(string Title, string Description, DateTime Deadline);
