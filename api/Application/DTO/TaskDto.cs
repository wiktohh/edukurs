namespace Application.DTO;


//pozniej dodac submity do taskow
//id,title description repositoryId deadline
public record TaskDto(Guid Id,string Title, string Description, Guid RepositoryId, DateTime Deadline);
