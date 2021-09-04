using System;
using System.Threading.Tasks;
using SW.Data.Http.Repo;

namespace SW.Logic
{
    public interface IUrlConvertionService
    {
        Task<string> CreateCleanUrl(string url);
    }

    public class UrlConvertionService : IUrlConvertionService
    {
        private readonly ICleanUrlApi _cleanUrlApi;

        public UrlConvertionService(ICleanUrlApi cleanUrlApi)
        {
            _cleanUrlApi = cleanUrlApi;
        }

        public async Task<string> CreateCleanUrl(string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                throw new ArgumentException("Url missing in CreateCleanUrl", nameof(url));
            }

            //var encodedUrl = System.Web.HttpUtility.HtmlAttributeEncode(url);
            return await _cleanUrlApi.ReturnCleanUrlAsync(url);
        }
    }
}
