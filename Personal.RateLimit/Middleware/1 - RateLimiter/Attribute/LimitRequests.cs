namespace Personal.RateLimit.Middleware;

[AttributeUsage(AttributeTargets.Method)]
public class LimitRequests : Attribute
{
}