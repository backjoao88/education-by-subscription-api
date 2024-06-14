using EducationBySubscription.Application.Exceptions;
using EducationSubscription.Core.Primitives;
using FluentValidation;
using MediatR;

namespace EducationBySubscription.Application.Behaviours;

/// <summary>
/// Represents a mediator behavior to validate incoming request objects.
/// </summary>
public sealed class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators) => _validators = validators;

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (!_validators.Any())
        {
            return await next();
        }
        var validationCtx = new ValidationContext<TRequest>(request);
        var errors = _validators
            .Select(o => o.Validate(validationCtx))
            .SelectMany(o => o.Errors)
            .Select(o => new Error(o.ErrorCode, o.ErrorMessage))
            .DistinctBy(o => o.Code)
            .ToArray();

        if (errors.Any())
        {
            throw new CustomValidationException(errors);
        }

        return await next();
        
    }
}