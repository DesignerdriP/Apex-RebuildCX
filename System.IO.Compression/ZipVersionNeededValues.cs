using System;

namespace System.IO.Compression
{
	internal enum ZipVersionNeededValues : ushort
	{
		Default = 10,
		Deflate = 20,
		ExplicitDirectory = 20,
		Zip64 = 45
	}
}