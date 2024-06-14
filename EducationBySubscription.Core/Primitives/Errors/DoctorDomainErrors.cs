namespace EducationSubscription.Core.Primitives.Errors;

public class DoctorDomainErrors
{
    public static Error DoctorNotFound => new("Doctor.NotFound", "This doctor was not found.");
    public static Error DoctorNotAvailable = new("Doctor.NotAvailable", "This doctor is not available at this period of time.");
    public static Error DoctorSpecialityNotAvailable = new("DoctorSpeciality.NotAvailable", "This doctor speciality is not available.");
}