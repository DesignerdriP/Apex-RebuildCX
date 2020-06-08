Imports System
Imports System.Diagnostics
Imports System.Runtime.CompilerServices
Imports System.Security.Permissions

Namespace ProcessPrivileges
	Public Module ProcessExtensions
		<Extension>
		<MethodImpl(MethodImplOptions.Synchronized)>
		<PermissionSet(SecurityAction.LinkDemand, Name:="FullTrust")>
		Public Function DisablePrivilege(ByVal accessTokenHandle As ProcessPrivileges.AccessTokenHandle, ByVal privilege As ProcessPrivileges.Privilege) As AdjustPrivilegeResult
			Return Privileges.DisablePrivilege(accessTokenHandle, privilege)
		End Function

		<Extension>
		<MethodImpl(MethodImplOptions.Synchronized)>
		<PermissionSet(SecurityAction.LinkDemand, Name:="FullTrust")>
		Public Function DisablePrivilege(ByVal process As System.Diagnostics.Process, ByVal privilege As ProcessPrivileges.Privilege) As ProcessPrivileges.AdjustPrivilegeResult
			Dim adjustPrivilegeResult As ProcessPrivileges.AdjustPrivilegeResult
			Using accessTokenHandle As ProcessPrivileges.AccessTokenHandle = New ProcessPrivileges.AccessTokenHandle(New ProcessHandle(process.Handle, False), TokenAccessRights.Query Or TokenAccessRights.AdjustPrivileges)
				adjustPrivilegeResult = accessTokenHandle.DisablePrivilege(privilege)
			End Using
			Return adjustPrivilegeResult
		End Function

		<Extension>
		<MethodImpl(MethodImplOptions.Synchronized)>
		<PermissionSet(SecurityAction.LinkDemand, Name:="FullTrust")>
		Public Function EnablePrivilege(ByVal accessTokenHandle As ProcessPrivileges.AccessTokenHandle, ByVal privilege As ProcessPrivileges.Privilege) As AdjustPrivilegeResult
			Return Privileges.EnablePrivilege(accessTokenHandle, privilege)
		End Function

		<Extension>
		<MethodImpl(MethodImplOptions.Synchronized)>
		<PermissionSet(SecurityAction.LinkDemand, Name:="FullTrust")>
		Public Function EnablePrivilege(ByVal process As System.Diagnostics.Process, ByVal privilege As ProcessPrivileges.Privilege) As ProcessPrivileges.AdjustPrivilegeResult
			Dim adjustPrivilegeResult As ProcessPrivileges.AdjustPrivilegeResult
			Using accessTokenHandle As ProcessPrivileges.AccessTokenHandle = New ProcessPrivileges.AccessTokenHandle(New ProcessHandle(process.Handle, False), TokenAccessRights.Query Or TokenAccessRights.AdjustPrivileges)
				adjustPrivilegeResult = accessTokenHandle.EnablePrivilege(privilege)
			End Using
			Return adjustPrivilegeResult
		End Function

		<Extension>
		<MethodImpl(MethodImplOptions.Synchronized)>
		<PermissionSet(SecurityAction.LinkDemand, Name:="FullTrust")>
		Public Function GetAccessTokenHandle(ByVal process As System.Diagnostics.Process) As AccessTokenHandle
			Return process.GetAccessTokenHandle(TokenAccessRights.AllAccess)
		End Function

		<Extension>
		<MethodImpl(MethodImplOptions.Synchronized)>
		<PermissionSet(SecurityAction.LinkDemand, Name:="FullTrust")>
		Public Function GetAccessTokenHandle(ByVal process As System.Diagnostics.Process, ByVal tokenAccessRights As ProcessPrivileges.TokenAccessRights) As AccessTokenHandle
			Return New AccessTokenHandle(New ProcessHandle(process.Handle, False), tokenAccessRights)
		End Function

		<Extension>
		<MethodImpl(MethodImplOptions.Synchronized)>
		<PermissionSet(SecurityAction.LinkDemand, Name:="FullTrust")>
		Public Function GetPrivilegeAttributes(ByVal accessTokenHandle As ProcessPrivileges.AccessTokenHandle, ByVal privilege As ProcessPrivileges.Privilege) As PrivilegeAttributes
			Return Privileges.GetPrivilegeAttributes(privilege, accessTokenHandle.GetPrivileges())
		End Function

		<Extension>
		<MethodImpl(MethodImplOptions.Synchronized)>
		<PermissionSet(SecurityAction.LinkDemand, Name:="FullTrust")>
		Public Function GetPrivilegeAttributes(ByVal process As System.Diagnostics.Process, ByVal privilege As ProcessPrivileges.Privilege) As PrivilegeAttributes
			Return Privileges.GetPrivilegeAttributes(privilege, process.GetPrivileges())
		End Function

		<Extension>
		<MethodImpl(MethodImplOptions.Synchronized)>
		<PermissionSet(SecurityAction.LinkDemand, Name:="FullTrust")>
		Public Function GetPrivileges(ByVal accessTokenHandle As ProcessPrivileges.AccessTokenHandle) As PrivilegeAndAttributesCollection
			Return Privileges.GetPrivileges(accessTokenHandle)
		End Function

		<Extension>
		<MethodImpl(MethodImplOptions.Synchronized)>
		<PermissionSet(SecurityAction.LinkDemand, Name:="FullTrust")>
		Public Function GetPrivileges(ByVal process As System.Diagnostics.Process) As PrivilegeAndAttributesCollection
			Dim privileges As PrivilegeAndAttributesCollection
			Using accessTokenHandle As ProcessPrivileges.AccessTokenHandle = New ProcessPrivileges.AccessTokenHandle(New ProcessHandle(process.Handle, False), TokenAccessRights.Query)
				privileges = ProcessPrivileges.Privileges.GetPrivileges(accessTokenHandle)
			End Using
			Return privileges
		End Function

		<Extension>
		<MethodImpl(MethodImplOptions.Synchronized)>
		<PermissionSet(SecurityAction.LinkDemand, Name:="FullTrust")>
		Public Function GetPrivilegeState(ByVal accessTokenHandle As ProcessPrivileges.AccessTokenHandle, ByVal privilege As ProcessPrivileges.Privilege) As PrivilegeState
			Return ProcessExtensions.GetPrivilegeState(accessTokenHandle.GetPrivilegeAttributes(privilege))
		End Function

		<Extension>
		<MethodImpl(MethodImplOptions.Synchronized)>
		<PermissionSet(SecurityAction.LinkDemand, Name:="FullTrust")>
		Public Function GetPrivilegeState(ByVal process As System.Diagnostics.Process, ByVal privilege As ProcessPrivileges.Privilege) As PrivilegeState
			Return ProcessExtensions.GetPrivilegeState(process.GetPrivilegeAttributes(privilege))
		End Function

		<MethodImpl(MethodImplOptions.Synchronized)>
		Public Function GetPrivilegeState(ByVal privilegeAttributes As ProcessPrivileges.PrivilegeAttributes) As PrivilegeState
			If ((privilegeAttributes And ProcessPrivileges.PrivilegeAttributes.Enabled) = ProcessPrivileges.PrivilegeAttributes.Enabled) Then
				Return PrivilegeState.Enabled
			End If
			If ((privilegeAttributes And ProcessPrivileges.PrivilegeAttributes.Removed) = ProcessPrivileges.PrivilegeAttributes.Removed) Then
				Return PrivilegeState.Removed
			End If
			Return PrivilegeState.Disabled
		End Function

		<Extension>
		<MethodImpl(MethodImplOptions.Synchronized)>
		<PermissionSet(SecurityAction.LinkDemand, Name:="FullTrust")>
		Public Function RemovePrivilege(ByVal accessTokenHandle As ProcessPrivileges.AccessTokenHandle, ByVal privilege As ProcessPrivileges.Privilege) As AdjustPrivilegeResult
			Return Privileges.RemovePrivilege(accessTokenHandle, privilege)
		End Function

		<Extension>
		<MethodImpl(MethodImplOptions.Synchronized)>
		<PermissionSet(SecurityAction.LinkDemand, Name:="FullTrust")>
		Public Function RemovePrivilege(ByVal process As System.Diagnostics.Process, ByVal privilege As ProcessPrivileges.Privilege) As ProcessPrivileges.AdjustPrivilegeResult
			Dim adjustPrivilegeResult As ProcessPrivileges.AdjustPrivilegeResult
			Using accessTokenHandle As ProcessPrivileges.AccessTokenHandle = New ProcessPrivileges.AccessTokenHandle(New ProcessHandle(process.Handle, False), TokenAccessRights.Query Or TokenAccessRights.AdjustPrivileges)
				adjustPrivilegeResult = accessTokenHandle.RemovePrivilege(privilege)
			End Using
			Return adjustPrivilegeResult
		End Function
	End Module
End Namespace