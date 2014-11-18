using Newtonsoft.Json;
using System;

namespace FeedlySharp.Models
{
  [JsonObject]
  public class FeedlyUser
  {
    public string Id { get; set; }

    public string Email { get; set; }

    public string GivenName { get; set; }

    public string FamilyName { get; set; }

    public string FullName { get; set; }

    public Uri Picture { get; set; }

    public Gender Gender { get; set; }

    public string Locale { get; set; }

    [JsonProperty("google")]
    public string OAuthGoogleId { get; set; }

    [JsonProperty("reader")]
    public string OAuthGoogleReaderId { get; set; }

    [JsonProperty("twitterUserId")]
    public string OAuthTwitterId { get; set; }

    [JsonProperty("facebookUserId")]
    public string OAuthFacebookId { get; set; }

    [JsonProperty("wordPressId")]
    public string OAuthWordpressId { get; set; }

    [JsonProperty("windowsLiveId")]
    public string OAuthMicrosoftId { get; set; }

    public string Wave { get; set; }

    public string Client { get; set; }

    [JsonProperty("source")]
    public string ClientSource { get; set; }

    [JsonProperty("created")]
    public DateTime? CreationDate { get; set; }

    [JsonProperty("product")]
    public ProProduct ProProduct { get; set; }

    [JsonProperty("productExpiration")]
    public TimeSpan? ProExpiration { get; set; }

    [JsonProperty("subscriptionStatus")]
    public ProStatus ProStatus { get; set; }

    [JsonProperty("evernoteConnected")]
    public bool IsEvernoteConnected { get; set; }

    [JsonProperty("pocketConnected")]
    public bool IsPocketConnected { get; set; }

    [JsonProperty("dropboxConnected")]
    public bool IsDropboxConnected { get; set; }

    [JsonProperty("facebookConnected")]
    public bool IsFacebookConnected { get; set; }

    [JsonProperty("twitterConnected")]
    public bool IsTwitterConnected { get; set; }

    [JsonProperty("windowsLiveConnected")]
    public bool IsMicrosoftConnected { get; set; }

    [JsonProperty("wordPressConnected")]
    public bool IsWordpressConnected { get; set; }
  }

  public enum Gender
  {
    Unknown,
    Male,
    Female
  }

  public enum ProProduct
  {
    None,
    FeedlyProMonthly,
    FeedlyProYearly,
    FeedlyProLifetime
  }

  public enum ProStatus
  {
    None,
    Active,
    PastDue,
    Canceled,
    Unpaid,
    Deleted,
    Expired
  }
}
