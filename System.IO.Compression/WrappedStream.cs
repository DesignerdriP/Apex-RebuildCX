using System;
using System.IO;
using System.Reflection;

namespace System.IO.Compression
{
	internal class WrappedStream : Stream
	{
		private readonly Stream _baseStream;

		private readonly EventHandler _onClosed;

		private bool _canRead;

		private bool _canWrite;

		private bool _canSeek;

		private bool _isDisposed;

		private readonly bool _closeBaseStream;

		public override bool CanRead
		{
			get
			{
				if (!this._canRead)
				{
					return false;
				}
				return this._baseStream.CanRead;
			}
		}

		public override bool CanSeek
		{
			get
			{
				if (!this._canSeek)
				{
					return false;
				}
				return this._baseStream.CanSeek;
			}
		}

		public override bool CanWrite
		{
			get
			{
				if (!this._canWrite)
				{
					return false;
				}
				return this._baseStream.CanWrite;
			}
		}

		public override long Length
		{
			get
			{
				this.ThrowIfDisposed();
				return this._baseStream.Length;
			}
		}

		public override long Position
		{
			get
			{
				this.ThrowIfDisposed();
				return this._baseStream.Position;
			}
			set
			{
				this.ThrowIfDisposed();
				this.ThrowIfCantSeek();
				this._baseStream.Position = value;
			}
		}

		internal WrappedStream(Stream baseStream, bool canRead, bool canWrite, bool canSeek, EventHandler onClosed) : this(baseStream, canRead, canWrite, canSeek, false, onClosed)
		{
		}

		internal WrappedStream(Stream baseStream, bool canRead, bool canWrite, bool canSeek, bool closeBaseStream, EventHandler onClosed)
		{
			this._baseStream = baseStream;
			this._onClosed = onClosed;
			this._canRead = canRead;
			this._canSeek = canSeek;
			this._canWrite = canWrite;
			this._isDisposed = false;
			this._closeBaseStream = closeBaseStream;
		}

		internal WrappedStream(Stream baseStream, EventHandler onClosed) : this(baseStream, true, true, true, onClosed)
		{
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && !this._isDisposed)
			{
				if (this._onClosed != null)
				{
					this._onClosed(this, null);
				}
				if (this._closeBaseStream)
				{
					this._baseStream.Dispose();
				}
				this._canRead = false;
				this._canWrite = false;
				this._canSeek = false;
				this._isDisposed = true;
			}
			base.Dispose(disposing);
		}

		public override void Flush()
		{
			this.ThrowIfDisposed();
			this.ThrowIfCantWrite();
			this._baseStream.Flush();
		}

		public override int Read(byte[] buffer, int offset, int count)
		{
			this.ThrowIfDisposed();
			this.ThrowIfCantRead();
			return this._baseStream.Read(buffer, offset, count);
		}

		public override long Seek(long offset, SeekOrigin origin)
		{
			this.ThrowIfDisposed();
			this.ThrowIfCantSeek();
			return this._baseStream.Seek(offset, origin);
		}

		public override void SetLength(long value)
		{
			this.ThrowIfDisposed();
			this.ThrowIfCantSeek();
			this.ThrowIfCantWrite();
			this._baseStream.SetLength(value);
		}

		private void ThrowIfCantRead()
		{
			if (!this.CanWrite)
			{
				throw new NotSupportedException(Messages.WritingNotSupported);
			}
		}

		private void ThrowIfCantSeek()
		{
			if (!this.CanSeek)
			{
				throw new NotSupportedException(Messages.SeekingNotSupported);
			}
		}

		private void ThrowIfCantWrite()
		{
			if (!this.CanWrite)
			{
				throw new NotSupportedException(Messages.WritingNotSupported);
			}
		}

		private void ThrowIfDisposed()
		{
			if (this._isDisposed)
			{
				throw new ObjectDisposedException(base.GetType().Name, Messages.HiddenStreamName);
			}
		}

		public override void Write(byte[] buffer, int offset, int count)
		{
			this.ThrowIfDisposed();
			this.ThrowIfCantWrite();
			this._baseStream.Write(buffer, offset, count);
		}
	}
}