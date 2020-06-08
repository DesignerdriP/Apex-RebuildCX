Imports System
Imports System.Runtime.InteropServices

Namespace ProcessPrivileges
	Friend NotInheritable Class AllocatedMemory
		Implements IDisposable
		Private pointer As IntPtr

		Friend ReadOnly Property Pointer As IntPtr
			Get
				Return Me.pointer
			End Get
		End Property

		Friend Sub New(ByVal bytesRequired As Integer)
			MyBase.New()
			Me.pointer = Marshal.AllocHGlobal(bytesRequired)
		End Sub

		Public Sub Dispose() Implements IDisposable.Dispose
			Me.InternalDispose()
			GC.SuppressFinalize(Me)
		End Sub

		Protected Overrides Sub Finalize()
			Try
				Me.InternalDispose()
			Finally
				Me.Finalize()
			End Try
		End Sub

		Private Sub InternalDispose()
			If (Me.pointer <> IntPtr.Zero) Then
				Marshal.FreeHGlobal(Me.pointer)
				Me.pointer = IntPtr.Zero
			End If
		End Sub
	End Class
End Namespace