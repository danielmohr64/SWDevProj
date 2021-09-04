using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SW.Logic;

namespace UrlShortenerApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShortenUrlController : ControllerBase
    {
        private readonly IUrlConvertionService _urlConvertionService;

        public ShortenUrlController(IUrlConvertionService urlConvertionService)
        {
            this._urlConvertionService = urlConvertionService;
        }

        [HttpPost]
        public async Task<string> Post([FromBody] string url)
        {
            return await _urlConvertionService.CreateCleanUrl(url);//string.Format("TODO: Call 3rd party URL Shortening API to shorten '{0}'", url);
        }
    }
}