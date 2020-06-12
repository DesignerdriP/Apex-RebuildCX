using System;

namespace loader
{
	public enum AccessProtectionFlags : uint
	{
		PAGE_NOACCESS = 1,
		PAGE_READONLY = 2,
		PAGE_READWRITE = 4,
		PAGE_WRITECOPY = 8,
		PAGE_EXECUTE = 16,
		PAGE_EXECUTE_READ = 32,
		PAGE_EXECUTE_READWRITE = 64,
		PAGE_EXECUTE_WRITECOPY = 128,
		PAGE_GUARD = 256,
		PAGE_NOCACHE = 512,
		PAGE_WRITECOMBINE = 1024
	}
}