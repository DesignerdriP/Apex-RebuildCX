Imports System
Imports System.ComponentModel
Imports System.Drawing
Imports System.Runtime.InteropServices

Namespace DevComponents.WinForms.Drawing
	<ComVisible(True)>
	<ToolboxItem(False)>
	Public MustInherit Class Fill
		Inherits Component
		Protected Sub New()
			MyBase.New()
		End Sub

		Public MustOverride Function CreateBrush(ByVal bounds As Rectangle) As Brush

		Public MustOverride Function CreatePen(ByVal width As Integer) As Pen
	End Class
End Namespace