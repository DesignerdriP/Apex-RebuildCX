Imports System

Namespace ProcessPrivileges
	Public Enum Privilege
		AssignPrimaryToken
		Audit
		Backup
		ChangeNotify
		CreateGlobal
		CreatePageFile
		CreatePermanent
		CreateSymbolicLink
		CreateToken
		Debug
		EnableDelegation
		Impersonate
		IncreaseBasePriority
		IncreaseQuota
		IncreaseWorkingSet
		LoadDriver
		LockMemory
		MachineAccount
		ManageVolume
		ProfileSingleProcess
		Relabel
		RemoteShutdown
		Restore
		Security
		Shutdown
		SyncAgent
		SystemEnvironment
		SystemProfile
		SystemTime
		TakeOwnership
		TrustedComputerBase
		TimeZone
		TrustedCredentialManagerAccess
		Undock
		UnsolicitedInput
	End Enum
End Namespace