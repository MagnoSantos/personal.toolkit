using FluentAssertions;
using Personal.Extensions;
using System.ComponentModel;
using Xunit;

namespace Personal.UnitTests
{
    public class EnumsTests
    {
        [Fact]
        public void GetDescription_Expecting_NotBeEmpty()
        {
            var description = Enums.GetDescription(EnumTest.EnumValueWithDescription);

            description.Should().NotBeEmpty();
            description.Should().Be("Test description");
        }

        [Fact]
        public void GetDescription_Expecting_Empty()
        {
            var description = Enums.GetDescription(EnumTest.EnumValueWithoutDescription);

            description.Should().BeEmpty();
        }

        internal enum EnumTest
        {
            [Description("Test description")]
            EnumValueWithDescription,

            EnumValueWithoutDescription
        }
    }
}