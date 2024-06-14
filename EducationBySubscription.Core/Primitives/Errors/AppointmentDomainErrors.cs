namespace EducationSubscription.Core.Primitives.Errors;

public class AppointmentDomainErrors
{
    public static Error AppointmentNotFound = new("Appointment.NotFound", "This appointment was not found.");
    public static Error AppointmentTokenInvalid = new("Appointment.TokenInvalid", "This token is expired or it was already used.");

}