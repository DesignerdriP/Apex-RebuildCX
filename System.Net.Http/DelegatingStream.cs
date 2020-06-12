using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace System.Net.Http
{
	internal abstract class DelegatingStream : Stream
	{
		public override bool CanRead
		{
			get
			{
			}
		}

		public override bool CanSeek
		{
			get
			{
			}
		}

		public override bool CanTimeout
		{
			get
			{
			}
		}

		public override bool CanWrite
		{
			get
			{
			}
		}

		public override long Length
		{
			get
			{
			}
		}

		public override long Position
		{
			get
			{
			}
			set
			{
			}
		}

		public override int ReadTimeout
		{
			get
			{
			}
			set
			{
			}
		}

		public override int WriteTimeout
		{
			get
			{
			}
			set
			{
			}
		}

		protected DelegatingStream(Stream innerStream)
		{
		}

		public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
		}

		public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
		}

		protected override void Dispose(bool disposing)
		{
		}

		public override int EndRead(IAsyncResult asyncResult)
		{
		}

		public override void EndWrite(IAsyncResult asyncResult)
		{
		}

		public override void Flush()
		{
		}

		public override Task FlushAsync(CancellationToken cancellationToken)
		{
		}

		public override int Read(byte[] buffer, int offset, int count)
		{
		}

		public override Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
		}

		public override int ReadByte()
		{
		}

		public override long Seek(long offset, SeekOrigin origin)
		{
		}

		public override void SetLength(long value)
		{
		}

		public override void Write(byte[] buffer, int offset, int count)
		{
		}

		public override Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
		}

		public override void WriteByte(byte value)
		{
		}
	}
}