using EducationBySubscription.Application.Core.Members.Queries.GetAllMembers;
using EducationBySubscription.Application.Core.Members.Views;
using EducationBySubscription.Application.Core.Plans.Views;
using EducationSubscription.Core.Repositories;
using MediatR;

namespace EducationBySubscription.Application.Core.Plans.Queries.GetAllPlans;

public class GetAllPlansQueryHandler : IRequestHandler<GetAllPlansQuery, List<PlanViewModel>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAllPlansQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<List<PlanViewModel>> Handle(GetAllPlansQuery request, CancellationToken cancellationToken)
    {
        var plans = await _unitOfWork.PlanRepository.ReadAll();
        return plans
            .Select(o => new PlanViewModel(o.Id, o.Title, o.Description, o.BasePrice))
            .ToList();
    }
}