namespace ContactManagement.Web.Api.Filters
{
    using ContactManagement.Domain.Exceptions;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;
    using Microsoft.AspNetCore.Routing;
    using Serilog;
    using System.Net;

    public sealed class CustomExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            var controllerClassName = $"{context.HttpContext.GetRouteValue("controller")}Controller";
            var action = context.HttpContext.GetRouteValue("action");

            if (context.Exception is ValidationException)
            {
                Log.Logger.Error(context.Exception, "{Controller} - {Method} - {Message}", controllerClassName, action);

                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;

                context.Result = new ContentResult()
                {
                    Content = context.Exception.Message
                };
            }
            else if (context.Exception is InternalException)
            {
                Log.Logger.Error(context.Exception, "{Controller} - {Method} - Unhandled internal application exception.", controllerClassName, action);
                SetGenericErrorContext(context);
            }
            else
            {
                Log.Logger.Error(context.Exception, "{Controller} - {Method} - Unhandled exception.", controllerClassName, action);
                SetGenericErrorContext(context);
            }

            base.OnException(context);
        }

        private static void SetGenericErrorContext(ExceptionContext context)
        {
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Result = new JsonResult(context.Exception.Message);
        }
    }
}
