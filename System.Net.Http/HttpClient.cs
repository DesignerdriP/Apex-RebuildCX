using System;
using System.IO;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace System.Net.Http
{
	public class HttpClient : HttpMessageInvoker
	{
		public Uri BaseAddress
		{
			get
			{
			}
			set
			{
			}
		}

		public HttpRequestHeaders DefaultRequestHeaders
		{
			get
			{
			}
		}

		public long MaxResponseContentBufferSize
		{
			get
			{
			}
			set
			{
			}
		}

		public TimeSpan Timeout
		{
			get
			{
			}
			set
			{
			}
		}

		static HttpClient()
		{
		}

		public HttpClient()
		{
		}

		public HttpClient(HttpMessageHandler handler)
		{
		}

		public HttpClient(HttpMessageHandler handler, bool disposeHandler)
		{
		}

		public void CancelPendingRequests()
		{
		}

		public Task<HttpResponseMessage> DeleteAsync(string requestUri)
		{
		}

		public Task<HttpResponseMessage> DeleteAsync(Uri requestUri)
		{
		}

		public Task<HttpResponseMessage> DeleteAsync(string requestUri, CancellationToken cancellationToken)
		{
		}

		public Task<HttpResponseMessage> DeleteAsync(Uri requestUri, CancellationToken cancellationToken)
		{
		}

		protected override void Dispose(bool disposing)
		{
		}

		public Task<HttpResponseMessage> GetAsync(string requestUri)
		{
		}

		public Task<HttpResponseMessage> GetAsync(Uri requestUri)
		{
		}

		public Task<HttpResponseMessage> GetAsync(string requestUri, HttpCompletionOption completionOption)
		{
		}

		public Task<HttpResponseMessage> GetAsync(Uri requestUri, HttpCompletionOption completionOption)
		{
		}

		public Task<HttpResponseMessage> GetAsync(string requestUri, CancellationToken cancellationToken)
		{
		}

		public Task<HttpResponseMessage> GetAsync(Uri requestUri, CancellationToken cancellationToken)
		{
		}

		public Task<HttpResponseMessage> GetAsync(string requestUri, HttpCompletionOption completionOption, CancellationToken cancellationToken)
		{
		}

		public Task<HttpResponseMessage> GetAsync(Uri requestUri, HttpCompletionOption completionOption, CancellationToken cancellationToken)
		{
		}

		public Task<byte[]> GetByteArrayAsync(string requestUri)
		{
		}

		public Task<byte[]> GetByteArrayAsync(Uri requestUri)
		{
		}

		public Task<Stream> GetStreamAsync(string requestUri)
		{
		}

		public Task<Stream> GetStreamAsync(Uri requestUri)
		{
		}

		public Task<string> GetStringAsync(string requestUri)
		{
		}

		public Task<string> GetStringAsync(Uri requestUri)
		{
		}

		public Task<HttpResponseMessage> PostAsync(string requestUri, HttpContent content)
		{
		}

		public Task<HttpResponseMessage> PostAsync(Uri requestUri, HttpContent content)
		{
		}

		public Task<HttpResponseMessage> PostAsync(string requestUri, HttpContent content, CancellationToken cancellationToken)
		{
		}

		public Task<HttpResponseMessage> PostAsync(Uri requestUri, HttpContent content, CancellationToken cancellationToken)
		{
		}

		public Task<HttpResponseMessage> PutAsync(string requestUri, HttpContent content)
		{
		}

		public Task<HttpResponseMessage> PutAsync(Uri requestUri, HttpContent content)
		{
		}

		public Task<HttpResponseMessage> PutAsync(string requestUri, HttpContent content, CancellationToken cancellationToken)
		{
		}

		public Task<HttpResponseMessage> PutAsync(Uri requestUri, HttpContent content, CancellationToken cancellationToken)
		{
		}

		public Task<HttpResponseMessage> SendAsync(HttpRequestMessage request)
		{
		}

		public override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
		{
		}

		public Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, HttpCompletionOption completionOption)
		{
		}

		public Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, HttpCompletionOption completionOption, CancellationToken cancellationToken)
		{
		}
	}
}