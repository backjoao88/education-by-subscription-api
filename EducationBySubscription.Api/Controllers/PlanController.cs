using EducationBySubscription.Api.Abstractions;
using EducationBySubscription.Application.Core.Members.Commands.DeleteMember;
using EducationBySubscription.Application.Core.Members.Queries.GetMemberById;
using EducationBySubscription.Application.Core.Plans.Commands.CreatePlan;
using EducationBySubscription.Application.Core.Plans.Commands.UpdatePlan;
using EducationBySubscription.Application.Core.Plans.Queries.GetAllPlans;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EducationBySubscription.Api.Controllers;

[ApiController]
[Route("api")]
public class PlanController : ApiController
{
        public PlanController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    private readonly IMediator _mediator;
    
    [HttpPost(ApiRoutes.Plan.BasePlan)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> PostPlan([FromBody] CreatePlanCommand createPlanCommand)
    {
        var result = await _mediator.Send(createPlanCommand);
        return result.IsSuccess ? CreatedAtAction(nameof(PostPlan), value: result.Value) : BadRequest(result.Error);
    }

    [HttpPut(ApiRoutes.Plan.BasePlanWithId)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> PutPlan(Guid id, [FromBody] UpdatePlanCommand updatePlanCommand)
    {
        updatePlanCommand.Id = id;
        var result = await _mediator.Send(updatePlanCommand);
        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
    }
    
    [HttpDelete(ApiRoutes.Plan.BasePlanWithId)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeletePlan(Guid id)
    {
        var result = await _mediator.Send(new DeleteMemberCommand(id));
        return result.IsSuccess ? NoContent() : BadRequest(result.Error);
    }
    
    [HttpGet(ApiRoutes.Plan.BasePlanWithId)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetPlanById(Guid id)
    {
        var result = await _mediator.Send(new GetMemberByIdQuery(id));
        return result.IsSuccess ? Ok(result.Value) : NotFound(result.Error);
    }
    
    [HttpGet(ApiRoutes.Plan.BasePlan)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllPlans()
    {
        var view = await _mediator.Send(new GetAllPlansQuery());
        return Ok(view);    
    }

}