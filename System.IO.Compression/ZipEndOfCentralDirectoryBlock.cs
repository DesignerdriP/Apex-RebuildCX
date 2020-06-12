using System;
using System.IO;

namespace System.IO.Compression
{
	internal struct ZipEndOfCentralDirectoryBlock
	{
		public const uint SignatureConstant = 101010256;

		public const int SizeOfBlockWithoutSignature = 18;

		public uint Signature;

		public ushort NumberOfThisDisk;

		public ushort NumberOfTheDiskWithTheStartOfTheCentralDirectory;

		public ushort NumberOfEntriesInTheCentralDirectoryOnThisDisk;

		public ushort NumberOfEntriesInTheCentralDirectory;

		public uint SizeOfCentralDirectory;

		public uint OffsetOfStartOfCentralDirectoryWithRespectToTheStartingDiskNumber;

		public byte[] ArchiveComment;

		public static bool TryReadBlock(BinaryReader reader, out ZipEndOfCentralDirectoryBlock eocdBlock)
		{
			eocdBlock = new ZipEndOfCentralDirectoryBlock();
			if (reader.ReadUInt32() != 101010256)
			{
				return false;
			}
			eocdBlock.Signature = 101010256;
			eocdBlock.NumberOfThisDisk = reader.ReadUInt16();
			eocdBlock.NumberOfTheDiskWithTheStartOfTheCentralDirectory = reader.ReadUInt16();
			eocdBlock.NumberOfEntriesInTheCentralDirectoryOnThisDisk = reader.ReadUInt16();
			eocdBlock.NumberOfEntriesInTheCentralDirectory = reader.ReadUInt16();
			eocdBlock.SizeOfCentralDirectory = reader.ReadUInt32();
			eocdBlock.OffsetOfStartOfCentralDirectoryWithRespectToTheStartingDiskNumber = reader.ReadUInt32();
			eocdBlock.ArchiveComment = reader.ReadBytes((int)reader.ReadUInt16());
			return true;
		}

		public static void WriteBlock(Stream stream, long numberOfEntries, long startOfCentralDirectory, long sizeOfCentralDirectory, byte[] archiveComment)
		{
			ushort num;
			uint num1;
			uint num2;
			ushort length;
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (numberOfEntries > (long)65535)
			{
				num = 65535;
			}
			else
			{
				num = (ushort)numberOfEntries;
			}
			ushort num3 = num;
			if (startOfCentralDirectory > (ulong)-1)
			{
				num1 = -1;
			}
			else
			{
				num1 = (uint)startOfCentralDirectory;
			}
			uint num4 = num1;
			if (sizeOfCentralDirectory > (ulong)-1)
			{
				num2 = -1;
			}
			else
			{
				num2 = (uint)sizeOfCentralDirectory;
			}
			uint num5 = num2;
			binaryWriter.Write((uint)101010256);
			binaryWriter.Write((ushort)0);
			binaryWriter.Write((ushort)0);
			binaryWriter.Write(num3);
			binaryWriter.Write(num3);
			binaryWriter.Write(num5);
			binaryWriter.Write(num4);
			BinaryWriter binaryWriter1 = binaryWriter;
			if (archiveComment != null)
			{
				length = (ushort)((int)archiveComment.Length);
			}
			else
			{
				length = 0;
			}
			binaryWriter1.Write(length);
			if (archiveComment != null)
			{
				binaryWriter.Write(archiveComment);
			}
		}
	}
}