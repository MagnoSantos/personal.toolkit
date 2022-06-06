namespace Personal.RateLimit.Middleware;

public class RateLimitOptions
{
    public string? Route { get; set; }
    public int Limit { get; set; }
}