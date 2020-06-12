using System;
using System.Collections.Generic;
using System.IO;

namespace System.IO.Compression
{
	internal struct ZipCentralDirectoryFileHeader
	{
		public const uint SignatureConstant = 33639248;

		public ushort VersionMadeBy;

		public ushort VersionNeededToExtract;

		public ushort GeneralPurposeBitFlag;

		public ushort CompressionMethod;

		public uint LastModified;

		public uint Crc32;

		public long CompressedSize;

		public long UncompressedSize;

		public ushort FilenameLength;

		public ushort ExtraFieldLength;

		public ushort FileCommentLength;

		public int DiskNumberStart;

		public ushort InternalFileAttributes;

		public uint ExternalFileAttributes;

		public long RelativeOffsetOfLocalHeader;

		public byte[] Filename;

		public byte[] FileComment;

		public List<ZipGenericExtraField> ExtraFields;

		public static bool TryReadBlock(BinaryReader reader, bool saveExtraFieldsAndComments, out ZipCentralDirectoryFileHeader header)
		{
			Zip64ExtraField justZip64Block;
			long? uncompressedSize;
			long value;
			long num;
			long value1;
			header = new ZipCentralDirectoryFileHeader();
			if (reader.ReadUInt32() != 33639248)
			{
				return false;
			}
			header.VersionMadeBy = reader.ReadUInt16();
			header.VersionNeededToExtract = reader.ReadUInt16();
			header.GeneralPurposeBitFlag = reader.ReadUInt16();
			header.CompressionMethod = reader.ReadUInt16();
			header.LastModified = reader.ReadUInt32();
			header.Crc32 = reader.ReadUInt32();
			uint num1 = reader.ReadUInt32();
			uint num2 = reader.ReadUInt32();
			header.FilenameLength = reader.ReadUInt16();
			header.ExtraFieldLength = reader.ReadUInt16();
			header.FileCommentLength = reader.ReadUInt16();
			ushort num3 = reader.ReadUInt16();
			header.InternalFileAttributes = reader.ReadUInt16();
			header.ExternalFileAttributes = reader.ReadUInt32();
			uint num4 = reader.ReadUInt32();
			header.Filename = reader.ReadBytes((int)header.FilenameLength);
			bool flag = num2 == -1;
			bool flag1 = num1 == -1;
			bool flag2 = num4 == -1;
			bool flag3 = num3 == 65535;
			long position = (long)(reader.BaseStream.Position + (ulong)header.ExtraFieldLength);
			using (Stream subReadStream = new SubReadStream(reader.BaseStream, reader.BaseStream.Position, (long)header.ExtraFieldLength))
			{
				if (!saveExtraFieldsAndComments)
				{
					header.ExtraFields = null;
					justZip64Block = Zip64ExtraField.GetJustZip64Block(subReadStream, flag, flag1, flag2, flag3);
				}
				else
				{
					header.ExtraFields = ZipGenericExtraField.ParseExtraField(subReadStream);
					justZip64Block = Zip64ExtraField.GetAndRemoveZip64Block(header.ExtraFields, flag, flag1, flag2, flag3);
				}
			}
			reader.BaseStream.AdvanceToPosition(position);
			if (!saveExtraFieldsAndComments)
			{
				Stream baseStream = reader.BaseStream;
				baseStream.Position = (long)(baseStream.Position + (ulong)header.FileCommentLength);
				header.FileComment = null;
			}
			else
			{
				header.FileComment = reader.ReadBytes((int)header.FileCommentLength);
			}
			if (!justZip64Block.UncompressedSize.HasValue)
			{
				value = (long)((ulong)num2);
			}
			else
			{
				uncompressedSize = justZip64Block.UncompressedSize;
				value = uncompressedSize.Value;
			}
			header.UncompressedSize = value;
			if (!justZip64Block.CompressedSize.HasValue)
			{
				num = (long)((ulong)num1);
			}
			else
			{
				uncompressedSize = justZip64Block.CompressedSize;
				num = uncompressedSize.Value;
			}
			header.CompressedSize = num;
			if (!justZip64Block.LocalHeaderOffset.HasValue)
			{
				value1 = (long)((ulong)num4);
			}
			else
			{
				uncompressedSize = justZip64Block.LocalHeaderOffset;
				value1 = uncompressedSize.Value;
			}
			header.RelativeOffsetOfLocalHeader = value1;
			header.DiskNumberStart = (!justZip64Block.StartDiskNumber.HasValue ? (int)num3 : justZip64Block.StartDiskNumber.Value);
			return true;
		}
	}
}