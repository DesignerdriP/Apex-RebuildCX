Imports System
Imports System.ComponentModel
Imports System.Drawing
Imports System.Runtime.InteropServices

Namespace DevComponents.WinForms.Drawing
	<ComVisible(True)>
	<ToolboxItem(False)>
	Public MustInherit Class Border
		Inherits Component
		Friend _ᇼ As Integer

		<DefaultValue(0)>
		<Description("Indicates border width.")>
		Public Property Width As Integer
			Get
				Return Me._ᇼ
			End Get
			Set(ByVal value As Integer)
				Me._ᇼ = value
			End Set
		End Property

		Protected Sub New()
			MyBase.New()
		End Sub

		Public MustOverride Function CreatePen() As Pen

		Friend Shared Function Deflate(ByVal bounds As Rectangle, ByVal border As DevComponents.WinForms.Drawing.Border) As Rectangle
			If (border Is Nothing) Then
				Return bounds
			End If
			bounds.Inflate(-border.Width, -border.Width)
			Return bounds
		End Function
	End Class
End Namespace