using System.Text;

namespace LogicExtensions;

/// <summary>
/// Provides extensions for a query string.
/// </summary>
public static class QueryStringExtensions
{
    /// <summary>
    /// Adds or updates the specified QueryString parameter to the URL string.
    /// </summary>
    /// <param name="url">Url to modify</param>
    /// <param name="name">Query string parameter name to set</param>
    /// <param name="value">Parameter value</param>
    /// <returns>New URL with given parameter set</returns>
    public static string SetQueryStringParam(this string? url, string name, string? value)
    {
        url ??= "";

        if (string.IsNullOrWhiteSpace(name))
        {
            return url;
        }

        name = name.Trim();
        value = value?.Trim() ?? "";

        var queryString = string.Empty;

        if (url.Contains("?"))
        {
            var questionIndex = url.IndexOf("?", StringComparison.Ordinal);
            var startIndex = questionIndex + 1;

            if (startIndex < url.Length)
            {
                queryString = url.Substring(startIndex);
            }

            url = url.Substring(0, questionIndex);
        }

        var dictionary = new Dictionary<string, string>();

        foreach (var str in queryString.Split('&'))
        {
            if (string.IsNullOrEmpty(str))
            {
                continue;
            }

            var strArray = str.Split('=');
            if (strArray.Length == 2)
            {
                dictionary[strArray[0]] = strArray[1];
            }
        }

        // Adds the specified parameter.
        dictionary[name] = value;

        var builder = new StringBuilder();

        foreach (var str in dictionary.Keys)
        {
            if (builder.Length > 0)
            {
                builder.Append("&");
            }

            builder.Append(str);
            builder.Append("=");
            builder.Append(dictionary[str]);
        }

        queryString = builder.ToString();

        return url + (string.IsNullOrEmpty(queryString) ? string.Empty : "?" + queryString);
    }
}
