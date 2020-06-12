using System;
using System.Threading;
using System.Threading.Tasks;

namespace System.Net.Http
{
	public class HttpMessageInvoker : IDisposable
	{
		public HttpMessageInvoker(HttpMessageHandler handler)
		{
		}

		public HttpMessageInvoker(HttpMessageHandler handler, bool disposeHandler)
		{
		}

		public void Dispose()
		{
		}

		protected virtual void Dispose(bool disposing)
		{
		}

		public virtual Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
		{
		}
	}
}