using FluentAssertions;
using Personal.IntegrationTests.Configuration;
using Personal.Projects.Worker._1___Domain.Abstractions.Entities;
using Personal.Projects.Worker._1___Domain.Abstractions.Repositories;
using System.Threading.Tasks;
using Xunit;

namespace Personal.IntegrationTests.Worker.Infra.Data
{
    public class CustomerRepositoryTests : BaseIntegrationTests<ICustomerRepository>
    {
        [Fact]
        public async Task Customer_AddAndGet_InMemoryDataBase()
        {
            var customer = new Customer
            {
                Name = "foo",
                LastName = "bar"
            };

            Dependency.Add(customer);
            await Dependency.SaveChangesAsync();

            var get = await Dependency.GetOneAsync(pred => pred.Id == customer.Id);

            get.Id.Should().Be(customer.Id);
        }
    }
}