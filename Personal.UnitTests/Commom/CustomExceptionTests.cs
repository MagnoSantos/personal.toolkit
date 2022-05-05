using FluentAssertions;
using Personal.Commom;
using System;
using Xunit;

namespace Personal.UnitTests.Commom
{
    public class CustomExceptionTests
    {
        [Fact]
        public void CustomExceptionBase()
        {
            var customException = new CustomException();

            customException.Message.Should()
                .Be("Exception of type 'Personal.Commom.CustomException' was thrown.");
        }

        [Fact]
        public void CustomExceptionWithMessage()
        {
            var customException = new CustomException("Error");

            customException.Message.Should().Be("Error");
        }

        [Fact]
        public void CustomExceptionWithInnerException()
        {
            var exception = new Exception();

            var customException = new CustomException("Error", exception);

            customException.Message.Should().Be("Error");
            customException.InnerException.Should().Be(exception);
        }
    }
}