using System;
using System.Threading;

namespace FeedlySharp
{
  public interface IFeedlyClient
  {
    void Activate(string accessToken, string userId);
    System.Threading.Tasks.Task<bool> AddOrUpdateSubscription(string id, System.Collections.Generic.List<FeedlySharp.Models.FeedlyCategory> categories = null, string optionalTitle = null, System.Threading.CancellationToken cancellationToken = default(CancellationToken));
    System.Threading.Tasks.Task<bool> AddOrUpdateTopic(string topic, FeedlySharp.Models.Interest interest = Interest.Low, System.Threading.CancellationToken cancellationToken = default(CancellationToken));
    System.Threading.Tasks.Task DeleteCategory(string id, System.Threading.CancellationToken cancellationToken = default(CancellationToken));
    void Dispose();
    string GetAuthenticationUri(string scope = "subscriptions", string state = default(String));
    System.Threading.Tasks.Task<System.Collections.Generic.List<FeedlySharp.Models.FeedlyCategory>> GetCategories(System.Threading.CancellationToken cancellationToken = default(CancellationToken));
    System.Threading.Tasks.Task<System.Collections.Generic.List<FeedlySharp.Models.FeedlyEntry>> GetEntries(string[] ids, System.Threading.CancellationToken cancellationToken = default(CancellationToken));
    System.Threading.Tasks.Task<FeedlySharp.Models.FeedlyEntry> GetEntry(string id, System.Threading.CancellationToken cancellationToken = default(CancellationToken));
    System.Threading.Tasks.Task<FeedlySharp.Models.FeedlyFeed> GetFeed(string id, System.Threading.CancellationToken cancellationToken = default(CancellationToken));
    System.Threading.Tasks.Task<System.Collections.Generic.List<FeedlySharp.Models.FeedlyFeed>> GetFeeds(string[] ids, System.Threading.CancellationToken cancellationToken = default(CancellationToken));
    System.Threading.Tasks.Task<string> GetOPML(System.Threading.CancellationToken cancellationToken = default(CancellationToken));
    System.Threading.Tasks.Task<System.Collections.Generic.Dictionary<string, string>> GetPreferences(System.Threading.CancellationToken cancellationToken = default(CancellationToken));
    System.Threading.Tasks.Task<FeedlySharp.Models.FeedlyUser> GetProfile(System.Threading.CancellationToken cancellationToken = default(CancellationToken));
    System.Threading.Tasks.Task<System.Collections.Generic.List<FeedlySharp.Models.FeedlySubscription>> GetSubscriptions(System.Threading.CancellationToken cancellationToken = default(CancellationToken));
    System.Threading.Tasks.Task<System.Collections.Generic.List<FeedlySharp.Models.FeedlyTag>> GetTags(System.Threading.CancellationToken cancellationToken = default(CancellationToken));
    System.Threading.Tasks.Task<System.Collections.Generic.List<FeedlySharp.Models.FeedlyTopic>> GetTopics(System.Threading.CancellationToken cancellationToken = default(CancellationToken));
    System.Threading.Tasks.Task<bool> ImportOPML(string opml, System.Threading.CancellationToken cancellationToken = default(CancellationToken));
    FeedlySharp.Models.AuthenticationResponse ParseAuthenticationResponseUri(string uri);
    FeedlySharp.Models.AuthenticationResponse ParseAuthenticationResponseUri(Uri uri);
    System.Threading.Tasks.Task<bool> RemoveSubscription(string id, System.Threading.CancellationToken cancellationToken = default(CancellationToken));
    System.Threading.Tasks.Task<bool> RemoveTags(string[] entryIds, string[] tags, System.Threading.CancellationToken cancellationToken = default(CancellationToken));
    System.Threading.Tasks.Task<bool> RemoveTags(string[] tags, System.Threading.CancellationToken cancellationToken = default(CancellationToken));
    System.Threading.Tasks.Task<bool> RemoveTopic(string topic, FeedlySharp.Models.Interest interest = Interest.Low, System.Threading.CancellationToken cancellationToken = default(CancellationToken));
    System.Threading.Tasks.Task RenameCategory(string id, string label, System.Threading.CancellationToken cancellationToken = default(CancellationToken));
    System.Threading.Tasks.Task<bool> RenameTag(string oldTag, string newTag, System.Threading.CancellationToken cancellationToken = default(CancellationToken));
    System.Threading.Tasks.Task<FeedlySharp.Models.AccessTokenResponse> RequestAccessToken(string authenticationCode, System.Threading.CancellationToken cancellationToken = default(CancellationToken));
    System.Threading.Tasks.Task<FeedlySharp.Models.AccessTokenResponse> RequestRefreshToken(string refreshToken, System.Threading.CancellationToken cancellationToken = default(CancellationToken));
    System.Threading.Tasks.Task RevokeRefreshToken(string refreshToken, System.Threading.CancellationToken cancellationToken = default(CancellationToken));
    System.Threading.Tasks.Task<System.Collections.Generic.Dictionary<string, string>> UpdatePreferences(System.Collections.Generic.Dictionary<string, string> preferences, System.Threading.CancellationToken cancellationToken = default(CancellationToken));
    System.Threading.Tasks.Task<FeedlySharp.Models.FeedlyUser> UpdateProfile(System.Collections.Generic.Dictionary<string, string> parameters, System.Threading.CancellationToken cancellationToken = default(CancellationToken));
    System.Threading.Tasks.Task<bool> UpdateTags(string entryId, string[] tags, System.Threading.CancellationToken cancellationToken = default(CancellationToken));
    System.Threading.Tasks.Task<bool> UpdateTags(string[] entryIds, string[] tags, System.Threading.CancellationToken cancellationToken = default(CancellationToken));
  }
}
