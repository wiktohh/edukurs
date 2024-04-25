using Application.Command.Tasks.CreateTask;
using Application.Command.Tasks.RemoveTask;
using Application.Command.Tasks.UpdateTask;
using Application.Command.Tickets.SendTIcketCommand;
using Application.DTO.Request;
using Application.Query.TasksQueries.GetAllTasks;
using Application.Query.TasksQueries.GetTaskById;
using Application.Query.TasksQueries.GetTasksFromRepo;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TaskController : ControllerBase
{
    private readonly IMediator _mediator;

    public TaskController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllTasks()
    {
        var query = new GetAllTasksQuery();
        var tasks = await _mediator.Send(query);
        return Ok(tasks);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetTask(Guid id)
    {
        var query = new GetTaskByIdQuery(){TaskId = id};
        var task = await _mediator.Send(query);
        return Ok(task);
    }
    
    [HttpGet("repository/{id}")]
    public async Task<IActionResult> GetTasksFromRepo(Guid id)
    {
        var query = new GetTasksFromRepoQuery(){RepositoryId = id};
        var tasks = await _mediator.Send(query);
        return Ok(tasks);
    }
    
    [HttpDelete("{taskId}")]
    [Authorize (Roles = "Teacher")]
    public async Task<IActionResult> RemoveTask([FromRoute]Guid taskId)
    {
        if(User.Identity?.Name is null)
        {
            return NotFound();
        }
        var guid = Guid.Parse(User.Identity?.Name);
        
        var command = new RemoveTaskCommand()
        {
            Id = taskId,
            UserId = guid
        };
        await _mediator.Send(command);
        return Accepted();
    }
    
    [HttpPut("{taskId}")]
    [Authorize (Roles = "Teacher")]
    public async Task<IActionResult> UpdateTask([FromRoute]Guid taskId,[FromBody] DateTime Deadline)
    {
        if(User.Identity?.Name is null)
        {
            return NotFound();
        }
        var guid = Guid.Parse(User.Identity?.Name);
        
        var command = new UpdateTaskCommand()
        {
            Id = taskId,
            Deadline = Deadline,
            UserId = guid
        };
        await _mediator.Send(command);
        return Accepted();
    }
    
    [HttpPost("send-ticket")]
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
    
    [HttpPost("repository/{id}")]
    [Authorize(Roles = "Teacher")]
    public async Task<IActionResult> AddTaskToRepo([FromRoute]Guid id,[FromBody] AddTaskRequest request)
    {
        if(User.Identity?.Name is null)
        {
            return NotFound();
        }
        var guid = Guid.Parse(User.Identity?.Name);
        
        var taskID = Guid.NewGuid();
        
        var command = new CreateTaskCommand()
        {
            Title = request.Title,
            Description = request.Description,
            Deadline = request.Deadline,
            RepositoryId = id,
            RepTaskId = taskID,
            UserId = guid
        };
        await _mediator.Send(command);
        return CreatedAtRoute("GetTask", new { id = taskID }, null);
    }
}