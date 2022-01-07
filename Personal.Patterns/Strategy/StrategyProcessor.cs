namespace Personal.Patterns.Strategy
{
    public class StrategyProcessor<T> where T : IStrategy, new()
    {
        private readonly IStrategy _typeStrategy;

        public StrategyProcessor()
        {
            _typeStrategy = new T();
        }

        public string Process() => _typeStrategy.ExecuteAsync();
    }
}