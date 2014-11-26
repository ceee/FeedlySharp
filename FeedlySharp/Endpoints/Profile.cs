using FeedlySharp.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace FeedlySharp
{
  public partial class FeedlyClient
  {
    /// <summary>
    /// Get the profile of the user.
    /// </summary>
    /// <remarks>profile-endpoint (https://developer.feedly.com/v3/profile/#get-the-profile-of-the-user)</remarks>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    public async Task<FeedlyUser> GetProfile(CancellationToken cancellationToken = default(CancellationToken))
    {
      return await Client.Request<FeedlyUser>(HttpMethod.Get, "v3/profile", null, false, true, cancellationToken);
    }

    /// <summary>
    /// Update the profile of the user.
    /// </summary>
    /// <remarks>profile-endpoint (https://developer.feedly.com/v3/profile/#update-the-profile-of-the-user)</remarks>
    /// <param name="parameters">The update profile parameters, see https://developer.feedly.com/v3/profile/#update-the-profile-of-the-user </param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    public async Task<FeedlyUser> UpdateProfile(Dictionary<string, string> parameters, CancellationToken cancellationToken = default(CancellationToken))
    {
      return await Client.Request<FeedlyUser>(HttpMethod.Post, "v3/profile", parameters, true, true, cancellationToken);
    }
  }
}
