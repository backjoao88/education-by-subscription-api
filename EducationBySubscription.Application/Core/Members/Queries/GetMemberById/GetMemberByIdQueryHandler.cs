using EducationBySubscription.Application.Core.Members.Views;
using EducationSubscription.Core.Domain.Members.Errors;
using EducationSubscription.Core.Primitives;
using EducationSubscription.Core.Repositories;
using MediatR;

namespace EducationBySubscription.Application.Core.Members.Queries.GetMemberById;

public class GetMemberByIdQueryHandler : IRequestHandler<GetMemberByIdQuery, Result<MemberViewModel>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetMemberByIdQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<MemberViewModel>> Handle(GetMemberByIdQuery request, CancellationToken cancellationToken)
    {
        var member = await _unitOfWork.MemberRepository.ReadById(request.Id);
        if (member is null) return Result.Fail<MemberViewModel>(MemberErrors.MemberNotFound);
        var memberViewModel = new MemberViewModel(member.Id, member.FirstName, member.LastName, member.DocumentNumber, member.Email);
        return Result.Ok(memberViewModel);
    }
}