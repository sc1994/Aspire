using static Newtonsoft.Json.JsonConvert;

namespace Aspire.Json
{
    public static class JsonExtensions
    {
        public static string Serialize<T>(this T @class)
            where T : class
        {
            return SerializeObject(@class);
        }

        public static T Deserialize<T>(this string json)
            where T : class
        {
            return DeserializeObject<T>(json);
        }
    }
}
