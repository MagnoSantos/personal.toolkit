namespace Personal.Patterns.Resilience
{
    public static class ResilienceConfiguration
    {
        public static int RetryCount { get; set; } = 3;
        public static int RetryPow { get; set; } = 2;
        public static int ExceptionsAllowedBeforeBreaking { get; set; } = 3;
        public static int  DurationOfBreakInSeconds { get; set; } = 2;
    }
}