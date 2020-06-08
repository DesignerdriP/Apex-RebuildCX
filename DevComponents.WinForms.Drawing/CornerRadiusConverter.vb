Imports System
Imports System.ComponentModel
Imports System.ComponentModel.Design.Serialization
Imports System.Globalization
Imports System.Reflection
Imports System.Runtime.InteropServices
Imports System.Text

Namespace DevComponents.WinForms.Drawing
	<ComVisible(True)>
	Public Class CornerRadiusConverter
		Inherits TypeConverter
		Public Sub New()
			MyBase.New()
		End Sub

		Public Overrides Function CanConvertFrom(ByVal typeDescriptorContext As ITypeDescriptorContext, ByVal sourceType As Type) As Boolean
			Select Case Type.GetTypeCode(sourceType)
				Case TypeCode.Int16
				Case TypeCode.UInt16
				Case TypeCode.Int32
				Case TypeCode.UInt32
				Case TypeCode.Int64
				Case TypeCode.UInt64
				Case TypeCode.[Single]
				Case TypeCode.[Double]
				Case TypeCode.[Decimal]
				Case TypeCode.[String]
					Return True
				Case TypeCode.DateTime
				Case TypeCode.[Object] Or TypeCode.DateTime
					Return False
				Case Else
					Return False
			End Select
		End Function

		Public Overrides Function CanConvertTo(ByVal typeDescriptorContext As ITypeDescriptorContext, ByVal destinationType As Type) As Boolean
			If (CObj(destinationType) <> CObj(GetType(InstanceDescriptor)) AndAlso CObj(destinationType) <> CObj(GetType(String))) Then
				Return False
			End If
			Return True
		End Function

		Public Overrides Function ConvertFrom(ByVal typeDescriptorContext As ITypeDescriptorContext, ByVal cultureInfo As System.Globalization.CultureInfo, ByVal source As Object) As Object
			If (source Is Nothing) Then
				Throw MyBase.GetConvertFromException(source)
			End If
			If (TypeOf source Is String) Then
				Return CornerRadiusConverter.FromString(CStr(source), cultureInfo)
			End If
			Return New CornerRadius(Convert.ToInt32(source, cultureInfo))
		End Function

		Public Overrides Function ConvertTo(ByVal typeDescriptorContext As ITypeDescriptorContext, ByVal cultureInfo As System.Globalization.CultureInfo, ByVal value As Object, ByVal destinationType As System.Type) As Object
			If (value Is Nothing) Then
				Throw New ArgumentNullException("value")
			End If
			If (destinationType Is Nothing) Then
				Throw New ArgumentNullException("destinationType")
			End If
			If (Not TypeOf value Is CornerRadius) Then
				Throw New ArgumentException("Unexpected parameter type", "value")
			End If
			Dim cornerRadiu As CornerRadius = DirectCast(value, CornerRadius)
			If (CObj(destinationType) = CObj(GetType(String))) Then
				Return CornerRadiusConverter.ToString(cornerRadiu, cultureInfo)
			End If
			If (CObj(destinationType) <> CObj(GetType(InstanceDescriptor))) Then
				Throw New ArgumentException(String.Concat("Cannot convert to type ", destinationType.FullName))
			End If
			Dim type As System.Type = GetType(CornerRadius)
			Dim typeArray() As System.Type = { GetType(Integer), GetType(Integer), GetType(Integer), GetType(Integer) }
			Dim constructor As ConstructorInfo = type.GetConstructor(typeArray)
			Dim topLeft() As Object = { cornerRadiu.TopLeft, cornerRadiu.TopRight, cornerRadiu.BottomRight, cornerRadiu.BottomLeft }
			Return New InstanceDescriptor(constructor, topLeft)
		End Function

		Friend Shared Function FromString(ByVal s As String, ByVal cultureInfo As System.Globalization.CultureInfo) As CornerRadius
			Dim numericListSeparator() As Char = { CornerRadiusConverter.GetNumericListSeparator(cultureInfo) }
			Dim strArrays As String() = s.Split(numericListSeparator)
			Dim numArray(3) As Integer
			Dim num As Integer = 0
			Do
				numArray(num) = Integer.Parse(strArrays(num), cultureInfo)
				num = num + 1
			Loop While num < CInt(strArrays.Length)
			Dim num1 As Integer = Math.Min(5, CInt(strArrays.Length))
			If (num1 = 1) Then
				Return New CornerRadius(numArray(0))
			End If
			If (num1 <> 4) Then
				Throw New FormatException("Invalid string corner radius")
			End If
			Return New CornerRadius(numArray(0), numArray(1), numArray(2), numArray(3))
		End Function

		Friend Shared Function GetNumericListSeparator(ByVal provider As IFormatProvider) As Char
			Dim chr As Char = ","C
			Dim instance As NumberFormatInfo = NumberFormatInfo.GetInstance(provider)
			If (instance.NumberDecimalSeparator.Length > 0 AndAlso chr = instance.NumberDecimalSeparator(0)) Then
				chr = ";"C
			End If
			Return chr
		End Function

		Friend Shared Function ToString(ByVal cr As CornerRadius, ByVal cultureInfo As System.Globalization.CultureInfo) As String
			Dim numericListSeparator As Char = CornerRadiusConverter.GetNumericListSeparator(cultureInfo)
			Dim stringBuilder As System.Text.StringBuilder = New System.Text.StringBuilder(64)
			Dim topLeft As Integer = cr.TopLeft
			stringBuilder.Append(topLeft.ToString(cultureInfo))
			stringBuilder.Append(numericListSeparator)
			Dim topRight As Integer = cr.TopRight
			stringBuilder.Append(topRight.ToString(cultureInfo))
			stringBuilder.Append(numericListSeparator)
			Dim bottomRight As Integer = cr.BottomRight
			stringBuilder.Append(bottomRight.ToString(cultureInfo))
			stringBuilder.Append(numericListSeparator)
			Dim bottomLeft As Integer = cr.BottomLeft
			stringBuilder.Append(bottomLeft.ToString(cultureInfo))
			Return stringBuilder.ToString()
		End Function
	End Class
End Namespace