using System;

namespace loader
{
	[Flags]
	public enum ThreadAccess
	{
		Terminate = 1,
		SuspendResume = 2,
		GetContext = 8,
		SetContext = 16,
		SetInformation = 32,
		QueryInformation = 64,
		SetThreadToken = 128,
		Impersonate = 256,
		DirectImpersonation = 512,
		All = 2097151
	}
}