using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace System.Net.Http
{
	public class MultipartContent : HttpContent, IEnumerable<HttpContent>, IEnumerable
	{
		public MultipartContent()
		{
		}

		public MultipartContent(string subtype)
		{
		}

		public MultipartContent(string subtype, string boundary)
		{
		}

		public virtual void Add(HttpContent content)
		{
		}

		protected override void Dispose(bool disposing)
		{
		}

		public IEnumerator<HttpContent> GetEnumerator()
		{
		}

		protected override Task SerializeToStreamAsync(Stream stream, TransportContext context)
		{
		}

		IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
		}

		protected internal override bool TryComputeLength(out long length)
		{
		}
	}
}