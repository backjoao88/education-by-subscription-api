using EducationBySubscription.Application.Providers.Payment;
using EducationBySubscription.Application.Providers.Payment.Models.Requests;
using EducationSubscription.Core.Domain.Subscriptions.Events;
using EducationSubscription.Core.Primitives.Contracts;
using EducationSubscription.Core.Repositories;
using Microsoft.Extensions.Logging;

namespace EducationBySubscription.Application.Core.Subscriptions.EventHandlers;

public class CreateBillingEventHandler : IDomainEventHandler<SubscriptionCreatedEvent>
{
    private readonly IPaymentProvider _paymentProvider;
    private readonly ILogger<CreateBillingEventHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public CreateBillingEventHandler(IPaymentProvider paymentProvider, ILogger<CreateBillingEventHandler> logger, IUnitOfWork unitOfWork)
    {
        _paymentProvider = paymentProvider;
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(SubscriptionCreatedEvent notification, CancellationToken cancellationToken)
    {
        var member = await _unitOfWork.MemberRepository.ReadById(notification.IdMember);
        if (member is null)
        {
            _logger.LogError("The member {0} was not found during external payment execution.", notification.IdMember.ToString());
            return;
        }
        
        var plan = await _unitOfWork.PlanRepository.ReadById(notification.IdPlan);
        if (plan is null)
        {
            _logger.LogError("The plan {0} was not found during external payment execution.", notification.IdPlan.ToString());
            return;
        }
        
        var customerFromApi = await _paymentProvider.GetCustomerByDocumentNumber(member.DocumentNumber);
        if (customerFromApi is null)
        {
            _logger.LogError("The member with doc. number {0} was not found in the external service.", member.DocumentNumber);
            return;
        } 
        
        var creationResponse = await _paymentProvider.CreateCreditCardSubscription(new CreateCreditCardSubscriptionRequest(
            customerFromApi.ExternalId,
            plan.BasePrice
        ));
        
        if (creationResponse is null)
        {
            _logger.LogError("Payment returned empty for credit card subscription creation.");
        }
    }
}   