using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Reflection;
using System.Text;

namespace System.IO.Compression
{
	[__DynamicallyInvokable]
	public class ZipArchive : IDisposable
	{
		private Stream _archiveStream;

		private ZipArchiveEntry _archiveStreamOwner;

		private BinaryReader _archiveReader;

		private ZipArchiveMode _mode;

		private List<ZipArchiveEntry> _entries;

		private ReadOnlyCollection<ZipArchiveEntry> _entriesCollection;

		private Dictionary<string, ZipArchiveEntry> _entriesDictionary;

		private bool _readEntries;

		private bool _leaveOpen;

		private long _centralDirectoryStart;

		private bool _isDisposed;

		private uint _numberOfThisDisk;

		private long _expectedNumberOfEntries;

		private Stream _backingStream;

		private byte[] _archiveComment;

		private Encoding _entryNameEncoding;

		internal BinaryReader ArchiveReader
		{
			get
			{
				return this._archiveReader;
			}
		}

		internal Stream ArchiveStream
		{
			get
			{
				return this._archiveStream;
			}
		}

		[__DynamicallyInvokable]
		public ReadOnlyCollection<ZipArchiveEntry> Entries
		{
			[__DynamicallyInvokable]
			get
			{
				if (this._mode == ZipArchiveMode.Create)
				{
					throw new NotSupportedException(Messages.EntriesInCreateMode);
				}
				this.ThrowIfDisposed();
				this.EnsureCentralDirectoryRead();
				return this._entriesCollection;
			}
		}

		internal Encoding EntryNameEncoding
		{
			get
			{
				return this._entryNameEncoding;
			}
			private set
			{
				if (value != null && (value.Equals(Encoding.BigEndianUnicode) || value.Equals(Encoding.Unicode) || value.Equals(Encoding.UTF32) || value.Equals(Encoding.UTF7)))
				{
					throw new ArgumentException(Messages.EntryNameEncodingNotSupported, "entryNameEncoding");
				}
				this._entryNameEncoding = value;
			}
		}

		[__DynamicallyInvokable]
		public ZipArchiveMode Mode
		{
			[__DynamicallyInvokable]
			get
			{
				return this._mode;
			}
		}

		internal uint NumberOfThisDisk
		{
			get
			{
				return this._numberOfThisDisk;
			}
		}

		[__DynamicallyInvokable]
		public ZipArchive(Stream stream) : this(stream, ZipArchiveMode.Read, false, null)
		{
		}

		[__DynamicallyInvokable]
		public ZipArchive(Stream stream, ZipArchiveMode mode) : this(stream, mode, false, null)
		{
		}

		[__DynamicallyInvokable]
		public ZipArchive(Stream stream, ZipArchiveMode mode, bool leaveOpen) : this(stream, mode, leaveOpen, null)
		{
		}

		[__DynamicallyInvokable]
		public ZipArchive(Stream stream, ZipArchiveMode mode, bool leaveOpen, Encoding entryNameEncoding)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			this.EntryNameEncoding = entryNameEncoding;
			this.Init(stream, mode, leaveOpen);
		}

		internal void AcquireArchiveStream(ZipArchiveEntry entry)
		{
			if (this._archiveStreamOwner != null)
			{
				if (this._archiveStreamOwner.EverOpenedForWrite)
				{
					throw new IOException(Messages.CreateModeCreateEntryWhileOpen);
				}
				this._archiveStreamOwner.WriteAndFinishLocalEntry();
			}
			this._archiveStreamOwner = entry;
		}

		private void AddEntry(ZipArchiveEntry entry)
		{
			this._entries.Add(entry);
			string fullName = entry.FullName;
			if (!this._entriesDictionary.ContainsKey(fullName))
			{
				this._entriesDictionary.Add(fullName, entry);
			}
		}

		private void CloseStreams()
		{
			if (!this._leaveOpen)
			{
				this._archiveStream.Close();
				if (this._backingStream != null)
				{
					this._backingStream.Close();
				}
				if (this._archiveReader != null)
				{
					this._archiveReader.Close();
					return;
				}
			}
			else if (this._backingStream != null)
			{
				this._archiveStream.Close();
			}
		}

		[__DynamicallyInvokable]
		public ZipArchiveEntry CreateEntry(string entryName)
		{
			return this.DoCreateEntry(entryName, null);
		}

		[__DynamicallyInvokable]
		public ZipArchiveEntry CreateEntry(string entryName, CompressionLevel compressionLevel)
		{
			return this.DoCreateEntry(entryName, new CompressionLevel?(compressionLevel));
		}

		[__DynamicallyInvokable]
		protected virtual void Dispose(bool disposing)
		{
			if (disposing && !this._isDisposed)
			{
				ZipArchiveMode zipArchiveMode = this._mode;
				if (zipArchiveMode != ZipArchiveMode.Read)
				{
					try
					{
						this.WriteFile();
					}
					catch (InvalidDataException invalidDataException)
					{
						this.CloseStreams();
						this._isDisposed = true;
						throw;
					}
				}
				this.CloseStreams();
				this._isDisposed = true;
			}
		}

		[__DynamicallyInvokable]
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		private ZipArchiveEntry DoCreateEntry(string entryName, CompressionLevel? compressionLevel)
		{
			if (entryName == null)
			{
				throw new ArgumentNullException("entryName");
			}
			if (string.IsNullOrEmpty(entryName))
			{
				throw new ArgumentException(Messages.CannotBeEmpty, "entryName");
			}
			if (this._mode == ZipArchiveMode.Read)
			{
				throw new NotSupportedException(Messages.CreateInReadMode);
			}
			this.ThrowIfDisposed();
			ZipArchiveEntry zipArchiveEntry = (compressionLevel.HasValue ? new ZipArchiveEntry(this, entryName, compressionLevel.Value) : new ZipArchiveEntry(this, entryName));
			this.AddEntry(zipArchiveEntry);
			return zipArchiveEntry;
		}

		private void EnsureCentralDirectoryRead()
		{
			if (!this._readEntries)
			{
				this.ReadCentralDirectory();
				this._readEntries = true;
			}
		}

		[__DynamicallyInvokable]
		public ZipArchiveEntry GetEntry(string entryName)
		{
			ZipArchiveEntry zipArchiveEntry;
			if (entryName == null)
			{
				throw new ArgumentNullException("entryName");
			}
			if (this._mode == ZipArchiveMode.Create)
			{
				throw new NotSupportedException(Messages.EntriesInCreateMode);
			}
			this.EnsureCentralDirectoryRead();
			this._entriesDictionary.TryGetValue(entryName, out zipArchiveEntry);
			return zipArchiveEntry;
		}

		private void Init(Stream stream, ZipArchiveMode mode, bool leaveOpen)
		{
			Stream stream1 = null;
			try
			{
				this._backingStream = null;
				switch (mode)
				{
					case ZipArchiveMode.Read:
					{
						if (!stream.CanRead)
						{
							throw new ArgumentException(Messages.ReadModeCapabilities);
						}
						if (stream.CanSeek)
						{
							break;
						}
						this._backingStream = stream;
						MemoryStream memoryStream = new MemoryStream();
						stream = memoryStream;
						stream1 = memoryStream;
						this._backingStream.CopyTo(stream);
						stream.Seek((long)0, SeekOrigin.Begin);
						break;
					}
					case ZipArchiveMode.Create:
					{
						if (stream.CanWrite)
						{
							break;
						}
						throw new ArgumentException(Messages.CreateModeCapabilities);
					}
					case ZipArchiveMode.Update:
					{
						if (stream.CanRead && stream.CanWrite && stream.CanSeek)
						{
							break;
						}
						throw new ArgumentException(Messages.UpdateModeCapabilities);
					}
					default:
					{
						throw new ArgumentOutOfRangeException("mode");
					}
				}
				this._mode = mode;
				this._archiveStream = stream;
				this._archiveStreamOwner = null;
				if (mode != ZipArchiveMode.Create)
				{
					this._archiveReader = new BinaryReader(stream);
				}
				else
				{
					this._archiveReader = null;
				}
				this._entries = new List<ZipArchiveEntry>();
				this._entriesCollection = new ReadOnlyCollection<ZipArchiveEntry>(this._entries);
				this._entriesDictionary = new Dictionary<string, ZipArchiveEntry>();
				this._readEntries = false;
				this._leaveOpen = leaveOpen;
				this._centralDirectoryStart = (long)0;
				this._isDisposed = false;
				this._numberOfThisDisk = 0;
				this._archiveComment = null;
				switch (mode)
				{
					case ZipArchiveMode.Read:
					{
						this.ReadEndOfCentralDirectory();
						break;
					}
					case ZipArchiveMode.Create:
					{
						this._readEntries = true;
						break;
					}
					case ZipArchiveMode.Update:
					{
						if (this._archiveStream.Length != 0)
						{
							this.ReadEndOfCentralDirectory();
							this.EnsureCentralDirectoryRead();
							List<ZipArchiveEntry>.Enumerator enumerator = this._entries.GetEnumerator();
							try
							{
								while (enumerator.MoveNext())
								{
									enumerator.Current.ThrowIfNotOpenable(false, true);
								}
								break;
							}
							finally
							{
								((IDisposable)enumerator).Dispose();
							}
						}
						else
						{
							this._readEntries = true;
							break;
						}
						break;
					}
					default:
					{
						goto case ZipArchiveMode.Update;
					}
				}
			}
			catch
			{
				if (stream1 != null)
				{
					stream1.Close();
				}
				throw;
			}
		}

		internal bool IsStillArchiveStreamOwner(ZipArchiveEntry entry)
		{
			return this._archiveStreamOwner == entry;
		}

		private void ReadCentralDirectory()
		{
			ZipCentralDirectoryFileHeader zipCentralDirectoryFileHeader;
			try
			{
				this._archiveStream.Seek(this._centralDirectoryStart, SeekOrigin.Begin);
				long num = (long)0;
				bool mode = this.Mode == ZipArchiveMode.Update;
				while (ZipCentralDirectoryFileHeader.TryReadBlock(this._archiveReader, mode, out zipCentralDirectoryFileHeader))
				{
					this.AddEntry(new ZipArchiveEntry(this, zipCentralDirectoryFileHeader));
					num += (long)1;
				}
				if (num != this._expectedNumberOfEntries)
				{
					throw new InvalidDataException(Messages.NumEntriesWrong);
				}
			}
			catch (EndOfStreamException endOfStreamException)
			{
				throw new InvalidDataException(Messages.CentralDirectoryInvalid, endOfStreamException);
			}
		}

		private void ReadEndOfCentralDirectory()
		{
			ZipEndOfCentralDirectoryBlock zipEndOfCentralDirectoryBlock;
			Zip64EndOfCentralDirectoryLocator zip64EndOfCentralDirectoryLocator;
			Zip64EndOfCentralDirectoryRecord zip64EndOfCentralDirectoryRecord;
			try
			{
				this._archiveStream.Seek((long)-18, SeekOrigin.End);
				if (!ZipHelper.SeekBackwardsToSignature(this._archiveStream, 101010256))
				{
					throw new InvalidDataException(Messages.EOCDNotFound);
				}
				long position = this._archiveStream.Position;
				ZipEndOfCentralDirectoryBlock.TryReadBlock(this._archiveReader, out zipEndOfCentralDirectoryBlock);
				if (zipEndOfCentralDirectoryBlock.NumberOfThisDisk != zipEndOfCentralDirectoryBlock.NumberOfTheDiskWithTheStartOfTheCentralDirectory)
				{
					throw new InvalidDataException(Messages.SplitSpanned);
				}
				this._numberOfThisDisk = zipEndOfCentralDirectoryBlock.NumberOfThisDisk;
				this._centralDirectoryStart = (long)zipEndOfCentralDirectoryBlock.OffsetOfStartOfCentralDirectoryWithRespectToTheStartingDiskNumber;
				if (zipEndOfCentralDirectoryBlock.NumberOfEntriesInTheCentralDirectory != zipEndOfCentralDirectoryBlock.NumberOfEntriesInTheCentralDirectoryOnThisDisk)
				{
					throw new InvalidDataException(Messages.SplitSpanned);
				}
				this._expectedNumberOfEntries = (long)zipEndOfCentralDirectoryBlock.NumberOfEntriesInTheCentralDirectory;
				if (this._mode == ZipArchiveMode.Update)
				{
					this._archiveComment = zipEndOfCentralDirectoryBlock.ArchiveComment;
				}
				if (zipEndOfCentralDirectoryBlock.NumberOfThisDisk == 65535 || zipEndOfCentralDirectoryBlock.OffsetOfStartOfCentralDirectoryWithRespectToTheStartingDiskNumber == -1 || zipEndOfCentralDirectoryBlock.NumberOfEntriesInTheCentralDirectory == 65535)
				{
					this._archiveStream.Seek(position - (long)16, SeekOrigin.Begin);
					if (ZipHelper.SeekBackwardsToSignature(this._archiveStream, 117853008))
					{
						Zip64EndOfCentralDirectoryLocator.TryReadBlock(this._archiveReader, out zip64EndOfCentralDirectoryLocator);
						if (zip64EndOfCentralDirectoryLocator.OffsetOfZip64EOCD > 9223372036854775807L)
						{
							throw new InvalidDataException(Messages.FieldTooBigOffsetToZip64EOCD);
						}
						long offsetOfZip64EOCD = (long)zip64EndOfCentralDirectoryLocator.OffsetOfZip64EOCD;
						this._archiveStream.Seek(offsetOfZip64EOCD, SeekOrigin.Begin);
						if (!Zip64EndOfCentralDirectoryRecord.TryReadBlock(this._archiveReader, out zip64EndOfCentralDirectoryRecord))
						{
							throw new InvalidDataException(Messages.Zip64EOCDNotWhereExpected);
						}
						this._numberOfThisDisk = zip64EndOfCentralDirectoryRecord.NumberOfThisDisk;
						if (zip64EndOfCentralDirectoryRecord.NumberOfEntriesTotal > 9223372036854775807L)
						{
							throw new InvalidDataException(Messages.FieldTooBigNumEntries);
						}
						if (zip64EndOfCentralDirectoryRecord.OffsetOfCentralDirectory > 9223372036854775807L)
						{
							throw new InvalidDataException(Messages.FieldTooBigOffsetToCD);
						}
						if (zip64EndOfCentralDirectoryRecord.NumberOfEntriesTotal != zip64EndOfCentralDirectoryRecord.NumberOfEntriesOnThisDisk)
						{
							throw new InvalidDataException(Messages.SplitSpanned);
						}
						this._expectedNumberOfEntries = (long)zip64EndOfCentralDirectoryRecord.NumberOfEntriesTotal;
						this._centralDirectoryStart = (long)zip64EndOfCentralDirectoryRecord.OffsetOfCentralDirectory;
					}
				}
				if (this._centralDirectoryStart > this._archiveStream.Length)
				{
					throw new InvalidDataException(Messages.FieldTooBigOffsetToCD);
				}
			}
			catch (EndOfStreamException endOfStreamException)
			{
				throw new InvalidDataException(Messages.CDCorrupt, endOfStreamException);
			}
			catch (IOException oException)
			{
				throw new InvalidDataException(Messages.CDCorrupt, oException);
			}
		}

		internal void ReleaseArchiveStream(ZipArchiveEntry entry)
		{
			this._archiveStreamOwner = null;
		}

		internal void RemoveEntry(ZipArchiveEntry entry)
		{
			this._entries.Remove(entry);
			this._entriesDictionary.Remove(entry.FullName);
		}

		internal void ThrowIfDisposed()
		{
			if (this._isDisposed)
			{
				throw new ObjectDisposedException(this.GetType().Name);
			}
		}

		private void WriteArchiveEpilogue(long startOfCentralDirectory, long sizeOfCentralDirectory)
		{
			bool flag = false;
			if (startOfCentralDirectory >= (ulong)-1 || sizeOfCentralDirectory >= (ulong)-1 || this._entries.Count >= 65535)
			{
				flag = true;
			}
			if (flag)
			{
				long position = this._archiveStream.Position;
				Zip64EndOfCentralDirectoryRecord.WriteBlock(this._archiveStream, (long)this._entries.Count, startOfCentralDirectory, sizeOfCentralDirectory);
				Zip64EndOfCentralDirectoryLocator.WriteBlock(this._archiveStream, position);
			}
			ZipEndOfCentralDirectoryBlock.WriteBlock(this._archiveStream, (long)this._entries.Count, startOfCentralDirectory, sizeOfCentralDirectory, this._archiveComment);
		}

		private void WriteFile()
		{
			if (this._mode == ZipArchiveMode.Update)
			{
				List<ZipArchiveEntry> zipArchiveEntries = new List<ZipArchiveEntry>();
				foreach (ZipArchiveEntry _entry in this._entries)
				{
					if (_entry.LoadLocalHeaderExtraFieldAndCompressedBytesIfNeeded())
					{
						continue;
					}
					zipArchiveEntries.Add(_entry);
				}
				foreach (ZipArchiveEntry zipArchiveEntry in zipArchiveEntries)
				{
					zipArchiveEntry.Delete();
				}
				this._archiveStream.Seek((long)0, SeekOrigin.Begin);
				this._archiveStream.SetLength((long)0);
			}
			foreach (ZipArchiveEntry _entry1 in this._entries)
			{
				_entry1.WriteAndFinishLocalEntry();
			}
			long position = this._archiveStream.Position;
			foreach (ZipArchiveEntry zipArchiveEntry1 in this._entries)
			{
				zipArchiveEntry1.WriteCentralDirectoryFileHeader();
			}
			this.WriteArchiveEpilogue(position, this._archiveStream.Position - position);
		}
	}
}