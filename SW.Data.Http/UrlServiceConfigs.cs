using System;
using System.Collections.Generic;
using System.Text;

namespace SW.Data.Http
{
    public class UrlServiceConfigs
    {
        public string DefaultBase { get; set; }

        public Dictionary<string, UrlServiceConfig> Services { get; set; }

        public (string url, string action) BuildQuery(string service, string action, object[] paramStrings = null)
        {
            if (!this.Services.ContainsKey(service))
                throw new ArgumentException(service + ": Could not be found.", nameof(service));
            if (!this.Services[service].Actions.ContainsKey(action))
                throw new ArgumentException(action + ": Could not be found.", nameof(action));

            var urlEnd = this.Services[service].Actions[action].EndUrl;

            var url = (string.IsNullOrWhiteSpace(this.Services[service].Base)
                          ? DefaultBase
                          : this.Services[service].Base) + (string.IsNullOrEmpty(this.Services[service].BaseSubUrl) ? "" : "/" + this.Services[service].BaseSubUrl);
            url += "/" + urlEnd;
            
            // TODO: Check query later.
            var query = this.Services[service].Actions[action].FormatQuery;
            if (paramStrings != null && query != "") { 
                query = string.Format(query, paramStrings);
                url += query;
            }

            return (url: url,action: this.Services[service].Actions[action].Action);
        }
    }

    public class UrlServiceConfig
    {
        public string Base { get; set; }
        public string BaseSubUrl { get; set; }

        public Dictionary<string, UrlServiceAction> Actions { get; set; }
    }

    public class UrlServiceAction
    {
        public string EndUrl { get; set; } = "";
        public string FormatQuery { get; set; } = "";
        public string Action { get; set; } = "GET";

    }
}
