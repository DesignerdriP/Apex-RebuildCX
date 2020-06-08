Imports System
Imports System.Drawing
Imports System.Runtime.InteropServices

Namespace DevComponents.UI.ContentManager
	<ComVisible(True)>
	Public Class LayoutManagerLayoutEventArgs
		Inherits EventArgs
		Public Block As IBlock

		Public CurrentPosition As Point = Point.Empty

		Public CancelLayout As Boolean

		Public BlockVisibleIndex As Integer

		Public Sub New(ByVal block As IBlock, ByVal currentPosition As Point, ByVal visibleIndex As Integer)
			MyBase.New()
			Me.Block = block
			Me.CurrentPosition = currentPosition
			Me.BlockVisibleIndex = visibleIndex
		End Sub
	End Class
End Namespace