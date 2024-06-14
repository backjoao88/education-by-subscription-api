using EducationBySubscription.Application.Core.Members.Views;
using EducationSubscription.Core.Primitives;
using MediatR;

namespace EducationBySubscription.Application.Core.Members.Commands.UpdateMember;

public class UpdateMemberCommand : IRequest<Result<MemberUpdatedViewModel>>
{
    public UpdateMemberCommand(Guid id, string firstName, string lastName, string documentNumber, string email)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        DocumentNumber = documentNumber;
        Email = email;
    }
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string DocumentNumber { get; set; }
    public string Email { get; set; }
}