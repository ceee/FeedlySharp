using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace FeedlySharp.Extensions
{
  internal static class UriExtensions
  {
    internal static Dictionary<string, string> ParseQueryString(this Uri uri)
    {
      string uriString = uri.OriginalString;
      string substring = uriString.Substring(((uriString.LastIndexOf('?') == -1) ? 0 : uriString.LastIndexOf('?') + 1));

      string[] pairs = substring.Split('&');

      Dictionary<string, string> output = new Dictionary<string, string>();

      foreach (string piece in pairs)
      {
        string[] pair = piece.Split('=');
        output.Add(pair[0], WebUtility.UrlDecode(pair[1]));
      }

      return output;
    }


    internal static string ToQueryString(this IDictionary<string, string> dict)
    {
      if (dict.Count == 0) return String.Empty;

      var buffer = new StringBuilder();
      int count = 0;
      bool end = false;

      foreach (var key in dict.Keys)
      {
        if (String.IsNullOrWhiteSpace(dict[key]))
        {
          continue;
        }

        string value = WebUtility.UrlEncode(dict[key]);

        if (count == dict.Count - 1)
        {
          end = true;
        }

        if (end) buffer.AppendFormat("{0}={1}", key, value);
        else buffer.AppendFormat("{0}={1}&", key, value);

        count++;
      }

      return buffer.ToString();
    }
  }
}
