using EducationBySubscription.Application.Core.Members.Views;
using EducationSubscription.Core.Domain.Members.Errors;
using EducationSubscription.Core.Primitives;
using EducationSubscription.Core.Repositories;
using MediatR;

namespace EducationBySubscription.Application.Core.Members.Commands.UpdateMember;

public class UpdateMemberCommandHandler : IRequestHandler<UpdateMemberCommand, Result<MemberUpdatedViewModel>>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateMemberCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<MemberUpdatedViewModel>> Handle(UpdateMemberCommand request, CancellationToken cancellationToken)
    {
        var member = await _unitOfWork.MemberRepository.ReadById(request.Id);
        if (member is null) return Result.Fail<MemberUpdatedViewModel>(MemberErrors.MemberNotFound);
        member.Update(request.FirstName, request.LastName, request.DocumentNumber, request.Email);
        await _unitOfWork.Complete();
        var memberUpdatedViewModel = new MemberUpdatedViewModel(member.Id);
        return Result.Ok(memberUpdatedViewModel);
    }
}