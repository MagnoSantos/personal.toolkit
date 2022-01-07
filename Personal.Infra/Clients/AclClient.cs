using Flurl;
using Flurl.Http;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Threading.Tasks;

namespace Personal.Infra.Clients
{
    public class AclClient : BaseAcl, IAclClient
    {
        public AclClient(string baseUrl, ILogger<object> logger) : base(baseUrl, logger)
        {
        }

        public async Task Delete(string path, object httpQueryParam = null, object headers = null, int? timeout = null)
        {
            await ExecuteAsync(async () =>
               await BaseUrl.AppendPathSegment(path)
                            .WithHeaders(headers)
                            .SetQueryParams(httpQueryParam)
                            .WithTimeout(timeout.Value)
                            .DeleteAsync(),
               verb: HttpMethod.Delete.ToString()
           );
        }

        public async Task<TResponse> Get<TResponse>(string path, object httpQueryParam = null, object headers = null, int? timeout = null)
        {
            return await ExecuteAsync(async () =>
                await BaseUrl.AppendPathSegment(path)
                             .WithHeaders(headers)
                             .SetQueryParams(httpQueryParam)
                             .WithTimeout(timeout.Value)
                             .GetJsonAsync<TResponse>(),
                verb: HttpMethod.Get.ToString()
            );
        }

        public async Task<TResponse> Post<TResponse>(string path, object requestContent, object httpQueryParam = null, object headers = null, int? timeout = null)
        {
            return await ExecuteAsync(async () =>
                await BaseUrl.AppendPathSegment(path)
                             .WithHeaders(headers)
                             .SetQueryParams(httpQueryParam)
                             .WithTimeout(timeout.Value)
                             .PostJsonAsync(requestContent)
                             .ReceiveJson<TResponse>(),
                verb: HttpMethod.Post.ToString(),
                data: SerializeRequestContent(requestContent)
            );
        }

        public async Task<TResponse> Put<TResponse>(string path, object requestContent, object httpQueryParam = null, object headers = null, int? timeout = null)
        {
            return await ExecuteAsync(async () =>
                await BaseUrl.AppendPathSegment(path)
                             .WithHeaders(headers)
                             .SetQueryParams(httpQueryParam)
                             .WithTimeout(timeout.Value)
                             .PutJsonAsync(requestContent)
                             .ReceiveJson<TResponse>(),
                verb: HttpMethod.Put.ToString(),
                data: SerializeRequestContent(requestContent)
            );
        }
    }
}