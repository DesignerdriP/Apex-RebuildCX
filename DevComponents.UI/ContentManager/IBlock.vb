Imports DevComponents.DotNetBar
Imports System
Imports System.Drawing
Imports System.Runtime.InteropServices

Namespace DevComponents.UI.ContentManager
	<ComVisible(True)>
	Public Interface IBlock
		Property Bounds As Rectangle

		Property Margin As Padding

		Property Visible As Boolean
	End Interface
End Namespace