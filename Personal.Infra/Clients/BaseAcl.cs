using Microsoft.Extensions.Logging;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace Personal.Infra.Clients
{
    public abstract class BaseAcl
    {
        protected readonly string BaseUrl;
        protected readonly ILogger<object> Logger;

        public BaseAcl(string baseUrl, ILogger<object> logger)
        {
            BaseUrl = baseUrl;
            Logger = logger;
        }

        protected static string SerializeRequestContent(object requestContent) => JsonSerializer.Serialize(requestContent);

        protected async Task<T> ExecuteAsync<T>(Func<Task<T>> function, string verb, string data = null)
        {
            try
            {
                Logger.LogInformation(@"{verb} - Chamada ao servico externo X \n", verb);

                if (data is not null) Logger.LogInformation(@"Corpo da requisicao: {data}", data);

                return await function();
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, $"Ocorreu um erro ao realizar chamada ao servico externo x às {DateTime.UtcNow:g}");
                throw;
            }
        }
    }
}