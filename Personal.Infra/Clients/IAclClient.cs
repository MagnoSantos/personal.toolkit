using System.Threading.Tasks;

namespace Personal.Infra.Clients
{
    public interface IAclClient
    {
        Task Delete(string url, object httpQueryParam = null, object headers = null, int? timeout = null);

        Task<TResponse> Get<TResponse>(string url, object httpQueryParam = null, object headers = null, int? timeout = null);

        Task<TResponse> Post<TResponse>(string url, object requestContent, object httpQueryParam = null, object headers = null, int? timeout = null);

        Task<TResponse> Put<TResponse>(string url, object requestContent, object httpQueryParam = null, object headers = null, int? timeout = null);
    }
}