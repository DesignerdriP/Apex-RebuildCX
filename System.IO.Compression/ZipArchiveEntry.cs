using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;

namespace System.IO.Compression
{
	[__DynamicallyInvokable]
	public class ZipArchiveEntry
	{
		private const ushort DefaultVersionToExtract = 10;

		private ZipArchive _archive;

		private readonly bool _originallyInArchive;

		private readonly int _diskNumberStart;

		private ZipVersionNeededValues _versionToExtract;

		private ZipArchiveEntry.BitFlagValues _generalPurposeBitFlag;

		private ZipArchiveEntry.CompressionMethodValues _storedCompressionMethod;

		private DateTimeOffset _lastModified;

		private long _compressedSize;

		private long _uncompressedSize;

		private long _offsetOfLocalHeader;

		private long? _storedOffsetOfCompressedData;

		private uint _crc32;

		private byte[] _compressedBytes;

		private MemoryStream _storedUncompressedData;

		private bool _currentlyOpenForWrite;

		private bool _everOpenedForWrite;

		private Stream _outstandingWriteStream;

		private uint _externalFileAttr;

		private string _storedEntryName;

		private byte[] _storedEntryNameBytes;

		private List<ZipGenericExtraField> _cdUnknownExtraFields;

		private List<ZipGenericExtraField> _lhUnknownExtraFields;

		private byte[] _fileComment;

		private CompressionLevel? _compressionLevel;

		[__DynamicallyInvokable]
		public ZipArchive Archive
		{
			[__DynamicallyInvokable]
			get
			{
				return this._archive;
			}
		}

		[__DynamicallyInvokable]
		public long CompressedLength
		{
			[__DynamicallyInvokable]
			get
			{
				if (this._everOpenedForWrite)
				{
					throw new InvalidOperationException(Messages.LengthAfterWrite);
				}
				return this._compressedSize;
			}
		}

		private ZipArchiveEntry.CompressionMethodValues CompressionMethod
		{
			get
			{
				return this._storedCompressionMethod;
			}
			set
			{
				if (value == ZipArchiveEntry.CompressionMethodValues.Deflate)
				{
					this.VersionToExtractAtLeast(ZipVersionNeededValues.ExplicitDirectory);
				}
				this._storedCompressionMethod = value;
			}
		}

		internal bool EverOpenedForWrite
		{
			get
			{
				return this._everOpenedForWrite;
			}
		}

		public int ExternalAttributes
		{
			get
			{
				return (int)this._externalFileAttr;
			}
			set
			{
				this.ThrowIfInvalidArchive();
				this._externalFileAttr = (uint)value;
			}
		}

		[__DynamicallyInvokable]
		public string FullName
		{
			[__DynamicallyInvokable]
			get
			{
				return this._storedEntryName;
			}
			private set
			{
				bool flag;
				if (value == null)
				{
					throw new ArgumentNullException("FullName");
				}
				this._storedEntryNameBytes = this.EncodeEntryName(value, out flag);
				this._storedEntryName = value;
				if (!flag)
				{
					this._generalPurposeBitFlag &= (ZipArchiveEntry.BitFlagValues)63487;
				}
				else
				{
					this._generalPurposeBitFlag |= ZipArchiveEntry.BitFlagValues.UnicodeFileName;
				}
				if (ZipHelper.EndsWithDirChar(value))
				{
					this.VersionToExtractAtLeast(ZipVersionNeededValues.ExplicitDirectory);
				}
			}
		}

		[__DynamicallyInvokable]
		public DateTimeOffset LastWriteTime
		{
			[__DynamicallyInvokable]
			get
			{
				return this._lastModified;
			}
			[__DynamicallyInvokable]
			set
			{
				this.ThrowIfInvalidArchive();
				if (this._archive.Mode == ZipArchiveMode.Read)
				{
					throw new NotSupportedException(Messages.ReadOnlyArchive);
				}
				if (this._archive.Mode == ZipArchiveMode.Create && this._everOpenedForWrite)
				{
					throw new IOException(Messages.FrozenAfterWrite);
				}
				if (value.DateTime.Year < 1980 || value.DateTime.Year > 2107)
				{
					throw new ArgumentOutOfRangeException("value", Messages.DateTimeOutOfRange);
				}
				this._lastModified = value;
			}
		}

		[__DynamicallyInvokable]
		public long Length
		{
			[__DynamicallyInvokable]
			get
			{
				if (this._everOpenedForWrite)
				{
					throw new InvalidOperationException(Messages.LengthAfterWrite);
				}
				return this._uncompressedSize;
			}
		}

		[__DynamicallyInvokable]
		public string Name
		{
			[__DynamicallyInvokable]
			get
			{
				return Path.GetFileName(this.FullName);
			}
		}

		private long OffsetOfCompressedData
		{
			get
			{
				if (!this._storedOffsetOfCompressedData.HasValue)
				{
					this._archive.ArchiveStream.Seek(this._offsetOfLocalHeader, SeekOrigin.Begin);
					if (!ZipLocalFileHeader.TrySkipBlock(this._archive.ArchiveReader))
					{
						throw new InvalidDataException(Messages.LocalFileHeaderCorrupt);
					}
					this._storedOffsetOfCompressedData = new long?(this._archive.ArchiveStream.Position);
				}
				return this._storedOffsetOfCompressedData.Value;
			}
		}

		private MemoryStream UncompressedData
		{
			get
			{
				if (this._storedUncompressedData == null)
				{
					this._storedUncompressedData = new MemoryStream((int)this._uncompressedSize);
					if (this._originallyInArchive)
					{
						using (Stream stream = this.OpenInReadMode(false))
						{
							try
							{
								stream.CopyTo(this._storedUncompressedData);
							}
							catch (InvalidDataException invalidDataException)
							{
								this._storedUncompressedData.Dispose();
								this._storedUncompressedData = null;
								this._currentlyOpenForWrite = false;
								this._everOpenedForWrite = false;
								throw;
							}
						}
					}
					this.CompressionMethod = ZipArchiveEntry.CompressionMethodValues.Deflate;
				}
				return this._storedUncompressedData;
			}
		}

		internal ZipArchiveEntry(ZipArchive archive, ZipCentralDirectoryFileHeader cd, CompressionLevel compressionLevel) : this(archive, cd)
		{
			this._compressionLevel = new CompressionLevel?(compressionLevel);
		}

		internal ZipArchiveEntry(ZipArchive archive, ZipCentralDirectoryFileHeader cd)
		{
			this._archive = archive;
			this._originallyInArchive = true;
			this._diskNumberStart = cd.DiskNumberStart;
			this._versionToExtract = (ZipVersionNeededValues)cd.VersionNeededToExtract;
			this._generalPurposeBitFlag = (ZipArchiveEntry.BitFlagValues)cd.GeneralPurposeBitFlag;
			this.CompressionMethod = (ZipArchiveEntry.CompressionMethodValues)cd.CompressionMethod;
			this._lastModified = new DateTimeOffset(ZipHelper.DosTimeToDateTime(cd.LastModified));
			this._compressedSize = cd.CompressedSize;
			this._uncompressedSize = cd.UncompressedSize;
			this._externalFileAttr = cd.ExternalFileAttributes;
			this._offsetOfLocalHeader = cd.RelativeOffsetOfLocalHeader;
			this._storedOffsetOfCompressedData = null;
			this._crc32 = cd.Crc32;
			this._compressedBytes = null;
			this._storedUncompressedData = null;
			this._currentlyOpenForWrite = false;
			this._everOpenedForWrite = false;
			this._outstandingWriteStream = null;
			this.FullName = this.DecodeEntryName(cd.Filename);
			this._lhUnknownExtraFields = null;
			this._cdUnknownExtraFields = cd.ExtraFields;
			this._fileComment = cd.FileComment;
			this._compressionLevel = null;
		}

		internal ZipArchiveEntry(ZipArchive archive, string entryName, CompressionLevel compressionLevel) : this(archive, entryName)
		{
			this._compressionLevel = new CompressionLevel?(compressionLevel);
		}

		internal ZipArchiveEntry(ZipArchive archive, string entryName)
		{
			this._archive = archive;
			this._originallyInArchive = false;
			this._diskNumberStart = 0;
			this._versionToExtract = ZipVersionNeededValues.Default;
			this._generalPurposeBitFlag = (ZipArchiveEntry.BitFlagValues)0;
			this.CompressionMethod = ZipArchiveEntry.CompressionMethodValues.Deflate;
			this._lastModified = DateTimeOffset.Now;
			this._compressedSize = (long)0;
			this._uncompressedSize = (long)0;
			this._externalFileAttr = 0;
			this._offsetOfLocalHeader = (long)0;
			this._storedOffsetOfCompressedData = null;
			this._crc32 = 0;
			this._compressedBytes = null;
			this._storedUncompressedData = null;
			this._currentlyOpenForWrite = false;
			this._everOpenedForWrite = false;
			this._outstandingWriteStream = null;
			this.FullName = entryName;
			this._cdUnknownExtraFields = null;
			this._lhUnknownExtraFields = null;
			this._fileComment = null;
			this._compressionLevel = null;
			if ((int)this._storedEntryNameBytes.Length > 65535)
			{
				throw new ArgumentException(Messages.EntryNamesTooLong);
			}
			if (this._archive.Mode == ZipArchiveMode.Create)
			{
				this._archive.AcquireArchiveStream(this);
			}
		}

		private void CloseStreams()
		{
			if (this._outstandingWriteStream != null)
			{
				this._outstandingWriteStream.Close();
			}
		}

		private string DecodeEntryName(byte[] entryNameBytes)
		{
			Encoding uTF8;
			if ((int)(this._generalPurposeBitFlag & ZipArchiveEntry.BitFlagValues.UnicodeFileName) != 0)
			{
				uTF8 = Encoding.UTF8;
			}
			else
			{
				uTF8 = (this._archive == null ? Encoding.GetEncoding(0) : this._archive.EntryNameEncoding ?? Encoding.GetEncoding(0));
			}
			return new string(uTF8.GetChars(entryNameBytes));
		}

		[__DynamicallyInvokable]
		public void Delete()
		{
			if (this._archive == null)
			{
				return;
			}
			if (this._currentlyOpenForWrite)
			{
				throw new IOException(Messages.DeleteOpenEntry);
			}
			if (this._archive.Mode != ZipArchiveMode.Update)
			{
				throw new NotSupportedException(Messages.DeleteOnlyInUpdate);
			}
			this._archive.ThrowIfDisposed();
			this._archive.RemoveEntry(this);
			this._archive = null;
			this.UnloadStreams();
		}

		private byte[] EncodeEntryName(string entryName, out bool isUTF8)
		{
			Encoding entryNameEncoding;
			if (this._archive == null || this._archive.EntryNameEncoding == null)
			{
				entryNameEncoding = (ZipHelper.RequiresUnicode(entryName) ? Encoding.UTF8 : Encoding.GetEncoding(0));
			}
			else
			{
				entryNameEncoding = this._archive.EntryNameEncoding;
			}
			isUTF8 = (!(entryNameEncoding is UTF8Encoding) ? false : entryNameEncoding.Equals(Encoding.UTF8));
			return entryNameEncoding.GetBytes(entryName);
		}

		private CheckSumAndSizeWriteStream GetDataCompressor(Stream backingStream, bool leaveBackingStreamOpen, EventHandler onClose)
		{
			Stream stream = (this._compressionLevel.HasValue ? new DeflateStream(backingStream, this._compressionLevel.Value, leaveBackingStreamOpen) : new DeflateStream(backingStream, CompressionMode.Compress, leaveBackingStreamOpen));
			bool flag = (!leaveBackingStreamOpen ? false : false);
			CheckSumAndSizeWriteStream checkSumAndSizeWriteStream = new CheckSumAndSizeWriteStream(stream, backingStream, flag, (long initialPosition, long currentPosition, uint checkSum) => {
				this._crc32 = checkSum;
				this._uncompressedSize = currentPosition;
				this._compressedSize = backingStream.Position - initialPosition;
				if (onClose != null)
				{
					onClose(this, EventArgs.Empty);
				}
			});
			return checkSumAndSizeWriteStream;
		}

		private Stream GetDataDecompressor(Stream compressedStreamToRead)
		{
			Stream deflateStream = null;
			ZipArchiveEntry.CompressionMethodValues compressionMethod = this.CompressionMethod;
			if (compressionMethod == ZipArchiveEntry.CompressionMethodValues.Stored || compressionMethod != ZipArchiveEntry.CompressionMethodValues.Deflate)
			{
				deflateStream = compressedStreamToRead;
			}
			else
			{
				deflateStream = new DeflateStream(compressedStreamToRead, CompressionMode.Decompress);
			}
			return deflateStream;
		}

		private bool IsOpenable(bool needToUncompress, bool needToLoadIntoMemory, out string message)
		{
			message = null;
			if (this._originallyInArchive)
			{
				if (needToUncompress && this.CompressionMethod != ZipArchiveEntry.CompressionMethodValues.Stored && this.CompressionMethod != ZipArchiveEntry.CompressionMethodValues.Deflate)
				{
					message = Messages.UnsupportedCompression;
					return false;
				}
				if ((long)this._diskNumberStart != (ulong)this._archive.NumberOfThisDisk)
				{
					message = Messages.SplitSpanned;
					return false;
				}
				if (this._offsetOfLocalHeader > this._archive.ArchiveStream.Length)
				{
					message = Messages.LocalFileHeaderCorrupt;
					return false;
				}
				this._archive.ArchiveStream.Seek(this._offsetOfLocalHeader, SeekOrigin.Begin);
				if (!ZipLocalFileHeader.TrySkipBlock(this._archive.ArchiveReader))
				{
					message = Messages.LocalFileHeaderCorrupt;
					return false;
				}
				if (this.OffsetOfCompressedData + this._compressedSize > this._archive.ArchiveStream.Length)
				{
					message = Messages.LocalFileHeaderCorrupt;
					return false;
				}
				if (needToLoadIntoMemory && this._compressedSize > (long)2147483647)
				{
					message = Messages.EntryTooLarge;
					return false;
				}
			}
			return true;
		}

		internal bool LoadLocalHeaderExtraFieldAndCompressedBytesIfNeeded()
		{
			if (this._originallyInArchive)
			{
				this._archive.ArchiveStream.Seek(this._offsetOfLocalHeader, SeekOrigin.Begin);
				this._lhUnknownExtraFields = ZipLocalFileHeader.GetExtraFields(this._archive.ArchiveReader);
			}
			if (!this._everOpenedForWrite && this._originallyInArchive)
			{
				this._compressedBytes = new byte[checked((IntPtr)this._compressedSize)];
				this._archive.ArchiveStream.Seek(this.OffsetOfCompressedData, SeekOrigin.Begin);
				ZipHelper.ReadBytes(this._archive.ArchiveStream, this._compressedBytes, (int)this._compressedSize);
			}
			return true;
		}

		[__DynamicallyInvokable]
		public Stream Open()
		{
			this.ThrowIfInvalidArchive();
			switch (this._archive.Mode)
			{
				case ZipArchiveMode.Read:
				{
					return this.OpenInReadMode(true);
				}
				case ZipArchiveMode.Create:
				{
					return this.OpenInWriteMode();
				}
				case ZipArchiveMode.Update:
				{
					return this.OpenInUpdateMode();
				}
				default:
				{
					return this.OpenInUpdateMode();
				}
			}
		}

		private Stream OpenInReadMode(bool checkOpenable)
		{
			if (checkOpenable)
			{
				this.ThrowIfNotOpenable(true, false);
			}
			Stream subReadStream = new SubReadStream(this._archive.ArchiveStream, this.OffsetOfCompressedData, this._compressedSize);
			return this.GetDataDecompressor(subReadStream);
		}

		private Stream OpenInUpdateMode()
		{
			if (this._currentlyOpenForWrite)
			{
				throw new IOException(Messages.UpdateModeOneStream);
			}
			this.ThrowIfNotOpenable(true, true);
			this._everOpenedForWrite = true;
			this._currentlyOpenForWrite = true;
			this.UncompressedData.Seek((long)0, SeekOrigin.Begin);
			return new WrappedStream(this.UncompressedData, (object o, EventArgs e) => this._currentlyOpenForWrite = false);
		}

		private Stream OpenInWriteMode()
		{
			if (this._everOpenedForWrite)
			{
				throw new IOException(Messages.CreateModeWriteOnceAndOneEntryAtATime);
			}
			this._everOpenedForWrite = true;
			CheckSumAndSizeWriteStream dataCompressor = this.GetDataCompressor(this._archive.ArchiveStream, true, (object o, EventArgs e) => {
				this._archive.ReleaseArchiveStream(this);
				this._outstandingWriteStream = null;
			});
			this._outstandingWriteStream = new ZipArchiveEntry.DirectToArchiveWriterStream(dataCompressor, this);
			return new WrappedStream(this._outstandingWriteStream, (object o, EventArgs e) => this._outstandingWriteStream.Close());
		}

		private bool SizesTooLarge()
		{
			if (this._compressedSize > (ulong)-1)
			{
				return true;
			}
			return this._uncompressedSize > (ulong)-1;
		}

		private void ThrowIfInvalidArchive()
		{
			if (this._archive == null)
			{
				throw new InvalidOperationException(Messages.DeletedEntry);
			}
			this._archive.ThrowIfDisposed();
		}

		internal void ThrowIfNotOpenable(bool needToUncompress, bool needToLoadIntoMemory)
		{
			string str;
			if (!this.IsOpenable(needToUncompress, needToLoadIntoMemory, out str))
			{
				throw new InvalidDataException(str);
			}
		}

		[__DynamicallyInvokable]
		public override string ToString()
		{
			return this.FullName;
		}

		private void UnloadStreams()
		{
			if (this._storedUncompressedData != null)
			{
				this._storedUncompressedData.Close();
			}
			this._compressedBytes = null;
			this._outstandingWriteStream = null;
		}

		private void VersionToExtractAtLeast(ZipVersionNeededValues value)
		{
			if (this._versionToExtract < value)
			{
				this._versionToExtract = value;
			}
		}

		internal void WriteAndFinishLocalEntry()
		{
			this.CloseStreams();
			this.WriteLocalFileHeaderAndDataIfNeeded();
			this.UnloadStreams();
		}

		internal void WriteCentralDirectoryFileHeader()
		{
			uint num;
			uint num1;
			uint num2;
			ushort num3;
			ushort totalSize;
			ushort num4;
			ushort length;
			object obj;
			BinaryWriter binaryWriter = new BinaryWriter(this._archive.ArchiveStream);
			Zip64ExtraField nullable = new Zip64ExtraField();
			bool flag = false;
			if (!this.SizesTooLarge())
			{
				num = (uint)this._compressedSize;
				num1 = (uint)this._uncompressedSize;
			}
			else
			{
				flag = true;
				num = -1;
				num1 = -1;
				nullable.CompressedSize = new long?(this._compressedSize);
				nullable.UncompressedSize = new long?(this._uncompressedSize);
			}
			if (this._offsetOfLocalHeader <= (ulong)-1)
			{
				num2 = (uint)this._offsetOfLocalHeader;
			}
			else
			{
				flag = true;
				num2 = -1;
				nullable.LocalHeaderOffset = new long?(this._offsetOfLocalHeader);
			}
			if (flag)
			{
				this.VersionToExtractAtLeast(ZipVersionNeededValues.Zip64);
			}
			if (flag)
			{
				totalSize = nullable.TotalSize;
			}
			else
			{
				totalSize = 0;
			}
			if (this._cdUnknownExtraFields != null)
			{
				num4 = (ushort)ZipGenericExtraField.TotalSize(this._cdUnknownExtraFields);
			}
			else
			{
				num4 = 0;
			}
			int num5 = totalSize + num4;
			if (num5 <= 65535)
			{
				num3 = (ushort)num5;
			}
			else
			{
				if (flag)
				{
					obj = nullable.TotalSize;
				}
				else
				{
					obj = null;
				}
				num3 = (ushort)obj;
				this._cdUnknownExtraFields = null;
			}
			binaryWriter.Write((uint)33639248);
			binaryWriter.Write((ushort)this._versionToExtract);
			binaryWriter.Write((ushort)this._versionToExtract);
			binaryWriter.Write((ushort)this._generalPurposeBitFlag);
			binaryWriter.Write((ushort)this.CompressionMethod);
			binaryWriter.Write(ZipHelper.DateTimeToDosTime(this._lastModified.DateTime));
			binaryWriter.Write(this._crc32);
			binaryWriter.Write(num);
			binaryWriter.Write(num1);
			binaryWriter.Write((ushort)((int)this._storedEntryNameBytes.Length));
			binaryWriter.Write(num3);
			BinaryWriter binaryWriter1 = binaryWriter;
			if (this._fileComment != null)
			{
				length = (ushort)((int)this._fileComment.Length);
			}
			else
			{
				length = 0;
			}
			binaryWriter1.Write(length);
			binaryWriter.Write((ushort)0);
			binaryWriter.Write((ushort)0);
			binaryWriter.Write(this._externalFileAttr);
			binaryWriter.Write(num2);
			binaryWriter.Write(this._storedEntryNameBytes);
			if (flag)
			{
				nullable.WriteBlock(this._archive.ArchiveStream);
			}
			if (this._cdUnknownExtraFields != null)
			{
				ZipGenericExtraField.WriteAllBlocks(this._cdUnknownExtraFields, this._archive.ArchiveStream);
			}
			if (this._fileComment != null)
			{
				binaryWriter.Write(this._fileComment);
			}
		}

		private void WriteCrcAndSizesInLocalHeader(bool zip64HeaderUsed)
		{
			uint num;
			uint num1;
			long position = this._archive.ArchiveStream.Position;
			BinaryWriter binaryWriter = new BinaryWriter(this._archive.ArchiveStream);
			bool flag = this.SizesTooLarge();
			bool flag1 = (!flag ? false : !zip64HeaderUsed);
			if (flag)
			{
				num = -1;
			}
			else
			{
				num = (uint)this._compressedSize;
			}
			uint num2 = num;
			if (flag)
			{
				num1 = -1;
			}
			else
			{
				num1 = (uint)this._uncompressedSize;
			}
			uint num3 = num1;
			if (flag1)
			{
				this._generalPurposeBitFlag |= ZipArchiveEntry.BitFlagValues.DataDescriptor;
				this._archive.ArchiveStream.Seek(this._offsetOfLocalHeader + (long)6, SeekOrigin.Begin);
				binaryWriter.Write((ushort)this._generalPurposeBitFlag);
			}
			this._archive.ArchiveStream.Seek(this._offsetOfLocalHeader + (long)14, SeekOrigin.Begin);
			if (flag1)
			{
				binaryWriter.Write((uint)0);
				binaryWriter.Write((uint)0);
				binaryWriter.Write((uint)0);
			}
			else
			{
				binaryWriter.Write(this._crc32);
				binaryWriter.Write(num2);
				binaryWriter.Write(num3);
			}
			if (zip64HeaderUsed)
			{
				this._archive.ArchiveStream.Seek(this._offsetOfLocalHeader + (long)30 + (long)((int)this._storedEntryNameBytes.Length) + (long)4, SeekOrigin.Begin);
				binaryWriter.Write(this._uncompressedSize);
				binaryWriter.Write(this._compressedSize);
				this._archive.ArchiveStream.Seek(position, SeekOrigin.Begin);
			}
			this._archive.ArchiveStream.Seek(position, SeekOrigin.Begin);
			if (flag1)
			{
				binaryWriter.Write(this._crc32);
				binaryWriter.Write(this._compressedSize);
				binaryWriter.Write(this._uncompressedSize);
			}
		}

		private void WriteDataDescriptor()
		{
			BinaryWriter binaryWriter = new BinaryWriter(this._archive.ArchiveStream);
			binaryWriter.Write((uint)134695760);
			binaryWriter.Write(this._crc32);
			if (this.SizesTooLarge())
			{
				binaryWriter.Write(this._compressedSize);
				binaryWriter.Write(this._uncompressedSize);
				return;
			}
			binaryWriter.Write((uint)this._compressedSize);
			binaryWriter.Write((uint)this._uncompressedSize);
		}

		private bool WriteLocalFileHeader(bool isEmptyFile)
		{
			uint num;
			uint num1;
			ushort num2;
			ushort totalSize;
			ushort num3;
			object obj;
			BinaryWriter binaryWriter = new BinaryWriter(this._archive.ArchiveStream);
			Zip64ExtraField nullable = new Zip64ExtraField();
			bool flag = false;
			if (isEmptyFile)
			{
				this.CompressionMethod = ZipArchiveEntry.CompressionMethodValues.Stored;
				num = 0;
				num1 = 0;
			}
			else if (this._archive.Mode == ZipArchiveMode.Create && !this._archive.ArchiveStream.CanSeek && !isEmptyFile)
			{
				this._generalPurposeBitFlag |= ZipArchiveEntry.BitFlagValues.DataDescriptor;
				flag = false;
				num = 0;
				num1 = 0;
			}
			else if (!this.SizesTooLarge())
			{
				flag = false;
				num = (uint)this._compressedSize;
				num1 = (uint)this._uncompressedSize;
			}
			else
			{
				flag = true;
				num = -1;
				num1 = -1;
				nullable.CompressedSize = new long?(this._compressedSize);
				nullable.UncompressedSize = new long?(this._uncompressedSize);
				this.VersionToExtractAtLeast(ZipVersionNeededValues.Zip64);
			}
			this._offsetOfLocalHeader = binaryWriter.BaseStream.Position;
			if (flag)
			{
				totalSize = nullable.TotalSize;
			}
			else
			{
				totalSize = 0;
			}
			if (this._lhUnknownExtraFields != null)
			{
				num3 = (ushort)ZipGenericExtraField.TotalSize(this._lhUnknownExtraFields);
			}
			else
			{
				num3 = 0;
			}
			int num4 = totalSize + num3;
			if (num4 <= 65535)
			{
				num2 = (ushort)num4;
			}
			else
			{
				if (flag)
				{
					obj = nullable.TotalSize;
				}
				else
				{
					obj = null;
				}
				num2 = (ushort)obj;
				this._lhUnknownExtraFields = null;
			}
			binaryWriter.Write((uint)67324752);
			binaryWriter.Write((ushort)this._versionToExtract);
			binaryWriter.Write((ushort)this._generalPurposeBitFlag);
			binaryWriter.Write((ushort)this.CompressionMethod);
			binaryWriter.Write(ZipHelper.DateTimeToDosTime(this._lastModified.DateTime));
			binaryWriter.Write(this._crc32);
			binaryWriter.Write(num);
			binaryWriter.Write(num1);
			binaryWriter.Write((ushort)((int)this._storedEntryNameBytes.Length));
			binaryWriter.Write(num2);
			binaryWriter.Write(this._storedEntryNameBytes);
			if (flag)
			{
				nullable.WriteBlock(this._archive.ArchiveStream);
			}
			if (this._lhUnknownExtraFields != null)
			{
				ZipGenericExtraField.WriteAllBlocks(this._lhUnknownExtraFields, this._archive.ArchiveStream);
			}
			return flag;
		}

		private void WriteLocalFileHeaderAndDataIfNeeded()
		{
			if (this._storedUncompressedData == null && this._compressedBytes == null)
			{
				if (this._archive.Mode == ZipArchiveMode.Update || !this._everOpenedForWrite)
				{
					this._everOpenedForWrite = true;
					this.WriteLocalFileHeader(true);
				}
			}
			else if (this._storedUncompressedData == null)
			{
				if (this._uncompressedSize == 0)
				{
					this.CompressionMethod = ZipArchiveEntry.CompressionMethodValues.Stored;
				}
				this.WriteLocalFileHeader(false);
				using (MemoryStream memoryStream = new MemoryStream(this._compressedBytes))
				{
					memoryStream.CopyTo(this._archive.ArchiveStream);
				}
			}
			else
			{
				this._uncompressedSize = this._storedUncompressedData.Length;
				using (Stream directToArchiveWriterStream = new ZipArchiveEntry.DirectToArchiveWriterStream(this.GetDataCompressor(this._archive.ArchiveStream, true, null), this))
				{
					this._storedUncompressedData.Seek((long)0, SeekOrigin.Begin);
					this._storedUncompressedData.CopyTo(directToArchiveWriterStream);
					this._storedUncompressedData.Close();
					this._storedUncompressedData = null;
				}
			}
		}

		[Flags]
		private enum BitFlagValues : ushort
		{
			DataDescriptor = 8,
			UnicodeFileName = 2048
		}

		private enum CompressionMethodValues : ushort
		{
			Stored = 0,
			Deflate = 8
		}

		private class DirectToArchiveWriterStream : Stream
		{
			private long _position;

			private CheckSumAndSizeWriteStream _crcSizeStream;

			private bool _everWritten;

			private bool _isDisposed;

			private ZipArchiveEntry _entry;

			private bool _usedZip64inLH;

			private bool _canWrite;

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

			public DirectToArchiveWriterStream(CheckSumAndSizeWriteStream crcSizeStream, ZipArchiveEntry entry)
			{
				this._position = (long)0;
				this._crcSizeStream = crcSizeStream;
				this._everWritten = false;
				this._isDisposed = false;
				this._entry = entry;
				this._usedZip64inLH = false;
				this._canWrite = true;
			}

			protected override void Dispose(bool disposing)
			{
				if (disposing && !this._isDisposed)
				{
					this._crcSizeStream.Close();
					if (!this._everWritten)
					{
						this._entry.WriteLocalFileHeader(true);
					}
					else if (!this._entry._archive.ArchiveStream.CanSeek)
					{
						this._entry.WriteDataDescriptor();
					}
					else
					{
						this._entry.WriteCrcAndSizesInLocalHeader(this._usedZip64inLH);
					}
					this._canWrite = false;
					this._isDisposed = true;
				}
				base.Dispose(disposing);
			}

			public override void Flush()
			{
				this.ThrowIfDisposed();
				this._crcSizeStream.Flush();
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
					this._everWritten = true;
					this._usedZip64inLH = this._entry.WriteLocalFileHeader(false);
				}
				this._crcSizeStream.Write(buffer, offset, count);
				this._position += (long)count;
			}
		}

		private enum OpenableValues
		{
			Openable,
			FileNonExistent,
			FileTooLarge
		}
	}
}