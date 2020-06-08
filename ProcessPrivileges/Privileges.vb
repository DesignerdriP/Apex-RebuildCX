Imports System
Imports System.Collections
Imports System.Collections.Generic
Imports System.Collections.ObjectModel
Imports System.ComponentModel
Imports System.Runtime.InteropServices
Imports System.Text

Namespace ProcessPrivileges
	Friend Module Privileges
		Private Const PrivilegesCount As Integer = 35

		Private Const SeAssignPrimaryTokenPrivilege As String = "SeAssignPrimaryTokenPrivilege"

		Private Const SeAuditPrivilege As String = "SeAuditPrivilege"

		Private Const SeBackupPrivilege As String = "SeBackupPrivilege"

		Private Const SeChangeNotifyPrivilege As String = "SeChangeNotifyPrivilege"

		Private Const SeCreateGlobalPrivilege As String = "SeCreateGlobalPrivilege"

		Private Const SeCreatePagefilePrivilege As String = "SeCreatePagefilePrivilege"

		Private Const SeCreatePermanentPrivilege As String = "SeCreatePermanentPrivilege"

		Private Const SeCreateSymbolicLinkPrivilege As String = "SeCreateSymbolicLinkPrivilege"

		Private Const SeCreateTokenPrivilege As String = "SeCreateTokenPrivilege"

		Private Const SeDebugPrivilege As String = "SeDebugPrivilege"

		Private Const SeEnableDelegationPrivilege As String = "SeEnableDelegationPrivilege"

		Private Const SeImpersonatePrivilege As String = "SeImpersonatePrivilege"

		Private Const SeIncreaseBasePriorityPrivilege As String = "SeIncreaseBasePriorityPrivilege"

		Private Const SeIncreaseQuotaPrivilege As String = "SeIncreaseQuotaPrivilege"

		Private Const SeIncreaseWorkingSetPrivilege As String = "SeIncreaseWorkingSetPrivilege"

		Private Const SeLoadDriverPrivilege As String = "SeLoadDriverPrivilege"

		Private Const SeLockMemoryPrivilege As String = "SeLockMemoryPrivilege"

		Private Const SeMachineAccountPrivilege As String = "SeMachineAccountPrivilege"

		Private Const SeManageVolumePrivilege As String = "SeManageVolumePrivilege"

		Private Const SeProfileSingleProcessPrivilege As String = "SeProfileSingleProcessPrivilege"

		Private Const SeRelabelPrivilege As String = "SeRelabelPrivilege"

		Private Const SeRemoteShutdownPrivilege As String = "SeRemoteShutdownPrivilege"

		Private Const SeRestorePrivilege As String = "SeRestorePrivilege"

		Private Const SeSecurityPrivilege As String = "SeSecurityPrivilege"

		Private Const SeShutdownPrivilege As String = "SeShutdownPrivilege"

		Private Const SeSyncAgentPrivilege As String = "SeSyncAgentPrivilege"

		Private Const SeSystemEnvironmentPrivilege As String = "SeSystemEnvironmentPrivilege"

		Private Const SeSystemProfilePrivilege As String = "SeSystemProfilePrivilege"

		Private Const SeSystemTimePrivilege As String = "SeSystemtimePrivilege"

		Private Const SeTakeOwnershipPrivilege As String = "SeTakeOwnershipPrivilege"

		Private Const SeTcbPrivilege As String = "SeTcbPrivilege"

		Private Const SeTimeZonePrivilege As String = "SeTimeZonePrivilege"

		Private Const SeTrustedCredManAccessPrivilege As String = "SeTrustedCredManAccessPrivilege"

		Private Const SeUndockPrivilege As String = "SeUndockPrivilege"

		Private Const SeUnsolicitedInputPrivilege As String = "SeUnsolicitedInputPrivilege"

		Private ReadOnly luidDictionary As Dictionary(Of Privilege, Luid)

		Private ReadOnly privilegeConstantsDictionary As Dictionary(Of Privilege, String)

		Private ReadOnly privilegesDictionary As Dictionary(Of String, Privilege)

		Sub New()
			Privileges.luidDictionary = New Dictionary(Of Privilege, Luid)(35)
			Privileges.privilegeConstantsDictionary = New Dictionary(Of Privilege, String)(35) From
			{
				{ Privilege.AssignPrimaryToken, "SeAssignPrimaryTokenPrivilege" },
				{ Privilege.Audit, "SeAuditPrivilege" },
				{ Privilege.Backup, "SeBackupPrivilege" },
				{ Privilege.ChangeNotify, "SeChangeNotifyPrivilege" },
				{ Privilege.CreateGlobal, "SeCreateGlobalPrivilege" },
				{ Privilege.CreatePageFile, "SeCreatePagefilePrivilege" },
				{ Privilege.CreatePermanent, "SeCreatePermanentPrivilege" },
				{ Privilege.CreateSymbolicLink, "SeCreateSymbolicLinkPrivilege" },
				{ Privilege.CreateToken, "SeCreateTokenPrivilege" },
				{ Privilege.Debug, "SeDebugPrivilege" },
				{ Privilege.EnableDelegation, "SeEnableDelegationPrivilege" },
				{ Privilege.Impersonate, "SeImpersonatePrivilege" },
				{ Privilege.IncreaseBasePriority, "SeIncreaseBasePriorityPrivilege" },
				{ Privilege.IncreaseQuota, "SeIncreaseQuotaPrivilege" },
				{ Privilege.IncreaseWorkingSet, "SeIncreaseWorkingSetPrivilege" },
				{ Privilege.LoadDriver, "SeLoadDriverPrivilege" },
				{ Privilege.LockMemory, "SeLockMemoryPrivilege" },
				{ Privilege.MachineAccount, "SeMachineAccountPrivilege" },
				{ Privilege.ManageVolume, "SeManageVolumePrivilege" },
				{ Privilege.ProfileSingleProcess, "SeProfileSingleProcessPrivilege" },
				{ Privilege.Relabel, "SeRelabelPrivilege" },
				{ Privilege.RemoteShutdown, "SeRemoteShutdownPrivilege" },
				{ Privilege.Restore, "SeRestorePrivilege" },
				{ Privilege.Security, "SeSecurityPrivilege" },
				{ Privilege.Shutdown, "SeShutdownPrivilege" },
				{ Privilege.SyncAgent, "SeSyncAgentPrivilege" },
				{ Privilege.SystemEnvironment, "SeSystemEnvironmentPrivilege" },
				{ Privilege.SystemProfile, "SeSystemProfilePrivilege" },
				{ Privilege.SystemTime, "SeSystemtimePrivilege" },
				{ Privilege.TakeOwnership, "SeTakeOwnershipPrivilege" },
				{ Privilege.TrustedComputerBase, "SeTcbPrivilege" },
				{ Privilege.TimeZone, "SeTimeZonePrivilege" },
				{ Privilege.TrustedCredentialManagerAccess, "SeTrustedCredManAccessPrivilege" },
				{ Privilege.Undock, "SeUndockPrivilege" },
				{ Privilege.UnsolicitedInput, "SeUnsolicitedInputPrivilege" }
			}
			Privileges.privilegesDictionary = New Dictionary(Of String, Privilege)(35) From
			{
				{ "SeAssignPrimaryTokenPrivilege", Privilege.AssignPrimaryToken },
				{ "SeAuditPrivilege", Privilege.Audit },
				{ "SeBackupPrivilege", Privilege.Backup },
				{ "SeChangeNotifyPrivilege", Privilege.ChangeNotify },
				{ "SeCreateGlobalPrivilege", Privilege.CreateGlobal },
				{ "SeCreatePagefilePrivilege", Privilege.CreatePageFile },
				{ "SeCreatePermanentPrivilege", Privilege.CreatePermanent },
				{ "SeCreateSymbolicLinkPrivilege", Privilege.CreateSymbolicLink },
				{ "SeCreateTokenPrivilege", Privilege.CreateToken },
				{ "SeDebugPrivilege", Privilege.Debug },
				{ "SeEnableDelegationPrivilege", Privilege.EnableDelegation },
				{ "SeImpersonatePrivilege", Privilege.Impersonate },
				{ "SeIncreaseBasePriorityPrivilege", Privilege.IncreaseBasePriority },
				{ "SeIncreaseQuotaPrivilege", Privilege.IncreaseQuota },
				{ "SeIncreaseWorkingSetPrivilege", Privilege.IncreaseWorkingSet },
				{ "SeLoadDriverPrivilege", Privilege.LoadDriver },
				{ "SeLockMemoryPrivilege", Privilege.LockMemory },
				{ "SeMachineAccountPrivilege", Privilege.MachineAccount },
				{ "SeManageVolumePrivilege", Privilege.ManageVolume },
				{ "SeProfileSingleProcessPrivilege", Privilege.ProfileSingleProcess },
				{ "SeRelabelPrivilege", Privilege.Relabel },
				{ "SeRemoteShutdownPrivilege", Privilege.RemoteShutdown },
				{ "SeRestorePrivilege", Privilege.Restore },
				{ "SeSecurityPrivilege", Privilege.Security },
				{ "SeShutdownPrivilege", Privilege.Shutdown },
				{ "SeSyncAgentPrivilege", Privilege.SyncAgent },
				{ "SeSystemEnvironmentPrivilege", Privilege.SystemEnvironment },
				{ "SeSystemProfilePrivilege", Privilege.SystemProfile },
				{ "SeSystemtimePrivilege", Privilege.SystemTime },
				{ "SeTakeOwnershipPrivilege", Privilege.TakeOwnership },
				{ "SeTcbPrivilege", Privilege.TrustedComputerBase },
				{ "SeTimeZonePrivilege", Privilege.TimeZone },
				{ "SeTrustedCredManAccessPrivilege", Privilege.TrustedCredentialManagerAccess },
				{ "SeUndockPrivilege", Privilege.Undock },
				{ "SeUnsolicitedInputPrivilege", Privilege.UnsolicitedInput }
			}
		End Sub

		Private Function AdjustPrivilege(ByVal accessTokenHandle As ProcessPrivileges.AccessTokenHandle, ByVal luid As ProcessPrivileges.Luid, ByVal privilegeAttributes As ProcessPrivileges.PrivilegeAttributes) As AdjustPrivilegeResult
			Dim tokenPrivilege As ProcessPrivileges.TokenPrivilege = New ProcessPrivileges.TokenPrivilege() With
			{
				.PrivilegeCount = 1
			}
			Dim luidAndAttribute As LuidAndAttributes = New LuidAndAttributes() With
			{
				.Attributes = privilegeAttributes,
				.Luid = luid
			}
			tokenPrivilege.Privilege = luidAndAttribute
			Dim tokenPrivilege1 As ProcessPrivileges.TokenPrivilege = tokenPrivilege
			Dim tokenPrivilege2 As ProcessPrivileges.TokenPrivilege = New ProcessPrivileges.TokenPrivilege()
			Dim num As Integer = 0
			If (Not ProcessPrivileges.NativeMethods.AdjustTokenPrivileges(accessTokenHandle, False, tokenPrivilege1, Marshal.SizeOf(Of ProcessPrivileges.TokenPrivilege)(tokenPrivilege2), tokenPrivilege2, num)) Then
				Throw New Win32Exception(Marshal.GetLastWin32Error())
			End If
			Return DirectCast(tokenPrivilege2.PrivilegeCount, AdjustPrivilegeResult)
		End Function

		Private Function AdjustPrivilege(ByVal accessTokenHandle As ProcessPrivileges.AccessTokenHandle, ByVal privilege As ProcessPrivileges.Privilege, ByVal privilegeAttributes As ProcessPrivileges.PrivilegeAttributes) As AdjustPrivilegeResult
			Return Privileges.AdjustPrivilege(accessTokenHandle, Privileges.GetLuid(privilege), privilegeAttributes)
		End Function

		Friend Function DisablePrivilege(ByVal accessTokenHandle As ProcessPrivileges.AccessTokenHandle, ByVal privilege As ProcessPrivileges.Privilege) As AdjustPrivilegeResult
			Return Privileges.AdjustPrivilege(accessTokenHandle, privilege, PrivilegeAttributes.Disabled)
		End Function

		Friend Function EnablePrivilege(ByVal accessTokenHandle As ProcessPrivileges.AccessTokenHandle, ByVal privilege As ProcessPrivileges.Privilege) As AdjustPrivilegeResult
			Return Privileges.AdjustPrivilege(accessTokenHandle, privilege, PrivilegeAttributes.Enabled)
		End Function

		Private Function GetLuid(ByVal privilege As ProcessPrivileges.Privilege) As ProcessPrivileges.Luid
			If (Privileges.luidDictionary.ContainsKey(privilege)) Then
				Return Privileges.luidDictionary(privilege)
			End If
			Dim luid As ProcessPrivileges.Luid = New ProcessPrivileges.Luid()
			If (Not ProcessPrivileges.NativeMethods.LookupPrivilegeValue(String.Empty, Privileges.privilegeConstantsDictionary(privilege), luid)) Then
				Throw New Win32Exception(Marshal.GetLastWin32Error())
			End If
			Privileges.luidDictionary.Add(privilege, luid)
			Return luid
		End Function

		Friend Function GetPrivilegeAttributes(ByVal privilege As ProcessPrivileges.Privilege, ByVal privileges As PrivilegeAndAttributesCollection) As ProcessPrivileges.PrivilegeAttributes
			Dim privilegeAttributes As ProcessPrivileges.PrivilegeAttributes
			Using enumerator As IEnumerator(Of PrivilegeAndAttributes) = privileges.GetEnumerator()
				While enumerator.MoveNext()
					Dim current As PrivilegeAndAttributes = enumerator.Current
					If (current.Privilege <> privilege) Then
						Continue While
					End If
					privilegeAttributes = current.PrivilegeAttributes
					Return privilegeAttributes
				End While
				ProcessPrivileges.Privileges.GetLuid(privilege)
				Return ProcessPrivileges.PrivilegeAttributes.Removed
			End Using
			Return privilegeAttributes
		End Function

		Private Function GetPrivilegeName(ByVal luid As ProcessPrivileges.Luid) As String
			Dim stringBuilder As System.Text.StringBuilder = New System.Text.StringBuilder()
			Dim num As Integer = 0
			If (ProcessPrivileges.NativeMethods.LookupPrivilegeName(String.Empty, luid, stringBuilder, num)) Then
				Return String.Empty
			End If
			Dim lastWin32Error As Integer = Marshal.GetLastWin32Error()
			If (lastWin32Error <> 122) Then
				Throw New Win32Exception(lastWin32Error)
			End If
			stringBuilder.EnsureCapacity(num)
			If (Not ProcessPrivileges.NativeMethods.LookupPrivilegeName(String.Empty, luid, stringBuilder, num)) Then
				Throw New Win32Exception(Marshal.GetLastWin32Error())
			End If
			Return stringBuilder.ToString()
		End Function

		Friend Function GetPrivileges(ByVal accessTokenHandle As ProcessPrivileges.AccessTokenHandle) As PrivilegeAndAttributesCollection
			Dim tokenPrivileges As LuidAndAttributes() = Privileges.GetTokenPrivileges(accessTokenHandle)
			Dim length As Integer = CInt(tokenPrivileges.Length)
			Dim privilegeAndAttributes As List(Of ProcessPrivileges.PrivilegeAndAttributes) = New List(Of ProcessPrivileges.PrivilegeAndAttributes)(length)
			Dim num As Integer = 0
			Do
				Dim luidAndAttribute As LuidAndAttributes = tokenPrivileges(num)
				Dim privilegeName As String = Privileges.GetPrivilegeName(luidAndAttribute.Luid)
				If (Privileges.privilegesDictionary.ContainsKey(privilegeName)) Then
					privilegeAndAttributes.Add(New ProcessPrivileges.PrivilegeAndAttributes(Privileges.privilegesDictionary(privilegeName), luidAndAttribute.Attributes))
				End If
				num = num + 1
			Loop While num < length
			Return New PrivilegeAndAttributesCollection(privilegeAndAttributes)
		End Function

		Private Function GetTokenPrivileges(ByVal accessTokenHandle As ProcessPrivileges.AccessTokenHandle) As LuidAndAttributes()
			Dim luidAndAttributesArray As LuidAndAttributes()
			Dim num As Integer = 0
			Dim num1 As Integer = 0
			If (ProcessPrivileges.NativeMethods.GetTokenInformation(accessTokenHandle, TokenInformationClass.TokenPrivileges, IntPtr.Zero, num, num1)) Then
				Return New LuidAndAttributes(-1) {}
			End If
			Dim lastWin32Error As Integer = Marshal.GetLastWin32Error()
			If (lastWin32Error <> 122) Then
				Throw New Win32Exception(lastWin32Error)
			End If
			num = num1
			num1 = 0
			Using allocatedMemory As ProcessPrivileges.AllocatedMemory = New ProcessPrivileges.AllocatedMemory(num)
				If (Not ProcessPrivileges.NativeMethods.GetTokenInformation(accessTokenHandle, TokenInformationClass.TokenPrivileges, allocatedMemory.Pointer, num, num1)) Then
					Throw New Win32Exception(Marshal.GetLastWin32Error())
				End If
				Dim num2 As Integer = Marshal.ReadInt32(allocatedMemory.Pointer)
				Dim [structure](num2 - 1) As LuidAndAttributes
				Dim pointer As IntPtr = allocatedMemory.Pointer
				Dim num3 As Long = pointer.ToInt64() + CLng(Marshal.SizeOf(Of Integer)(num2))
				Dim type As System.Type = GetType(LuidAndAttributes)
				Dim num4 As Long = CLng(Marshal.SizeOf(type))
				Dim num5 As Integer = 0
				Do
					[structure](num5) = DirectCast(Marshal.PtrToStructure(New IntPtr(num3), type), LuidAndAttributes)
					num3 += num4
					num5 = num5 + 1
				Loop While num5 < num2
				luidAndAttributesArray = [structure]
			End Using
			Return luidAndAttributesArray
		End Function

		Friend Function RemovePrivilege(ByVal accessTokenHandle As ProcessPrivileges.AccessTokenHandle, ByVal privilege As ProcessPrivileges.Privilege) As AdjustPrivilegeResult
			Return Privileges.AdjustPrivilege(accessTokenHandle, privilege, PrivilegeAttributes.Removed)
		End Function
	End Module
End Namespace