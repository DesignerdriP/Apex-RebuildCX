Imports System
Imports System.Runtime.InteropServices

Namespace DevComponents.UI.ContentManager
	<ComVisible(True)>
	Public Interface IBlockExtended
		Inherits IBlock
		ReadOnly Property CanStartNewLine As Boolean

		ReadOnly Property IsBlockContainer As Boolean

		ReadOnly Property IsBlockElement As Boolean

		ReadOnly Property IsNewLineAfterElement As Boolean
	End Interface
End Namespace