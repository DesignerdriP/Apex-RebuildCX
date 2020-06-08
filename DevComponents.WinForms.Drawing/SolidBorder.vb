Imports System
Imports System.ComponentModel
Imports System.Drawing
Imports System.Runtime.InteropServices

Namespace DevComponents.WinForms.Drawing
	<ComVisible(True)>
	Public Class SolidBorder
		Inherits Border
		Private _ኺ As System.Drawing.Color = System.Drawing.Color.Empty

		<Description("Indicates the fill color.")>
		Public Property Color As System.Drawing.Color
			Get
				Return Me._ኺ
			End Get
			Set(ByVal value As System.Drawing.Color)
				Me._ኺ = value
			End Set
		End Property

		Public Sub New(ByVal color As System.Drawing.Color, ByVal width As Integer)
			MyBase.New()
			Me._ኺ = color
			Me._ᇼ = width
		End Sub

		Public Sub New(ByVal color As System.Drawing.Color)
			MyBase.New()
			Me._ኺ = color
		End Sub

		Public Sub New()
			MyBase.New()
		End Sub

		Public Overrides Function CreatePen() As Pen
			If (Not Me.ኼ()) Then
				Return Nothing
			End If
			Return New Pen(Me._ኺ, CSng(Me._ᇼ))
		End Function

		<EditorBrowsable(EditorBrowsableState.Never)>
		Public Sub ResetColor()
			Me.Color = System.Drawing.Color.Empty
		End Sub

		<EditorBrowsable(EditorBrowsableState.Never)>
		Public Function ShouldSerializeColor() As Boolean
			Return Not Me._ኺ.IsEmpty
		End Function

		Private Function ኼ() As Boolean
			If (Me._ኺ.IsEmpty) Then
				Return False
			End If
			Return Me._ᇼ > 0
		End Function
	End Class
End Namespace