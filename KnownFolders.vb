Imports System
Imports System.Runtime.InteropServices

Public Module KnownFolders
	Private _knownFolderGuids As String()

	Sub New()
		KnownFolders._knownFolderGuids = New String() { "{56784854-C6CB-462B-8169-88E350ACB882}", "{B4BFCC3A-DB2C-424C-B029-7FE99A87C641}", "{FDD39AD0-238F-46AF-ADB4-6C85480369C7}", "{374DE290-123F-4565-9164-39C4925E467B}", "{1777F761-68AD-4D8A-87BD-30B759FA33DD}", "{BFB9D5E0-C6A9-404C-B2B2-AE6DB6AF4968}", "{4BD8D571-6D19-48D3-BE97-422220080E43}", "{33E28130-4E1E-4676-835A-98395C3BC3BB}", "{4C5C32FF-BB9D-43B0-B5B4-2D72E54EAAA4}", "{7D1D3A04-DEBB-4115-95CF-2F29DA2920DA}", "{18989B1D-99B5-455B-841C-AB7C74E4DDFC}" }
	End Sub

	Public Function GetPath(ByVal knownFolder As KnownFolder) As String
		Return KnownFolders.GetPath(knownFolder, False)
	End Function

	Public Function GetPath(ByVal knownFolder As KnownFolder, ByVal defaultUser As Boolean) As String
		Return KnownFolders.GetPath(knownFolder, KnownFolders.KnownFolderFlags.DontVerify, defaultUser)
	End Function

	Private Function GetPath(ByVal knownFolder As KnownFolder, ByVal flags As KnownFolders.KnownFolderFlags, ByVal defaultUser As Boolean) As String
		Dim intPtr As System.IntPtr
		Dim num As Integer = KnownFolders.SHGetKnownFolderPath(New Guid(KnownFolders._knownFolderGuids(CInt(knownFolder))), CUInt(flags), New System.IntPtr(If(defaultUser, -1, 0)), intPtr)
		If (num < 0) Then
			Throw New ExternalException("Unable to retrieve the known folder path. It may not be available on this system.", num)
		End If
		Dim stringUni As String = Marshal.PtrToStringUni(intPtr)
		Marshal.FreeCoTaskMem(intPtr)
		Return stringUni
	End Function

	<DllImport("Shell32.dll", CharSet:=CharSet.None, ExactSpelling:=False)>
	Private Function SHGetKnownFolderPath(ByVal rfid As Guid, ByVal dwFlags As UInteger, ByVal hToken As IntPtr, <Out> ByRef ppszPath As IntPtr) As Integer
	End Function

	<Flags>
	Private Enum KnownFolderFlags As UInteger
		SimpleIDList = 256
		NotParentRelative = 512
		DefaultPath = 1024
		Init = 2048
		NoAlias = 4096
		DontUnexpand = 8192
		DontVerify = 16384
		Create = 32768
		NoAppcontainerRedirection = 65536
		AliasOnly = 2147483648
	End Enum
End Module