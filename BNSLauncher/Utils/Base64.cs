using System;
using System.Text;

namespace BNSLauncher.Utils
{
    static class Base64
    {
        public static string Base64Encode(string text)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(text);
            return Convert.ToBase64String(bytes);
        }

        public static string Base64Decode(string base64)
        {
            byte[] bytes = Convert.FromBase64String(base64);
            return Encoding.UTF8.GetString(bytes);
        }
    }
}
