using System.Threading.Tasks;

namespace Personal.Infra.Clients
{
    public interface IAclClient
    {
        Task Delete(string path, object httpQueryParam = null, object headers = null, int? timeout = null);

        Task<TResponse> Get<TResponse>(string path, object httpQueryParam = null, object headers = null, int? timeout = null);

        Task<TResponse> Post<TResponse>(string path, object requestContent, object httpQueryParam = null, object headers = null, int? timeout = null);

        Task<TResponse> Put<TResponse>(string path, object requestContent, object httpQueryParam = null, object headers = null, int? timeout = null);
    }
}