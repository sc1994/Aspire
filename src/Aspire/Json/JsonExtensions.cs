namespace Aspire.Json
{
    public static class JsonExtensions
    {
        public static string ToJson<T>(this T @class)
            where T : class
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(@class);
        }
    }
}
