using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Tata.IGetIT.Learner.Service.Helpers;

namespace Tata.IGetIT.Learner.Web
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        ILogger logger = LogManager.GetCurrentClassLogger();


        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                var response = context.Response;
                response.ContentType = "application/json";
                //TODO: request ID required for correlation.
                logger.LogError($"RequestId-TODO", error);

                switch (error)
                {
                    case ApiException e:
                        // Custom api errors
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;
                    case KeyNotFoundException e:
                        // Resource not found error
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        break;
                    default:
                        // unhandled exceptions
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }

                var result = JsonSerializer.Serialize(new ReturnResponse<string> { Message = error?.Message, Data = null }) ;
                await response.WriteAsync(result);
            }
        }
    }
}
