using FeedlySharp.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace FeedlySharp
{
  public partial class FeedlyClient
  {
    public async Task<List<FeedlyTag>> GetTags(CancellationToken cancellationToken = default(CancellationToken))
    {
      return await Client.AuthRequest<List<FeedlyTag>>(HttpMethod.Get, "v3/tags", null, cancellationToken);
    }


    public async Task<bool> UpdateTags(string entryId, string[] tags, CancellationToken cancellationToken = default(CancellationToken))
    {
      string tagsString = String.Join(",", tags.Select(x => ValueToResource("tag", x)));
      await Client.AuthRequest<object>(HttpMethod.Put, String.Format("v3/tags/{0}", tagsString), new { entryId = entryId }, cancellationToken);
      return true;
    }


    public async Task<bool> UpdateTags(string[] entryIds, string[] tags, CancellationToken cancellationToken = default(CancellationToken))
    {
      string tagsString = String.Join(",", tags.Select(x => ValueToResource("tag", x)));
      await Client.AuthRequest<object>(HttpMethod.Put, String.Format("v3/tags/{0}", tagsString), new { entryIds = entryIds }, cancellationToken);
      return true;
    }


    public async Task<bool> RenameTag(string oldTag, string newTag, CancellationToken cancellationToken = default(CancellationToken))
    {
      await Client.AuthRequest<object>(HttpMethod.Post, String.Format("v3/tags/{0}", ValueToResource("tag", oldTag)), new { label = newTag }, cancellationToken);
      return true;
    }


    public async Task<bool> RemoveTags(string[] entryIds, string[] tags, CancellationToken cancellationToken = default(CancellationToken))
    {
      string tagsString = String.Join(",", tags.Select(x => ValueToResource("tag", x)));
      string entryIdsString = String.Join(",", entryIds.Select(x => WebUtility.UrlEncode(x)));
      await Client.AuthRequest<object>(HttpMethod.Delete, String.Format("v3/tags/{0}/{1}", tagsString, entryIdsString), null, cancellationToken);
      return true;
    }


    public async Task<bool> RemoveTags(string[] tags, CancellationToken cancellationToken = default(CancellationToken))
    {
      string tagsString = String.Join(",", tags.Select(x => ValueToResource("tag", x)));
      await Client.AuthRequest<object>(HttpMethod.Delete, String.Format("v3/tags/{0}", tagsString), null, cancellationToken);
      return true;
    }
  }
}
