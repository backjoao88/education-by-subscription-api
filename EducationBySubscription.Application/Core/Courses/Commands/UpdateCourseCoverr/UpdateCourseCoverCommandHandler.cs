using EducationBySubscription.Application.Providers.Storage;
using EducationSubscription.Core.Domain.Courses.Errors;
using EducationSubscription.Core.Primitives;
using EducationSubscription.Core.Repositories;
using MediatR;

namespace EducationBySubscription.Application.Core.Courses.Commands.UpdateCourseCoverr;

public class UpdateCourseCoverCommandHandler : IRequestHandler<UpdateCourseCoverCommand, Result<SendStorageProviderResponse>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IStorageProvider _storageProvider;

    public UpdateCourseCoverCommandHandler(IUnitOfWork unitOfWork, IStorageProvider storageProvider)
    {
        _unitOfWork = unitOfWork;
        _storageProvider = storageProvider;
    }
    
    public async Task<Result<SendStorageProviderResponse>> Handle(UpdateCourseCoverCommand request, CancellationToken cancellationToken)
    {
        var course = await _unitOfWork.CourseRepository.ReadById(request.IdCourse);
        if (course is null) return Result.Fail<SendStorageProviderResponse>(CourseErrors.Course.CourseNotFound);
        var memStream = new MemoryStream();
        await request.Cover.CopyToAsync(memStream);
        var response = await _storageProvider.Send(course.Id, memStream.ToArray());
        course.UpdateCover(response.Link);
        await _unitOfWork.Complete();
        return Result.Ok(response);
    }
}