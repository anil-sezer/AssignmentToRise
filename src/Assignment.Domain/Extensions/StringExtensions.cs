using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace Assignment.Domain.Extensions;

public static class StringExtensions
{
    public static string ToQueryString(this object obj)
    {
        var properties = from p in obj.GetType().GetProperties()
            where p.GetValue(obj, null) != null
            select p.Name + "=" + HttpUtility.UrlEncode(p.GetValue(obj, null).ToString());

        return string.Join("&", properties.ToArray());
    }

    public static string ToSnakeCase(this string input)
    {
        if (string.IsNullOrEmpty(input)) { return input; }

        var startUnderscores = Regex.Match(input, @"^_+");
        return startUnderscores + CultureInfo.GetCultureInfo("en-US").TextInfo.ToLower(Regex.Replace(input, @"([a-z0-9])([A-Z])", "$1_$2"));
    }

    public static string ToPascalCase(this string input)
    {
        var text = input.Replace("_", " ");
        var textInfo = CultureInfo.CurrentCulture.TextInfo;

        return textInfo.ToTitleCase(text).Replace(" ", string.Empty);
    }

    public static string ToTurkishTimeString(this DateTime input)
    {
        return input.ToString("dd/MM/yyyy HH:mm:ss");
    }

    public static string ToTurkishShortTimeString(this DateTime input)
    {
        return input.ToString("dd/MM/yyyy");
    }

    public static string ToTurkishTimeString(this TimeSpan input)
    {
        return input.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
    }

    public static string ToMD5String(this string input)
    {
        using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
        {
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            byte[] hashBytes = md5.ComputeHash(inputBytes);

            var sb = new StringBuilder();
            foreach (var t in hashBytes)
            {
                sb.Append(t.ToString("X2"));
            }
            return sb.ToString();
        }
    }

    public static bool Contains(this string source, string toCheck, StringComparison comp)
    {
        return source?.IndexOf(toCheck, comp) >= 0;
    }
}