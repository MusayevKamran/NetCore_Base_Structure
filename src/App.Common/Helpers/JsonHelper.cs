using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace App.Common.Helpers;

/// <summary>
///     Json helper
/// </summary>
public static class JsonHelper
{
    /// <summary>
    ///     Serialize from model T to JSON in string format
    /// </summary>
    public static string SerializeToString<T>(this T obj) where T : class
    {
        return JsonConvert.SerializeObject(obj, new JsonSerializerSettings
        {
            Formatting = Formatting.None,
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
            Converters = new List<JsonConverter> { new StringEnumConverter() }
        });
    }
}