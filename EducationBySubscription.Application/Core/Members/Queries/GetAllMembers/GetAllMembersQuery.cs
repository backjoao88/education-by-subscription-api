using EducationBySubscription.Application.Core.Members.Views;
using MediatR;

namespace EducationBySubscription.Application.Core.Members.Queries.GetAllMembers;

public record GetAllMembersQuery : IRequest<List<MemberViewModel>>;