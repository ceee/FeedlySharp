using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace FeedlySharp.Tests
{
  public class TestsBase : IDisposable
  {
    protected FeedlyClient client;

    // setup
    public TestsBase()
    {
      
    }


    // teardown
    public void Dispose()
    {
      
    }


    // async throws
    public static async Task ThrowsAsync<TException>(Func<Task> func)
    {
      var expected = typeof(TException);
      Type actual = null;
      try
      {
        await func();
      }
      catch (Exception e)
      {
        actual = e.GetType();
      }
      Assert.Equal(expected, actual);
    }
  }
}
