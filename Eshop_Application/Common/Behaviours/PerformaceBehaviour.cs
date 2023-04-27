using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshop_Application.Common.Behaviours
{
    public class PerformaceBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
    {
        private readonly Stopwatch _watcher;
        private readonly ILogger<TRequest> _logger;

        public PerformaceBehaviour(ILogger<TRequest> logger)
        {
            _watcher = new Stopwatch();
            _logger = logger;
        }
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            //StopWatch is used to see the time that a request is take or any other services or method.
            _watcher.Start();

            var response = await next();

            _watcher.Stop();

            var elapsedMilliseconds = _watcher.ElapsedMilliseconds;

            var requestName = typeof(TRequest).Name; // Getting the name of the request made

            _logger.LogInformation("The current request {@name} complete in {@time} miliseconds", requestName, elapsedMilliseconds);

            return response;
        }
    }
}
