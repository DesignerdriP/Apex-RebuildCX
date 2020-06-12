using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace System.Net.Http
{
	public class StreamContent : HttpContent
	{
		public StreamContent(Stream content)
		{
		}

		public StreamContent(Stream content, int bufferSize)
		{
		}

		protected override Task<Stream> CreateContentReadStreamAsync()
		{
		}

		protected override void Dispose(bool disposing)
		{
		}

		protected override Task SerializeToStreamAsync(Stream stream, TransportContext context)
		{
		}

		protected internal override bool TryComputeLength(out long length)
		{
		}
	}
}