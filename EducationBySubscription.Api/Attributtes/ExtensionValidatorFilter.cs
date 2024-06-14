using EducationSubscription.Core.Primitives;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;

namespace EducationBySubscription.Api.Attributtes;

public class ExtensionValidatorFilter : ActionFilterAttribute
{
    public ExtensionValidatorFilter(string[] allowedExtensions)
    {
        AllowedExtensions = allowedExtensions;
    }

    private string[] AllowedExtensions { get; set; }
    
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var fileParam =
            context.ActionArguments.SingleOrDefault(o => o.Value is IFormCollection).Value as IFormCollection;
        if (fileParam is null) return;
        var file = fileParam.Files[0];
        var fileExtension = Path.GetExtension(file.FileName);
        if (!AllowedExtensions.Contains(fileExtension))
        {
            context.Result = new ContentResult()
            {
                Content = JsonConvert.SerializeObject(new Error("CourseCover.NotSupported",
                    "This course cover extension is not supported.")),
                ContentType = "application/json"
            };
            context.HttpContext.Response.StatusCode = 400;
            return;
        }
        base.OnActionExecuting(context);
    }
}