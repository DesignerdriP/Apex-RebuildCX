Imports System
Imports System.Runtime.ConstrainedExecution
Imports System.Runtime.InteropServices
Imports System.Security
Imports System.Text

Namespace ProcessPrivileges
	Friend Module NativeMethods
		Friend Const ErrorInsufficientBuffer As Integer = 122

		Private Const AdvApi32 As String = "advapi32.dll"

		Private Const Kernel32 As String = "kernel32.dll"

		<DllImport("advapi32.dll", CharSet:=CharSet.None, ExactSpelling:=False, SetLastError:=True)>
		<SuppressUnmanagedCodeSecurity>
		Friend Function AdjustTokenPrivileges(<InAttribute> ByVal accessTokenHandle As ProcessPrivileges.AccessTokenHandle, <InAttribute> ByVal disableAllPrivileges As Boolean, <InAttribute> ByRef newState As TokenPrivilege, <InAttribute> ByVal bufferLength As Integer, <InAttribute> <Out> ByRef previousState As TokenPrivilege, <InAttribute> <Out> ByRef returnLength As Integer) As Boolean
		End Function

		<DllImport("kernel32.dll", CharSet:=CharSet.None, ExactSpelling:=False, SetLastError:=True)>
		<ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)>
		<SuppressUnmanagedCodeSecurity>
		Friend Function CloseHandle(<InAttribute> ByVal handle As IntPtr) As Boolean
		End Function

		<DllImport("advapi32.dll", CharSet:=CharSet.None, ExactSpelling:=False, SetLastError:=True)>
		<SuppressUnmanagedCodeSecurity>
		Friend Function GetTokenInformation(<InAttribute> ByVal accessTokenHandle As ProcessPrivileges.AccessTokenHandle, <InAttribute> ByVal tokenInformationClass As ProcessPrivileges.TokenInformationClass, <Out> ByVal tokenInformation As IntPtr, <InAttribute> ByVal tokenInformationLength As Integer, <InAttribute> <Out> ByRef returnLength As Integer) As Boolean
		End Function

		<DllImport("advapi32.dll", CharSet:=CharSet.Unicode, ExactSpelling:=False, SetLastError:=True)>
		<SuppressUnmanagedCodeSecurity>
		Friend Function LookupPrivilegeName(<InAttribute> ByVal systemName As String, <InAttribute> ByRef luid As ProcessPrivileges.Luid, <InAttribute> <Out> ByVal name As StringBuilder, <InAttribute> <Out> ByRef nameLength As Integer) As Boolean
		End Function

		<DllImport("advapi32.dll", CharSet:=CharSet.Unicode, ExactSpelling:=False, SetLastError:=True)>
		<SuppressUnmanagedCodeSecurity>
		Friend Function LookupPrivilegeValue(<InAttribute> ByVal systemName As String, <InAttribute> ByVal name As String, <InAttribute> <Out> ByRef luid As ProcessPrivileges.Luid) As Boolean
		End Function

		<DllImport("advapi32.dll", CharSet:=CharSet.None, ExactSpelling:=False, SetLastError:=True)>
		<SuppressUnmanagedCodeSecurity>
		Friend Function OpenProcessToken(<InAttribute> ByVal processHandle As ProcessPrivileges.ProcessHandle, <InAttribute> ByVal desiredAccess As TokenAccessRights, <InAttribute> <Out> ByRef tokenHandle As IntPtr) As Boolean
		End Function
	End Module
End Namespace