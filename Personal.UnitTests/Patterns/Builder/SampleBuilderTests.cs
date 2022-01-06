using FluentAssertions;
using Personal.Patterns.Builder;
using Xunit;

namespace Personal.UnitTests.Patterns.Builder
{
    public class SampleBuilderTests
    {
        private readonly SampleBuilder _builder;

        public SampleBuilderTests()
        {
            _builder = new();
        }

        [Fact]
        public void BuilderObject_Sample()
        {
            var sample = "foo";

            var result = _builder.AddProperty(sample)
                                 .Build();

            result.Should().NotBeNull();
            result.Sample.Should().Be(sample);
        }
    }
}