namespace Application.DTO.Request;

public record CreateRepositoryRequest(string Name);
public record GetRepositoriesRequest(RepoEnum RepoEnum);