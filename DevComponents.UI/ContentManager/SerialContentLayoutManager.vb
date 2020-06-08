Imports DevComponents.DotNetBar
Imports System
Imports System.Collections
Imports System.Drawing
Imports System.Runtime.CompilerServices
Imports System.Runtime.InteropServices

Namespace DevComponents.UI.ContentManager
	<ComVisible(True)>
	Public Class SerialContentLayoutManager
		Implements IContentLayout
		Private ಞ As LayoutManagerPositionEventHandler

		Private ಟ As LayoutManagerLayoutEventHandler

		Private ಠ As Integer

		Private ಡ As Boolean

		Private ಢ As Boolean

		Private ಣ As Boolean

		Private ತ As Boolean

		Private ಥ As eContentOrientation

		Private ದ As eContentAlignment

		Private ಧ As eContentVerticalAlignment = eContentVerticalAlignment.Middle

		Private ನ As eContentVerticalAlignment = eContentVerticalAlignment.Middle

		Private ಩ As Boolean

		Private ಪ As Boolean

		Private ಫ As Boolean

		Private ಬ As Boolean

		Public Overridable Property BlockLineAlignment As eContentVerticalAlignment
			Get
				Return Me.ನ
			End Get
			Set(ByVal value As eContentVerticalAlignment)
				Me.ನ = value
			End Set
		End Property

		Public Overridable Property BlockSpacing As Integer
			Get
				Return Me.ಠ
			End Get
			Set(ByVal value As Integer)
				Me.ಠ = value
			End Set
		End Property

		Public Overridable Property ContentAlignment As eContentAlignment
			Get
				Return Me.ದ
			End Get
			Set(ByVal value As eContentAlignment)
				Me.ದ = value
			End Set
		End Property

		Public Overridable Property ContentOrientation As eContentOrientation
			Get
				Return Me.ಥ
			End Get
			Set(ByVal value As eContentOrientation)
				Me.ಥ = value
			End Set
		End Property

		Public Overridable Property ContentVerticalAlignment As eContentVerticalAlignment
			Get
				Return Me.ಧ
			End Get
			Set(ByVal value As eContentVerticalAlignment)
				Me.ಧ = value
			End Set
		End Property

		Public Overridable Property EvenHeight As Boolean
			Get
				Return Me.಩
			End Get
			Set(ByVal value As Boolean)
				Me.಩ = value
			End Set
		End Property

		Public Overridable Property FitContainer As Boolean
			Get
				Return Me.ಢ
			End Get
			Set(ByVal value As Boolean)
				Me.ಢ = value
			End Set
		End Property

		Public Overridable Property FitContainerOversize As Boolean
			Get
				Return Me.ಡ
			End Get
			Set(ByVal value As Boolean)
				Me.ಡ = value
			End Set
		End Property

		Public Overridable Property HorizontalFitContainerHeight As Boolean
			Get
				Return Me.ತ
			End Get
			Set(ByVal value As Boolean)
				Me.ತ = value
			End Set
		End Property

		Public Property MultiLine As Boolean
			Get
				Return Me.ಪ
			End Get
			Set(ByVal value As Boolean)
				Me.ಪ = value
			End Set
		End Property

		Public Overridable Property OversizeDistribute As Boolean
			Get
				Return Me.ಬ
			End Get
			Set(ByVal value As Boolean)
				Me.ಬ = value
			End Set
		End Property

		Public Property RightToLeft As Boolean
			Get
				Return Me.ಫ
			End Get
			Set(ByVal value As Boolean)
				Me.ಫ = value
			End Set
		End Property

		Public Overridable Property VerticalFitContainerWidth As Boolean
			Get
				Return Me.ಣ
			End Get
			Set(ByVal value As Boolean)
				Me.ಣ = value
			End Set
		End Property

		Private Function ಴(ByVal ಮ As System.Drawing.Rectangle, ByVal ವ As System.Drawing.Rectangle, ByVal ಶ As IBlock()) As System.Drawing.Rectangle
			Dim x As Integer = ವ.X
			Dim num As Integer = ಮ.X
			If (ವ.Width < ಮ.Width) Then
				ವ.X = ಮ.Right - (ವ.X - ಮ.X + ವ.Width)
			ElseIf (ವ.Width > ಮ.Width) Then
				ಮ.Width = ವ.Width
			End If
			Dim blockArray As IBlock() = ಶ
			Dim num1 As Integer = 0
			Do
				Dim rectangle As IBlock = blockArray(num1)
				If (rectangle.Visible) Then
					Dim bounds As System.Drawing.Rectangle = rectangle.Bounds
					rectangle.Bounds = New System.Drawing.Rectangle(ಮ.Right - (bounds.X - ಮ.X + bounds.Width), bounds.Y, bounds.Width, bounds.Height)
				End If
				num1 = num1 + 1
			Loop While num1 < CInt(blockArray.Length)
			Return ವ
		End Function

		Public Sub New()
			MyBase.New()
		End Sub

		Public Overridable Function Layout(ByVal containerBounds As System.Drawing.Rectangle, ByVal contentBlocks As IBlock(), ByVal blockLayout As BlockLayoutManager) As System.Drawing.Rectangle Implements IContentLayout.Layout
			Dim empty As System.Drawing.Rectangle = System.Drawing.Rectangle.Empty
			Dim location As Point = containerBounds.Location
			Dim arrayLists As ArrayList = New ArrayList()
			arrayLists.Add(New SerialContentLayoutManager.ಷ())
			Dim item As SerialContentLayoutManager.ಷ = TryCast(arrayLists(0), SerialContentLayoutManager.ಷ)
			Dim flag As Boolean = False
			Dim canStartNewLine As Boolean = True
			Dim num As Integer = 0
			Dim blockArray As IBlock() = contentBlocks
			Dim num1 As Integer = 0
			Do
				Dim block As IBlock = blockArray(num1)
				If (block.Visible) Then
					If (Me.ಟ IsNot Nothing) Then
						Dim layoutManagerLayoutEventArg As LayoutManagerLayoutEventArgs = New LayoutManagerLayoutEventArgs(block, location, num)
						Me.ಟ(Me, layoutManagerLayoutEventArg)
						location = layoutManagerLayoutEventArg.CurrentPosition
						If (layoutManagerLayoutEventArg.CancelLayout) Then
							GoTo Label0
						End If
					End If
					num = num + 1
					Dim size As System.Drawing.Size = containerBounds.Size
					Dim isBlockElement As Boolean = False
					Dim isNewLineAfterElement As Boolean = False
					Dim isBlockContainer As Boolean = False
					If (Not TypeOf block Is IBlockExtended) Then
						canStartNewLine = True
					Else
						Dim blockExtended As IBlockExtended = TryCast(block, IBlockExtended)
						isBlockElement = blockExtended.IsBlockElement
						isNewLineAfterElement = blockExtended.IsNewLineAfterElement
						canStartNewLine = blockExtended.CanStartNewLine
						isBlockContainer = blockExtended.IsBlockContainer
					End If
					If (Not isBlockElement AndAlso Not isBlockContainer) Then
						If (Me.ಥ <> eContentOrientation.Horizontal) Then
							size.Height = containerBounds.Bottom - location.Y
						Else
							size.Width = containerBounds.Right - location.X
						End If
					End If
					blockLayout.Layout(block, size)
					If (Me.ಪ AndAlso item.ಸ.Count > 0) Then
						If (Me.ಥ = eContentOrientation.Horizontal AndAlso (location.X + block.Bounds.Width > containerBounds.Right AndAlso canStartNewLine OrElse isBlockElement OrElse flag)) Then
							location.X = containerBounds.X
							location.Y = location.Y + item.ಹ.Height + Me.ಠ
							item = New SerialContentLayoutManager.ಷ() With
							{
								.಺ = arrayLists.Count
							}
							arrayLists.Add(item)
						ElseIf (Me.ಥ = eContentOrientation.Vertical AndAlso (location.Y + block.Bounds.Height > containerBounds.Bottom AndAlso canStartNewLine OrElse isBlockElement OrElse flag)) Then
							location.Y = containerBounds.Y
							location.X = location.X + item.ಹ.Width + Me.ಠ
							item = New SerialContentLayoutManager.ಷ() With
							{
								.಺ = arrayLists.Count
							}
							arrayLists.Add(item)
						End If
					End If
					If (Me.ಥ = eContentOrientation.Horizontal) Then
						If (block.Bounds.Height > item.ಹ.Height) Then
							Dim bounds As System.Drawing.Rectangle = block.Bounds
							item.ಹ.Height = bounds.Height
						End If
						Dim x As Integer = location.X
						Dim rectangle As System.Drawing.Rectangle = block.Bounds
						item.ಹ.Width = x + rectangle.Width - containerBounds.X
					ElseIf (Me.ಥ = eContentOrientation.Vertical) Then
						If (block.Bounds.Width > item.ಹ.Width) Then
							Dim bounds1 As System.Drawing.Rectangle = block.Bounds
							item.ಹ.Width = bounds1.Width
						End If
						Dim y As Integer = location.Y
						Dim rectangle1 As System.Drawing.Rectangle = block.Bounds
						item.ಹ.Height = y + rectangle1.Height - containerBounds.Y
					End If
					item.ಸ.Add(block)
					If (block.Visible) Then
						item.಻ = item.಻ + 1
					End If
					Dim bounds2 As System.Drawing.Rectangle = block.Bounds
					Dim width As System.Drawing.Rectangle = New System.Drawing.Rectangle(location, bounds2.Size) With
					{
						.X = width.X + block.Margin.Left,
						.Y = width.Y + block.Margin.Top
					}
					block.Bounds = width
					If (Not empty.IsEmpty) Then
						empty = System.Drawing.Rectangle.Union(empty, block.Bounds)
					Else
						width = block.Bounds
						width.X = width.X - block.Margin.Left
						width.Y = width.Y - block.Margin.Top
						width.Width = width.Width + block.Margin.Horizontal
						width.Height = width.Height + block.Margin.Vertical
						empty = width
					End If
					flag = isBlockElement Or isNewLineAfterElement
					location = Me.ಱ(block, location)
				Else
					block.Bounds = System.Drawing.Rectangle.Empty
				End If
			Label0:
				num1 = num1 + 1
			Loop While num1 < CInt(blockArray.Length)
			empty = Me.ಭ(containerBounds, empty, arrayLists)
			If (Me.ಫ) Then
				empty = Me.಴(containerBounds, empty, contentBlocks)
			End If
			empty = blockLayout.FinalizeLayout(containerBounds, empty, arrayLists)
			Return empty
		End Function

		Private Function ಭ(ByVal ಮ As Rectangle, ByVal ಯ As Rectangle, ByVal ರ As ArrayList) As Rectangle
			' 
			' Current member / type: System.Drawing.Rectangle DevComponents.UI.ContentManager.SerialContentLayoutManager::ಭ(System.Drawing.Rectangle,System.Drawing.Rectangle,System.Collections.ArrayList)
			' File path: C:\Users\Admin\Desktop\pvix2e\CxUpdater.exe
			' 
			' Product version: 2019.1.118.0
			' Exception in: System.Drawing.Rectangle ಭ(System.Drawing.Rectangle,System.Drawing.Rectangle,System.Collections.ArrayList)
			' 
			' The unary opperator AddressReference is not supported in VisualBasic
			'    at ..(DecompilationContext ,  ) in C:\DeveloperTooling_JD_Agent1\_work\15\s\OpenSource\Cecil.Decompiler\Steps\DetermineNotSupportedVBCodeStep.cs:line 22
			'    at ..(MethodBody ,  , ILanguage ) in C:\DeveloperTooling_JD_Agent1\_work\15\s\OpenSource\Cecil.Decompiler\Decompiler\DecompilationPipeline.cs:line 88
			'    at ..(MethodBody , ILanguage ) in C:\DeveloperTooling_JD_Agent1\_work\15\s\OpenSource\Cecil.Decompiler\Decompiler\DecompilationPipeline.cs:line 70
			'    at Telerik.JustDecompiler.Decompiler.Extensions.( , ILanguage , MethodBody , DecompilationContext& ) in C:\DeveloperTooling_JD_Agent1\_work\15\s\OpenSource\Cecil.Decompiler\Decompiler\Extensions.cs:line 95
			'    at Telerik.JustDecompiler.Decompiler.Extensions.(MethodBody , ILanguage , DecompilationContext& ,  ) in C:\DeveloperTooling_JD_Agent1\_work\15\s\OpenSource\Cecil.Decompiler\Decompiler\Extensions.cs:line 58
			'    at ..(ILanguage , MethodDefinition ,  ) in C:\DeveloperTooling_JD_Agent1\_work\15\s\OpenSource\Cecil.Decompiler\Decompiler\WriterContextServices\BaseWriterContextService.cs:line 117
			' 
			' mailto: JustDecompilePublicFeedback@telerik.com

		End Function

		Private Function ಱ(ByVal ಲ As IBlock, ByVal ಳ As Point) As Point
			If (Me.ಞ IsNot Nothing) Then
				Dim layoutManagerPositionEventArg As LayoutManagerPositionEventArgs = New LayoutManagerPositionEventArgs() With
				{
					.Block = ಲ,
					.CurrentPosition = ಳ
				}
				Me.ಞ(Me, layoutManagerPositionEventArg)
				If (layoutManagerPositionEventArg.Cancel) Then
					Return layoutManagerPositionEventArg.NextPosition
				End If
			End If
			If (Me.ಥ <> eContentOrientation.Horizontal) Then
				Dim y As Integer = ಳ.Y
				Dim bounds As System.Drawing.Rectangle = ಲ.Bounds
				ಳ.Y = y + bounds.Height + Me.ಠ + ಲ.Margin.Vertical
			Else
				Dim x As Integer = ಳ.X
				Dim rectangle As System.Drawing.Rectangle = ಲ.Bounds
				ಳ.X = x + rectangle.Width + Me.ಠ + ಲ.Margin.Horizontal
			End If
			Return ಳ
		End Function

		Public Custom Event BeforeNewBlockLayout As LayoutManagerLayoutEventHandler
			<MethodImpl(MethodImplOptions.Synchronized)>
			AddHandler(ByVal value As LayoutManagerLayoutEventHandler)
				AddHandler Me.ಟ,  value
			End AddHandler
			<MethodImpl(MethodImplOptions.Synchronized)>
			RemoveHandler(ByVal value As LayoutManagerLayoutEventHandler)
				RemoveHandler Me.ಟ,  value
			End RemoveHandler
		End Event

		Public Custom Event NextPosition As LayoutManagerPositionEventHandler
			<MethodImpl(MethodImplOptions.Synchronized)>
			AddHandler(ByVal value As LayoutManagerPositionEventHandler)
				AddHandler Me.ಞ,  value
			End AddHandler
			<MethodImpl(MethodImplOptions.Synchronized)>
			RemoveHandler(ByVal value As LayoutManagerPositionEventHandler)
				RemoveHandler Me.ಞ,  value
			End RemoveHandler
		End Event

		Private Structure ಼
			Public ܀ As Integer

			Public ܁ As Integer

			Public ಽ As Single

			Public ಾ As Single

			Public ಿ As Boolean
		End Structure

		Friend Class ಷ
			Public ಸ As ArrayList

			Public ಹ As Size

			Public ಺ As Integer

			Public ಻ As Integer

			Public Sub New()
				MyBase.New()
			End Sub
		End Class
	End Class
End Namespace