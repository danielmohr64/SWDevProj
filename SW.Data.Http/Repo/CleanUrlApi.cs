using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SW.Data.Http.Repo
{
    public interface ICleanUrlApi : IHttpRepoBase
    {
        Task<string> ReturnCleanUrlAsync(string url);
    }

    public class CleanUrlApi : HttpRepoBase, ICleanUrlApi
    {
        //private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IConvertJsonTools _jsonTools;
        protected string RETURN_CLEAN_URL_SETTINGS = "ReturnShortenUrl";

        public CleanUrlApi(HttpClient client, 
            IHttpContextAccessor httpContextAccessor, 
            IOptions<UrlServiceConfigs> urlServiceConfigs, IConvertJsonTools jsonTools) : base(client, urlServiceConfigs)
        {
            SetupHttpClient("CleanUrlApi");
            //this.httpContextAccessor = httpContextAccessor;
            this._jsonTools = jsonTools;
        }

        public async Task<string> ReturnCleanUrlAsync(string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                throw new ArgumentException("Url is not set for ReturnCleanUrlAsync.", nameof(url));
            }

            var parameters = new Dictionary<string,string>();
            parameters.Add("url", url);
            var form = new FormUrlEncodedContent(parameters);
            var retString = await base.HttpPostAsync<FormUrlEncodedContent>(RETURN_CLEAN_URL_SETTINGS, form);

            // TODO: Come back to this. You could use a better JSON convert class.
            var jsonDict = _jsonTools.DeserializeToDictionary(retString);
            if (!jsonDict.ContainsKey("result_url"))
            {
                throw new Exception("ResultUrl is not found. Raw Return : "+ retString);
            }

            return jsonDict["result_url"];
        }
    }
}
