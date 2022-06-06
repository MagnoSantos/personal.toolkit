using Microsoft.Extensions.Options;
using System.Collections.Concurrent;

namespace Personal.RateLimit.Middleware.A;

public class RateLimiterMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<RateLimiterMiddleware> _logger;
    private readonly RateLimitOptions _rateLimitOptions;
    private ConcurrentDictionary<string, int> _activeRequests;

    public RateLimiterMiddleware(RequestDelegate next,
                                 ILogger<RateLimiterMiddleware> logger,
                                 IOptions<RateLimitOptions> rateLimitOptions)
    {
        _next = next;
        _logger = logger;
        _rateLimitOptions = rateLimitOptions?.Value ?? throw new ArgumentNullException(nameof(rateLimitOptions));

        _activeRequests = new ConcurrentDictionary<string, int>();
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var endpoint = context.GetEndpoint();

        if (endpoint is null)
        {
            await _next(context);
            return;
        }

        var routePattern = (endpoint as RouteEndpoint)?.RoutePattern.RawText;

        if (string.IsNullOrWhiteSpace(routePattern))
        {
            await _next(context);
            return;
        }

        var decorator = endpoint.Metadata.GetMetadata<LimitRequests>();

        if (decorator is null)
        {
            await _next(context);
            return;
        }

        var limit = _rateLimitOptions.Limit;
        var hasActiveReqs = _activeRequests.TryGetValue(routePattern, out var activeReqs);
        
        if (hasActiveReqs && activeReqs >= limit)
        {
            _logger.LogError($"Too many requests for endpoint {routePattern} -> {activeReqs} active requests");
            context.Response.StatusCode = StatusCodes.Status429TooManyRequests;
            return;
        }

        if (!hasActiveReqs)
        {
            _logger.LogInformation($"{routePattern} -> 1 active requests.");
            _activeRequests.AddOrUpdate(routePattern, 1, (x, y) => y + 1);
        }
        else
        {
            _logger.LogInformation($"{routePattern} -> {activeReqs + 1} active requests.");
            _activeRequests.AddOrUpdate(routePattern, 1, (x, y) => y + 1);
        }

        try
        {
            await _next(context);
        }
        catch (System.Exception)
        {
            _activeRequests.AddOrUpdate(routePattern, 0, (x, y) => y - 1);
            throw;
        }

        _activeRequests.AddOrUpdate(routePattern, 0, (x, y) => y - 1);

        return;
    }
}

public static class RateLimiterMiddlewareExtensions
{
    public static IApplicationBuilder UseRateLimiter(this IApplicationBuilder builder)
        => builder.UseMiddleware<RateLimiterMiddleware>();
}