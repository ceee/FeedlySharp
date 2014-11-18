using FeedlySharp.Extensions;
using Newtonsoft.Json;
using System;

namespace FeedlySharp.Models
{
  public class AuthenticationResponse
  {
    public bool IsSuccess { get { return !String.IsNullOrWhiteSpace(Code); } }

    public string Error { get; set; }

    public string Code { get; set; }

    public string State { get; set; }
  }


  [JsonObject]
  public class AccessTokenResponse
  {
    public string Id { get; set; }

    [JsonProperty("access_token")]
    public string AccessToken { get; set; }

    [JsonProperty("expires_in")]
    [JsonConverter(typeof(TimeSpanConverter))]
    public TimeSpan ExpiresIn { get; set; }

    [JsonProperty("refresh_token")]
    public string RefreshToken { get; set; }

    [JsonProperty("plan")]
    public FeedlyUserAccountPlan UserAccountPlan { get; set; } 
  }


  public enum FeedlyUserAccountPlan
  {
    Standard,
    Pro
  }
}
