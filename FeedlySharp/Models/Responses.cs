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


  public class AccessTokenResponse
  {
    public string Id { get; set; }

    public string AccessToken { get; set; }

    public int ExpiresIn { get; set; }

    public string RefreshToken { get; set; }

    public FeedlyUserAccountPlan UserAccountPlan { get; set; } 
  }


  public enum FeedlyUserAccountPlan
  {
    Standard,
    Pro
  }
}
