using EntityFramework.Exceptions.Common;
using Eshop_Application.Common.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace Eshop_WebApi.Filters
{
    public class ExceptionHandlerFilter : IExceptionFilter
    {
        //Exception Dictionary
        private readonly IDictionary<Type, Action<ExceptionContext>> _exceptionDictionary;

        public ExceptionHandlerFilter()
        {
            //Initializing it
            _exceptionDictionary = new Dictionary<Type, Action<ExceptionContext>>
            {
                {typeof(NotFoundException), NotFoundExceptionHandler},
                {typeof(ValidationsException),ValidationsExceptionHandler},
                {typeof(TokenException),TokenExceptionHandler},
                {typeof(PasswordException),PasswordExceptionHandler},
                {typeof(ProductException),ProductExceptionHandler},
                {typeof(UniqueConstraintException), UniqueConstraintException}
            };
        }
        public void OnException(ExceptionContext context)
        {
            HandleException(context);
            context.ExceptionHandled = true;
        }

        
        private void HandleException(ExceptionContext context)
        {
            //Getting the Exception type
            Type type = context.Exception.GetType();

            //if the dict dont have the exception
            if (!_exceptionDictionary.ContainsKey(type))
            {
                UnknownExceptionHandler(context);
                return;
            }
             // Its going to invoke the action (method)
            _exceptionDictionary[type].Invoke(context);
            return;
        }

        private void NotFoundExceptionHandler(ExceptionContext context)
        {
            //Creating the reponse
            NotFoundException exception = (NotFoundException)context.Exception;

            ProblemDetails details = new ProblemDetails()
            {
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.4",
                Title = "The specific resource was not found",
                Detail = exception.Message
            };

            context.Result = new NotFoundObjectResult(details) { StatusCode = 400};
        }

        public void UniqueConstraintException(ExceptionContext context)
        {
            UniqueConstraintException exception = (UniqueConstraintException)context.Exception;

            ProblemDetails problemDetails = new ProblemDetails()
            {
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.4",
                Title = exception.Message,
                Detail = exception.InnerException?.Message
            };

            context.Result = new ObjectResult(problemDetails) { StatusCode = 400 };
        }

        private void ValidationsExceptionHandler(ExceptionContext context)
        {
            ValidationsException exception = (ValidationsException)context.Exception;

            //ValidationProblemsDetails accepted a IDictionary<string,string[]> with it, can add the error prop
            ValidationProblemDetails validationProblem = new ValidationProblemDetails(exception._Errors)
            {
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.4",
                Title = "Validation Exception"
            };

            context.Result = new BadRequestObjectResult(validationProblem) {StatusCode = 400};
        }


        private void UnknownExceptionHandler(ExceptionContext context)
        {
            Exception exception = context.Exception;

            ProblemDetails details = new ProblemDetails()
            {
                Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.6.1",
                Title = "Internal Server Error",
                Detail = exception.Message
            };

            context.Result = new NotFoundObjectResult(details) { StatusCode = 500 };
        }

        //by now...
        private void TokenExceptionHandler(ExceptionContext context)
        {
            Exception exception = (TokenException)context.Exception;

            ProblemDetails details = new ProblemDetails()
            {
                Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.6.1",
                Title = "Token Error",
                Detail = exception.Message
            };

            context.Result = new ObjectResult(details) { StatusCode = 400 };
        }

        private void PasswordExceptionHandler(ExceptionContext context)
        {
            Exception exception = (PasswordException)context.Exception;

            ProblemDetails details = new ProblemDetails()
            {
                Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.6.1",
                Title = "Password Error",
                Detail = exception.Message
            };

            context.Result = new BadRequestObjectResult(details) { StatusCode = 404 };
        }

        private void ProductExceptionHandler(ExceptionContext context)
        {
            Exception exception = (ProductException)context.Exception;

            ProblemDetails details = new ProblemDetails()
            {
                Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.6.1",
                Title = "Product File Exception",
                Detail = exception.Message
            };

            context.Result = new BadRequestObjectResult(details) { StatusCode = 404 };
        }
    }
}
