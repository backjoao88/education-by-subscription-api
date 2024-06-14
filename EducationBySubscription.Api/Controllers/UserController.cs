using EducationBySubscription.Api.Abstractions;
using EducationBySubscription.Application.Core.Users.Commands.Create;
using EducationBySubscription.Application.Core.Users.Commands.Login;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EducationBySubscription.Api.Controllers;

[ApiController]
[Route("api")]
public class UserController : ApiController
{
    private readonly IMediator _mediator;

    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Endpoint to save a new patient.
    /// </summary>
    /// <param name="createUserCommand"></param>
    [HttpPost(ApiRoutes.User.BaseUser)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> PostUser([FromBody] CreateUserCommand createUserCommand)
    { 
        var result = await _mediator.Send(createUserCommand);
        return result.IsSuccess ? CreatedAtAction(nameof(PostUser), value: result.Value) : BadRequest(result.Error);
    }
    
    /// <summary>
    /// Endpoint to login into the system.
    /// </summary>
    [HttpPut(ApiRoutes.User.BaseUserLogin)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Login([FromBody] LoginUserCommand loginUserCommand)
    {
        var result = await _mediator.Send(loginUserCommand);
        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
    }
}