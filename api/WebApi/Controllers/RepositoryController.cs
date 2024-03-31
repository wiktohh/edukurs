using Application.Command.RepositoryCommand.CreateRepository;
using Application.DTO;
using Application.DTO.Request;
using Application.Query.RepoQueries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RepositoryController : ControllerBase
{
    private readonly IMediator _mediator;

    public RepositoryController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPost]
    [Authorize(Roles = "Teacher")]
    public async Task<IActionResult> CreateRepository([FromBody] CreateRepositoryRequest request)
    {
        if(User.Identity?.Name is null)
        {
            return NotFound();
        }
        
        var guid = Guid.Parse(User.Identity?.Name);
        var Id = Guid.NewGuid();
        var command = new CreateRepositoryCommand()
        {
            Id = Id,
            Name = request.Name,
            OwnerId = guid
        };
        await _mediator.Send(command);
        return Ok();
    }
    
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetRepos([FromQuery] RepoEnum repoEnum)
    {
        if(User.Identity?.Name is null)
        {
            return NotFound();
        }
        
        var guid = Guid.Parse(User.Identity?.Name);
        var query = new GetReposQuery(){RepoEnum = repoEnum, Id = guid};
        var repos = await _mediator.Send(query);
        return Ok(repos);
    }
    
}