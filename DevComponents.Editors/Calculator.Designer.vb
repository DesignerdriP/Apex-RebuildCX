Imports DevComponents.DotNetBar
Imports System
Imports System.ComponentModel
Imports System.Drawing
Imports System.Globalization
Imports System.Media
Imports System.Resources
Imports System.Runtime.CompilerServices
Imports System.Runtime.InteropServices
Imports System.Windows.Forms

Namespace DevComponents.Editors
	<ComVisible(True)>
	<DefaultEvent("ValueChanged")>
	<DefaultProperty("Value")>
	<Designer("DevComponents.DotNetBar.Design.CalculatorDesigner, DevComponents.DotNetBar.Design, Version=11.3.0.0, Culture=neutral,  PublicKeyToken=2c9ff1fddc42653c")>
	<ToolboxBitmap(GetType(DotNetBarManager), "Calculator.ico")>
	<ToolboxItem(True)>
	Public Class Calculator
		Inherits Control
		Private ₍ As EventHandler(Of ButtonClickEventArgs)

		Private ᑽ As EventHandler(Of ValueChangedEventArgs)

		Private ₎ As EventHandler

		Private _₏ As String

		Private _ᒃ As Double

		Private _ₐ As Double

		Private _ₑ As Double

		Private _ₒ As Operators

		Private _ₓ As Operators

		Private _ₔ As Boolean

		Private _ₕ As Boolean

		Private _ₖ As DevComponents.Editors.DecimalKeyVisibility

		Private _ₗ As Timer

		Private _ₘ As String

		Private _ያ As System.Drawing.Size = System.Drawing.Size.Empty

		Private _ₙ As Boolean = True

		Private _ₚ As Boolean = True

		Private Ո As IContainer

		Private ₛ As PanelEx

		Private ₜ As LabelX

		Private ₝ As LabelX

		Private ₞ As PanelEx

		Private ₟ As PanelEx

		Public BtnNegate As ButtonX

		Public BtnDecimal As ButtonX

		Public BtnDigit7 As ButtonX

		Public BtnBack As ButtonX

		Public BtnMemClear As ButtonX

		Public BtnMemRestore As ButtonX

		Public BtnMemStore As ButtonX

		Public BtnMemSubtract As ButtonX

		Public BtnSqrt As ButtonX

		Public BtnPercent As ButtonX

		Public BtnReciprocal As ButtonX

		Public BtnDivide As ButtonX

		Public BtnMultiply As ButtonX

		Public BtnSubtract As ButtonX

		Public BtnAdd As ButtonX

		Public BtnClear As ButtonX

		Public BtnDigit4 As ButtonX

		Public BtnDigit2 As ButtonX

		Public BtnClearEntry As ButtonX

		Public BtnDigit9 As ButtonX

		Public BtnDigit6 As ButtonX

		Public BtnDigit3 As ButtonX

		Public BtnDigit1 As ButtonX

		Public BtnDigit8 As ButtonX

		Public BtnDigit5 As ButtonX

		Public BtnDigit0 As ButtonX

		Public BtnMemAdd As ButtonX

		Public BtnEquals As ButtonX

		Private ₠ As LabelX

		Friend Property ₌ As Boolean
			Get
				Return Me._ₙ
			End Get
			Set(ByVal value As Boolean)
				If (value <> Me._ₙ) Then
					Dim flag As Boolean = Me._ₙ
					Me._ₙ = value
					Me.OnFocusButtonsOnMouseDownChanged(flag, value)
				End If
			End Set
		End Property

		<Browsable(True)>
		<DefaultValue(False)>
		<DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
		<EditorBrowsable(EditorBrowsableState.Always)>
		Public Overrides Property AutoSize As Boolean
			Get
				Return MyBase.AutoSize
			End Get
			Set(ByVal value As Boolean)
				If (Me.AutoSize <> value) Then
					MyBase.AutoSize = value
					Me.ዹ()
				End If
			End Set
		End Property

		<Category("Appearance")>
		<DefaultValue(DevComponents.Editors.DecimalKeyVisibility.Auto)>
		<Description("Indicates visibility of the decimal calculator key.")>
		Public Property DecimalKeyVisibility As DevComponents.Editors.DecimalKeyVisibility
			Get
				Return Me._ₖ
			End Get
			Set(ByVal value As DevComponents.Editors.DecimalKeyVisibility)
				If (Me._ₖ <> value) Then
					Me._ₖ = value
					Me.ᒰ()
				End If
			End Set
		End Property

		<Browsable(False)>
		Public ReadOnly Property DisplaySValue As String
			Get
				If (Not String.IsNullOrEmpty(Me._₏)) Then
					Return Me._₏
				End If
				Return Me._ᒃ.ToString()
			End Get
		End Property

		<Browsable(False)>
		Public ReadOnly Property DisplayValue As Double
			Get
				If (String.IsNullOrEmpty(Me._₏)) Then
					Return Me._ᒃ
				End If
				Return Double.Parse(Me._₏)
			End Get
		End Property

		<Category("Appearance")>
		<DefaultValue(True)>
		<Description("Indicates whether calculator display is visible.")>
		Public Property DisplayVisible As Boolean
			Get
				Return Me._ₚ
			End Get
			Set(ByVal value As Boolean)
				If (value <> Me._ₚ) Then
					Dim flag As Boolean = Me._ₚ
					Me._ₚ = value
					Me.OnDisplayVisibleChanged(flag, value)
				End If
			End Set
		End Property

		<Category("Behavior")>
		<DefaultValue(False)>
		<Description("Indicates whether calculator displays only Integer values.")>
		Public Property IsIntValue As Boolean
			Get
				Return Me._ₔ
			End Get
			Set(ByVal value As Boolean)
				If (Me._ₔ <> value) Then
					Me._ₔ = value
					Me.ᒰ()
				End If
			End Set
		End Property

		<Category("Appearance")>
		<DefaultValue(True)>
		<Description("Indicates whether memory keys are visible.")>
		Public Property ShowMemKeys As Boolean
			Get
				Return Me._ₕ
			End Get
			Set(ByVal value As Boolean)
				If (Me._ₕ <> value) Then
					Me._ₕ = value
					If (Not Me._ₕ) Then
						Dim height As DevComponents.DotNetBar.PanelEx = Me.ₛ
						height.Height = height.Height - (Me.₞.Height - 1)
						If (Not Me._ₚ) Then
							Me.₟.Location = New Point(1, Me.₠.Top)
						Else
							Me.₟.Location = New Point(1, Me.₠.Bottom)
						End If
					Else
						Dim panelEx As DevComponents.DotNetBar.PanelEx = Me.ₛ
						panelEx.Height = panelEx.Height + (Me.₞.Height - 1)
						Me.₟.Location = New Point(1, Me.₞.Bottom)
					End If
					Me.InvalidateAutoSize()
				End If
			End Set
		End Property

		<Browsable(False)>
		<DefaultValue(Nothing)>
		<DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>
		Public Property StringValue As String
			Get
				Return Me._₏
			End Get
			Set(ByVal value As String)
				Me._₏ = value
				Me.OnValueChanged()
			End Set
		End Property

		<Browsable(False)>
		<DefaultValue(0)>
		<DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>
		Public Property Value As Double
			Get
				Return Me._ᒃ
			End Get
			Set(ByVal value As Double)
				Me._ᒃ = value
				Me.OnValueChanged()
			End Set
		End Property

		Private Function ₶() As ButtonX
			Me.⃉()
			Me._ₓ = Operators.Calc
			Me.₝.Text = ""
			Me.OnValueChanged()
			Return Me.BtnEquals
		End Function

		Private Sub ₷(ByVal u0656 As Object, ByVal u0657 As EventArgs)
			If (Not Me.⃊(u0656)) Then
				Me.₸()
			End If
			If (Me._ₙ) Then
				MyBase.Focus()
			End If
		End Sub

		Private Function ₸() As ButtonX
			If (String.IsNullOrEmpty(Me._₏)) Then
				Me._ᒃ = 0
			Else
				Me._₏ = "0"
			End If
			Me.OnValueChanged()
			Return Me.BtnClearEntry
		End Function

		Private Sub ₻(ByVal u0656 As Object, ByVal u0657 As EventArgs)
			If (Not Me.⃊(u0656)) Then
				Me.₼()
			End If
			If (Me._ₙ) Then
				MyBase.Focus()
			End If
		End Sub

		Private Function ₼() As ButtonX
			Me.⃅(Me.DisplayValue)
			Return Me.BtnMemStore
		End Function

		Private Sub ₽(ByVal u0656 As Object, ByVal u0657 As EventArgs)
			If (Not Me.⃊(u0656)) Then
				Me.₾()
			End If
			If (Me._ₙ) Then
				MyBase.Focus()
			End If
		End Sub

		Private Function ₾() As ButtonX
			Me._₏ = Me._ₑ.ToString()
			Me.OnValueChanged()
			Return Me.BtnMemRestore
		End Function

		Private Sub ₿(ByVal u0656 As Object, ByVal u0657 As EventArgs)
			If (Not Me.⃊(u0656)) Then
				Me.⃀()
			End If
			If (Me._ₙ) Then
				MyBase.Focus()
			End If
		End Sub

		Private Function ⃀() As ButtonX
			Me.⃅(0)
			Return Me.BtnMemClear
		End Function

		Private Sub ⃁(ByVal u0656 As Object, ByVal u0657 As EventArgs)
			If (Not Me.⃊(u0656)) Then
				Me.⃂()
			End If
			If (Me._ₙ) Then
				MyBase.Focus()
			End If
		End Sub

		Private Function ⃂() As ButtonX
			Me.⃅(Me._ₑ + Me.DisplayValue)
			Return Me.BtnMemAdd
		End Function

		Private Sub ⃃(ByVal u0656 As Object, ByVal u0657 As EventArgs)
			If (Not Me.⃊(u0656)) Then
				Me.⃄()
			End If
			If (Me._ₙ) Then
				MyBase.Focus()
			End If
		End Sub

		Private Function ⃄() As ButtonX
			Me.⃅(Me._ₑ - Me.DisplayValue)
			Return Me.BtnMemSubtract
		End Function

		Private Sub ⃅(ByVal Ԝ As Double)
			Me._ₑ = Ԝ
			Me.ₜ.Text = If(Ԝ = 0, "", "M")
		End Sub

		Private Sub ⃆(ByVal u20c7 As Operators)
			Me._ₒ = u20c7
			Me._ₓ = u20c7
			Me.₝.Text = CChar(u20c7).ToString()
		End Sub

		Private Function ⃈(ByVal u0945 As ButtonX, ByVal u20c7 As Operators) As ButtonX
			If (Not String.IsNullOrEmpty(Me._₏)) Then
				Me.⃉()
			End If
			Me.⃆(u20c7)
			Me.OnValueChanged()
			Return u0945
		End Function

		Private Sub ⃉()
			If (Not String.IsNullOrEmpty(Me._₏)) Then
				Me._ₐ = Double.Parse(Me._₏)
			End If
			Dim [operator] As Operators = Me._ₒ
			If ([operator] = Operators.None) Then
				Me._ᒃ = Me._ₐ
			Else
				Select Case [operator]
					Case Operators.Multiply
						Me._ᒃ *= Me._ₐ
						Exit Select
					Case Operators.Add
						Me._ᒃ += Me._ₐ
						Exit Select
					Case Operators.Subtract
						Me._ᒃ -= Me._ₐ
						Exit Select
					Case Operators.Divide
						If (Me._ₐ <= 0) Then
							SystemSounds.Beep.Play()
							Exit Select
						Else
							Me._ᒃ /= Me._ₐ
							Exit Select
						End If
				End Select
			End If
			Me._₏ = Nothing
			Me.OnValueChanged()
		End Sub

		Private Function ⃊(ByVal Ꭸ As Object) As Boolean
			If (Me.₍ Is Nothing) Then
				Return False
			End If
			Dim buttonClickEventArg As ButtonClickEventArgs = New ButtonClickEventArgs(Ꭸ)
			Me.₍(Me, buttonClickEventArg)
			Return buttonClickEventArg.Cancel
		End Function

		Private Sub ⃋(ByVal u0945 As ButtonX)
			Dim x As Integer = u0945.Location.X
			Dim location As Point = u0945.Location
			Dim mouseEventArg As MouseEventArgs = New MouseEventArgs(System.Windows.Forms.MouseButtons.Left, 1, x, location.Y, 0)
			u0945.InternalItem.InternalMouseDown(mouseEventArg)
		End Sub

		Private Sub ⃌(ByVal u0945 As ButtonX)
			Dim x As Integer = u0945.Location.X
			Dim location As Point = u0945.Location
			Dim mouseEventArg As MouseEventArgs = New MouseEventArgs(System.Windows.Forms.MouseButtons.Left, 1, x, location.Y, 0)
			u0945.InternalItem.InternalMouseUp(mouseEventArg)
		End Sub

		Private Sub ⃍(ByVal u0656 As Object, ByVal u0657 As EventArgs)
			Dim tag As ButtonX = TryCast(Me._ₗ.Tag, ButtonX)
			If (tag IsNot Nothing) Then
				Me.⃌(tag)
			End If
			Me._ₗ.[Stop]()
		End Sub

		Public Sub New()
			MyBase.New()
			Me.ظ()
			Me._ₕ = True
			Me._ₖ = DevComponents.Editors.DecimalKeyVisibility.Auto
			MyBase.SetStyle(DisplayHelp.DoubleBufferFlag Or ControlStyles.AllPaintingInWmPaint Or ControlStyles.Opaque, True)
			Me.₠.BackColor = Color.White
			Me.ₛ.BackColor = Color.White
			Me.₟.BackColor = Color.White
			Me._ₘ = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator
			Me.BtnDecimal.Text = Me._ₘ
		End Sub

		Private Sub ₱(ByVal u0656 As Object, ByVal u0657 As EventArgs)
			If (Not Me.⃊(Me.BtnReciprocal)) Then
				Me.₲()
			End If
			If (Me._ₙ) Then
				MyBase.Focus()
			End If
		End Sub

		Private Function ₣(ByVal u0945 As ButtonX) As ButtonX
			Me.₤(u0945)
			Me.OnValueChanged()
			Return u0945
		End Function

		Private Sub ₤(ByVal u0656 As Object)
			Dim buttonX As DevComponents.DotNetBar.ButtonX = TryCast(u0656, DevComponents.DotNetBar.ButtonX)
			If (buttonX IsNot Nothing) Then
				Dim name As Char = buttonX.Name(8)
				If (Me._₏ Is Nothing OrElse Me._ₓ = Operators.[Set]) Then
					Me._₏ = ""
					If (Me._ₓ = Operators.Calc OrElse Me._ₓ = Operators.[Set]) Then
						Me.⃆(Operators.None)
					End If
				End If
				Dim calculator As DevComponents.Editors.Calculator = Me
				calculator._₏ = String.Concat(calculator._₏, name)
				Me.₡()
				Me.OnCalculatorDisplayChanged(EventArgs.Empty)
			End If
		End Sub

		Private Sub ₥(ByVal u0656 As Object, ByVal u0657 As EventArgs)
			If (Not Me.⃊(Me.BtnDecimal)) Then
				Me.₦()
			End If
			If (Me._ₙ) Then
				MyBase.Focus()
			End If
		End Sub

		Private Sub ₭(ByVal u0656 As Object, ByVal u0657 As EventArgs)
			If (Not Me.⃊(u0656)) Then
				Me.₮()
			End If
			If (Me._ₙ) Then
				MyBase.Focus()
			End If
		End Sub

		Private Function ₦() As ButtonX
			If (Not Me._ₔ) Then
				If (Me._₏ Is Nothing OrElse Me._ₓ = Operators.[Set]) Then
					Me._₏ = String.Concat("0", Me._ₘ)
					If (Me._ₓ = Operators.Calc OrElse Me._ₓ = Operators.[Set]) Then
						Me.⃆(Operators.None)
					End If
				ElseIf (Not Me._₏.Contains(Me._ₘ)) Then
					Dim calculator As DevComponents.Editors.Calculator = Me
					calculator._₏ = String.Concat(calculator._₏, Me._ₘ)
				End If
			End If
			Me.OnValueChanged()
			Return Me.BtnDecimal
		End Function

		Private Sub ₧(ByVal u0656 As Object, ByVal u0657 As EventArgs)
			If (Not Me.⃊(Me.BtnBack)) Then
				Me.₨()
			End If
			If (Me._ₙ) Then
				MyBase.Focus()
			End If
		End Sub

		Private Function ₨() As ButtonX
			If (String.IsNullOrEmpty(Me._₏)) Then
				SystemSounds.Beep.Play()
			Else
				Me._₏ = Me._₏.Substring(0, Me._₏.Length - 1)
				If (Me._₏.Length = 0 OrElse Me._₏.Length = 1 AndAlso Me._₏(0) = "-"C) Then
					Me._₏ = "0"
				End If
			End If
			Me.OnValueChanged()
			Return Me.BtnBack
		End Function

		Private Sub ₩(ByVal u0656 As Object, ByVal u0657 As EventArgs)
			If (Not Me.⃊(u0656)) Then
				Me.⃈(Me.BtnAdd, Operators.Add)
			End If
			If (Me._ₙ) Then
				MyBase.Focus()
			End If
		End Sub

		Private Sub ₯(ByVal u0656 As Object, ByVal u0657 As EventArgs)
			If (Not Me.⃊(u0656)) Then
				Me.₰()
			End If
			If (Me._ₙ) Then
				MyBase.Focus()
			End If
		End Sub

		Private Function ₰() As ButtonX
			If (Not String.IsNullOrEmpty(Me._₏)) Then
				Dim num As Double = Double.Parse(Me._₏)
				If (num <= 0) Then
					SystemSounds.Beep.Play()
				Else
					num = Math.Sqrt(num)
					If (Me.IsIntValue) Then
						num = Math.Round(num)
					End If
					Me._₏ = num.ToString()
				End If
			ElseIf (Me._ᒃ <= 0) Then
				SystemSounds.Beep.Play()
			Else
				Me._ᒃ = Math.Sqrt(Me._ᒃ)
			End If
			Me._ₓ = Operators.[Set]
			Me.OnValueChanged()
			Return Me.BtnSqrt
		End Function

		Private Function ₮() As ButtonX
			If (String.IsNullOrEmpty(Me._₏)) Then
				Me._ᒃ = -Me._ᒃ
			Else
				Me._₏ = String.Concat("-"C, Me._₏)
				Me._₏ = Me._₏.Replace("--", "")
			End If
			Me.OnValueChanged()
			Return Me.BtnNegate
		End Function

		Private Sub ₢(ByVal u0656 As Object, ByVal u0657 As EventArgs)
			If (Not Me.⃊(u0656)) Then
				Me.₣(DirectCast(u0656, ButtonX))
			End If
			If (Me._ₙ) Then
				MyBase.Focus()
			End If
		End Sub

		Private Sub €(ByVal u0656 As Object, ByVal u0657 As EventArgs)
			If (Not Me.⃊(u0656)) Then
				Me.⃈(Me.BtnDivide, Operators.Divide)
			End If
			If (Me._ₙ) Then
				MyBase.Focus()
			End If
		End Sub

		Private Sub ₡()
			Me.₠.Text = Me.DisplaySValue
		End Sub

		Private Function ₲() As ButtonX
			If (Not String.IsNullOrEmpty(Me._₏)) Then
				Dim num As Double = Double.Parse(Me._₏)
				If (num = 0) Then
					SystemSounds.Beep.Play()
				Else
					num = 1 / num
					If (Me.IsIntValue) Then
						num = Math.Round(num)
					End If
					Me._₏ = num.ToString()
				End If
			ElseIf (Me._ᒃ = 0) Then
				SystemSounds.Beep.Play()
			Else
				Me._ᒃ = 1 / Me._ᒃ
			End If
			Me._ₓ = Operators.[Set]
			Me.OnValueChanged()
			Return Me.BtnReciprocal
		End Function

		Private Sub ₳(ByVal u0656 As Object, ByVal u0657 As EventArgs)
			If (Not Me.⃊(Me.BtnPercent)) Then
				Me.₴()
			End If
			If (Me._ₙ) Then
				MyBase.Focus()
			End If
		End Sub

		Private Function ₴() As ButtonX
			If (String.IsNullOrEmpty(Me._₏)) Then
				Me._ᒃ = 0
			Else
				Dim num As Double = Double.Parse(Me._₏)
				num = Me._ᒃ * num / 100
				If (Me.IsIntValue) Then
					num = Math.Round(num)
				End If
				Me._₏ = num.ToString()
			End If
			Me.OnValueChanged()
			Return Me.BtnPercent
		End Function

		Private Sub ₵(ByVal u0656 As Object, ByVal u0657 As EventArgs)
			If (Not Me.⃊(u0656)) Then
				Me.₶()
			End If
			If (Me._ₙ) Then
				MyBase.Focus()
			End If
		End Sub

		Private Sub ₪(ByVal u0656 As Object, ByVal u0657 As EventArgs)
			If (Not Me.⃊(u0656)) Then
				Me.⃈(Me.BtnSubtract, Operators.Subtract)
			End If
			If (Me._ₙ) Then
				MyBase.Focus()
			End If
		End Sub

		Private Sub ₫(ByVal u0656 As Object, ByVal u0657 As EventArgs)
			If (Not Me.⃊(u0656)) Then
				Me.⃈(Me.BtnMultiply, Operators.Multiply)
			End If
			If (Me._ₙ) Then
				MyBase.Focus()
			End If
		End Sub

		Private Sub ₹(ByVal u0656 As Object, ByVal u0657 As EventArgs)
			If (Not Me.⃊(u0656)) Then
				Me.₺()
			End If
			If (Me._ₙ) Then
				MyBase.Focus()
			End If
		End Sub

		Private Function ₺() As ButtonX
			Me._ᒃ = 0
			Me._ₐ = 0
			Me._₏ = Nothing
			Me.⃆(Operators.None)
			Me.OnValueChanged()
			Return Me.BtnClear
		End Function

		Friend Sub ᴥ(ByVal u0657 As KeyPressEventArgs)
			Dim buttonX As DevComponents.DotNetBar.ButtonX = Nothing
			Dim keyChar As Char = u0657.KeyChar
			If (keyChar > "@"C) Then
				Select Case keyChar
					Case "c"C
						buttonX = Me.₺()
						Exit Select
					Case "d"C
						Exit Select
					Case "e"C
						buttonX = Me.₸()
						Exit Select
					Case Else
						If (keyChar = "n"C) Then
							buttonX = Me.₮()
							Exit Select
						ElseIf (keyChar = "r"C) Then
							buttonX = Me.₲()
							Exit Select
						Else
							Exit Select
						End If
				End Select
			Else
				Select Case keyChar
					Case Strings.ChrW(8)
						buttonX = Me.₨()
						Exit Select
					Case Strings.ChrW(9)
					Case Strings.ChrW(10)
					Case Strings.ChrW(11)
					Case Strings.ChrW(14)
					Case Strings.ChrW(15)
						Exit Select
					Case Strings.ChrW(12)
						buttonX = Me.⃀()
						Exit Select
					Case Strings.ChrW(13)
						buttonX = If((Control.ModifierKeys And Keys.Control) = Keys.Control, Me.₼(), Me.₶())
						Exit Select
					Case Strings.ChrW(16)
						buttonX = Me.⃂()
						Exit Select
					Case Strings.ChrW(17)
						buttonX = Me.⃄()
						Exit Select
					Case Strings.ChrW(18)
						buttonX = Me.₾()
						Exit Select
					Case Else
						Select Case keyChar
							Case "%"C
								buttonX = Me.₴()

							Case "*"C
								buttonX = Me.⃈(Me.BtnMultiply, Operators.Multiply)

							Case "+"C
								buttonX = Me.⃈(Me.BtnAdd, Operators.Add)

							Case "-"C
								buttonX = Me.⃈(Me.BtnSubtract, Operators.Subtract)

							Case "/"C
								buttonX = Me.⃈(Me.BtnDivide, Operators.Divide)

							Case "0"C
								buttonX = Me.₣(Me.BtnDigit0)

							Case "1"C
								buttonX = Me.₣(Me.BtnDigit1)

							Case "2"C
								buttonX = Me.₣(Me.BtnDigit2)

							Case "3"C
								buttonX = Me.₣(Me.BtnDigit3)

							Case "4"C
								buttonX = Me.₣(Me.BtnDigit4)

							Case "5"C
								buttonX = Me.₣(Me.BtnDigit5)

							Case "6"C
								buttonX = Me.₣(Me.BtnDigit6)

							Case "7"C
								buttonX = Me.₣(Me.BtnDigit7)

							Case "8"C
								buttonX = Me.₣(Me.BtnDigit8)

							Case "9"C
								buttonX = Me.₣(Me.BtnDigit9)

							Case "="C
								buttonX = Me.₶()

							Case "@"C
								buttonX = Me.₰()

						End Select

				End Select
			End If
			If (buttonX Is Nothing AndAlso u0657.KeyChar.ToString() = Me._ₘ) Then
				buttonX = Me.₦()
			End If
			If (buttonX IsNot Nothing) Then
				Me.FlashKey(buttonX)
				u0657.Handled = True
			End If
		End Sub

		Protected Overrides Sub Dispose(ByVal disposing As Boolean)
			If (disposing AndAlso Me.Ո IsNot Nothing) Then
				Me.Ո.Dispose()
			End If
			MyBase.Dispose(disposing)
		End Sub

		Public Sub FlashKey(ByVal keyButton As ButtonX)
			If (Me._ₗ Is Nothing) Then
				Me._ₗ = New Timer() With
				{
					.Interval = 125
				}
				AddHandler Me._ₗ.Tick,  New EventHandler(AddressOf Me.⃍)
			End If
			Dim tag As ButtonX = TryCast(Me._ₗ.Tag, ButtonX)
			If (tag IsNot Nothing) Then
				Me._ₗ.[Stop]()
				Me.⃌(tag)
			End If
			Me.⃋(keyButton)
			Me._ₗ.Tag = keyButton
			Me._ₗ.Start()
		End Sub

		Public Overrides Function GetPreferredSize(ByVal proposedSize As System.Drawing.Size) As System.Drawing.Size
			If (Not Me._ያ.IsEmpty) Then
				Return Me._ያ
			End If
			If (Not BarFunctions.IsHandleValid(Me)) Then
				Return MyBase.GetPreferredSize(proposedSize)
			End If
			Me._ያ = Me.ₛ.Size
			Return Me._ያ
		End Function

		Public Sub InvalidateAutoSize()
			Me._ያ = System.Drawing.Size.Empty
			Me.ዹ()
		End Sub

		Protected Overridable Sub OnCalculatorDisplayChanged(ByVal e As EventArgs)
			Dim eventHandler As System.EventHandler = Me.₎
			If (eventHandler IsNot Nothing) Then
				eventHandler(Me, e)
			End If
		End Sub

		Protected Overridable Sub OnDisplayVisibleChanged(ByVal oldValue As Boolean, ByVal newValue As Boolean)
			Me.₠.Visible = newValue
			If (Not newValue) Then
				Dim top As DevComponents.DotNetBar.PanelEx = Me.₟
				top.Top = top.Top - Me.₠.Height
				Dim panelEx As DevComponents.DotNetBar.PanelEx = Me.₞
				panelEx.Top = panelEx.Top - Me.₠.Height
				Dim height As DevComponents.DotNetBar.PanelEx = Me.ₛ
				height.Height = height.Height - Me.₠.Height
			Else
				Dim top1 As DevComponents.DotNetBar.PanelEx = Me.₟
				top1.Top = top1.Top + Me.₠.Height
				Dim panelEx1 As DevComponents.DotNetBar.PanelEx = Me.₞
				panelEx1.Top = panelEx1.Top + Me.₠.Height
				Dim height1 As DevComponents.DotNetBar.PanelEx = Me.ₛ
				height1.Height = height1.Height + Me.₠.Height
			End If
			Me._ያ = System.Drawing.Size.Empty
			Me.ዹ()
		End Sub

		Protected Overridable Sub OnFocusButtonsOnMouseDownChanged(ByVal oldValue As Boolean, ByVal newValue As Boolean)
			Me.BtnBack.FocusOnLeftMouseButtonDown = newValue
			Me.BtnDigit8.FocusOnLeftMouseButtonDown = newValue
			Me.BtnReciprocal.FocusOnLeftMouseButtonDown = newValue
			Me.BtnDigit5.FocusOnLeftMouseButtonDown = newValue
			Me.BtnSqrt.FocusOnLeftMouseButtonDown = newValue
			Me.BtnDivide.FocusOnLeftMouseButtonDown = newValue
			Me.BtnDigit9.FocusOnLeftMouseButtonDown = newValue
			Me.BtnClearEntry.FocusOnLeftMouseButtonDown = newValue
			Me.BtnDecimal.FocusOnLeftMouseButtonDown = newValue
			Me.BtnAdd.FocusOnLeftMouseButtonDown = newValue
			Me.BtnEquals.FocusOnLeftMouseButtonDown = newValue
			Me.BtnDigit4.FocusOnLeftMouseButtonDown = newValue
			Me.BtnSubtract.FocusOnLeftMouseButtonDown = newValue
			Me.BtnMultiply.FocusOnLeftMouseButtonDown = newValue
			Me.BtnDigit6.FocusOnLeftMouseButtonDown = newValue
			Me.BtnClear.FocusOnLeftMouseButtonDown = newValue
			Me.BtnDigit1.FocusOnLeftMouseButtonDown = newValue
			Me.BtnDigit2.FocusOnLeftMouseButtonDown = newValue
			Me.BtnNegate.FocusOnLeftMouseButtonDown = newValue
			Me.BtnDigit0.FocusOnLeftMouseButtonDown = newValue
			Me.BtnDigit3.FocusOnLeftMouseButtonDown = newValue
			Me.BtnPercent.FocusOnLeftMouseButtonDown = newValue
			Me.BtnDigit7.FocusOnLeftMouseButtonDown = newValue
			Me.BtnMemStore.FocusOnLeftMouseButtonDown = newValue
			Me.BtnMemRestore.FocusOnLeftMouseButtonDown = newValue
			Me.BtnMemAdd.FocusOnLeftMouseButtonDown = newValue
			Me.BtnMemClear.FocusOnLeftMouseButtonDown = newValue
			Me.BtnMemSubtract.FocusOnLeftMouseButtonDown = newValue
		End Sub

		Protected Overrides Sub OnFontChanged(ByVal e As EventArgs)
			Me.InvalidateAutoSize()
			Me.BtnMemAdd.Font = New System.Drawing.Font(Me.Font.FontFamily, Me.BtnMemAdd.Font.SizeInPoints)
			Me.BtnMemClear.Font = New System.Drawing.Font(Me.Font.FontFamily, Me.BtnMemClear.Font.SizeInPoints)
			Me.BtnMemRestore.Font = New System.Drawing.Font(Me.Font.FontFamily, Me.BtnMemRestore.Font.SizeInPoints)
			Me.BtnMemStore.Font = New System.Drawing.Font(Me.Font.FontFamily, Me.BtnMemStore.Font.SizeInPoints)
			Me.BtnMemSubtract.Font = New System.Drawing.Font(Me.Font.FontFamily, Me.BtnMemSubtract.Font.SizeInPoints)
			Me.BtnDigit0.Font = New System.Drawing.Font(Me.Font.FontFamily, Me.BtnDigit0.Font.SizeInPoints)
			Me.BtnDigit1.Font = New System.Drawing.Font(Me.Font.FontFamily, Me.BtnDigit1.Font.SizeInPoints)
			Me.BtnDigit2.Font = New System.Drawing.Font(Me.Font.FontFamily, Me.BtnDigit2.Font.SizeInPoints)
			Me.BtnDigit3.Font = New System.Drawing.Font(Me.Font.FontFamily, Me.BtnDigit3.Font.SizeInPoints)
			Me.BtnDigit4.Font = New System.Drawing.Font(Me.Font.FontFamily, Me.BtnDigit4.Font.SizeInPoints)
			Me.BtnDigit5.Font = New System.Drawing.Font(Me.Font.FontFamily, Me.BtnDigit5.Font.SizeInPoints)
			Me.BtnDigit6.Font = New System.Drawing.Font(Me.Font.FontFamily, Me.BtnDigit6.Font.SizeInPoints)
			Me.BtnDigit7.Font = New System.Drawing.Font(Me.Font.FontFamily, Me.BtnDigit7.Font.SizeInPoints)
			Me.BtnDigit8.Font = New System.Drawing.Font(Me.Font.FontFamily, Me.BtnDigit8.Font.SizeInPoints)
			Me.BtnDigit9.Font = New System.Drawing.Font(Me.Font.FontFamily, Me.BtnDigit9.Font.SizeInPoints)
			Me.₠.Font = New System.Drawing.Font(Me.Font.FontFamily, Me.₠.Font.SizeInPoints)
			MyBase.OnFontChanged(e)
		End Sub

		Protected Overrides Sub OnHandleCreated(ByVal e As EventArgs)
			If (Me.AutoSize) Then
				Me.ዹ()
			End If
			MyBase.OnHandleCreated(e)
		End Sub

		Protected Overrides Sub OnKeyPress(ByVal e As KeyPressEventArgs)
			MyBase.OnKeyPress(e)
			If (Not e.Handled) Then
				Me.ᴥ(e)
			End If
		End Sub

		Protected Overrides Sub OnPaint(ByVal e As PaintEventArgs)
			Using solidBrush As System.Drawing.SolidBrush = New System.Drawing.SolidBrush(Me.BackColor)
				e.Graphics.FillRectangle(solidBrush, MyBase.ClientRectangle)
			End Using
			MyBase.OnPaint(e)
		End Sub

		Public Sub OnValueChanged()
			If (Me.ᑽ IsNot Nothing) Then
				Me.ᑽ(Me, New ValueChangedEventArgs(Me.DisplaySValue, Me.DisplayValue))
			End If
			Me.₡()
			Me.OnCalculatorDisplayChanged(EventArgs.Empty)
		End Sub

		Protected Overrides Sub OnVisibleChanged(ByVal e As EventArgs)
			If (MyBase.Visible) Then
				Me._₏ = Nothing
				Me._ₐ = 0
				Me.⃅(0)
				Me.⃆(Operators.[Set])
				Me.OnValueChanged()
			End If
			MyBase.OnVisibleChanged(e)
		End Sub

		Protected Overrides Sub SetBoundsCore(ByVal x As Integer, ByVal y As Integer, ByVal width As Integer, ByVal height As Integer, ByVal specified As BoundsSpecified)
			If (Me.AutoSize) Then
				Dim preferredSize As System.Drawing.Size = MyBase.PreferredSize
				width = preferredSize.Width
				height = preferredSize.Height
			End If
			MyBase.SetBoundsCore(x, y, width, height, specified)
		End Sub

		Private Sub ظ()
			Dim componentResourceManager As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Calculator))
			Me.ₛ = New PanelEx()
			Me.₟ = New PanelEx()
			Me.BtnBack = New ButtonX()
			Me.BtnDigit8 = New ButtonX()
			Me.BtnReciprocal = New ButtonX()
			Me.BtnDigit5 = New ButtonX()
			Me.BtnSqrt = New ButtonX()
			Me.BtnDivide = New ButtonX()
			Me.BtnDigit9 = New ButtonX()
			Me.BtnClearEntry = New ButtonX()
			Me.BtnDecimal = New ButtonX()
			Me.BtnAdd = New ButtonX()
			Me.BtnEquals = New ButtonX()
			Me.BtnDigit4 = New ButtonX()
			Me.BtnSubtract = New ButtonX()
			Me.BtnMultiply = New ButtonX()
			Me.BtnDigit6 = New ButtonX()
			Me.BtnClear = New ButtonX()
			Me.BtnDigit1 = New ButtonX()
			Me.BtnDigit2 = New ButtonX()
			Me.BtnNegate = New ButtonX()
			Me.BtnDigit0 = New ButtonX()
			Me.BtnDigit3 = New ButtonX()
			Me.BtnPercent = New ButtonX()
			Me.BtnDigit7 = New ButtonX()
			Me.₞ = New PanelEx()
			Me.BtnMemStore = New ButtonX()
			Me.₝ = New LabelX()
			Me.BtnMemRestore = New ButtonX()
			Me.BtnMemAdd = New ButtonX()
			Me.BtnMemClear = New ButtonX()
			Me.BtnMemSubtract = New ButtonX()
			Me.ₜ = New LabelX()
			Me.₠ = New LabelX()
			Me.ₛ.SuspendLayout()
			Me.₟.SuspendLayout()
			Me.₞.SuspendLayout()
			MyBase.SuspendLayout()
			Me.ₛ.CanvasColor = SystemColors.Control
			Me.ₛ.ColorSchemeStyle = eDotNetBarStyle.StyleManagerControlled
			Me.ₛ.Controls.Add(Me.₟)
			Me.ₛ.Controls.Add(Me.₞)
			Me.ₛ.Controls.Add(Me.₠)
			Me.ₛ.Location = New Point(0, 0)
			Me.ₛ.Name = "pnlCalc"
			Me.ₛ.Padding = New System.Windows.Forms.Padding(1)
			Me.ₛ.Size = New System.Drawing.Size(190, 211)
			Me.ₛ.Style.Alignment = StringAlignment.Center
			Me.ₛ.Style.BackColor1.ColorSchemePart = eColorSchemePart.BarBackground
			Me.ₛ.Style.Border = eBorderType.SingleLine
			Me.ₛ.Style.BorderColor.ColorSchemePart = eColorSchemePart.BarDockedBorder
			Me.ₛ.Style.ForeColor.ColorSchemePart = eColorSchemePart.ItemText
			Me.ₛ.Style.GradientAngle = 90
			Me.ₛ.TabIndex = 0
			Me.₟.CanvasColor = SystemColors.Control
			Me.₟.ColorSchemeStyle = eDotNetBarStyle.StyleManagerControlled
			Me.₟.Controls.Add(Me.BtnBack)
			Me.₟.Controls.Add(Me.BtnDigit8)
			Me.₟.Controls.Add(Me.BtnReciprocal)
			Me.₟.Controls.Add(Me.BtnDigit5)
			Me.₟.Controls.Add(Me.BtnSqrt)
			Me.₟.Controls.Add(Me.BtnDivide)
			Me.₟.Controls.Add(Me.BtnDigit9)
			Me.₟.Controls.Add(Me.BtnClearEntry)
			Me.₟.Controls.Add(Me.BtnDecimal)
			Me.₟.Controls.Add(Me.BtnAdd)
			Me.₟.Controls.Add(Me.BtnEquals)
			Me.₟.Controls.Add(Me.BtnDigit4)
			Me.₟.Controls.Add(Me.BtnSubtract)
			Me.₟.Controls.Add(Me.BtnMultiply)
			Me.₟.Controls.Add(Me.BtnDigit6)
			Me.₟.Controls.Add(Me.BtnClear)
			Me.₟.Controls.Add(Me.BtnDigit1)
			Me.₟.Controls.Add(Me.BtnDigit2)
			Me.₟.Controls.Add(Me.BtnNegate)
			Me.₟.Controls.Add(Me.BtnDigit0)
			Me.₟.Controls.Add(Me.BtnDigit3)
			Me.₟.Controls.Add(Me.BtnPercent)
			Me.₟.Controls.Add(Me.BtnDigit7)
			Me.₟.Location = New Point(1, 58)
			Me.₟.Name = "pnlPad"
			Me.₟.Size = New System.Drawing.Size(187, 151)
			Me.₟.Style.Alignment = StringAlignment.Center
			Me.₟.Style.BackColor1.ColorSchemePart = eColorSchemePart.BarBackground
			Me.₟.Style.BorderColor.ColorSchemePart = eColorSchemePart.BarDockedBorder
			Me.₟.Style.ForeColor.ColorSchemePart = eColorSchemePart.ItemText
			Me.₟.Style.GradientAngle = 90
			Me.₟.TabIndex = 35
			Me.BtnBack.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
			Me.BtnBack.ColorTable = eButtonColor.OrangeWithBackground
			Me.BtnBack.Image = DirectCast(componentResourceManager.GetObject("BtnBack.Image"), Image)
			Me.BtnBack.Location = New Point(8, 6)
			Me.BtnBack.Name = "BtnBack"
			Me.BtnBack.Size = New System.Drawing.Size(30, 23)
			Me.BtnBack.Style = eDotNetBarStyle.StyleManagerControlled
			Me.BtnBack.TabIndex = 0
			AddHandler Me.BtnBack.Click,  New EventHandler(AddressOf Me.₧)
			Me.BtnDigit8.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
			Me.BtnDigit8.ColorTable = eButtonColor.OrangeWithBackground
			Me.BtnDigit8.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, FontStyle.Regular, GraphicsUnit.Point, 0)
			Me.BtnDigit8.Location = New Point(43, 36)
			Me.BtnDigit8.Name = "BtnDigit8"
			Me.BtnDigit8.Size = New System.Drawing.Size(30, 23)
			Me.BtnDigit8.Style = eDotNetBarStyle.StyleManagerControlled
			Me.BtnDigit8.TabIndex = 6
			Me.BtnDigit8.Text = "8"
			AddHandler Me.BtnDigit8.Click,  New EventHandler(AddressOf Me.₢)
			Me.BtnReciprocal.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
			Me.BtnReciprocal.ColorTable = eButtonColor.OrangeWithBackground
			Me.BtnReciprocal.Location = New Point(150, 65)
			Me.BtnReciprocal.Name = "BtnReciprocal"
			Me.BtnReciprocal.Size = New System.Drawing.Size(30, 23)
			Me.BtnReciprocal.Style = eDotNetBarStyle.StyleManagerControlled
			Me.BtnReciprocal.TabIndex = 14
			Me.BtnReciprocal.Text = "1 / x"
			AddHandler Me.BtnReciprocal.Click,  New EventHandler(AddressOf Me.₱)
			Me.BtnDigit5.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
			Me.BtnDigit5.ColorTable = eButtonColor.OrangeWithBackground
			Me.BtnDigit5.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, FontStyle.Regular, GraphicsUnit.Point, 0)
			Me.BtnDigit5.Location = New Point(43, 65)
			Me.BtnDigit5.Name = "BtnDigit5"
			Me.BtnDigit5.Size = New System.Drawing.Size(30, 23)
			Me.BtnDigit5.Style = eDotNetBarStyle.StyleManagerControlled
			Me.BtnDigit5.TabIndex = 11
			Me.BtnDigit5.Text = "5"
			AddHandler Me.BtnDigit5.Click,  New EventHandler(AddressOf Me.₢)
			Me.BtnSqrt.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
			Me.BtnSqrt.ColorTable = eButtonColor.OrangeWithBackground
			Me.BtnSqrt.Location = New Point(150, 6)
			Me.BtnSqrt.Name = "BtnSqrt"
			Me.BtnSqrt.Size = New System.Drawing.Size(30, 23)
			Me.BtnSqrt.Style = eDotNetBarStyle.StyleManagerControlled
			Me.BtnSqrt.TabIndex = 4
			Me.BtnSqrt.Text = "√"
			AddHandler Me.BtnSqrt.Click,  New EventHandler(AddressOf Me.₯)
			Me.BtnDivide.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
			Me.BtnDivide.ColorTable = eButtonColor.OrangeWithBackground
			Me.BtnDivide.Location = New Point(114, 36)
			Me.BtnDivide.Name = "BtnDivide"
			Me.BtnDivide.Size = New System.Drawing.Size(30, 23)
			Me.BtnDivide.Style = eDotNetBarStyle.StyleManagerControlled
			Me.BtnDivide.TabIndex = 8
			Me.BtnDivide.Text = "/"
			AddHandler Me.BtnDivide.Click,  New EventHandler(AddressOf Me.€)
			Me.BtnDigit9.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
			Me.BtnDigit9.ColorTable = eButtonColor.OrangeWithBackground
			Me.BtnDigit9.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, FontStyle.Regular, GraphicsUnit.Point, 0)
			Me.BtnDigit9.Location = New Point(78, 36)
			Me.BtnDigit9.Name = "BtnDigit9"
			Me.BtnDigit9.Size = New System.Drawing.Size(30, 23)
			Me.BtnDigit9.Style = eDotNetBarStyle.StyleManagerControlled
			Me.BtnDigit9.TabIndex = 7
			Me.BtnDigit9.Text = "9"
			AddHandler Me.BtnDigit9.Click,  New EventHandler(AddressOf Me.₢)
			Me.BtnClearEntry.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
			Me.BtnClearEntry.ColorTable = eButtonColor.OrangeWithBackground
			Me.BtnClearEntry.Location = New Point(43, 6)
			Me.BtnClearEntry.Name = "BtnClearEntry"
			Me.BtnClearEntry.Size = New System.Drawing.Size(30, 23)
			Me.BtnClearEntry.Style = eDotNetBarStyle.StyleManagerControlled
			Me.BtnClearEntry.TabIndex = 1
			Me.BtnClearEntry.Text = "CE"
			AddHandler Me.BtnClearEntry.Click,  New EventHandler(AddressOf Me.₷)
			Me.BtnDecimal.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
			Me.BtnDecimal.ColorTable = eButtonColor.OrangeWithBackground
			Me.BtnDecimal.Font = New System.Drawing.Font("Modern No. 20", 12!, FontStyle.Regular, GraphicsUnit.Point, 0)
			Me.BtnDecimal.Location = New Point(78, 123)
			Me.BtnDecimal.Name = "BtnDecimal"
			Me.BtnDecimal.Size = New System.Drawing.Size(30, 23)
			Me.BtnDecimal.Style = eDotNetBarStyle.StyleManagerControlled
			Me.BtnDecimal.TabIndex = 21
			Me.BtnDecimal.Text = "."
			AddHandler Me.BtnDecimal.Click,  New EventHandler(AddressOf Me.₥)
			Me.BtnAdd.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
			Me.BtnAdd.ColorTable = eButtonColor.OrangeWithBackground
			Me.BtnAdd.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, FontStyle.Regular, GraphicsUnit.Point, 0)
			Me.BtnAdd.Location = New Point(114, 123)
			Me.BtnAdd.Name = "BtnAdd"
			Me.BtnAdd.Size = New System.Drawing.Size(30, 23)
			Me.BtnAdd.Style = eDotNetBarStyle.StyleManagerControlled
			Me.BtnAdd.TabIndex = 22
			Me.BtnAdd.Text = "+"
			AddHandler Me.BtnAdd.Click,  New EventHandler(AddressOf Me.₩)
			Me.BtnEquals.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
			Me.BtnEquals.ColorTable = eButtonColor.OrangeWithBackground
			Me.BtnEquals.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, FontStyle.Regular, GraphicsUnit.Point, 0)
			Me.BtnEquals.Location = New Point(150, 94)
			Me.BtnEquals.Name = "BtnEquals"
			Me.BtnEquals.Size = New System.Drawing.Size(30, 52)
			Me.BtnEquals.Style = eDotNetBarStyle.StyleManagerControlled
			Me.BtnEquals.TabIndex = 19
			Me.BtnEquals.Text = "="
			AddHandler Me.BtnEquals.Click,  New EventHandler(AddressOf Me.₵)
			Me.BtnDigit4.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
			Me.BtnDigit4.ColorTable = eButtonColor.OrangeWithBackground
			Me.BtnDigit4.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, FontStyle.Regular, GraphicsUnit.Point, 0)
			Me.BtnDigit4.Location = New Point(8, 65)
			Me.BtnDigit4.Name = "BtnDigit4"
			Me.BtnDigit4.Size = New System.Drawing.Size(30, 23)
			Me.BtnDigit4.Style = eDotNetBarStyle.StyleManagerControlled
			Me.BtnDigit4.TabIndex = 10
			Me.BtnDigit4.Text = "4"
			AddHandler Me.BtnDigit4.Click,  New EventHandler(AddressOf Me.₢)
			Me.BtnSubtract.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
			Me.BtnSubtract.ColorTable = eButtonColor.OrangeWithBackground
			Me.BtnSubtract.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, FontStyle.Regular, GraphicsUnit.Point, 0)
			Me.BtnSubtract.Location = New Point(114, 94)
			Me.BtnSubtract.Name = "BtnSubtract"
			Me.BtnSubtract.Size = New System.Drawing.Size(30, 23)
			Me.BtnSubtract.Style = eDotNetBarStyle.StyleManagerControlled
			Me.BtnSubtract.TabIndex = 18
			Me.BtnSubtract.Text = "-"
			AddHandler Me.BtnSubtract.Click,  New EventHandler(AddressOf Me.₪)
			Me.BtnMultiply.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
			Me.BtnMultiply.ColorTable = eButtonColor.OrangeWithBackground
			Me.BtnMultiply.Font = New System.Drawing.Font("Symbol", 11.25!, FontStyle.Regular, GraphicsUnit.Point, 2)
			Me.BtnMultiply.Location = New Point(114, 65)
			Me.BtnMultiply.Name = "BtnMultiply"
			Me.BtnMultiply.Size = New System.Drawing.Size(30, 23)
			Me.BtnMultiply.Style = eDotNetBarStyle.StyleManagerControlled
			Me.BtnMultiply.TabIndex = 13
			Me.BtnMultiply.Text = "*"
			AddHandler Me.BtnMultiply.Click,  New EventHandler(AddressOf Me.₫)
			Me.BtnDigit6.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
			Me.BtnDigit6.ColorTable = eButtonColor.OrangeWithBackground
			Me.BtnDigit6.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, FontStyle.Regular, GraphicsUnit.Point, 0)
			Me.BtnDigit6.Location = New Point(78, 65)
			Me.BtnDigit6.Name = "BtnDigit6"
			Me.BtnDigit6.Size = New System.Drawing.Size(30, 23)
			Me.BtnDigit6.Style = eDotNetBarStyle.StyleManagerControlled
			Me.BtnDigit6.TabIndex = 12
			Me.BtnDigit6.Text = "6"
			AddHandler Me.BtnDigit6.Click,  New EventHandler(AddressOf Me.₢)
			Me.BtnClear.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
			Me.BtnClear.ColorTable = eButtonColor.OrangeWithBackground
			Me.BtnClear.Location = New Point(79, 6)
			Me.BtnClear.Name = "BtnClear"
			Me.BtnClear.Size = New System.Drawing.Size(30, 23)
			Me.BtnClear.Style = eDotNetBarStyle.StyleManagerControlled
			Me.BtnClear.TabIndex = 2
			Me.BtnClear.Text = "C"
			AddHandler Me.BtnClear.Click,  New EventHandler(AddressOf Me.₹)
			Me.BtnDigit1.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
			Me.BtnDigit1.ColorTable = eButtonColor.OrangeWithBackground
			Me.BtnDigit1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, FontStyle.Regular, GraphicsUnit.Point, 0)
			Me.BtnDigit1.Location = New Point(8, 94)
			Me.BtnDigit1.Name = "BtnDigit1"
			Me.BtnDigit1.Size = New System.Drawing.Size(30, 23)
			Me.BtnDigit1.Style = eDotNetBarStyle.StyleManagerControlled
			Me.BtnDigit1.TabIndex = 15
			Me.BtnDigit1.Text = "1"
			AddHandler Me.BtnDigit1.Click,  New EventHandler(AddressOf Me.₢)
			Me.BtnDigit2.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
			Me.BtnDigit2.ColorTable = eButtonColor.OrangeWithBackground
			Me.BtnDigit2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, FontStyle.Regular, GraphicsUnit.Point, 0)
			Me.BtnDigit2.Location = New Point(43, 94)
			Me.BtnDigit2.Name = "BtnDigit2"
			Me.BtnDigit2.Size = New System.Drawing.Size(30, 23)
			Me.BtnDigit2.Style = eDotNetBarStyle.StyleManagerControlled
			Me.BtnDigit2.TabIndex = 16
			Me.BtnDigit2.Text = "2"
			AddHandler Me.BtnDigit2.Click,  New EventHandler(AddressOf Me.₢)
			Me.BtnNegate.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
			Me.BtnNegate.ColorTable = eButtonColor.OrangeWithBackground
			Me.BtnNegate.Location = New Point(114, 6)
			Me.BtnNegate.Name = "BtnNegate"
			Me.BtnNegate.Size = New System.Drawing.Size(30, 23)
			Me.BtnNegate.Style = eDotNetBarStyle.StyleManagerControlled
			Me.BtnNegate.TabIndex = 3
			Me.BtnNegate.Text = "±"
			AddHandler Me.BtnNegate.Click,  New EventHandler(AddressOf Me.₭)
			Me.BtnDigit0.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
			Me.BtnDigit0.ColorTable = eButtonColor.OrangeWithBackground
			Me.BtnDigit0.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, FontStyle.Regular, GraphicsUnit.Point, 0)
			Me.BtnDigit0.Location = New Point(8, 123)
			Me.BtnDigit0.Name = "BtnDigit0"
			Me.BtnDigit0.Size = New System.Drawing.Size(65, 23)
			Me.BtnDigit0.Style = eDotNetBarStyle.StyleManagerControlled
			Me.BtnDigit0.TabIndex = 20
			Me.BtnDigit0.Text = "0"
			AddHandler Me.BtnDigit0.Click,  New EventHandler(AddressOf Me.₢)
			Me.BtnDigit3.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
			Me.BtnDigit3.ColorTable = eButtonColor.OrangeWithBackground
			Me.BtnDigit3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, FontStyle.Regular, GraphicsUnit.Point, 0)
			Me.BtnDigit3.Location = New Point(78, 94)
			Me.BtnDigit3.Name = "BtnDigit3"
			Me.BtnDigit3.Size = New System.Drawing.Size(30, 23)
			Me.BtnDigit3.Style = eDotNetBarStyle.StyleManagerControlled
			Me.BtnDigit3.TabIndex = 17
			Me.BtnDigit3.Text = "3"
			AddHandler Me.BtnDigit3.Click,  New EventHandler(AddressOf Me.₢)
			Me.BtnPercent.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
			Me.BtnPercent.ColorTable = eButtonColor.OrangeWithBackground
			Me.BtnPercent.Location = New Point(150, 36)
			Me.BtnPercent.Name = "BtnPercent"
			Me.BtnPercent.Size = New System.Drawing.Size(30, 23)
			Me.BtnPercent.Style = eDotNetBarStyle.StyleManagerControlled
			Me.BtnPercent.TabIndex = 9
			Me.BtnPercent.Text = "%"
			AddHandler Me.BtnPercent.Click,  New EventHandler(AddressOf Me.₳)
			Me.BtnDigit7.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
			Me.BtnDigit7.ColorTable = eButtonColor.OrangeWithBackground
			Me.BtnDigit7.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, FontStyle.Regular, GraphicsUnit.Point, 0)
			Me.BtnDigit7.Location = New Point(8, 36)
			Me.BtnDigit7.Name = "BtnDigit7"
			Me.BtnDigit7.Size = New System.Drawing.Size(30, 23)
			Me.BtnDigit7.Style = eDotNetBarStyle.StyleManagerControlled
			Me.BtnDigit7.TabIndex = 5
			Me.BtnDigit7.Text = "7"
			AddHandler Me.BtnDigit7.Click,  New EventHandler(AddressOf Me.₢)
			Me.₞.CanvasColor = SystemColors.Control
			Me.₞.ColorSchemeStyle = eDotNetBarStyle.StyleManagerControlled
			Me.₞.Controls.Add(Me.BtnMemStore)
			Me.₞.Controls.Add(Me.₝)
			Me.₞.Controls.Add(Me.BtnMemRestore)
			Me.₞.Controls.Add(Me.BtnMemAdd)
			Me.₞.Controls.Add(Me.BtnMemClear)
			Me.₞.Controls.Add(Me.BtnMemSubtract)
			Me.₞.Controls.Add(Me.ₜ)
			Me.₞.Location = New Point(1, 33)
			Me.₞.Name = "pnlMem"
			Me.₞.Size = New System.Drawing.Size(187, 26)
			Me.₞.Style.Alignment = StringAlignment.Center
			Me.₞.Style.BackColor1.ColorSchemePart = eColorSchemePart.BarBackground
			Me.₞.Style.BorderColor.ColorSchemePart = eColorSchemePart.BarDockedBorder
			Me.₞.Style.BorderWidth = 0
			Me.₞.Style.ForeColor.ColorSchemePart = eColorSchemePart.ItemText
			Me.₞.Style.GradientAngle = 90
			Me.₞.TabIndex = 34
			Me.BtnMemStore.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
			Me.BtnMemStore.ColorTable = eButtonColor.OrangeWithBackground
			Me.BtnMemStore.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, FontStyle.Regular, GraphicsUnit.Point, 0)
			Me.BtnMemStore.Location = New Point(80, 6)
			Me.BtnMemStore.Name = "BtnMemStore"
			Me.BtnMemStore.Size = New System.Drawing.Size(27, 18)
			Me.BtnMemStore.TabIndex = 0
			Me.BtnMemStore.Text = "MS"
			AddHandler Me.BtnMemStore.Click,  New EventHandler(AddressOf Me.₻)
			Me.₝.BackgroundStyle.CornerType = eCornerType.Square
			Me.₝.Font = New System.Drawing.Font("Symbol", 9.75!, FontStyle.Regular, GraphicsUnit.Point, 2)
			Me.₝.Location = New Point(174, 6)
			Me.₝.Name = "lbxOperator"
			Me.₝.Size = New System.Drawing.Size(12, 18)
			Me.₝.TabIndex = 33
			Me.₝.TextAlignment = StringAlignment.Center
			Me.BtnMemRestore.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
			Me.BtnMemRestore.ColorTable = eButtonColor.OrangeWithBackground
			Me.BtnMemRestore.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, FontStyle.Regular, GraphicsUnit.Point, 0)
			Me.BtnMemRestore.Location = New Point(48, 6)
			Me.BtnMemRestore.Name = "BtnMemRestore"
			Me.BtnMemRestore.Size = New System.Drawing.Size(27, 18)
			Me.BtnMemRestore.TabIndex = 6
			Me.BtnMemRestore.Text = "MR"
			AddHandler Me.BtnMemRestore.Click,  New EventHandler(AddressOf Me.₽)
			Me.BtnMemAdd.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
			Me.BtnMemAdd.ColorTable = eButtonColor.OrangeWithBackground
			Me.BtnMemAdd.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, FontStyle.Regular, GraphicsUnit.Point, 0)
			Me.BtnMemAdd.Location = New Point(112, 6)
			Me.BtnMemAdd.Name = "BtnMemAdd"
			Me.BtnMemAdd.Size = New System.Drawing.Size(27, 18)
			Me.BtnMemAdd.TabIndex = 1
			Me.BtnMemAdd.Text = "M+"
			AddHandler Me.BtnMemAdd.Click,  New EventHandler(AddressOf Me.⃁)
			Me.BtnMemClear.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
			Me.BtnMemClear.ColorTable = eButtonColor.OrangeWithBackground
			Me.BtnMemClear.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, FontStyle.Regular, GraphicsUnit.Point, 0)
			Me.BtnMemClear.Location = New Point(16, 6)
			Me.BtnMemClear.Name = "BtnMemClear"
			Me.BtnMemClear.Size = New System.Drawing.Size(27, 18)
			Me.BtnMemClear.TabIndex = 5
			Me.BtnMemClear.Text = "MC"
			AddHandler Me.BtnMemClear.Click,  New EventHandler(AddressOf Me.₿)
			Me.BtnMemSubtract.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
			Me.BtnMemSubtract.ColorTable = eButtonColor.OrangeWithBackground
			Me.BtnMemSubtract.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, FontStyle.Regular, GraphicsUnit.Point, 0)
			Me.BtnMemSubtract.Location = New Point(144, 6)
			Me.BtnMemSubtract.Name = "BtnMemSubtract"
			Me.BtnMemSubtract.Size = New System.Drawing.Size(27, 18)
			Me.BtnMemSubtract.TabIndex = 2
			Me.BtnMemSubtract.Text = "M-"
			AddHandler Me.BtnMemSubtract.Click,  New EventHandler(AddressOf Me.⃃)
			Me.ₜ.BackgroundStyle.CornerType = eCornerType.Square
			Me.ₜ.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, FontStyle.Regular, GraphicsUnit.Point, 0)
			Me.ₜ.Location = New Point(4, 5)
			Me.ₜ.Name = "lbxMemory"
			Me.ₜ.Size = New System.Drawing.Size(12, 18)
			Me.ₜ.TabIndex = 31
			Me.ₜ.TextAlignment = StringAlignment.Center
			Me.₠.BackgroundStyle.BackColor2SchemePart = eColorSchemePart.MenuBackground
			Me.₠.BackgroundStyle.BorderBottom = eStyleBorderType.Solid
			Me.₠.BackgroundStyle.BorderBottomColorSchemePart = eColorSchemePart.PanelBorder
			Me.₠.BackgroundStyle.BorderBottomWidth = 1
			Me.₠.BackgroundStyle.CornerType = eCornerType.Square
			Me.₠.BackgroundStyle.PaddingRight = 6
			Me.₠.Dock = DockStyle.Top
			Me.₠.Font = New System.Drawing.Font("Consolas", 18!, FontStyle.Regular, GraphicsUnit.Point, 0)
			Me.₠.Location = New Point(1, 1)
			Me.₠.Name = "labelValue"
			Me.₠.Size = New System.Drawing.Size(188, 30)
			Me.₠.TabIndex = 1
			Me.₠.Text = "0"
			Me.₠.TextAlignment = StringAlignment.Far
			Me.₠.BackgroundStyle.TextTrimming = eStyleTextTrimming.Character
			MyBase.Controls.Add(Me.ₛ)
			MyBase.Name = "Calculator"
			MyBase.Size = New System.Drawing.Size(190, 212)
			Me.ₛ.ResumeLayout(False)
			Me.₟.ResumeLayout(False)
			Me.₞.ResumeLayout(False)
			MyBase.ResumeLayout(False)
		End Sub

		Private Sub ᒰ()
			Dim isIntValue As Boolean
			Select Case Me._ₖ
				Case DevComponents.Editors.DecimalKeyVisibility.Auto
					isIntValue = Not Me.IsIntValue
					Exit Select
				Case DevComponents.Editors.DecimalKeyVisibility.Always
					isIntValue = True
					Exit Select
				Case Else
					isIntValue = False
					Exit Select
			End Select
			If (Not isIntValue) Then
				Me.BtnDecimal.Visible = False
				Me.BtnDigit0.Width = Me.BtnDecimal.Bounds.Right - Me.BtnDigit0.Bounds.X
			Else
				Me.BtnDecimal.Visible = True
				Me.BtnDigit0.Width = Me.BtnDigit2.Bounds.Right - Me.BtnDigit0.Bounds.X
			End If
			Me.InvalidateAutoSize()
		End Sub

		Private Sub ዹ()
			If (Me.AutoSize) Then
				MyBase.Size = MyBase.PreferredSize
			End If
		End Sub

		<Description("Occurs when a calc button has been clicked.")>
		Public Custom Event ButtonClick As EventHandler(Of ButtonClickEventArgs)
			<MethodImpl(MethodImplOptions.Synchronized)>
			AddHandler(ByVal value As EventHandler(Of ButtonClickEventArgs))
				AddHandler Me.₍,  value
			End AddHandler
			<MethodImpl(MethodImplOptions.Synchronized)>
			RemoveHandler(ByVal value As EventHandler(Of ButtonClickEventArgs))
				RemoveHandler Me.₍,  value
			End RemoveHandler
		End Event

		Public Custom Event CalculatorDisplayChanged As EventHandler
			<MethodImpl(MethodImplOptions.Synchronized)>
			AddHandler(ByVal value As EventHandler)
				AddHandler Me.₎,  value
			End AddHandler
			<MethodImpl(MethodImplOptions.Synchronized)>
			RemoveHandler(ByVal value As EventHandler)
				RemoveHandler Me.₎,  value
			End RemoveHandler
		End Event

		<Description("Occurs when the calculator value has changed.")>
		Public Custom Event ValueChanged As EventHandler(Of ValueChangedEventArgs)
			<MethodImpl(MethodImplOptions.Synchronized)>
			AddHandler(ByVal value As EventHandler(Of ValueChangedEventArgs))
				AddHandler Me.ᑽ,  value
			End AddHandler
			<MethodImpl(MethodImplOptions.Synchronized)>
			RemoveHandler(ByVal value As EventHandler(Of ValueChangedEventArgs))
				RemoveHandler Me.ᑽ,  value
			End RemoveHandler
		End Event
	End Class
End Namespace