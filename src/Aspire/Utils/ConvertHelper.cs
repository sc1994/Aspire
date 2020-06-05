using System.Collections.Concurrent;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace Aspire.Utils
{
    public static class ConvertHelper
    {
        private static readonly ConcurrentDictionary<string, string> _slugifyNamingStyleDic = new ConcurrentDictionary<string, string>();

        public static string ToSlugifyNamingStyle(string name)
        {
            return _slugifyNamingStyleDic.GetOrAdd(name, Regex.Replace(name, "([a-z])([A-Z])", "$1-$2")).ToLower();
        }

        public static string ToMd5(string input, string salt)
        {
            string source = input;
            if (!string.IsNullOrWhiteSpace(salt))
            {
                source += salt;
            }

            using MD5 md5Hash = MD5.Create();
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(source));

            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }
    }
}
