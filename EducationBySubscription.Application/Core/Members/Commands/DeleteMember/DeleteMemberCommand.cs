using EducationSubscription.Core.Primitives;
using MediatR;

namespace EducationBySubscription.Application.Core.Members.Commands.DeleteMember;

public class DeleteMemberCommand : IRequest<Result>
{
    public DeleteMemberCommand(Guid id)
    {
        Id = id;
    }
    public Guid Id { get; set; }
}