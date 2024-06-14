using EducationBySubscription.Api.Abstractions;
using EducationBySubscription.Application.Core.Payments.Commands.ConfirmAsaasPayment;
using EducationBySubscription.Application.Core.Payments.Queries.GetAllPayments;
using EducationBySubscription.Infrastructure.Providers.Asaas.Serialization.Dtos.Payments;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EducationBySubscription.Api.Controllers.Webhooks;

[ApiController]
[Route("api")]
public class AsaasController : ApiController
{
    private readonly IMediator _mediator;

    public AsaasController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet(ApiRoutes.Payment.BasePayment)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllPayments()
    {
        var payments = await _mediator.Send(new GetAllPaymentsQuery());
        return Ok(payments);
    }

    [HttpPost(ApiRoutes.Payment.BasePayment)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> PostConfirmPayment([FromBody] PostConfirmPaymentDto postConfirmPaymentDto)
    {
        var createPaymentCommand = new ConfirmAsaasPaymentCommand(postConfirmPaymentDto.Payment.ExternalSubscriptionId,
            postConfirmPaymentDto.Payment.Value);
        await _mediator.Send(createPaymentCommand);
        return Ok();
    }
}