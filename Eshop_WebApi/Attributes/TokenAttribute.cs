using Eshop_Application.Common.Exceptions;
using Eshop_Application.Common.Interfaces;
using Eshop_Application.Common.Settings;
using Eshop_Domain.Entities.TokenEntities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;

namespace Eshop_WebApi.Attributes
{
    public class TokenAttribute : Attribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            //after logic 

            if (context.Exception is not null)
            {
                throw context.Exception; // if an exception occurred is going to throw the exception
            }

            ObjectResult? result = (ObjectResult?)context.Result;

            //check if the result is null else exception
            if (result is null)
            {
                throw new Exception("Something happen , check the result object");
            }

            TokenResponse? response = (TokenResponse?)result.Value;

            CokkieSet(context, response);
        }

        private void CokkieSet(ActionExecutedContext? context, TokenResponse? response)
        {
            if (context is null || response is null) return;

            //Instantiating a service
            IOptions<JwtSettings>? options = context.HttpContext.RequestServices.GetService<IOptions<JwtSettings>?>();

            CookieOptions cookieOptions = new()
            {
                Expires = DateTime.UtcNow.AddMinutes(Convert.ToDouble(options?.Value.ExpiresIn)),
                Path = "/",
                Secure = true
            };

            //Setting the token and refresh token in the cookies 
            context.HttpContext.Response.Cookies.Append("token", response.TokenValue, cookieOptions);
            context.HttpContext.Response.Cookies.Append("refreshtoken", response.RefreshTokenValue, cookieOptions);
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            // before logic
            if (context.HttpContext.Request.Cookies.ContainsKey("token")
                && context.HttpContext.Request.Cookies.ContainsKey("refreshtoken"))
            {
                throw new TokenException("First, need to lotgout to generate new tokens");
            }
        }
    }
}
