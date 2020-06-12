using System;
using System.Collections.Generic;
using System.IO;

namespace System.IO.Compression
{
	internal struct ZipLocalFileHeader
	{
		public const uint DataDescriptorSignature = 134695760;

		public const uint SignatureConstant = 67324752;

		public const int OffsetToCrcFromHeaderStart = 14;

		public const int OffsetToBitFlagFromHeaderStart = 6;

		public const int SizeOfLocalHeader = 30;

		public static List<ZipGenericExtraField> GetExtraFields(BinaryReader reader)
		{
			List<ZipGenericExtraField> zipGenericExtraFields;
			reader.BaseStream.Seek((long)26, SeekOrigin.Current);
			ushort num = reader.ReadUInt16();
			ushort num1 = reader.ReadUInt16();
			reader.BaseStream.Seek((long)num, SeekOrigin.Current);
			using (Stream subReadStream = new SubReadStream(reader.BaseStream, reader.BaseStream.Position, (long)num1))
			{
				zipGenericExtraFields = ZipGenericExtraField.ParseExtraField(subReadStream);
			}
			Zip64ExtraField.RemoveZip64Blocks(zipGenericExtraFields);
			return zipGenericExtraFields;
		}

		public static bool TrySkipBlock(BinaryReader reader)
		{
			if (reader.ReadUInt32() != 67324752)
			{
				return false;
			}
			if (reader.BaseStream.Length < reader.BaseStream.Position + (long)22)
			{
				return false;
			}
			reader.BaseStream.Seek((long)22, SeekOrigin.Current);
			ushort num = reader.ReadUInt16();
			ushort num1 = reader.ReadUInt16();
			if (reader.BaseStream.Length < reader.BaseStream.Position + (ulong)num + (ulong)num1)
			{
				return false;
			}
			reader.BaseStream.Seek((long)(num + num1), SeekOrigin.Current);
			return true;
		}
	}
}