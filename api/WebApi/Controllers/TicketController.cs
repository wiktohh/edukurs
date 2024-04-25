using Application.Command.Tickets.RespondToTicket;
using Application.DTO.Request;
using Application.Query.TicketQueries.GetPendingTickets;
using Application.Query.TicketQueries.GetRepoTickets;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;


[ApiController]
[Route("api/[controller]")]
public class TicketController : ControllerBase
{
    private readonly IMediator _mediator;

    public TicketController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet("repository/{repId}")]
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
    
    [HttpGet("pending")]
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
    
    [HttpPut("{id}")]
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
}