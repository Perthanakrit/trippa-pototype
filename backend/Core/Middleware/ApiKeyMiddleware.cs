using System.Net;
using Core.Interface.security;
using Core.Utility;
using Microsoft.AspNetCore.Http;

namespace Core.Middleware
{
    public class ApiKeyMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IApiKeyValidation _apiKeyValidation;
        public ApiKeyMiddleware(RequestDelegate next, IApiKeyValidation apiKeyValidation)
        {
            _next = next;
            _apiKeyValidation = apiKeyValidation;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            if (string.IsNullOrWhiteSpace(context.Request.Headers[SD.ApiKeyHeaderName]))
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return;
            }
            string userApiKey = context.Request.Headers[SD.ApiKeyHeaderName];
            if (!_apiKeyValidation.Validate(userApiKey!))
            {
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                return;
            }
            await _next(context);
        }
    }
}