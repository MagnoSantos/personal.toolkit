using AutoFixture;
using FluentAssertions;
using Personal.Patterns.Converter;
using Xunit;

namespace Personal.UnitTests.Patterns.Converter
{
    public class SampleConverterTests
    {
        private readonly SampleConverter _converter;
        private readonly IFixture _fixture;

        public SampleConverterTests()
        {
            _converter = new();
            _fixture = new Fixture();
        }

        [Fact]
        public void Converter_Expecting_Successfull()
        {
            var sampleDto = _fixture.Create<SampleDto>();

            var result = _converter.Convert(sampleDto);

            result.Should().NotBeNull();
            result.Id.Should().Be(sampleDto.Id);
        }
    }
}