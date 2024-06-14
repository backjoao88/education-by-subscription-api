using EducationBySubscription.Api.Abstractions;
using EducationBySubscription.Application.Core.Members.Commands.CreateMember;
using EducationBySubscription.Application.Core.Members.Commands.DeleteMember;
using EducationBySubscription.Application.Core.Members.Commands.UpdateMember;
using EducationBySubscription.Application.Core.Members.Queries.GetAllMembers;
using EducationBySubscription.Application.Core.Members.Queries.GetMemberById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EducationBySubscription.Api.Controllers;

[ApiController]
[Route("api")]
public class MemberController : ApiController
{
    public MemberController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    private readonly IMediator _mediator;
    
    [HttpPost(ApiRoutes.Member.BaseMember)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> PostMember([FromBody] CreateMemberCommand createMemberCommand)
    {
        var result = await _mediator.Send(createMemberCommand);
        return result.IsSuccess ? CreatedAtAction(nameof(PostMember), value: result.Value) : BadRequest(result.Error);
    }

    [HttpPut(ApiRoutes.Member.BaseMemberWithId)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> PutMember(Guid id, [FromBody] UpdateMemberCommand updateMemberCommand)
    {
        updateMemberCommand.Id = id;
        var result = await _mediator.Send(updateMemberCommand);
        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
    }
    
    [HttpDelete(ApiRoutes.Member.BaseMemberWithId)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteMember(Guid id)
    {
        var result = await _mediator.Send(new DeleteMemberCommand(id));
        return result.IsSuccess ? NoContent() : BadRequest(result.Error);
    }
    
    [HttpGet(ApiRoutes.Member.BaseMemberWithId)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetMemberById(Guid id)
    {
        var result = await _mediator.Send(new GetMemberByIdQuery(id));
        return result.IsSuccess ? Ok(result.Value) : NotFound(result.Error);
    }
    
    [HttpGet(ApiRoutes.Member.BaseMember)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllMembers()
    {
        var view = await _mediator.Send(new GetAllMembersQuery());
        return Ok(view);    
    }

    
}