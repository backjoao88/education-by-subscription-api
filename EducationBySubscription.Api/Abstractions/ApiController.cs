using EducationSubscription.Core.Primitives;
using Microsoft.AspNetCore.Mvc;

namespace EducationBySubscription.Api.Abstractions;

/// <summary>
/// Represents a base controller.
/// </summary>
public class ApiController : ControllerBase
{
    [NonAction]
    public IActionResult BadRequest(Error error) => BadRequest(new ApiErrorResponse(new []{ error }));
    [NonAction]
    public IActionResult NotFound(Error error) => NotFound(new ApiErrorResponse(new[] { error }));
    
    [NonAction]
    public Guid GetAuthenticatedUserId()
    {
        var userSubjectClaim = User.Claims.FirstOrDefault()!.Value;
        if (userSubjectClaim is null)
        {
            throw new ArgumentException();
        }
        if (!Guid.TryParse(userSubjectClaim, out Guid userId))
        {
            throw new ArgumentException();
        }
        return userId;
    }
}