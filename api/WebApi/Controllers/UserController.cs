using Application.Command.UserCommands.DeleteUser;
using Application.Command.UserCommands.SignInUser;
using Application.Command.UserCommands.SignUpUser;
using Application.Command.UserCommands.UpdateUser;
using Application.DTO;
using Application.DTO.Request;
using Application.Query.UserQueries.GetUser;
using Application.Query.UserQueries.GetUser.GetAllUsers;
using Application.Query.UserQueries.GetUser.GetUserByEmail;
using Application.Query.UserQueries.GetUser.GetUserById;
using Application.Security;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ITokenStorage _tokenStorage;

    public UserController(IMediator mediator, ITokenStorage tokenStorage)
    {
        _mediator = mediator;
        _tokenStorage = tokenStorage;
    }
    
    [HttpPost("signup")]
    public async Task<IActionResult> SignUp([FromBody] AccountSignUp accountSignUp)
    {
        Guid guid = Guid.NewGuid();
        var command = new SignUpUserCommand()
        {
            UserId = guid,
            Email = accountSignUp.Email, 
            Password = accountSignUp.Password,
            FirstName = accountSignUp.FirstName,
            LastName = accountSignUp.LastName,
            Role = accountSignUp.Role
        };
        await _mediator.Send(command);
        /*return CreatedAtAction(nameof(Get), new {guid}, null);*/
        return Created($"api/account/{guid}", "User created successfully!");
    }
    
    [HttpPost("signin")]
    public async Task<ActionResult<JwtDto>> SignIn([FromBody] AccountLogin accountLogin)
    {
        var command = new SignInUserCommand(){Email = accountLogin.Email, Password = accountLogin.Password};
        await _mediator.Send(command);
        var jwt = _tokenStorage.Get();
        return jwt;
    }
    
    [HttpGet("{id}")]
    [Authorize]
    public async Task<ActionResult<UserDto>> Get(Guid id)
    {
        var query = new GetUserByIdQuery(){Id = id};
        var user = await _mediator.Send(query);
        return user;
    }
    
    [HttpGet("email/{email}")]
    [Authorize]
    public async Task<ActionResult<UserDto>> Get(string email)
    {
        var query = new GetUserByEmailQuery(){Email = email};
        var user = await _mediator.Send(query);
        return user;
    }
    
    [HttpGet("me")]
    [Authorize]
    public async Task<ActionResult<UserDto>> Get()
    {
        if(User.Identity?.Name is null)
        {
            return NotFound();
        }
        
        var guid = Guid.Parse(User.Identity?.Name);
        var query = new GetUserByIdQuery(){Id = guid};
        var user = await _mediator.Send(query);
        return Ok(user);
    }
    
    [HttpGet("all")]
    [Authorize(Roles = "Teacher")]
    public async Task<ActionResult<ICollection<UserDto>>> GetAll()
    {
        var query = new GetAllUsersQuery();
        var users = await _mediator.Send(query);
        return Ok(users);
    }
    
    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update(Guid id, [FromBody] string Role)
    {
        var command = new UpdateUserCommand()
        {
            Id = id,
            Role = Role
        };
        await _mediator.Send(command);
        return Ok();
    }
    
    [HttpDelete("{id}")]
    [Authorize]
    public async Task<IActionResult> Delete(Guid id)
    {
        var command = new DeleteUserCommand(){Id = id};
        await _mediator.Send(command);
        return Ok();
    }
    
}