using Application.Command.Files.UpdateFile;
using Application.Query.FilesQueries.DownloadFilesFromTask;
using Application.Query.FilesQueries.DownloadSingleReport;
using Application.Query.FilesQueries.GetAllFilePaths;
using Application.Query.FilesQueries.GetAllFilesFromRepository;
using Application.Query.FilesQueries.GetAllFilesFromTask;
using Domain.ValueObjects.Ticket;
using Domain.ValueObjects.User;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

namespace WebApi.Controllers;


[ApiController]
[Route("api/[controller]")]
public class FileController : ControllerBase    
{
    private readonly IMediator _mediator;

    public FileController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet("all")]
    [Authorize(Roles = "Teacher")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllFilePaths()
    {
        var query = new GetAllFilePathsQuery();
        var paths = await _mediator.Send(query);
        return Ok(paths);
    }
    
    
    
    
    [HttpPost("upload/{taskid}")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UploadFile(IFormFile file,[FromRoute]Guid taskid,CancellationToken cancellationToken)
    {
        if(User.Identity?.Name is null)
        {
            return NotFound();
        }
        
        var guid = Guid.Parse(User.Identity?.Name);
        await _mediator.Send(new UploadFileCommand() { File = file, UserId = guid,TaskId = taskid}, cancellationToken);
        return Ok();
    }
    
    [HttpGet("repository/{repId}")]
    [Authorize(Roles = "Teacher")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetFilesFromRepo(Guid repId)
    {
        if(User.Identity?.Name is null)
        {
            return NotFound();
        }
        
        var guid = Guid.Parse(User.Identity?.Name);
        
        var query = new GetAllFilesFromRepositoryQuery() {RepositoryId = repId, UserId = guid};
        var files = await _mediator.Send(query);    
        return Ok(files);
    }

    [HttpGet("task/{taskId}")]
    [Authorize(Roles = "Teacher")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetFilesFromTask(Guid taskId)
    {
        if(User.Identity?.Name is null)
        {
            return NotFound();
        }
        
        var guid = Guid.Parse(User.Identity?.Name);
        
        var query = new GetAllFilesFromTaskQuery() {RepTaskId = taskId, UserId = guid};
        var files = await _mediator.Send(query);    
        return Ok(files);
    }

    
    [HttpGet]
    [Route("download")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DownloadFile(string filename)
    {
      var filepath =  Path.Combine(Directory.GetCurrentDirectory(), "Upload\\files", filename);   
      
      var provider = new FileExtensionContentTypeProvider();
      if (!provider.TryGetContentType(filename, out var contentType))
      {
          contentType = "application/octet-stream";
      }
      var bytes = await System.IO.File.ReadAllBytesAsync(filepath);
      return File(bytes, contentType, Path.GetFileName(filepath));
    }
    
    [HttpGet]
    [Route("download/{ReportId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DownloadFrom(Guid ReportId)
    {   
        if(User.Identity?.Name is null)
        {
            return NotFound();
        }
        
        var guid = Guid.Parse(User.Identity?.Name);
        var query = new DownloadSingleReportQuery(){ReportId = ReportId,UserId = guid };
        var response = await _mediator.Send(query);
        var contentType = "application/octet-stream";
        return File(response.MemoryStream, "application/zip", response.Directory);
    }
    
    [HttpGet]
    [Route("download/task/{taskId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DownloadFromTask(Guid taskId)
    {
        if(User.Identity?.Name is null)
        {
            return NotFound();
        }
        
        var guid = Guid.Parse(User.Identity?.Name);
        
        var query = new DownloadFilesFromTaskQuery(){TaskId = taskId,UserId = guid};
        var result = await _mediator.Send(query);
        return File(result.MemoryStream, "application/zip", result.Directory);
    }
} 