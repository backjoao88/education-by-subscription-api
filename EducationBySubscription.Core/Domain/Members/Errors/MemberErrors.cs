using EducationSubscription.Core.Primitives;

namespace EducationSubscription.Core.Domain.Members.Errors;

public static class MemberErrors
{ 
    public static Error MemberNotFound = new("Member.NotFound", "This member was not found.");
    public static Error MemberNotGranted = new("Member.NotGranted", "This member has no grant to access this resource.");
}