Imports System

Namespace ProcessPrivileges
	Friend Enum TokenInformationClass
		None
		TokenUser
		TokenGroups
		TokenPrivileges
		TokenOwner
		TokenPrimaryGroup
		TokenDefaultDacl
		TokenSource
		TokenType
		TokenImpersonationLevel
		TokenStatistics
		TokenRestrictedSids
		TokenSessionId
		TokenGroupsAndPrivileges
		TokenSessionReference
		TokenSandBoxInert
		TokenAuditPolicy
		TokenOrigin
		TokenElevationType
		TokenLinkedToken
		TokenElevation
		TokenHasRestrictions
		TokenAccessInformation
		TokenVirtualizationAllowed
		TokenVirtualizationEnabled
		TokenIntegrityLevel
		TokenUIAccess
		TokenMandatoryPolicy
		TokenLogonSid
		MaxTokenInfoClass
	End Enum
End Namespace