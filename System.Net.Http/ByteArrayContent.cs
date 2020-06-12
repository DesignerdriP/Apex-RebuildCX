using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace System.Net.Http
{
	public class ByteArrayContent : HttpContent
	{
		public ByteArrayContent(byte[] content)
		{
		}

		public ByteArrayContent(byte[] content, int offset, int count)
		{
		}

		protected override Task<Stream> CreateContentReadStreamAsync()
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