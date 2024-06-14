using EducationBySubscription.Application.Core.Members.Views;
using EducationSubscription.Core.Primitives;
using MediatR;

namespace EducationBySubscription.Application.Core.Members.Commands.CreateMember;

public class CreateMemberCommand : IRequest<Result<MemberCreatedViewModel>>
{
    public CreateMemberCommand(string firstName, string lastName, string documentNumber, string email)
    {
        FirstName = firstName;
        LastName = lastName;
        DocumentNumber = documentNumber;
        Email = email;
    }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string DocumentNumber { get; set; }
    public string Email { get; set; }
}