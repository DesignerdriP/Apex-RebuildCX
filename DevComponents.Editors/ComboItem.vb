Imports DevComponents.DotNetBar
Imports DevComponents.DotNetBar.Controls
Imports System
Imports System.ComponentModel
Imports System.Drawing
Imports System.Drawing.Design
Imports System.Runtime.InteropServices
Imports System.Windows.Forms

Namespace DevComponents.Editors
	<ComVisible(True)>
	<DesignTimeVisible(False)>
	<ToolboxItem(False)>
	Public Class ComboItem
		Inherits Component
		Private ө As String = ""

		Private բ As Integer = -1

		Private ม As System.Drawing.Image

		Private Ẋ As StringFormat

		Private ฬ As HorizontalAlignment

		Private ẋ As String = ""

		Private Ẍ As System.Drawing.FontStyle

		Private ẍ As Single = 8!

		Private ೉ As Color = Color.Empty

		Private ݹ As Color = Color.Empty

		Private ӧ As Object

		Friend Ꮈ As ComboBoxEx

		Friend Ẏ As Boolean

		Private _ᒃ As Object

		Public Property BackColor As Color
			Get
				Return Me.ݹ
			End Get
			Set(ByVal value As Color)
				Me.ݹ = value
			End Set
		End Property

		<DefaultValue("")>
		Public Property FontName As String
			Get
				Return Me.ẋ
			End Get
			Set(ByVal value As String)
				Me.ẋ = value
			End Set
		End Property

		<DefaultValue(8!)>
		Public Property FontSize As Single
			Get
				Return Me.ẍ
			End Get
			Set(ByVal value As Single)
				Me.ẍ = value
			End Set
		End Property

		<DefaultValue(System.Drawing.FontStyle.Regular)>
		Public Property FontStyle As System.Drawing.FontStyle
			Get
				Return Me.Ẍ
			End Get
			Set(ByVal value As System.Drawing.FontStyle)
				Me.Ẍ = value
			End Set
		End Property

		Public Property ForeColor As Color
			Get
				Return Me.೉
			End Get
			Set(ByVal value As Color)
				Me.೉ = value
			End Set
		End Property

		<DefaultValue(Nothing)>
		<Localizable(True)>
		Public Property Image As System.Drawing.Image
			Get
				Return Me.ม
			End Get
			Set(ByVal value As System.Drawing.Image)
				Me.ม = value
			End Set
		End Property

		<DefaultValue(-1)>
		<Editor("DevComponents.DotNetBar.Design.ImageIndexEditor, DevComponents.DotNetBar.Design, Version=11.3.0.0, Culture=neutral,  PublicKeyToken=2c9ff1fddc42653c", GetType(UITypeEditor))>
		<Localizable(True)>
		<TypeConverter(GetType(ImageIndexConverter))>
		Public Property ImageIndex As Integer
			Get
				Return Me.բ
			End Get
			Set(ByVal value As Integer)
				Me.բ = value
			End Set
		End Property

		<Browsable(False)>
		<EditorBrowsable(EditorBrowsableState.Never)>
		Public ReadOnly Property ImageList As System.Windows.Forms.ImageList
			Get
				If (Me.Ꮈ Is Nothing) Then
					Return Nothing
				End If
				Return Me.Ꮈ.Images
			End Get
		End Property

		<DefaultValue(HorizontalAlignment.Left)>
		Public Property ImagePosition As HorizontalAlignment
			Get
				Return Me.ฬ
			End Get
			Set(ByVal value As HorizontalAlignment)
				Me.ฬ = value
			End Set
		End Property

		<Browsable(False)>
		<EditorBrowsable(EditorBrowsableState.Never)>
		Public ReadOnly Property Parent As ComboBoxEx
			Get
				Return Me.Ꮈ
			End Get
		End Property

		<Browsable(False)>
		<DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>
		Public Property Tag As Object
			Get
				Return Me.ӧ
			End Get
			Set(ByVal value As Object)
				Me.ӧ = value
			End Set
		End Property

		<Browsable(True)>
		<DefaultValue("")>
		<Localizable(True)>
		Public Property Text As String
			Get
				Return Me.ө
			End Get
			Set(ByVal value As String)
				Me.ө = value
			End Set
		End Property

		<DefaultValue(StringAlignment.Near)>
		Public Property TextAlignment As StringAlignment
			Get
				Return Me.Ẋ.Alignment
			End Get
			Set(ByVal value As StringAlignment)
				Me.Ẋ.Alignment = value
			End Set
		End Property

		<Browsable(False)>
		<DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>
		Public Property TextFormat As StringFormat
			Get
				Return Me.Ẋ
			End Get
			Set(ByVal value As StringFormat)
				Me.Ẋ = value
			End Set
		End Property

		<DefaultValue(StringAlignment.Near)>
		Public Property TextLineAlignment As StringAlignment
			Get
				Return Me.Ẋ.LineAlignment
			End Get
			Set(ByVal value As StringAlignment)
				Me.Ẋ.LineAlignment = value
			End Set
		End Property

		<Category("Data")>
		<DefaultValue(Nothing)>
		<Localizable(True)>
		<TypeConverter(GetType(StringConverter))>
		Public Property Value As Object
			Get
				Return Me._ᒃ
			End Get
			Set(ByVal value As Object)
				Me._ᒃ = value
			End Set
		End Property

		Public Sub New()
			MyBase.New()
			Me.Ẋ = BarFunctions.CreateStringFormat()
			Me.Ẋ.Alignment = StringAlignment.Near
		End Sub

		Public Sub New(ByVal text As String)
			MyClass.New()
			Me.ө = text
		End Sub

		Public Sub New(ByVal text As String, ByVal foreColor As Color)
			MyClass.New()
			Me.ө = text
			Me.೉ = foreColor
		End Sub

		Public Sub New(ByVal text As String, ByVal foreColor As Color, ByVal backColor As Color)
			MyClass.New()
			Me.ө = text
			Me.೉ = foreColor
			Me.ݹ = backColor
		End Sub

		Public Sub New(ByVal text As String, ByVal image As System.Drawing.Image)
			MyClass.New()
			Me.ө = text
			Me.ม = image
		End Sub

		Protected Overrides Sub Dispose(ByVal disposing As Boolean)
			If (BarUtilities.DisposeItemImages AndAlso Not MyBase.DesignMode) Then
				BarUtilities.ᄾ(Me.ม)
			End If
			MyBase.Dispose(disposing)
		End Sub

		<EditorBrowsable(EditorBrowsableState.Never)>
		Public Function ShouldSerializeBackColor() As Boolean
			Return Not Me.ݹ.IsEmpty
		End Function

		<EditorBrowsable(EditorBrowsableState.Never)>
		Public Function ShouldSerializeForeColor() As Boolean
			Return Not Me.೉.IsEmpty
		End Function

		Public Overrides Function ToString() As String
			Return Me.ө
		End Function
	End Class
End Namespace