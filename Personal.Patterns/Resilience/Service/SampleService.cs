using System.Threading.Tasks;

namespace Personal.Patterns.Resilience.Service
{
    public class SampleService : ISampleService
    {
        public Task AnyMethod()
        {
            throw new System.NotImplementedException();
        }
    }
}