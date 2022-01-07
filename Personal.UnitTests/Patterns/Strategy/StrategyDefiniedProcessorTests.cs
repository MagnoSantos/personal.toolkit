using FluentAssertions;
using Personal.Patterns.Strategy;
using Xunit;

namespace Personal.UnitTests.Patterns.Strategy
{
    public class StrategyDefiniedProcessorTests
    {
        [Fact]
        public void StrategyA_Expecting_Successfull()
        {
            var strategyProcessor = new StrategyDefiniedProcessor();

            var result = strategyProcessor.Process(Type.StrategyA);

            result.Should().Be("Strategy A processed");
        }

        [Fact]
        public void StrategyB_Expecting_Successfull()
        {
            var strategyProcessor = new StrategyDefiniedProcessor();

            var result = strategyProcessor.Process(Type.StrategyB);

            result.Should().Be("Strategy B processed");
        }
    }
}