using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
namespace DenounceBeasts.Presentation.Filters
{
    public sealed class ValidateModelFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext ctx)
        {
            if (!ctx.ModelState.IsValid)
            {
                var errors = ctx.ModelState
                    .Where(x => x.Value?.Errors?.Count > 0)
                    .ToDictionary(
                        kvp => kvp.Key,
                        kvp => kvp.Value!.Errors.Select(e => e.ErrorMessage).ToArray());

                var problem = new ProblemDetails
                {
                    Title = "Validation Failed",
                    Status = StatusCodes.Status400BadRequest,
                    Instance = ctx.HttpContext.Request.Path,
                    Detail = "One or more validation errors occurred."
                };

                ctx.Result = new ObjectResult(problem) { StatusCode = problem.Status };
            }
        }
    }
}
