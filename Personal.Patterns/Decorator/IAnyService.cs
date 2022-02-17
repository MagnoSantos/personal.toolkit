using System.Threading.Tasks;

namespace Personal.Patterns.Decorator
{
    public interface IAnyService
    {
        Task<string> GetAnyValueAsync();
    }
}