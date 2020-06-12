using System;
using System.Threading;
using System.Threading.Tasks;

namespace System.Net.Http
{
	public abstract class HttpMessageHandler : IDisposable
	{
		protected HttpMessageHandler()
		{
		}

		protected virtual void Dispose(bool disposing)
		{
		}

		public void Dispose()
		{
		}

		protected internal abstract Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken);
	}
}