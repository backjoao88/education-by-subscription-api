using EducationBySubscription.Application.Core.Members.Views;
using EducationSubscription.Core.Domain.Members;
using EducationSubscription.Core.Primitives;
using EducationSubscription.Core.Repositories;
using MediatR;

namespace EducationBySubscription.Application.Core.Members.Commands.CreateMember;

public class CreateMemberCommandHandler : IRequestHandler<CreateMemberCommand, Result<MemberCreatedViewModel>>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateMemberCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<MemberCreatedViewModel>> Handle(CreateMemberCommand request, CancellationToken cancellationToken)
    {
        var member = new Member(request.FirstName, request.LastName, request.DocumentNumber, request.Email, true);
        await _unitOfWork.MemberRepository.Add(member);
        await _unitOfWork.Complete();
        var memberCreatedViewModel = new MemberCreatedViewModel(member.Id);
        return Result.Ok(memberCreatedViewModel);
    }
}