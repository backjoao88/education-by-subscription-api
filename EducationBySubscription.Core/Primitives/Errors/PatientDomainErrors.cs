namespace EducationSubscription.Core.Primitives.Errors;

/// <summary>
/// Represents the <see cref="PatientNotFound"/> domain errors.
/// </summary>
public class PatientDomainErrors
{
    public static Error PatientNotFound => new("Patient.NotFound", "This patient was not found");
}