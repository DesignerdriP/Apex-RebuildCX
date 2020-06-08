Imports DevComponents.DotNetBar
Imports System
Imports System.ComponentModel
Imports System.Drawing
Imports System.Drawing.Drawing2D

Namespace DevComponents.WinForms.Drawing
	Friend Class ┡
		Inherits ᓨ
		Private _ሎ As Border

		Private _┣ As Fill

		Private _┤ As CornerRadius

		Public Property ┢ As CornerRadius
			Get
				Return Me._┤
			End Get
			Set(ByVal value As CornerRadius)
				Me._┤ = value
			End Set
		End Property

		<DefaultValue(Nothing)>
		<Description("Indicates shape border.")>
		Public Property ᔀ As Border
			Get
				Return Me._ሎ
			End Get
			Set(ByVal value As Border)
				Me._ሎ = value
			End Set
		End Property

		<DefaultValue(Nothing)>
		<Description("Indicates shape fill")>
		Public Property ᔁ As Fill
			Get
				Return Me._┣
			End Get
			Set(ByVal value As Fill)
				Me._┣ = value
			End Set
		End Property

		Public Sub New()
			MyBase.New()
		End Sub

		Public Overrides Sub Paint(ByVal g As Graphics, ByVal bounds As System.Drawing.Rectangle)
			If (bounds.Width < 2 OrElse bounds.Height < 2 OrElse g Is Nothing OrElse Me._┣ Is Nothing AndAlso Me._ሎ Is Nothing) Then
				Return
			End If
			Dim roundedRectanglePath As GraphicsPath = Nothing
			If (Not Me._┤.ᔭ) Then
				roundedRectanglePath = DisplayHelp.GetRoundedRectanglePath(bounds, Me._┤.TopLeft, Me._┤.TopRight, Me._┤.BottomRight, Me._┤.BottomLeft)
			End If
			If (Me._┣ IsNot Nothing) Then
				Dim brush As System.Drawing.Brush = Me._┣.CreateBrush(bounds)
				If (brush IsNot Nothing) Then
					Dim smoothingMode As System.Drawing.Drawing2D.SmoothingMode = g.SmoothingMode
					If (TypeOf brush Is SolidBrush AndAlso roundedRectanglePath Is Nothing) Then
						g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None
					End If
					If (roundedRectanglePath IsNot Nothing) Then
						g.FillPath(brush, roundedRectanglePath)
					Else
						g.FillRectangle(brush, bounds)
					End If
					g.SmoothingMode = smoothingMode
					brush.Dispose()
				End If
			End If
			If (Me._ሎ IsNot Nothing) Then
				Dim pen As System.Drawing.Pen = Me._ሎ.CreatePen()
				If (pen IsNot Nothing) Then
					If (roundedRectanglePath IsNot Nothing) Then
						g.DrawPath(pen, roundedRectanglePath)
					Else
						g.DrawRectangle(pen, bounds)
					End If
					pen.Dispose()
				End If
			End If
			Dim _ᓨ As ᓨ = MyBase.┝
			If (_ᓨ IsNot Nothing) Then
				Dim rectangle As System.Drawing.Rectangle = Border.Deflate(bounds, Me._ሎ)
				Dim clip As Region = Nothing
				If (roundedRectanglePath IsNot Nothing AndAlso MyBase.┞) Then
					clip = g.Clip
					g.SetClip(roundedRectanglePath, CombineMode.Intersect)
				End If
				_ᓨ.Paint(g, rectangle)
				If (clip IsNot Nothing) Then
					g.Clip = clip
				End If
			End If
			If (roundedRectanglePath IsNot Nothing) Then
				roundedRectanglePath.Dispose()
			End If
		End Sub

		<EditorBrowsable(EditorBrowsableState.Never)>
		Public Sub ResetCornerRadius()
			Me.┢ = New CornerRadius()
		End Sub

		<EditorBrowsable(EditorBrowsableState.Never)>
		Public Function ShouldSerializeCornerRadius() As Boolean
			Return Not Me._┤.ᔭ
		End Function
	End Class
End Namespace