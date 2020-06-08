Imports System
Imports System.ComponentModel
Imports System.ComponentModel.Design.Serialization
Imports System.Drawing
Imports System.Globalization
Imports System.Reflection
Imports System.Runtime.InteropServices

Namespace DevComponents.WinForms.Drawing
	<ComVisible(True)>
	Public Class ColorStopConverter
		Inherits TypeConverter
		Public Sub New()
			MyBase.New()
		End Sub

		Public Overrides Function CanConvertTo(ByVal context As ITypeDescriptorContext, ByVal destinationType As Type) As Boolean
			If (CObj(destinationType) = CObj(GetType(InstanceDescriptor))) Then
				Return True
			End If
			Return MyBase.CanConvertTo(context, destinationType)
		End Function

		Public Overrides Function ConvertTo(ByVal context As ITypeDescriptorContext, ByVal culture As CultureInfo, ByVal value As Object, ByVal destinationType As Type) As Object
			If (destinationType Is Nothing) Then
				Throw New ArgumentNullException("destinationType")
			End If
			If (CObj(destinationType) = CObj(GetType(InstanceDescriptor)) AndAlso TypeOf value Is DevComponents.WinForms.Drawing.ColorStop) Then
				Dim colorStop As DevComponents.WinForms.Drawing.ColorStop = DirectCast(value, DevComponents.WinForms.Drawing.ColorStop)
				Dim constructor As MemberInfo = Nothing
				Dim objArray As Object() = Nothing
				Dim typeArray() As Type = { GetType(System.Drawing.Color), GetType(Single) }
				constructor = GetType(DevComponents.WinForms.Drawing.ColorStop).GetConstructor(typeArray)
				Dim color() As Object = { colorStop.Color, colorStop.Position }
				objArray = color
				If (constructor IsNot Nothing) Then
					Return New InstanceDescriptor(constructor, objArray)
				End If
			End If
			Return MyBase.ConvertTo(context, culture, value, destinationType)
		End Function
	End Class
End Namespace