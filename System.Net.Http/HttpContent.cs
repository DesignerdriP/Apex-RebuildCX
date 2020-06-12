using System;
using System.IO;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace System.Net.Http
{
	public abstract class HttpContent : IDisposable
	{
		internal const long MaxBufferSize = 2147483647L;

		internal readonly static Encoding DefaultStringEncoding;

		public HttpContentHeaders Headers
		{
			get
			{
			}
		}

		static HttpContent()
		{
		}

		protected HttpContent()
		{
		}

		internal void CopyTo(Stream stream)
		{
		}

		public Task CopyToAsync(Stream stream, TransportContext context)
		{
		}

		public Task CopyToAsync(Stream stream)
		{
		}

		protected virtual Task<Stream> CreateContentReadStreamAsync()
		{
		}

		protected virtual void Dispose(bool disposing)
		{
		}

		public void Dispose()
		{
		}

		public Task LoadIntoBufferAsync()
		{
		}

		public Task LoadIntoBufferAsync(long maxBufferSize)
		{
		}

		public Task<byte[]> ReadAsByteArrayAsync()
		{
		}

		public Task<Stream> ReadAsStreamAsync()
		{
		}

		public Task<string> ReadAsStringAsync()
		{
		}

		protected abstract Task SerializeToStreamAsync(Stream stream, TransportContext context);

		protected internal abstract bool TryComputeLength(out long length);
	}
}