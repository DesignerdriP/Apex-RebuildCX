Imports System
Imports System.ComponentModel
Imports System.Globalization
Imports System.Runtime.InteropServices

Namespace DevComponents.WinForms.Drawing
	<ComVisible(True)>
	<TypeConverter(GetType(CornerRadiusConverter))>
	Public Structure CornerRadius
		Implements IEquatable(Of CornerRadius)
		Private _ᬻ As Integer

		Private _┥ As Integer

		Private _┦ As Integer

		Private _┧ As Integer

		Public Property BottomLeft As Integer
			Get
				Return Me._┦
			End Get
			Set(ByVal value As Integer)
				Me._┦ = value
			End Set
		End Property

		Public Property BottomRight As Integer
			Get
				Return Me._┧
			End Get
			Set(ByVal value As Integer)
				Me._┧ = value
			End Set
		End Property

		Public Property TopLeft As Integer
			Get
				Return Me._ᬻ
			End Get
			Set(ByVal value As Integer)
				Me._ᬻ = value
			End Set
		End Property

		Public Property TopRight As Integer
			Get
				Return Me._┥
			End Get
			Set(ByVal value As Integer)
				Me._┥ = value
			End Set
		End Property

		Friend ReadOnly Property ᔭ As Boolean
			Get
				If (Me._ᬻ <> 0 OrElse Me._┥ <> 0 OrElse Me._┧ <> 0) Then
					Return False
				End If
				Return Me._┦ = 0
			End Get
		End Property

		Public Sub New(ByVal uniformRadius As Integer)
			Dim num As Integer = uniformRadius
			Dim num1 As Integer = num
			Me._┧ = num
			Dim num2 As Integer = num1
			Dim num3 As Integer = num2
			Me._┦ = num2
			Dim num4 As Integer = num3
			Dim num5 As Integer = num4
			Me._┥ = num4
			Me._ᬻ = num5
		End Sub

		Public Sub New(ByVal topLeft As Integer, ByVal topRight As Integer, ByVal bottomRight As Integer, ByVal bottomLeft As Integer)
			Me._ᬻ = topLeft
			Me._┥ = topRight
			Me._┧ = bottomRight
			Me._┦ = bottomLeft
		End Sub

		Public Overrides Function Equals(ByVal obj As Object) As Boolean Implements IEquatable(Of CornerRadius).Equals
			If (Not TypeOf obj Is CornerRadius) Then
				Return False
			End If
			Return Me = DirectCast(obj, CornerRadius)
		End Function

		Public Function Equals(ByVal cornerRadius As DevComponents.WinForms.Drawing.CornerRadius) As Boolean Implements IEquatable(Of DevComponents.WinForms.Drawing.CornerRadius).Equals
			Return Me = cornerRadius
		End Function

		Public Overrides Function GetHashCode() As Integer
			Return Me._ᬻ.GetHashCode() Xor Me._┥.GetHashCode() Xor Me._┦.GetHashCode() Xor Me._┧.GetHashCode()
		End Function

		Public Shared Operator =(ByVal cr1 As CornerRadius, ByVal cr2 As CornerRadius) As Boolean
			If (cr1._ᬻ <> cr2._ᬻ OrElse cr1._┥ <> cr2._┥ OrElse cr1._┧ <> cr2._┧) Then
				Return False
			End If
			Return cr1._┦ = cr2._┦
		End Operator

		Public Shared Operator <>(ByVal cr1 As CornerRadius, ByVal cr2 As CornerRadius) As Boolean
			Return Not (cr1 = cr2)
		End Operator

		Public Overrides Function ToString() As String
			Return CornerRadiusConverter.ToString(Me, CultureInfo.InvariantCulture)
		End Function

		Friend Function ᔠ(ByVal ᔡ As Boolean) As Boolean
			If (Not ᔡ AndAlso (CDbl(Me._ᬻ) < 0 OrElse CDbl(Me._┥) < 0 OrElse CDbl(Me._┦) < 0 OrElse CDbl(Me._┧) < 0)) Then
				Return False
			End If
			Return True
		End Function
	End Structure
End Namespace