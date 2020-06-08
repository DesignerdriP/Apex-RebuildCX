Imports System
Imports System.Collections
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Reflection
Imports System.Runtime.InteropServices

Namespace DevComponents.WinForms.Drawing
	<ComVisible(True)>
	Public Class ColorBlendCollection
		Inherits CollectionBase
		Default Public Property Item(ByVal index As Integer) As ColorStop
			Get
				Return DirectCast(MyBase.List(index), ColorStop)
			End Get
			Set(ByVal value As ColorStop)
				MyBase.List(index) = value
			End Set
		End Property

		Public Sub New()
			MyBase.New()
		End Sub

		Public Function Add(ByVal item As ColorStop) As Integer
			Return MyBase.List.Add(item)
		End Function

		Public Sub AddRange(ByVal items As ColorStop())
			Dim colorStopArray As ColorStop() = items
			For i As Integer = 0 To CInt(colorStopArray.Length)
				Me.Add(colorStopArray(i))
			Next

		End Sub

		Public Function Contains(ByVal value As ColorStop) As Boolean
			Return MyBase.List.Contains(value)
		End Function

		Public Sub CopyFrom(ByVal col As ColorBlendCollection)
			For Each colorStop As DevComponents.WinForms.Drawing.ColorStop In col
				Me.Add(colorStop)
			Next
		End Sub

		Public Sub CopyTo(ByVal array As ColorStop(), ByVal index As Integer)
			MyBase.List.CopyTo(array, index)
		End Sub

		Public Function GetColorBlend() As System.Drawing.Drawing2D.ColorBlend
			Dim colorBlend As System.Drawing.Drawing2D.ColorBlend = New System.Drawing.Drawing2D.ColorBlend()
			Dim color(MyBase.Count - 1) As System.Drawing.Color
			Dim position(MyBase.Count - 1) As Single
			Dim num As Integer = 0
			Do
				Dim item As ColorStop = Me(num)
				color(num) = item.Color
				position(num) = item.Position
				num = num + 1
			Loop While num < MyBase.Count
			colorBlend.Colors = color
			colorBlend.Positions = position
			Return colorBlend
		End Function

		Public Function IndexOf(ByVal value As ColorStop) As Integer
			Return MyBase.List.IndexOf(value)
		End Function

		Public Shared Sub InitializeCollection(ByVal collection As ColorBlendCollection, ByVal backColor1 As Color, ByVal backColor2 As Color)
			collection.Clear()
			collection.Add(New ColorStop(backColor1, 0!))
			collection.Add(New ColorStop(backColor2, 1!))
		End Sub

		Public Sub Insert(ByVal index As Integer, ByVal value As ColorStop)
			MyBase.List.Insert(index, value)
		End Sub

		Public Sub Remove(ByVal value As ColorStop)
			MyBase.List.Remove(value)
		End Sub

		Friend Sub ಚ(ByVal u096c As ColorStop())
			MyBase.List.CopyTo(u096c, 0)
		End Sub

		Friend Function ᇴ() As ዀ
			Dim colorBlendCollection As DevComponents.WinForms.Drawing.ColorBlendCollection = Me
			If (colorBlendCollection.Count <= 1) Then
				Return ዀ.ᇸ
			End If
			Dim _ዀ As ዀ = ዀ.ᇸ
			For Each colorStop As DevComponents.WinForms.Drawing.ColorStop In colorBlendCollection
				If (colorStop.Position = 0! OrElse colorStop.Position = 1!) Then
					Continue For
				End If
				If (colorStop.Position <= 1!) Then
					If (_ዀ <> ዀ.ᇸ) Then
						If (_ዀ <> ዀ.ᇺ) Then
							Continue For
						End If
						_ዀ = ዀ.ᇸ
						Exit For
					Else
						_ዀ = ዀ.ᇹ
					End If
				ElseIf (_ዀ <> ዀ.ᇸ) Then
					If (_ዀ <> ዀ.ᇹ) Then
						Continue For
					End If
					_ዀ = ዀ.ᇸ
					Exit For
				Else
					_ዀ = ዀ.ᇺ
				End If
			Next
			If (colorBlendCollection.Count = 2 AndAlso colorBlendCollection(0).Position = 0! AndAlso colorBlendCollection(1).Position = 1!) Then
				Return ዀ.ᇹ
			End If
			If (_ዀ = ዀ.ᇸ) Then
				Return _ዀ
			End If
			If (_ዀ = ዀ.ᇹ AndAlso colorBlendCollection(0).Position <> 0! AndAlso colorBlendCollection(colorBlendCollection.Count - 1).Position <> 1!) Then
				Return ዀ.ᇸ
			End If
			If (_ዀ = ዀ.ᇺ AndAlso colorBlendCollection.Count / 2 * 2 <> colorBlendCollection.Count) Then
				Return ዀ.ᇸ
			End If
			Return _ዀ
		End Function
	End Class
End Namespace