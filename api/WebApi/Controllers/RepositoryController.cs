using Application.Command.Repositories.CreateRepository;
using Application.Command.Repositories.RemoveRepository;
using Application.Command.Repositories.RemoveUserFromRepo;
using Application.Command.Repositories.UpdateRepository;
using Application.Command.Tasks;
using Application.Command.Tasks.CreateTask;
using Application.Command.Tasks.RemoveTask;
using Application.Command.Tasks.UpdateTask;
using Application.Command.Tickets.RespondToTicket;
using Application.Command.Tickets.SendTIcketCommand;
using Application.DTO;
using Application.DTO.Request;
using Application.Query.RepoQueries;
using Application.Query.RepoQueries.GetRepoById;
using Application.Query.RepoQueries.GetRepos;
using Application.Query.TicketQueries;
using Application.Query.TicketQueries.GetPendingTickets;
using Application.Query.TicketQueries.GetRepoTickets;
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

    
    [HttpPost("remove-user/{id}")]
    [Authorize]
    public async Task<IActionResult> RemoveUserFromRepo([FromRoute]Guid id,[FromBody] Guid UserId)
    {
        if(User.Identity?.Name is null)
        {
            return NotFound();
        }
        var guid = Guid.Parse(User.Identity?.Name);
        var command = new RemoveUserFromRepoCommand()
        {
            RepositoryId = id,
            UserId = UserId,
            SenderId = guid
        };
        await _mediator.Send(command);
        return Ok();
    }
    
    
    [HttpDelete("{id}")]
    [Authorize (Roles = "Teacher")]
    public async Task<IActionResult> DeleteRepository([FromRoute]Guid id)
    {
        if(User.Identity?.Name is null)
        {
            return NotFound();
        }
        var guid = Guid.Parse(User.Identity?.Name);
        var command = new RemoveRepositoryCommand()
        {
            Id = id,
            SenderId = guid
        };
        await _mediator.Send(command);
        return Ok();
    }
    
    [HttpPut("{id}")]
    [Authorize (Roles = "Teacher")]
    public async Task<IActionResult> UpdateRepository([FromRoute]Guid id,[FromBody] UpdateRepositoryRequest request)
    {
        if(User.Identity?.Name is null)
        {
            return NotFound();
        }
        var guid = Guid.Parse(User.Identity?.Name);
        var command = new UpdateRepositoryCommand()
        {
            Id = id,
            Name = request.Name,
            SenderId = guid
        };
        await _mediator.Send(command);
        return Ok();
    }
    
    [HttpGet("{id}")]
    [Authorize]
    public async Task<IActionResult> GetRepositoryById([FromRoute]Guid id)
    {
        var command = new GetRepoByIdQuery()
        {
            Id = id,
        };
        var response = await _mediator.Send(command);
        return Ok(response);
    }
}