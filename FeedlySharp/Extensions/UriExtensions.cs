using System;
using System.Collections.Generic;
using System.Net;

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
  }
}
