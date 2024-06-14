using EducationBySubscription.Api.Abstractions;
using EducationBySubscription.Api.Attributtes;
using EducationBySubscription.Application.Core.Courses.Commands.CreateCourse;
using EducationBySubscription.Application.Core.Courses.Commands.CreateCourseLesson;
using EducationBySubscription.Application.Core.Courses.Commands.DeleteCourse;
using EducationBySubscription.Application.Core.Courses.Commands.UpdateCourse;
using EducationBySubscription.Application.Core.Courses.Commands.UpdateCourseCoverr;
using EducationBySubscription.Application.Core.Courses.Queries.GetAllCourses;
using EducationBySubscription.Application.Core.Courses.Queries.GetCourseById;
using EducationSubscription.Core.Domain.Users;
using EducationSubscription.Core.Primitives;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EducationBySubscription.Api.Controllers;

[ApiController]
[Route("api")]
public class CourseController : ApiController
{
    public CourseController(IMediator mediator)
    {
        _mediator = mediator;
    }

    private readonly IMediator _mediator;

    [HttpPost(ApiRoutes.Course.BaseCourseLessonWithId)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HasPermission(EUserRole.Admin)]
    public async Task<IActionResult> PostCourseLesson(Guid id,
        [FromBody] CreateCourseLessonCommand createCourseLessonCommand)
    {
        createCourseLessonCommand.IdCourse = id;
        var result = await _mediator.Send(createCourseLessonCommand);
        return result.IsSuccess
            ? CreatedAtAction(nameof(PostCourseLesson), value: result.Value)
            : BadRequest(result.Error);
    }

    [HttpPost(ApiRoutes.Course.BaseCourse)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HasPermission(EUserRole.Admin)]
    public async Task<IActionResult> PostCourse([FromBody] CreateCourseCommand createCourseCommand)
    {
        var result = await _mediator.Send(createCourseCommand);
        return result.IsSuccess ? CreatedAtAction(nameof(PostCourse), value: result.Value) : BadRequest(result.Error);
    }

    [HttpPut(ApiRoutes.Course.BaseCourseWithId)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HasPermission(EUserRole.Admin)]
    public async Task<IActionResult> PutCourse(Guid id, [FromBody] UpdateCourseCommand updateCourseCommand)
    {
        updateCourseCommand.Id = id;
        var result = await _mediator.Send(updateCourseCommand);
        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
    }

    [HttpDelete(ApiRoutes.Course.BaseCourseWithId)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HasPermission(EUserRole.Admin)]
    public async Task<IActionResult> DeleteCourse(Guid id)
    {
        var result = await _mediator.Send(new DeleteCourseCommand(id));
        return result.IsSuccess ? NoContent() : BadRequest(result.Error);
    }

    [HttpGet(ApiRoutes.Course.BaseCourseWithId)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetCourseById(Guid id)
    {
        var result = await _mediator.Send(new GetCourseByIdQuery(id));
        return result.IsSuccess ? Ok(result.Value) : NotFound(result.Error);
    }

    [HttpGet(ApiRoutes.Course.BaseCourse)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [HasPermission(EUserRole.Admin)]
    public async Task<IActionResult> GetAllCourses()
    {
        var view = await _mediator.Send(new GetAllCoursesQuery());
        return Ok(view);
    }

    [HttpPut(ApiRoutes.Course.BaseCourseWithIdCover)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ExtensionValidatorFilter(new[]{ ".png" })]
    [HasPermission(EUserRole.Admin)]
    public async Task<IActionResult> UpdateCourseCover(Guid id, IFormCollection formCollection)
    {
        var imageFile = formCollection.Files[0];
        var result = await _mediator.Send(new UpdateCourseCoverCommand(id, imageFile.OpenReadStream()));
        return result.IsSuccess ? Ok() : BadRequest(result.Error);
    }
    
}
 
