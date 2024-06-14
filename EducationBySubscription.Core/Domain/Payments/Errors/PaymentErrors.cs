using EducationSubscription.Core.Primitives;

namespace EducationSubscription.Core.Domain.Payments.Errors;

public static class PaymentErrors
{ 
    public static Error PaymentNotFound = new("Payment.NotFound", "This payment was not found.");
}