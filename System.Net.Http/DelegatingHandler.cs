using System;
using System.Threading;
using System.Threading.Tasks;

namespace System.Net.Http
{
	public abstract class DelegatingHandler : HttpMessageHandler
	{
		public HttpMessageHandler InnerHandler
		{
			get
			{
			}
			set
			{
			}
		}

		protected DelegatingHandler()
		{
		}

		protected DelegatingHandler(HttpMessageHandler innerHandler)
		{
		}

		protected override void Dispose(bool disposing)
		{
		}

		protected internal override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
		{
		}
	}
}