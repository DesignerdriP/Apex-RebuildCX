Imports System
Imports System.ComponentModel
Imports System.Drawing
Imports System.Runtime.InteropServices

Namespace DevComponents.WinForms.Drawing
	<ComVisible(True)>
	Public Class SolidFill
		Inherits Fill
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

		Public Sub New(ByVal color As System.Drawing.Color)
			MyBase.New()
			Me._ኺ = color
		End Sub

		Public Sub New()
			MyBase.New()
		End Sub

		Public Overrides Function CreateBrush(ByVal bounds As Rectangle) As Brush
			If (Me._ኺ.IsEmpty) Then
				Return Nothing
			End If
			Return New SolidBrush(Me._ኺ)
		End Function

		Public Overrides Function CreatePen(ByVal width As Integer) As Pen
			If (Me._ኺ.IsEmpty) Then
				Return Nothing
			End If
			Return New Pen(Me._ኺ, CSng(width))
		End Function

		<EditorBrowsable(EditorBrowsableState.Never)>
		Public Sub ResetColor()
			Me.Color = System.Drawing.Color.Empty
		End Sub

		<EditorBrowsable(EditorBrowsableState.Never)>
		Public Function ShouldSerializeColor() As Boolean
			Return Not Me._ኺ.IsEmpty
		End Function
	End Class
End Namespace