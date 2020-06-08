Imports System
Imports System.ComponentModel
Imports System.Drawing
Imports System.Runtime.InteropServices

Namespace DevComponents.WinForms.Drawing
	<ComVisible(True)>
	<DesignTimeVisible(False)>
	<ToolboxItem(False)>
	<TypeConverter(GetType(ColorStopConverter))>
	Public Class ColorStop
		Private _ኺ As System.Drawing.Color = System.Drawing.Color.Empty

		Private _ኻ As Single

		<Browsable(True)>
		<Description("Indicates the Color to use in multicolor gradient blend at specified position.")>
		Public Property Color As System.Drawing.Color
			Get
				Return Me._ኺ
			End Get
			Set(ByVal value As System.Drawing.Color)
				Me._ኺ = value
				Me.ᇶ()
			End Set
		End Property

		<Browsable(True)>
		<DefaultValue(0!)>
		<Description("")>
		Public Property Position As Single
			Get
				Return Me._ኻ
			End Get
			Set(ByVal value As Single)
				Me._ኻ = value
				Me.ᇶ()
			End Set
		End Property

		Public Sub New()
			MyBase.New()
		End Sub

		Public Sub New(ByVal color As System.Drawing.Color, ByVal position As Single)
			MyBase.New()
			Me._ኺ = color
			Me._ኻ = position
		End Sub

		Private Function ShouldSerializeColor() As Boolean
			Return Not Me._ኺ.IsEmpty
		End Function

		Private Sub ᇶ()
		End Sub
	End Class
End Namespace