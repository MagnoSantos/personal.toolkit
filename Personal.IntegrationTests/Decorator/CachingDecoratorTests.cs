using FluentAssertions;
using Personal.IntegrationTests.Configuration;
using Personal.Patterns.Decorator;
using System.Threading.Tasks;
using Xunit;

namespace Personal.IntegrationTests.Decorator
{
    public class CachingDecoratorTests : BaseIntegrationTests<IAnyService>
    {
        [Fact]
        public async Task GetAnyValueAsync_Expecting_RunDecorateCaching()
        {
            var result = await Dependency.GetAnyValueAsync();

            result.Should().NotBeNullOrEmpty();
            result.Should().Be("foo");
        }
    }
}