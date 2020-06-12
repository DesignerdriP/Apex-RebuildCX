using System;

namespace loader.App
{
	public enum EAppState
	{
		Disabled,
		NotInstalled,
		Paused,
		NeedsUpdate,
		Installed,
		Running
	}
}