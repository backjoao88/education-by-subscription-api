using EducationSubscription.Core.Domain.Members.Errors;
using EducationSubscription.Core.Primitives;
using EducationSubscription.Core.Repositories;
using MediatR;

namespace EducationBySubscription.Application.Core.Members.Commands.DeleteMember;

public class DeleteMemberCommandHandler : IRequestHandler<DeleteMemberCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteMemberCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(DeleteMemberCommand request, CancellationToken cancellationToken)
    {
        var result = await _unitOfWork.MemberRepository.Delete(request.Id);
        await _unitOfWork.Complete();
        if (!result) return Result.Fail(MemberErrors.MemberNotFound);
        return Result.Ok(result);
    }
}