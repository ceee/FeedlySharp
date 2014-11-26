using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace FeedlySharp
{
  public partial class FeedlyClient
  {
    /// <summary>
    /// Get the preferences of the user.
    /// Applications can store and sync some user preferences. For example, Feedly stores the views users assign to each category and feed in the preferences module. Preferences are application specific - applications can not share preferences.
    /// An application can save up to 10,000 preference knobs per user.
    /// </summary>
    /// <remarks>preferences-endpoint (https://developer.feedly.com/v3/preferences/#get-the-preferences-of-the-user)</remarks>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A string dictionary, as the preferences list is dynamic and different for every application.</returns>
    public async Task<Dictionary<string, string>> GetPreferences(CancellationToken cancellationToken = default(CancellationToken))
    {
      return (await Client.Request<Dictionary<string, string>>(HttpMethod.Get, "v3/preferences", null, false, true, cancellationToken)) ?? new Dictionary<string, string>();
    }


    /// <summary>
    /// Update the preferences of the user.
    /// </summary>
    /// <remarks>preferences-endpoint (https://developer.feedly.com/v3/preferences/#update-the-preferences-of-the-user)</remarks>
    /// <param name="preferences">The new or updated preferences. If you wish to delete a key, you can pass the special value "==DELETE==".</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A string dictionary, as the preferences list is dynamic and different for every application.</returns>
    public async Task<Dictionary<string, string>> UpdatePreferences(Dictionary<string, string> preferences, CancellationToken cancellationToken = default(CancellationToken))
    {
      return (await Client.Request<Dictionary<string, string>>(HttpMethod.Post, "v3/preferences", preferences, true, true, cancellationToken)) ?? new Dictionary<string, string>();
    }
  }
}
