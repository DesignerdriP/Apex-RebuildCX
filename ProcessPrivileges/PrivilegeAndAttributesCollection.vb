Imports System
Imports System.Collections.Generic
Imports System.Collections.ObjectModel
Imports System.Linq
Imports System.Runtime.CompilerServices
Imports System.Text

Namespace ProcessPrivileges
	<Serializable>
	Public NotInheritable Class PrivilegeAndAttributesCollection
		Inherits ReadOnlyCollection(Of PrivilegeAndAttributes)
		Friend Sub New(ByVal list As IList(Of PrivilegeAndAttributes))
			MyBase.New(list)
		End Sub

		Public Overrides Function ToString() As String
			Dim stringBuilder As System.Text.StringBuilder = New System.Text.StringBuilder()
			Dim num As Integer = Me.Max(Function(privilegeAndAttributes As PrivilegeAndAttributes) privilegeAndAttributes.Privilege.ToString().Length)
			For Each privilegeAndAttribute As PrivilegeAndAttributes In Me
				stringBuilder.Append(privilegeAndAttribute.Privilege)
				Dim length As Integer = num - privilegeAndAttribute.Privilege.ToString().Length
				Dim chrArray(length - 1) As Char
				Dim num1 As Integer = 0
				Do
					chrArray(num1) = Strings.ChrW(32)
					num1 = num1 + 1
				Loop While num1 < length
				stringBuilder.Append(chrArray)
				stringBuilder.Append(" => ")
				stringBuilder.AppendLine(privilegeAndAttribute.PrivilegeAttributes.ToString())
			Next
			Return stringBuilder.ToString()
		End Function
	End Class
End Namespace