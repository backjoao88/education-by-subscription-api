using EducationBySubscription.Application.Providers.Storage;
using EducationSubscription.Core.Primitives;
using MediatR;

namespace EducationBySubscription.Application.Core.Courses.Commands.UpdateCourseCoverr;

public class UpdateCourseCoverCommand : IRequest<Result<SendStorageProviderResponse>>
{
    public UpdateCourseCoverCommand(Guid idCourse, Stream cover)
    {
        IdCourse = idCourse;
        Cover = cover;
    }
    public Guid IdCourse { get; private set; }
    public Stream Cover { get; private set; }
}