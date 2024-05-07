using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Command.Files.UpdateFile;

public class UploadFileCommand : IRequest
{
    public IFormFile File { get; set; }
    public Guid TaskId { get; set; }
    public Guid UserId { get; set; }
}    
