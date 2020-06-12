using System;

namespace loader
{
	[Flags]
	public enum ProcessAccessFlags : uint
	{
		Terminate = 1,
		CreateThread = 2,
		VirtualMemoryOperation = 8,
		VirtualMemoryRead = 16,
		VirtualMemoryWrite = 32,
		DuplicateHandle = 64,
		CreateProcess = 128,
		SetQuota = 256,
		SetInformation = 512,
		QueryInformation = 1024,
		QueryLimitedInformation = 4096,
		Synchronize = 1048576,
		All = 2035711
	}
}