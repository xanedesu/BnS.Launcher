using System;
using System.Text;

namespace BNSLauncher.Core.Utils
{
    static class Base64
    {
        public static string Encode(string text)
        {
            return Convert.ToBase64String(Encoding.Default.GetBytes(text));
        }

        public static string Decode(string base64)
        {
            return Encoding.Default.GetString(Convert.FromBase64String(base64));
        }
    }
}
