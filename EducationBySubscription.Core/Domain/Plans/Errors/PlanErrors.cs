using EducationSubscription.Core.Primitives;

namespace EducationSubscription.Core.Domain.Plans.Errors;

public static class PlanErrors
{ 
    public static Error PlanNotFound = new("Plan.NotFound", "This plan was not found.");
}