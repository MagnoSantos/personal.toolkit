using System.Threading.Tasks;

namespace Personal.Patterns.Decorator
{
    public class AnyService : IAnyService
    {
        public Task<string> GetAnyValueAsync() => Task.FromResult("foo");
    }
}