using EducationBySubscription.Api.Abstractions;
using EducationBySubscription.Application.Core.Subscriptions.Commands.CreateSubscription;
using EducationBySubscription.Application.Core.Subscriptions.Commands.DeleteSubscription;
using EducationBySubscription.Application.Core.Subscriptions.Queries.GetAllSubscriptions;
using EducationBySubscription.Application.Core.Subscriptions.Queries.GetSubscriptionById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EducationBySubscription.Api.Controllers;

[ApiController]
[Route("api")]
public class SubscriptionController : ApiController
{
    public SubscriptionController(IMediator mediator)
    {
        _mediator = mediator;
    }

    private readonly IMediator _mediator;
    
    [HttpPost(ApiRoutes.Subscription.BaseSubscription)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> PostSubscription([FromBody] CreateSubscriptionCommand createSubscriptionCommand)
    {
        var result = await _mediator.Send(createSubscriptionCommand);
        return result.IsSuccess ? CreatedAtAction(nameof(PostSubscription), value: result.Value) : BadRequest(result.Error);
    }

    [HttpGet(ApiRoutes.Subscription.BaseSubscriptionWithId)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetSubscriptionById(Guid id)
    {
        var result = await _mediator.Send(new GetSubscriptionByIdQuery(id));
        return result.IsSuccess ? Ok(result.Value) : NotFound(result.Error);
    }
    
    [HttpGet(ApiRoutes.Subscription.BaseSubscription)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllSubscriptions()
    {
        var view = await _mediator.Send(new GetAllSubscriptionsQuery());
        return Ok(view);    
    }
    
    [HttpDelete(ApiRoutes.Subscription.BaseSubscriptionWithId)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteSubscription(Guid id)
    {
        var result = await _mediator.Send(new DeleteSubscriptionCommand(id));
        return result.IsSuccess ? NoContent() : BadRequest(result.Error);
    }
    
}