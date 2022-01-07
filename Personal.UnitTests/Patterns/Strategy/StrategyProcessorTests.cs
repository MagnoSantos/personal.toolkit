using FluentAssertions;
using Personal.Patterns.Strategy;
using Xunit;

namespace Personal.UnitTests.Patterns.Strategy
{
    public class StrategyProcessorTests
    {
        [Fact]
        public void StrategyA_Expecting_Successfull()
        {
            var strategyProcessor = new StrategyProcessor<StrategyA>();

            var result = strategyProcessor.Process();

            result.Should().Be("Strategy A processed");
        }

        [Fact]
        public void StrategyB_Expecting_Successfull()
        {
            var strategyProcessor = new StrategyProcessor<StrategyB>();

            var result = strategyProcessor.Process();

            result.Should().Be("Strategy B processed");
        }
    }
}