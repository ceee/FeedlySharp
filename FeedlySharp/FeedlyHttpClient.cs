using FeedlySharp.Extensions;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FeedlySharp
{
  internal class FeedlyHttpClient : HttpClient
  {
    public string AccessToken { get; set; }


    public FeedlyHttpClient(Uri baseUri) : base()
    {
      BaseAddress = baseUri;
      //DefaultRequestHeaders.Add("Accept", "application/json");
    }


    public async Task<T> Request<T>(
      HttpMethod method, 
      string requestUri, 
      object body = null, 
      bool bodyAsJson = false, 
      bool isOauth = true,
      CancellationToken cancellationToken = default(CancellationToken)
    ) where T : class, new()
    {
      string responseString = await Request(method, requestUri, body, bodyAsJson, isOauth, cancellationToken);

      if (responseString == "[]")
      {
        return new T();
      }
      if ((new string[] { "", "{}" }).Contains(responseString))
      {
        return null;
      }

      return DeserializeJson<T>(responseString);
    }


    public async Task<string> Request(
      HttpMethod method, 
      string requestUri, 
      object body = null, 
      bool bodyAsJson = false, 
      bool isOauth = true,
      CancellationToken cancellationToken = default(CancellationToken)
    )
    {
      string append = body != null && !bodyAsJson && method == HttpMethod.Get ? ((requestUri.Contains("?") ? "&" : "?") + (body as Dictionary<string, string>).ToQueryString()) : "";
      HttpRequestMessage request = new HttpRequestMessage(method, requestUri + append);

      // content of the request
      if (body != null && !bodyAsJson && method != HttpMethod.Get)
      {
        request.Content = new FormUrlEncodedContent(body as Dictionary<string, string>);
      }
      else if (body != null && method != HttpMethod.Get)
      {
        request.Content = new StringContent(JsonConvert.SerializeObject(body));
      }

      // OAuth header
      if (isOauth)
      {
        request.Headers.Add("Authorization", String.Format("OAuth {0}", AccessToken));
      }

      return await Request(request, cancellationToken);
    }


    public async Task<string> Request(HttpRequestMessage request, CancellationToken cancellationToken = default(CancellationToken))
    {
      HttpResponseMessage response = null;
      string responseString = null;

      // make async request
      try
      {
        response = await SendAsync(request, cancellationToken);

        // validate HTTP response
        ValidateResponse(response);

        // read response
        responseString = await response.Content.ReadAsStringAsync();
      }
      catch (HttpRequestException exc)
      {
        throw new FeedlySharpException(exc.Message, exc);
      }
      catch (FeedlySharpException exc)
      {
        throw exc;
      }
      finally
      {
        request.Dispose();

        if (response != null)
        {
          response.Dispose();
        }
      }

      return responseString;
    }


    /// <summary>
    /// Converts JSON to Pocket objects
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="json">Raw JSON response</param>
    /// <returns></returns>
    /// <exception cref="PocketException">Parse error.</exception>
    private T DeserializeJson<T>(string json) where T : class, new()
    {
      json = json.Replace("[]", "{}");

      // deserialize object
      T parsedResponse = JsonConvert.DeserializeObject<T>(
        json,
        new JsonSerializerSettings
        {
          Error = (object sender, ErrorEventArgs args) =>
          {
            throw new FeedlySharpException(String.Format("Parse error: {0}", args.ErrorContext.Error.Message));
          },
          Converters =
          {
            new BoolConverter(),
            new UnixDateTimeConverter(),
            new TimeSpanConverter(),
            new NullableIntConverter(),
            new UriConverter()
          }
        }
      );

      return parsedResponse;
    }



    /// <summary>
    /// Validates the response.
    /// </summary>
    /// <param name="response">The response.</param>
    /// <returns></returns>
    /// <exception cref="PocketException">
    /// Error retrieving response
    /// </exception>
    private void ValidateResponse(HttpResponseMessage response)
    {
      // no error found
      if (response.IsSuccessStatusCode)
      {
        return;
      }

      throw new Exception(response.StatusCode.ToString()); // TODO
    }


    /// <summary>
    /// Tries to fetch a header value.
    /// </summary>
    /// <param name="headers">The headers.</param>
    /// <param name="key">The key.</param>
    /// <returns></returns>
    private string TryGetHeaderValue(HttpResponseHeaders headers, string key)
    {
      string result = null;

      if (headers == null || String.IsNullOrEmpty(key))
      {
        return null;
      }

      foreach (var header in headers)
      {
        if (header.Key == key)
        {
          var headerEnumerator = header.Value.GetEnumerator();
          headerEnumerator.MoveNext();

          result = headerEnumerator.Current;
          break;
        }
      }

      return result;
    }
  }
}
