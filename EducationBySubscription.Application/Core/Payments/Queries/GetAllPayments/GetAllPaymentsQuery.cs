using EducationBySubscription.Application.Core.Payments.Views;
using MediatR;

namespace EducationBySubscription.Application.Core.Payments.Queries.GetAllPayments;

public class GetAllPaymentsQuery : IRequest<List<PaymentViewModel>>;