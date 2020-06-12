using System;
using System.IO;
using System.Runtime.CompilerServices;

namespace System.IO.Compression
{
	internal static class ZipHelper
	{
		internal const uint Mask32Bit = 4294967295;

		internal const ushort Mask16Bit = 65535;

		private const int BackwardsSeekingBufferSize = 32;

		internal const int ValidZipDate_YearMin = 1980;

		internal const int ValidZipDate_YearMax = 2107;

		private readonly static DateTime InvalidDateIndicator;

		static ZipHelper()
		{
			ZipHelper.InvalidDateIndicator = new DateTime(1980, 1, 1, 0, 0, 0);
		}

		internal static void AdvanceToPosition(this Stream stream, long position)
		{
			int num = 0;
			int num1;
			for (long i = position - stream.Position; i != 0; i -= (long)num)
			{
				num1 = (i > (long)64 ? 64 : (int)i);
				num = stream.Read(new byte[64], 0, num1);
				if (num == 0)
				{
					throw new IOException(Messages.UnexpectedEndOfStream);
				}
			}
		}

		internal static uint DateTimeToDosTime(DateTime dateTime)
		{
			int year = dateTime.Year - 1980 & 127;
			year = (year << 4) + dateTime.Month;
			year = (year << 5) + dateTime.Day;
			year = (year << 5) + dateTime.Hour;
			year = (year << 6) + dateTime.Minute;
			year = (year << 5) + dateTime.Second / 2;
			return (uint)year;
		}

		internal static DateTime DosTimeToDateTime(uint dateTime)
		{
			DateTime invalidDateIndicator;
			int num = (int)(1980 + (dateTime >> 25));
			int num1 = (int)(dateTime >> 21 & 15);
			int num2 = (int)(dateTime >> 16 & 31);
			int num3 = (int)(dateTime >> 11 & 31);
			int num4 = (int)(dateTime >> 5 & 63);
			int num5 = (int)((dateTime & 31) * 2);
			try
			{
				invalidDateIndicator = new DateTime(num, num1, num2, num3, num4, num5, 0);
			}
			catch (ArgumentOutOfRangeException argumentOutOfRangeException)
			{
				invalidDateIndicator = ZipHelper.InvalidDateIndicator;
			}
			catch (ArgumentException argumentException)
			{
				invalidDateIndicator = ZipHelper.InvalidDateIndicator;
			}
			return invalidDateIndicator;
		}

		internal static bool EndsWithDirChar(string test)
		{
			return Path.GetFileName(test) == "";
		}

		internal static void ReadBytes(Stream stream, byte[] buffer, int bytesToRead)
		{
			int num = bytesToRead;
			int num1 = 0;
			while (num > 0)
			{
				int num2 = stream.Read(buffer, num1, num);
				if (num2 == 0)
				{
					throw new IOException(Messages.UnexpectedEndOfStream);
				}
				num1 += num2;
				num -= num2;
			}
		}

		internal static bool RequiresUnicode(string test)
		{
			string str = test;
			for (int i = 0; i < str.Length; i++)
			{
				if (str[i] > '\u007F')
				{
					return true;
				}
			}
			return false;
		}

		private static bool SeekBackwardsAndRead(Stream stream, byte[] buffer, out int bufferPointer)
		{
			if (stream.Position < (long)((int)buffer.Length))
			{
				int position = (int)stream.Position;
				stream.Seek((long)0, SeekOrigin.Begin);
				ZipHelper.ReadBytes(stream, buffer, position);
				stream.Seek((long)0, SeekOrigin.Begin);
				bufferPointer = position - 1;
				return true;
			}
			stream.Seek((long)(-(int)buffer.Length), SeekOrigin.Current);
			ZipHelper.ReadBytes(stream, buffer, (int)buffer.Length);
			stream.Seek((long)(-(int)buffer.Length), SeekOrigin.Current);
			bufferPointer = (int)buffer.Length - 1;
			return false;
		}

		internal static bool SeekBackwardsToSignature(Stream stream, uint signatureToFind)
		{
			int num = 0;
			uint num1 = 0;
			byte[] numArray = new byte[32];
			bool flag = false;
			bool flag1 = false;
			while (!flag1 && !flag)
			{
				flag = ZipHelper.SeekBackwardsAndRead(stream, numArray, out num);
				while (num >= 0 && !flag1)
				{
					num1 = num1 << 8 | numArray[num];
					if (num1 != signatureToFind)
					{
						num--;
					}
					else
					{
						flag1 = true;
					}
				}
			}
			if (!flag1)
			{
				return false;
			}
			stream.Seek((long)num, SeekOrigin.Current);
			return true;
		}
	}
}