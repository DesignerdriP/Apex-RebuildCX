Imports System
Imports System.Drawing
Imports System.Runtime.InteropServices

Namespace DevComponents.UI.ContentManager
	<ComVisible(True)>
	Public Class LayoutManagerPositionEventArgs
		Inherits EventArgs
		Public Block As IBlock

		Public CurrentPosition As Point = Point.Empty

		Public NextPosition As Point = Point.Empty

		Public Cancel As Boolean

		Public Sub New()
			MyBase.New()
		End Sub
	End Class
End Namespace