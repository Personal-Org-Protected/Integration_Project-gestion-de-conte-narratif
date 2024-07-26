﻿
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogginLibrary.LogginRequest
{
    public class PerformanceBehaviours<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly Stopwatch _timer;
        private readonly ILogger<TRequest> _logger;

        public PerformanceBehaviours(
            ILogger<TRequest> logger)
        {
            _timer = new Stopwatch();
            _logger = logger;

        }
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            _timer.Start();

            var response = await next();

            _timer.Stop();

            var requestName = typeof(TRequest).Name;
            var elapsedMilliseconds = _timer.ElapsedMilliseconds;

            if (elapsedMilliseconds > 500 && response is not null)
            {

                _logger
                    .LogInformation("Request: {Name} ({ElapsedMilliseconds} milliseconds) {@Request} (Reponse : {Response})",
                    requestName, elapsedMilliseconds, request,response.GetType());
            }
            else
            {
                _logger
                  .LogWarning("Request status Unknown: {Name} ({ElapsedMilliseconds} milliseconds) {@Request})",
                  requestName, elapsedMilliseconds, request);
            }
        


            return response;
        }
    }
}
