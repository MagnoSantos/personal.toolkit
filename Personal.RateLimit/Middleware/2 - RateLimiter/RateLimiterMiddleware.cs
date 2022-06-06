using Microsoft.AspNetCore.Mvc.Controllers;
using Personal.RateLimit.Middleware._2___RateLimiter;
using System.Collections.Concurrent;

namespace Personal.RateLimit.Middleware.B;

public class RateLimitMiddlware
{
    private readonly RequestDelegate _next;
    private static readonly ConcurrentDictionary<string, DateTime?> ApiCallsInMemory = new();

    public RateLimitMiddlware(RequestDelegate next)
    {
        _next = next;
    }

    /// <summary>
    /// Rate Limit para mesmo IP dentro do prazo definido
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public async Task InvokeAsync(HttpContext context)
    {
        var endpoint = context.GetEndpoint();
        var controllerActionDescriptor = endpoint?.Metadata.GetMetadata<ControllerActionDescriptor>();

        if (controllerActionDescriptor is null)
        {
            await _next(context);
            return;
        }

        var apiDecorator = (RateLimitDecorator)controllerActionDescriptor.MethodInfo
                                                                         .GetCustomAttributes(true)
                                                                         .SingleOrDefault(w => w.GetType() == typeof(RateLimitDecorator));

        if (apiDecorator is null)
        {
            await _next(context);
            return;
        }

        string key = GetCurrentClientKey(apiDecorator, context);

        var previousApiCall = GetPreviousApiCallByKey(key);
        if (previousApiCall != null)
        {
            if (DateTime.UtcNow < previousApiCall.Value.AddSeconds(10))
            {
                context.Response.StatusCode = StatusCodes.Status429TooManyRequests;
                return;
            }
        }

        UpdateApiCallFor(key);

        await _next(context);
    }

    private static void UpdateApiCallFor(string key)
    {
        ApiCallsInMemory.TryRemove(key, out _);
        ApiCallsInMemory.TryAdd(key, DateTime.UtcNow);
    }

    private static DateTime? GetPreviousApiCallByKey(string key)
    {
        ApiCallsInMemory.TryGetValue(key, out DateTime? value);
        return value;
    }

    private static string GetCurrentClientKey(RateLimitDecorator apiDecorator, HttpContext context)
    {
        var keys = new List<string>
        {
            context.Request.Path
        };

        switch (apiDecorator.StrategyType)
        {
            case StrategyTypeEnum.IpAddress:
                keys.Add(GetClientIpAddress(context));
                break;

            case StrategyTypeEnum.PerUser:
                break;

            case StrategyTypeEnum.PerApiKey:
                break;
        }

        return string.Join('_', keys);
    }
    private static string GetClientIpAddress(HttpContext context) => context?.Connection?.RemoteIpAddress?.ToString();
}
