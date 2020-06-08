Imports System.Drawing
Imports System.Runtime.InteropServices

Namespace DevComponents.UI.ContentManager
	<ComVisible(True)>
	Public Interface IContentLayout
		Function Layout(ByVal containerBounds As Rectangle, ByVal contentBlocks As IBlock(), ByVal blockLayout As BlockLayoutManager) As Rectangle
	End Interface
End Namespace