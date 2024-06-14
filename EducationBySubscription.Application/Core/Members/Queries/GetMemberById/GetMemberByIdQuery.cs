using EducationBySubscription.Application.Core.Members.Views;
using EducationSubscription.Core.Primitives;
using MediatR;

namespace EducationBySubscription.Application.Core.Members.Queries.GetMemberById;

public class GetMemberByIdQuery : IRequest<Result<MemberViewModel>>
{
    public GetMemberByIdQuery(Guid id)
    {
        Id = id;
    }
    public Guid Id { get; set; }
}