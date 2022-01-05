using FluentAssertions;
using Personal.Extensions;
using Xunit;

namespace Personal.UnitTests.Extensions
{
    public class StringTests
    {
        [Fact]
        public void IsNullOrEmpty_Expectiong_True()
        {
            var value = string.Empty;

            var result = String.IsNullOrEmpty(value);

            result.Should().BeTrue();
        }

        [Fact]
        public void ToInt_Expectiong_IntValue()
        {
            var value = "1";

            var result = String.ToInt(value);

            result.Should().Be(1);
        }
    }
}