using FluentAssertions;
using Personal.Commom;
using Xunit;

namespace Personal.UnitTests.Commom
{
    public class EntityTests
    {
        [Fact]
        public void EntityInstantiate_Expecting_Successfull()
        {
            var entity = new Entity();

            entity.Should().NotBeNull();
        }
    }
}