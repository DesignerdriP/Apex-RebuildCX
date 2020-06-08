Imports System
Imports System.Collections.Generic
Imports System.Diagnostics
Imports System.Linq
Imports System.Runtime.CompilerServices
Imports System.Runtime.InteropServices
Imports System.Security.Permissions

Namespace ProcessPrivileges
	Public NotInheritable Class PrivilegeEnabler
		Implements IDisposable
		Private ReadOnly Shared sharedPrivileges As Dictionary(Of Privilege, PrivilegeEnabler)

		Private ReadOnly Shared accessTokenHandles As Dictionary(Of System.Diagnostics.Process, ProcessPrivileges.AccessTokenHandle)

		Private accessTokenHandle As ProcessPrivileges.AccessTokenHandle

		Private disposed As Boolean

		Private ownsHandle As Boolean

		Private process As System.Diagnostics.Process

		Shared Sub New()
			PrivilegeEnabler.sharedPrivileges = New Dictionary(Of Privilege, PrivilegeEnabler)()
			PrivilegeEnabler.accessTokenHandles = New Dictionary(Of System.Diagnostics.Process, ProcessPrivileges.AccessTokenHandle)()
		End Sub

		<PermissionSet(SecurityAction.LinkDemand, Name:="FullTrust")>
		Public Sub New(ByVal accessTokenHandle As ProcessPrivileges.AccessTokenHandle)
			MyBase.New()
			Me.accessTokenHandle = accessTokenHandle
		End Sub

		<PermissionSet(SecurityAction.LinkDemand, Name:="FullTrust")>
		Public Sub New(ByVal process As System.Diagnostics.Process)
			MyBase.New()
			SyncLock PrivilegeEnabler.accessTokenHandles
				If (Not PrivilegeEnabler.accessTokenHandles.ContainsKey(process)) Then
					Me.accessTokenHandle = process.GetAccessTokenHandle(TokenAccessRights.Query Or TokenAccessRights.AdjustPrivileges)
					PrivilegeEnabler.accessTokenHandles.Add(process, Me.accessTokenHandle)
					Me.ownsHandle = True
				Else
					Me.accessTokenHandle = PrivilegeEnabler.accessTokenHandles(process)
				End If
			End SyncLock
			Me.process = process
		End Sub

		<PermissionSet(SecurityAction.LinkDemand, Name:="FullTrust")>
		Public Sub New(ByVal accessTokenHandle As ProcessPrivileges.AccessTokenHandle, ByVal ParamArray privileges As Privilege())
			MyClass.New(accessTokenHandle)
			Dim privilegeArray As Privilege() = privileges
			For i As Integer = 0 To CInt(privilegeArray.Length)
				Me.EnablePrivilege(privilegeArray(i))
			Next

		End Sub

		<PermissionSet(SecurityAction.LinkDemand, Name:="FullTrust")>
		Public Sub New(ByVal process As System.Diagnostics.Process, ByVal ParamArray privileges As Privilege())
			MyClass.New(process)
			Dim privilegeArray As Privilege() = privileges
			For i As Integer = 0 To CInt(privilegeArray.Length)
				Me.EnablePrivilege(privilegeArray(i))
			Next

		End Sub

		<PermissionSet(SecurityAction.Demand, Name:="FullTrust")>
		Public Sub Dispose() Implements IDisposable.Dispose
			Me.InternalDispose()
			GC.SuppressFinalize(Me)
		End Sub

		<PermissionSet(SecurityAction.LinkDemand, Name:="FullTrust")>
		Public Function EnablePrivilege(ByVal privilege As ProcessPrivileges.Privilege) As ProcessPrivileges.AdjustPrivilegeResult
			Dim adjustPrivilegeResult As ProcessPrivileges.AdjustPrivilegeResult
			SyncLock PrivilegeEnabler.sharedPrivileges
				If (PrivilegeEnabler.sharedPrivileges.ContainsKey(privilege) OrElse Me.accessTokenHandle.GetPrivilegeState(privilege) <> PrivilegeState.Disabled OrElse Me.accessTokenHandle.EnablePrivilege(privilege) <> ProcessPrivileges.AdjustPrivilegeResult.PrivilegeModified) Then
					adjustPrivilegeResult = ProcessPrivileges.AdjustPrivilegeResult.None
				Else
					PrivilegeEnabler.sharedPrivileges.Add(privilege, Me)
					adjustPrivilegeResult = ProcessPrivileges.AdjustPrivilegeResult.PrivilegeModified
				End If
			End SyncLock
			Return adjustPrivilegeResult
		End Function

		Protected Overrides Sub Finalize()
			Try
				Me.InternalDispose()
			Finally
				Me.Finalize()
			End Try
		End Sub

		<PermissionSet(SecurityAction.LinkDemand, Name:="FullTrust")>
		Private Sub InternalDispose()
			If (Not Me.disposed) Then
				SyncLock PrivilegeEnabler.sharedPrivileges
					Dim array As ProcessPrivileges.Privilege() = PrivilegeEnabler.sharedPrivileges.Where(Function(keyValuePair As KeyValuePair(Of ProcessPrivileges.Privilege, PrivilegeEnabler)) keyValuePair.Value = Me).[Select](Of ProcessPrivileges.Privilege)(Function(keyValuePair As KeyValuePair(Of ProcessPrivileges.Privilege, PrivilegeEnabler)) keyValuePair.Key).ToArray()
					Dim num As Integer = 0
					Do
						Dim privilege As ProcessPrivileges.Privilege = array(num)
						Me.accessTokenHandle.DisablePrivilege(privilege)
						PrivilegeEnabler.sharedPrivileges.Remove(privilege)
						num = num + 1
					Loop While num < CInt(array.Length)
					If (Me.ownsHandle) Then
						Me.accessTokenHandle.Dispose()
						SyncLock Me.accessTokenHandle
							PrivilegeEnabler.accessTokenHandles.Remove(Me.process)
						End SyncLock
					End If
					Me.accessTokenHandle = Nothing
					Me.ownsHandle = False
					Me.process = Nothing
					Me.disposed = True
				End SyncLock
			End If
		End Sub
	End Class
End Namespace