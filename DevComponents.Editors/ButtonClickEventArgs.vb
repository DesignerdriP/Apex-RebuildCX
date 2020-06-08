Imports System
Imports System.ComponentModel
Imports System.Runtime.InteropServices

Namespace DevComponents.Editors
	<ComVisible(True)>
	Public Class ButtonClickEventArgs
		Inherits CancelEventArgs
		Private _⃎ As Object

		Public ReadOnly Property Button As Object
			Get
				Return Me._⃎
			End Get
		End Property

		Public Sub New(ByVal button As Object)
			MyBase.New()
			Me._⃎ = button
		End Sub
	End Class
End Namespace