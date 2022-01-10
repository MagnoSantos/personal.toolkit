using Personal.Projects.Worker._1___Domain.Abstractions.Entities;
using Personal.Projects.Worker._1___Domain.Abstractions.Repositories;
using Personal.Projects.Worker._2___Infra.Data.EntityFramework;

namespace Personal.Projects.Worker._2___Infra.Data.Repositories
{
    public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(DataContext context) : base(context)
        {
        }
    }
}