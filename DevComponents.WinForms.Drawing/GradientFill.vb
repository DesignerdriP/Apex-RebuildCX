Imports System
Imports System.Collections
Imports System.ComponentModel
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Runtime.InteropServices

Namespace DevComponents.WinForms.Drawing
	<ComVisible(True)>
	Public Class GradientFill
		Inherits Fill
		Private _ኽ As Color = Color.Empty

		Private _ኾ As Color = Color.Empty

		Private _኿ As ColorBlendCollection = New ColorBlendCollection()

		Private _ሀ As Single = 90!

		<DefaultValue(90)>
		<Description("Indicates gradient fill angle.")>
		Public Property Angle As Single
			Get
				Return Me._ሀ
			End Get
			Set(ByVal value As Single)
				Me._ሀ = value
			End Set
		End Property

		<Description("Indicates the fill color.")>
		Public Property Color1 As Color
			Get
				Return Me._ኽ
			End Get
			Set(ByVal value As Color)
				Me._ኽ = value
			End Set
		End Property

		<Description("Indicates the fill color.")>
		Public Property Color2 As Color
			Get
				Return Me._ኾ
			End Get
			Set(ByVal value As Color)
				Me._ኾ = value
			End Set
		End Property

		<Browsable(True)>
		<Description("Collection that defines the multicolor gradient background.")>
		<DesignerSerializationVisibility(DesignerSerializationVisibility.Content)>
		Public ReadOnly Property InterpolationColors As ColorBlendCollection
			Get
				Return Me._኿
			End Get
		End Property

		Public Sub New()
			MyBase.New()
		End Sub

		Public Sub New(ByVal color1 As Color, ByVal color2 As Color)
			MyBase.New()
			Me._ኽ = color1
			Me._ኾ = color2
		End Sub

		Public Sub New(ByVal color1 As Color, ByVal color2 As Color, ByVal angle As Single)
			MyBase.New()
			Me._ኽ = color1
			Me._ኾ = color2
			Me._ሀ = angle
		End Sub

		Public Sub New(ByVal interpolationColors As ColorStop())
			MyBase.New()
			Me._኿.AddRange(interpolationColors)
		End Sub

		Public Sub New(ByVal interpolationColors As ColorStop(), ByVal angle As Integer)
			MyBase.New()
			Me._኿.AddRange(interpolationColors)
			Me._ሀ = CSng(angle)
		End Sub

		Public Overrides Function CreateBrush(ByVal bounds As Rectangle) As Brush
			If (Me._ኽ.IsEmpty AndAlso Me._ኾ.IsEmpty AndAlso Me._኿.Count = 0 OrElse bounds.Width < 1 OrElse bounds.Height < 1) Then
				Return Nothing
			End If
			Dim linearGradientBrush As System.Drawing.Drawing2D.LinearGradientBrush = New System.Drawing.Drawing2D.LinearGradientBrush(bounds, Me._ኽ, Me._ኾ, Me._ሀ)
			If (Me._኿.Count = 0) Then
				Return linearGradientBrush
			End If
			linearGradientBrush.InterpolationColors = Me._኿.GetColorBlend()
			Return linearGradientBrush
		End Function

		Public Overrides Function CreatePen(ByVal width As Integer) As Pen
			If (Not Me._ኽ.IsEmpty) Then
				Return New Pen(Me._ኽ, CSng(width))
			End If
			If (Me._ኾ.IsEmpty) Then
				Return Nothing
			End If
			Return New Pen(Me._ኾ, CSng(width))
		End Function

		<EditorBrowsable(EditorBrowsableState.Never)>
		Public Sub ResetColor1()
			Me.Color1 = Color.Empty
		End Sub

		<EditorBrowsable(EditorBrowsableState.Never)>
		Public Sub ResetColor2()
			Me.Color2 = Color.Empty
		End Sub

		<EditorBrowsable(EditorBrowsableState.Never)>
		Public Function ShouldSerializeColor1() As Boolean
			Return Not Me._ኽ.IsEmpty
		End Function

		<EditorBrowsable(EditorBrowsableState.Never)>
		Public Function ShouldSerializeColor2() As Boolean
			Return Not Me._ኾ.IsEmpty
		End Function
	End Class
End Namespace