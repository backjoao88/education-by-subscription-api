using EducationSubscription.Core.Primitives;

namespace EducationBySubscription.Api.Abstractions;

/// <summary>
/// Represents an api set of errors response.
/// </summary>
public class ApiErrorResponse
{
    public ApiErrorResponse(Error[] errors)
    {
        Errors = errors;
    }
    public Error[] Errors { get; set; }
}