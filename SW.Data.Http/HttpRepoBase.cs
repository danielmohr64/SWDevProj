using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace SW.Data.Http
{
    public interface IHttpRepoBase
    {
        void SetupHttpClient(string service);
    }
    public class HttpRepoBase: IHttpRepoBase
    {
        protected string _service = "";
        protected HttpClient _httpClient;
        protected IOptions<UrlServiceConfigs> _urlServiceConfigs;

        public HttpRepoBase(HttpClient client, IOptions<UrlServiceConfigs> urlServiceConfigs)
        {
            _httpClient = client ?? throw new ArgumentNullException(nameof(client));
            _urlServiceConfigs = urlServiceConfigs;
        }

        public void SetupHttpClient(string service)
        {
            _httpClient.BaseAddress = new Uri(_urlServiceConfigs.Value.Services[service].Base);
            _service = service;
        }

        protected async Task<string> HttpPostAsync<Q>(string operation, Q obj, string[] parms=null) where Q : HttpContent
        {
            var url = _urlServiceConfigs.Value.BuildQuery(_service, operation, parms).url;
            HttpResponseMessage response;
            response = await _httpClient.PostAsync(new Uri(url).AbsolutePath,obj);

            
            var dataString = await response.Content.ReadAsStringAsync();
            response.EnsureSuccessStatusCode();
            return dataString;
        }

        protected async Task<string> HttpGetAsync(string operation, string[] parms = null)
        {
            var queryAndAction = _urlServiceConfigs.Value.BuildQuery(_service, operation, parms);
            HttpResponseMessage response;
            response = await _httpClient.GetAsync(queryAndAction.url);

            response.EnsureSuccessStatusCode();

            var dataString = await response.Content.ReadAsStringAsync();
            return dataString;
        }
    }
}
