using EducationBySubscription.Application.Core.Members.Views;
using EducationBySubscription.Application.Core.Plans.Views;
using MediatR;

namespace EducationBySubscription.Application.Core.Plans.Queries.GetAllPlans;

public record GetAllPlansQuery : IRequest<List<PlanViewModel>>;