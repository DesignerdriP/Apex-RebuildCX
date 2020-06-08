Imports Microsoft.Win32.SafeHandles
Imports System
Imports System.Runtime.ConstrainedExecution
Imports System.Runtime.InteropServices
Imports System.Security
Imports System.Security.Permissions

Namespace DevComponents.Schedule
	<HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort:=True)>
	<SuppressUnmanagedCodeSecurity>
	Friend NotInheritable Class ⟵
		Inherits SafeHandleZeroOrMinusOneIsInvalid
		Friend Sub New()
			MyBase.New(True)
		End Sub

		<DllImport("kernel32.dll", CharSet:=CharSet.Unicode, ExactSpelling:=False)>
		<ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)>
		Private Shared Function FreeLibrary(ByVal hModule As IntPtr) As Boolean
		End Function

		<DllImport("kernel32.dll", CharSet:=CharSet.Unicode, ExactSpelling:=False, SetLastError:=True)>
		Friend Shared Function LoadLibraryEx(ByVal libFilename As String, ByVal reserved As IntPtr, ByVal flags As Integer) As ⟵
		End Function

		Protected Overrides Function ReleaseHandle() As Boolean
			Return ⟵.FreeLibrary(Me.handle)
		End Function
	End Class
End Namespace