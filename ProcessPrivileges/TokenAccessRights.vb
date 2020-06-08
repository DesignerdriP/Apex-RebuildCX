Imports System

Namespace ProcessPrivileges
	<Flags>
	Public Enum TokenAccessRights
		AssignPrimary = 0
		Duplicate = 1
		Impersonate = 4
		Query = 8
		QuerySource = 16
		AdjustPrivileges = 32
		AdjustGroups = 64
		AdjustDefault = 128
		AdjustSessionId = 256
		Execute = 131076
		Read = 131080
		Write = 131296
		AllAccess = 983549
	End Enum
End Namespace