using System;
using System.Collections.Generic;
using System.IO;

namespace System.IO.Compression
{
	internal struct Zip64ExtraField
	{
		public const int OffsetToFirstField = 4;

		private const ushort TagConstant = 1;

		private ushort _size;

		private long? _uncompressedSize;

		private long? _compressedSize;

		private long? _localHeaderOffset;

		private int? _startDiskNumber;

		public long? CompressedSize
		{
			get
			{
				return this._compressedSize;
			}
			set
			{
				this._compressedSize = value;
				this.UpdateSize();
			}
		}

		public long? LocalHeaderOffset
		{
			get
			{
				return this._localHeaderOffset;
			}
			set
			{
				this._localHeaderOffset = value;
				this.UpdateSize();
			}
		}

		public int? StartDiskNumber
		{
			get
			{
				return this._startDiskNumber;
			}
		}

		public ushort TotalSize
		{
			get
			{
				return (ushort)(this._size + 4);
			}
		}

		public long? UncompressedSize
		{
			get
			{
				return this._uncompressedSize;
			}
			set
			{
				this._uncompressedSize = value;
				this.UpdateSize();
			}
		}

		public static Zip64ExtraField GetAndRemoveZip64Block(List<ZipGenericExtraField> extraFields, bool readUncompressedSize, bool readCompressedSize, bool readLocalHeaderOffset, bool readStartDiskNumber)
		{
			Zip64ExtraField zip64ExtraField = new Zip64ExtraField()
			{
				_compressedSize = null,
				_uncompressedSize = null,
				_localHeaderOffset = null,
				_startDiskNumber = null
			};
			List<ZipGenericExtraField> zipGenericExtraFields = new List<ZipGenericExtraField>();
			bool flag = false;
			foreach (ZipGenericExtraField extraField in extraFields)
			{
				if (extraField.Tag != 1)
				{
					continue;
				}
				zipGenericExtraFields.Add(extraField);
				if (flag || !Zip64ExtraField.TryGetZip64BlockFromGenericExtraField(extraField, readUncompressedSize, readCompressedSize, readLocalHeaderOffset, readStartDiskNumber, out zip64ExtraField))
				{
					continue;
				}
				flag = true;
			}
			foreach (ZipGenericExtraField zipGenericExtraField in zipGenericExtraFields)
			{
				extraFields.Remove(zipGenericExtraField);
			}
			return zip64ExtraField;
		}

		public static Zip64ExtraField GetJustZip64Block(Stream extraFieldStream, bool readUncompressedSize, bool readCompressedSize, bool readLocalHeaderOffset, bool readStartDiskNumber)
		{
			Zip64ExtraField zip64ExtraField;
			ZipGenericExtraField zipGenericExtraField;
			Zip64ExtraField zip64ExtraField1;
			using (BinaryReader binaryReader = new BinaryReader(extraFieldStream))
			{
				while (ZipGenericExtraField.TryReadBlock(binaryReader, extraFieldStream.Length, out zipGenericExtraField))
				{
					if (!Zip64ExtraField.TryGetZip64BlockFromGenericExtraField(zipGenericExtraField, readUncompressedSize, readCompressedSize, readLocalHeaderOffset, readStartDiskNumber, out zip64ExtraField))
					{
						continue;
					}
					zip64ExtraField1 = zip64ExtraField;
					return zip64ExtraField1;
				}
				zip64ExtraField = new Zip64ExtraField()
				{
					_compressedSize = null,
					_uncompressedSize = null,
					_localHeaderOffset = null,
					_startDiskNumber = null
				};
				return zip64ExtraField;
			}
			return zip64ExtraField1;
		}

		public static void RemoveZip64Blocks(List<ZipGenericExtraField> extraFields)
		{
			List<ZipGenericExtraField> zipGenericExtraFields = new List<ZipGenericExtraField>();
			foreach (ZipGenericExtraField extraField in extraFields)
			{
				if (extraField.Tag != 1)
				{
					continue;
				}
				zipGenericExtraFields.Add(extraField);
			}
			foreach (ZipGenericExtraField zipGenericExtraField in zipGenericExtraFields)
			{
				extraFields.Remove(zipGenericExtraField);
			}
		}

		private static bool TryGetZip64BlockFromGenericExtraField(ZipGenericExtraField extraField, bool readUncompressedSize, bool readCompressedSize, bool readLocalHeaderOffset, bool readStartDiskNumber, out Zip64ExtraField zip64Block)
		{
			bool flag;
			zip64Block = new Zip64ExtraField()
			{
				_compressedSize = null,
				_uncompressedSize = null,
				_localHeaderOffset = null,
				_startDiskNumber = null
			};
			if (extraField.Tag != 1)
			{
				return false;
			}
			MemoryStream memoryStream = null;
			try
			{
				memoryStream = new MemoryStream(extraField.Data);
				using (BinaryReader binaryReader = new BinaryReader(memoryStream))
				{
					memoryStream = null;
					zip64Block._size = extraField.Size;
					ushort num = 0;
					if (readUncompressedSize)
					{
						num = (ushort)(num + 8);
					}
					if (readCompressedSize)
					{
						num = (ushort)(num + 8);
					}
					if (readLocalHeaderOffset)
					{
						num = (ushort)(num + 8);
					}
					if (readStartDiskNumber)
					{
						num = (ushort)(num + 4);
					}
					if (num == zip64Block._size)
					{
						if (readUncompressedSize)
						{
							zip64Block._uncompressedSize = new long?(binaryReader.ReadInt64());
						}
						if (readCompressedSize)
						{
							zip64Block._compressedSize = new long?(binaryReader.ReadInt64());
						}
						if (readLocalHeaderOffset)
						{
							zip64Block._localHeaderOffset = new long?(binaryReader.ReadInt64());
						}
						if (readStartDiskNumber)
						{
							zip64Block._startDiskNumber = new int?(binaryReader.ReadInt32());
						}
						long? nullable = zip64Block._uncompressedSize;
						long num1 = (long)0;
						if ((nullable.GetValueOrDefault() < num1 ? nullable.HasValue : false))
						{
							throw new InvalidDataException(Messages.FieldTooBigUncompressedSize);
						}
						nullable = zip64Block._compressedSize;
						num1 = (long)0;
						if ((nullable.GetValueOrDefault() < num1 ? nullable.HasValue : false))
						{
							throw new InvalidDataException(Messages.FieldTooBigCompressedSize);
						}
						nullable = zip64Block._localHeaderOffset;
						num1 = (long)0;
						if ((nullable.GetValueOrDefault() < num1 ? nullable.HasValue : false))
						{
							throw new InvalidDataException(Messages.FieldTooBigLocalHeaderOffset);
						}
						int? nullable1 = zip64Block._startDiskNumber;
						if ((nullable1.GetValueOrDefault() < 0 ? nullable1.HasValue : false))
						{
							throw new InvalidDataException(Messages.FieldTooBigStartDiskNumber);
						}
						flag = true;
					}
					else
					{
						flag = false;
					}
				}
			}
			finally
			{
				if (memoryStream != null)
				{
					memoryStream.Close();
				}
			}
			return flag;
		}

		private void UpdateSize()
		{
			this._size = 0;
			if (this._uncompressedSize.HasValue)
			{
				this._size = (ushort)(this._size + 8);
			}
			if (this._compressedSize.HasValue)
			{
				this._size = (ushort)(this._size + 8);
			}
			if (this._localHeaderOffset.HasValue)
			{
				this._size = (ushort)(this._size + 8);
			}
			if (this._startDiskNumber.HasValue)
			{
				this._size = (ushort)(this._size + 4);
			}
		}

		public void WriteBlock(Stream stream)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			binaryWriter.Write((ushort)1);
			binaryWriter.Write(this._size);
			if (this._uncompressedSize.HasValue)
			{
				binaryWriter.Write(this._uncompressedSize.Value);
			}
			if (this._compressedSize.HasValue)
			{
				binaryWriter.Write(this._compressedSize.Value);
			}
			if (this._localHeaderOffset.HasValue)
			{
				binaryWriter.Write(this._localHeaderOffset.Value);
			}
			if (this._startDiskNumber.HasValue)
			{
				binaryWriter.Write(this._startDiskNumber.Value);
			}
		}
	}
}