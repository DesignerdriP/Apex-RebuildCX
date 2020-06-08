Imports System

Namespace ProcessPrivileges
	<Flags>
	Public Enum PrivilegeAttributes
		UsedForAccess = -2147483648
		Disabled = 0
		EnabledByDefault = 1
		Enabled = 2
		Removed = 4
	End Enum
End Namespace