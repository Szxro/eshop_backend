using Eshop_Application.Common.Exceptions;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Eshop_WebApi.Attributes
{
    public class LogoutAttribute : Attribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            //checking if some exception happen
            if (context.Exception is not null)
            {
                throw context.Exception;
            }

            //it just to clean up the cookies
            context.HttpContext.Response.Cookies.Delete("token");
            context.HttpContext.Response.Cookies.Delete("refreshtoken");
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!(context.HttpContext.Request.Cookies.ContainsKey("token")
                && context.HttpContext.Request.Cookies.ContainsKey("refreshtoken")))
            {
                throw new TokenException("First, need to generate some new tokens");
            }
        }
    }
}
