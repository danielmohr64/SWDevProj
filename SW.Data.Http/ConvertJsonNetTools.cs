using System.Collections.Generic;
using Newtonsoft.Json;

namespace SW.Data.Http.Repo
{
    public class ConvertJsonDotNetTools : IConvertJsonTools
    {
        public Dictionary<string, string> DeserializeToDictionary(string json)
        {
            return JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
        }
    }
}
