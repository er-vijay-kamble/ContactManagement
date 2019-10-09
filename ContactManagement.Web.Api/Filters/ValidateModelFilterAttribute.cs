namespace ContactManagement.Web.Api.Filters
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;
    using Microsoft.AspNetCore.Routing;
    using Serilog;
    using System;
    using System.Linq;
    using System.Net;

    public class ValidateModelAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext actionContext)
        {
            var controllerClassName = $"{actionContext.HttpContext.GetRouteValue("controller")}Controller";
            var action = actionContext.HttpContext.GetRouteValue("action");

            if (actionContext != null && actionContext.ModelState.IsValid == false)
            {
                var errors = new
                {
                    Message = "Invalid request",
                    Error = actionContext.ModelState.Values.SelectMany(v => v.Errors).Where(v => !string.IsNullOrEmpty(v.ErrorMessage)).Select(e => e.ErrorMessage).ToList()
                };
                errors.Error.AddRange(actionContext.ModelState.Values.SelectMany(v => v.Errors).Where(v => v.Exception != null).Select(e => e.Exception.Message).ToList());
                string errorList = string.Join(Environment.NewLine, errors.Error);
                Log.Logger.Error("{Controller} - {Method} - {Message}", controllerClassName, action, errorList);
                actionContext.Result = new ContentResult
                {
                    Content = errorList,
                    StatusCode = 400
                };
            }
        }
    }
}