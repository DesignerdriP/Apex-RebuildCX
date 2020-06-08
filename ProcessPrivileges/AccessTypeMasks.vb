Imports System

Namespace ProcessPrivileges
	<Flags>
	Friend Enum AccessTypeMasks
		SpecificRightsAll = 65535
		Delete = 65536
		ReadControl = 131072
		StandardRightsExecute = 131072
		StandardRightsRead = 131072
		StandardRightsWrite = 131072
		WriteDAC = 262144
		WriteOwner = 524288
		StandardRightsRequired = 983040
		Synchronize = 1048576
		StandardRightsAll = 2031616
	End Enum
End Namespace