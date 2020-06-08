Imports System
Imports System.Runtime.InteropServices
Imports System.Security
Imports System.Text

Namespace DevComponents.Schedule
	<SuppressUnmanagedCodeSecurity>
	Friend Module ⟴
		<DllImport("kernel32.dll", CharSet:=CharSet.None, ExactSpelling:=True, SetLastError:=True)>
		Friend Function GetDynamicTimeZoneInformation(<Out> ByRef lpDynamicTimeZoneInformation As ⟥.⟯) As Integer
		End Function

		<DllImport("kernel32.dll", CharSet:=CharSet.None, ExactSpelling:=True, SetLastError:=True)>
		Friend Function GetFileMUIPath(ByVal flags As Integer, ByVal filePath As String, ByVal language As StringBuilder, ByRef languageLength As Integer, ByVal fileMuiPath As StringBuilder, ByRef fileMuiPathLength As Integer, ByRef enumerator As Long) As Boolean
		End Function

		<DllImport("kernel32.dll", CharSet:=CharSet.None, ExactSpelling:=True, SetLastError:=True)>
		Friend Function GetTimeZoneInformation(<Out> ByRef lpTimeZoneInformation As ⟥.⟝) As Integer
		End Function

		<DllImport("kernel32.dll", CharSet:=CharSet.Unicode, ExactSpelling:=False, SetLastError:=True)>
		<SecurityCritical>
		Friend Function LoadLibraryEx(ByVal libFilename As String, ByVal reserved As IntPtr, ByVal flags As Integer) As ⟵
		End Function

		<DllImport("user32.dll", CallingConvention:=CallingConvention.StdCall, CharSet:=CharSet.Unicode, EntryPoint:="LoadStringW", ExactSpelling:=True, SetLastError:=True)>
		<SecurityCritical>
		Friend Function LoadString(ByVal handle As ⟵, ByVal id As Integer, ByVal buffer As StringBuilder, ByVal bufferLength As Integer) As Integer
		End Function
	End Module
End Namespace