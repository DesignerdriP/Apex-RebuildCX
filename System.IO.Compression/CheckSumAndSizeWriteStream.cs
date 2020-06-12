using System;
using System.IO;
using System.Reflection;

namespace System.IO.Compression
{
	internal class CheckSumAndSizeWriteStream : Stream
	{
		private readonly Stream _baseStream;

		private readonly Stream _baseBaseStream;

		private long _position;

		private uint _checksum;

		private readonly bool _leaveOpenOnClose;

		private bool _canWrite;

		private bool _isDisposed;

		private bool _everWritten;

		private long _initialPosition;

		private readonly Action<long, long, uint> _saveCrcAndSizes;

		public override bool CanRead
		{
			get
			{
				return false;
			}
		}

		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		public override bool CanWrite
		{
			get
			{
				return this._canWrite;
			}
		}

		public override long Length
		{
			get
			{
				this.ThrowIfDisposed();
				throw new NotSupportedException(Messages.SeekingNotSupported);
			}
		}

		public override long Position
		{
			get
			{
				this.ThrowIfDisposed();
				return this._position;
			}
			set
			{
				this.ThrowIfDisposed();
				throw new NotSupportedException(Messages.SeekingNotSupported);
			}
		}

		public CheckSumAndSizeWriteStream(Stream baseStream, Stream baseBaseStream, bool leaveOpenOnClose, Action<long, long, uint> saveCrcAndSizes)
		{
			this._baseStream = baseStream;
			this._baseBaseStream = baseBaseStream;
			this._position = (long)0;
			this._checksum = 0;
			this._leaveOpenOnClose = leaveOpenOnClose;
			this._canWrite = true;
			this._isDisposed = false;
			this._initialPosition = (long)0;
			this._saveCrcAndSizes = saveCrcAndSizes;
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && !this._isDisposed)
			{
				if (!this._everWritten)
				{
					this._initialPosition = this._baseBaseStream.Position;
				}
				if (!this._leaveOpenOnClose)
				{
					this._baseStream.Close();
				}
				if (this._saveCrcAndSizes != null)
				{
					this._saveCrcAndSizes(this._initialPosition, this.Position, this._checksum);
				}
				this._isDisposed = true;
			}
			base.Dispose(disposing);
		}

		public override void Flush()
		{
			this.ThrowIfDisposed();
			this._baseStream.Flush();
		}

		public override int Read(byte[] buffer, int offset, int count)
		{
			this.ThrowIfDisposed();
			throw new NotSupportedException(Messages.ReadingNotSupported);
		}

		public override long Seek(long offset, SeekOrigin origin)
		{
			this.ThrowIfDisposed();
			throw new NotSupportedException(Messages.SeekingNotSupported);
		}

		public override void SetLength(long value)
		{
			this.ThrowIfDisposed();
			throw new NotSupportedException(Messages.SetLengthRequiresSeekingAndWriting);
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
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset", Messages.ArgumentNeedNonNegative);
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", Messages.ArgumentNeedNonNegative);
			}
			if ((int)buffer.Length - offset < count)
			{
				throw new ArgumentException(Messages.OffsetLengthInvalid);
			}
			this.ThrowIfDisposed();
			if (count == 0)
			{
				return;
			}
			if (!this._everWritten)
			{
				this._initialPosition = this._baseBaseStream.Position;
				this._everWritten = true;
			}
			this._checksum = Crc32Helper.UpdateCrc32(this._checksum, buffer, offset, count);
			this._baseStream.Write(buffer, offset, count);
			this._position += (long)count;
		}
	}
}