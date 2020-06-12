using System;
using System.Collections.Generic;
using System.IO;

namespace System.IO.Compression
{
	internal struct ZipGenericExtraField
	{
		private const int SizeOfHeader = 4;

		private ushort _tag;

		private ushort _size;

		private byte[] _data;

		public byte[] Data
		{
			get
			{
				return this._data;
			}
		}

		public ushort Size
		{
			get
			{
				return this._size;
			}
		}

		public ushort Tag
		{
			get
			{
				return this._tag;
			}
		}

		public static List<ZipGenericExtraField> ParseExtraField(Stream extraFieldData)
		{
			ZipGenericExtraField zipGenericExtraField;
			List<ZipGenericExtraField> zipGenericExtraFields = new List<ZipGenericExtraField>();
			using (BinaryReader binaryReader = new BinaryReader(extraFieldData))
			{
				while (ZipGenericExtraField.TryReadBlock(binaryReader, extraFieldData.Length, out zipGenericExtraField))
				{
					zipGenericExtraFields.Add(zipGenericExtraField);
				}
			}
			return zipGenericExtraFields;
		}

		public static int TotalSize(List<ZipGenericExtraField> fields)
		{
			int size = 0;
			foreach (ZipGenericExtraField field in fields)
			{
				size = size + field.Size + 4;
			}
			return size;
		}

		public static bool TryReadBlock(BinaryReader reader, long endExtraField, out ZipGenericExtraField field)
		{
			field = new ZipGenericExtraField();
			if (endExtraField - reader.BaseStream.Position < (long)4)
			{
				return false;
			}
			field._tag = reader.ReadUInt16();
			field._size = reader.ReadUInt16();
			if (endExtraField - reader.BaseStream.Position < (ulong)field._size)
			{
				return false;
			}
			field._data = reader.ReadBytes((int)field._size);
			return true;
		}

		public static void WriteAllBlocks(List<ZipGenericExtraField> fields, Stream stream)
		{
			foreach (ZipGenericExtraField field in fields)
			{
				field.WriteBlock(stream);
			}
		}

		public void WriteBlock(Stream stream)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			binaryWriter.Write(this.Tag);
			binaryWriter.Write(this.Size);
			binaryWriter.Write(this.Data);
		}
	}
}