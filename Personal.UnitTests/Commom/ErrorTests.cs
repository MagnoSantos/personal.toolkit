using FluentAssertions;
using Personal.Commom;
using System;
using Xunit;

namespace Personal.UnitTests.Commom
{
    public class ErrorTests
    {
        [Fact]
        public void ConstructedError_Successfull()
        {
            var exception = new Exception();

            var result = Error.FromDefault(exception);

            result.Should().NotBeNull();
            result.Errors.Should().HaveCount(1);
        }
    }
}