Imports Microsoft.Win32.SafeHandles
Imports System
Imports System.ComponentModel
Imports System.Runtime.ConstrainedExecution
Imports System.Runtime.InteropServices

Namespace ProcessPrivileges
	Public NotInheritable Class AccessTokenHandle
		Inherits SafeHandleZeroOrMinusOneIsInvalid
		Friend Sub New(ByVal processHandle As ProcessPrivileges.ProcessHandle, ByVal tokenAccessRights As ProcessPrivileges.TokenAccessRights)
			MyBase.New(True)
			If (Not ProcessPrivileges.NativeMethods.OpenProcessToken(processHandle, tokenAccessRights, Me.handle)) Then
				Throw New Win32Exception(Marshal.GetLastWin32Error())
			End If
		End Sub

		<ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)>
		Protected Overrides Function ReleaseHandle() As Boolean
			If (Not ProcessPrivileges.NativeMethods.CloseHandle(Me.handle)) Then
				Throw New Win32Exception(Marshal.GetLastWin32Error())
			End If
			Return True
		End Function
	End Class
End Namespace