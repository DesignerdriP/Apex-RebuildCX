Imports System

Namespace DevComponents.Schedule
	Friend Class ⟥
		Public Sub New()
			MyBase.New()
		End Sub

		Friend Structure ⟦
			Public ⟧ As Short

			Public ⟨ As Short

			Public ⟩ As Short

			Public ⟪ As Short

			Public ⟫ As Short

			Public ⟬ As Short

			Public ⟭ As Short

			Public ⟮ As Short
		End Structure

		Friend Structure ⟯
			Public ⟞ As Integer

			Public ⟟ As String

			Public ⟠ As ⟥.⟦

			Public ⟡ As Integer

			Public ⟢ As String

			Public ⟣ As ⟥.⟦

			Public ⟤ As Integer

			Public ⟰ As String
		End Structure

		Friend Structure ⟱
			Public ⟞ As Integer

			Public ⟡ As Integer

			Public ⟤ As Integer

			Public ⟠ As ⟥.⟦

			Public ⟣ As ⟥.⟦

			Public Sub New(ByVal tzi As ⟥.⟝)
				Me.⟞ = tzi.⟞
				Me.⟠ = tzi.⟠
				Me.⟡ = tzi.⟡
				Me.⟣ = tzi.⟣
				Me.⟤ = tzi.⟤
			End Sub

			Public Sub New(ByVal bytes As Byte())
				If (bytes Is Nothing OrElse CInt(bytes.Length) <> 44) Then
					Throw New ArgumentException("Argument_InvalidREG_TZI_FORMAT", "bytes")
				End If
				Me.⟞ = BitConverter.ToInt32(bytes, 0)
				Me.⟡ = BitConverter.ToInt32(bytes, 4)
				Me.⟤ = BitConverter.ToInt32(bytes, 8)
				Me.⟠.⟧ = BitConverter.ToInt16(bytes, 12)
				Me.⟠.⟨ = BitConverter.ToInt16(bytes, 14)
				Me.⟠.⟩ = BitConverter.ToInt16(bytes, 16)
				Me.⟠.⟪ = BitConverter.ToInt16(bytes, 18)
				Me.⟠.⟫ = BitConverter.ToInt16(bytes, 20)
				Me.⟠.⟬ = BitConverter.ToInt16(bytes, 22)
				Me.⟠.⟭ = BitConverter.ToInt16(bytes, 24)
				Me.⟠.⟮ = BitConverter.ToInt16(bytes, 26)
				Me.⟣.⟧ = BitConverter.ToInt16(bytes, 28)
				Me.⟣.⟨ = BitConverter.ToInt16(bytes, 30)
				Me.⟣.⟩ = BitConverter.ToInt16(bytes, 32)
				Me.⟣.⟪ = BitConverter.ToInt16(bytes, 34)
				Me.⟣.⟫ = BitConverter.ToInt16(bytes, 36)
				Me.⟣.⟬ = BitConverter.ToInt16(bytes, 38)
				Me.⟣.⟭ = BitConverter.ToInt16(bytes, 40)
				Me.⟣.⟮ = BitConverter.ToInt16(bytes, 42)
			End Sub
		End Structure

		Friend Structure ⟝
			Public ⟞ As Integer

			Public ⟟ As String

			Public ⟠ As ⟥.⟦

			Public ⟡ As Integer

			Public ⟢ As String

			Public ⟣ As ⟥.⟦

			Public ⟤ As Integer

			Public Sub New(ByVal dtzi As ⟥.⟯)
				Me.⟞ = dtzi.⟞
				Me.⟟ = dtzi.⟟
				Me.⟠ = dtzi.⟠
				Me.⟡ = dtzi.⟡
				Me.⟢ = dtzi.⟢
				Me.⟣ = dtzi.⟣
				Me.⟤ = dtzi.⟤
			End Sub
		End Structure
	End Class
End Namespace