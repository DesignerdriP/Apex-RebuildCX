using System;

namespace loader
{
	public enum DllInjectionStatus
	{
		Success,
		AlreadyExists,
		InjectionFailed,
		RemoteAllocationFailed,
		ProcessWriteFailed,
		WaitForSingleObjectFailed,
		GetExitCodeThreadFailed,
		FreeMemoryFailed,
		FreeHandleFailed,
		GetProcAddressFailed
	}
}