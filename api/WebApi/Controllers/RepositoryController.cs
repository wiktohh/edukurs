using Application.Command.RepositoryCommand.CreateRepository;
using Application.Command.TicketCommands;
using Application.DTO;
using Application.DTO.Request;
using Application.Query.RepoQueries;
using Application.Query.TicketQueries;
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
    [HttpPost("ticket")]
    [Authorize]
    public async Task<IActionResult> AddUserToRepo([FromBody] SendTicketRequest request)
    {
        if(User.Identity?.Name is null)
        {
            return NotFound();
        }
        var id = Guid.NewGuid();
        var guid = Guid.Parse(User.Identity?.Name);
        var command = new SendTicketCommand()
        {
            RepositoryId = request.RepositoryId,
            UserId = guid,
            Id = id
        };
        await _mediator.Send(command);
        return Ok();
    }
    
    [HttpPut("ticket/{id}")]
    [Authorize]
    public async Task<IActionResult> RespondToTicket([FromRoute]Guid id,[FromBody] SendTicketResponse request)
    {
        
        if(User.Identity?.Name is null)
        {
            return NotFound();
        }
        var guid = Guid.Parse(User.Identity?.Name);
        
        var command = new RespondToTicketCommand()
        {
            UserId = guid,
            TicketId = id,
            Status = request.Status
        };
        await _mediator.Send(command);
        return Ok();
    }
    
    [HttpGet("tickets")]
    [Authorize(Roles = "Teacher")]
    public async Task<IActionResult> GetPendingTickets()
    {
        if(User.Identity?.Name is null)
        {
            return NotFound();
        }
        var guid = Guid.Parse(User.Identity?.Name);
        var query = new GetPendingTicketsQuery(){UserId = guid};
        var tickets = await _mediator.Send(query);
        return Ok(tickets);
    }
    
    [HttpGet("tickets/{repId}")]
    [Authorize(Roles = "Teacher")]
    public async Task<IActionResult> GetPendingTickets(Guid repId)
    {
        if(User.Identity?.Name is null)
        {
            return NotFound();
        }
        var guid = Guid.Parse(User.Identity?.Name);
        var query = new GetRepoTicketsQuery(){UserId = guid, RepositoryId = repId};
        var tickets = await _mediator.Send(query);
        return Ok(tickets);
    }
}