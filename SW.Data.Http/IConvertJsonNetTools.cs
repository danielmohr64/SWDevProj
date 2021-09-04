using System.Collections.Generic;

namespace SW.Data.Http.Repo
{
    public interface IConvertJsonTools
    {
        Dictionary<string, string> DeserializeToDictionary(string json);
    }
}