Imports System
Imports System.Collections
Imports System.Drawing
Imports System.Runtime.InteropServices

Namespace DevComponents.UI.ContentManager
	<ComVisible(True)>
	Public MustInherit Class BlockLayoutManager
		Private ܎ As System.Drawing.Graphics

		Public Property Graphics As System.Drawing.Graphics
			Get
				Return Me.܎
			End Get
			Set(ByVal value As System.Drawing.Graphics)
				Me.܎ = value
			End Set
		End Property

		Protected Sub New()
			MyBase.New()
		End Sub

		Public MustOverride Function FinalizeLayout(ByVal containerBounds As Rectangle, ByVal blocksBounds As Rectangle, ByVal lines As ArrayList) As Rectangle

		Public MustOverride Sub Layout(ByVal block As IBlock, ByVal availableSize As Size)
	End Class
End Namespace