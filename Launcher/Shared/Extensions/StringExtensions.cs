using System;
using System.Text;
using System.Text.RegularExpressions;

namespace Unlakki.Bns.Launcher.Shared.Extensions
{
    public static class StringExtensions
    {
        public static string RemoveLeadingSymbols(this string str, char[] symbols)
        {
            if (str == null)
            {
                return null;
            }

            foreach (char symbol in symbols)
            {
                if (str.Length > 0 && str[0] == symbol)
                {
                    str = str.Substring(1);
                }
            }

            return str;
        }

        public static string RemoveTrailingSymbols(this string str, char[] symbols)
        {
            if (str == null)
            {
                return null;
            }

            foreach (char symbol in symbols)
            {
                if (str.Length > 0 && str[str.Length - 1] == symbol)
                {
                    str = str.Substring(0, str.Length - 1);
                }
            }

            return str;
        }

        public static string RemoveLeadingSlash(this string url)
        {
            return url.RemoveLeadingChar('/');
        }

        public static string RemoveTrailingSlash(this string url)
        {
            return url.RemoveTrailingChar('/');
        }

        public static string EnsureLeadingSlash(this string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                return "/";
            }

            return url[0] != '/' ? "/" + url : url;
        }

        public static string EnsureTrailingSlash(this string url)
        {
            return url.EnsureTrailingChar('/');
        }

        public static string EnsureTrailingChar(this string url, char symbol)
        {
            return string.IsNullOrEmpty(url)
              || url.Length > 0 && url[url.Length - 1] == symbol ? url : url + symbol.ToString();
        }

        public static string RemoveLast(this string value, int length)
        {
            return value.Substring(0, value.Length - length);
        }

        public static string RemoveLast(this string value, string remove)
        {
            return value.EndsWith(remove) ? value.Substring(0, value.Length - remove.Length) : value;
        }

        public static string EscapeCmdInQuotesPart(this string part)
        {
            return part == null ? null : part.Replace("\"", "\\\"").RemoveTrailingSlash();
        }

        public static string RemoveLeadingChar(this string url, char chr)
        {
            if (!string.IsNullOrEmpty(url) && url[0] == chr)
            {
                url = url.Substring(1);
            }

            return url;
        }

        public static string RemoveTrailingChar(this string url, char chr)
        {
            if (!string.IsNullOrEmpty(url) && url[url.Length - 1] == chr)
            {
                url = url.Substring(0, url.Length - 1);
            }

            return url;
        }

        public static string SubstringOrSelf(this string str, int startIndex, int length)
        {
            return str?.Substring(startIndex, Math.Min(length, str.Length));
        }

        public static string ReplaceNewLineWith(this string str, string replace)
        {
            return string.Join(replace, Regex.Split(str, "\\r?\\n|\\r"));
        }

        public static string AddOrUpdateParameterToUrl(this string url, string name, string value)
        {
            try
            {
                UriBuilder uriBuilder = new UriBuilder(url);
                string str1 = uriBuilder.Query.RemoveLeadingChar('?');
                StringBuilder stringBuilder = new StringBuilder();
                bool flag = false;
                int num = 0;

                if (!string.IsNullOrWhiteSpace(str1))
                {
                    string str2 = str1;
                    char[] chArray = new char[1] { '&' };

                    foreach (string str3 in str2.Split(chArray))
                    {
                        string str4;

                        if ((str4 = str3).StartsWith(name + "="))
                        {
                            flag = true;

                            if (value != null)
                            {
                                str4 = name + "=" + Uri.EscapeUriString(value);
                            }
                            else
                            {
                                continue;
                            }
                        }

                        if (num > 0)
                        {
                            stringBuilder.Append("&");
                        }

                        stringBuilder.Append(str4);
                        ++num;
                    }
                }

                if (!flag && value != null)
                {
                    if (num > 0)
                    {
                        stringBuilder.Append("&");
                    }

                    stringBuilder.Append(name + "=" + Uri.EscapeUriString(value));
                }

                uriBuilder.Query = stringBuilder.ToString();

                return uriBuilder.Uri.ToString();
            }
            catch (UriFormatException ex)
            {
                throw new Exception("StartPage config parameter had bad format", ex);
            }
        }
    }
}
