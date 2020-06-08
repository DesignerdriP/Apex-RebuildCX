Imports DevComponents.DotNetBar
Imports System
Imports System.Collections
Imports System.ComponentModel
Imports System.Drawing
Imports System.Reflection
Imports System.Windows.Forms
Imports System.Windows.Forms.Design

Namespace DevComponents.UI
	<ToolboxItem(False)>
	Friend Class ӓ
		Inherits UserControl
		Private ❃ As Color() = New Color(47) {}

		Private ❄ As Rectangle() = New Rectangle(47) {}

		Private ժ As Object

		Private ❅ As String = ""

		Private ❆ As System.Windows.Forms.TabControl

		Private ✍ As TabPage

		Private ✋ As TabPage

		Private ✌ As TabPage

		Private ❇ As TabPage

		Private ṋ As Label

		Private ❈ As ListBox

		Private ❉ As Button

		Private ❊ As Button

		Private ❋ As Panel

		Private ❌ As ListBox

		Private ❍ As ListBox

		Private Ể As Color = Color.Empty

		Private ❎ As String = ""

		Private ❏ As Panel

		Private ❐ As TrackBar

		Private ❑ As Boolean

		Private ⚑ As IWindowsFormsEditorService

		Private Ո As System.ComponentModel.Container

		<Browsable(False)>
		<DefaultValue(Nothing)>
		Public Property Ԫ As Object
			Get
				Return Me.ժ
			End Get
			Set(ByVal value As Object)
				Me.ժ = value
				Me.❔()
			End Set
		End Property

		Public Property ≨ As Color
			Get
				Return Me.Ể
			End Get
			Set(ByVal value As Color)
				Me.Ể = value
				Me.❙()
			End Set
		End Property

		Friend Property ❀ As IWindowsFormsEditorService
			Get
				Return Me.⚑
			End Get
			Set(ByVal value As IWindowsFormsEditorService)
				Me.⚑ = value
			End Set
		End Property

		Public Property ❁ As String
			Get
				Return Me.❎
			End Get
			Set(ByVal value As String)
				Me.❎ = value
			End Set
		End Property

		Public ReadOnly Property ❂ As Boolean
			Get
				Return Me.❑
			End Get
		End Property

		Public Sub New()
			MyBase.New()
			Me.ظ()
			Me.❒()
			Me.❓()
			Me.❅ = Me.ṋ.Text
		End Sub

		Private Sub ❒()
			Me.❃(0) = Color.FromArgb(255, 255, 255)
			Me.❃(1) = Color.FromArgb(255, 195, 198)
			Me.❃(2) = Color.FromArgb(255, 227, 198)
			Me.❃(3) = Color.FromArgb(255, 255, 198)
			Me.❃(4) = Color.FromArgb(198, 255, 198)
			Me.❃(5) = Color.FromArgb(198, 255, 255)
			Me.❃(6) = Color.FromArgb(198, 195, 255)
			Me.❃(7) = Color.FromArgb(255, 195, 255)
			Me.❃(8) = Color.FromArgb(231, 227, 231)
			Me.❃(9) = Color.FromArgb(255, 130, 132)
			Me.❃(10) = Color.FromArgb(255, 195, 132)
			Me.❃(11) = Color.FromArgb(255, 255, 132)
			Me.❃(12) = Color.FromArgb(132, 255, 132)
			Me.❃(13) = Color.FromArgb(132, 255, 255)
			Me.❃(14) = Color.FromArgb(132, 130, 255)
			Me.❃(15) = Color.FromArgb(255, 130, 255)
			Me.❃(16) = Color.FromArgb(198, 195, 198)
			Me.❃(17) = Color.FromArgb(255, 0, 0)
			Me.❃(18) = Color.FromArgb(255, 130, 0)
			Me.❃(19) = Color.FromArgb(255, 255, 0)
			Me.❃(20) = Color.FromArgb(0, 255, 0)
			Me.❃(21) = Color.FromArgb(0, 255, 255)
			Me.❃(22) = Color.FromArgb(0, 0, 255)
			Me.❃(23) = Color.FromArgb(255, 0, 255)
			Me.❃(24) = Color.FromArgb(132, 130, 132)
			Me.❃(25) = Color.FromArgb(198, 0, 0)
			Me.❃(26) = Color.FromArgb(198, 65, 0)
			Me.❃(27) = Color.FromArgb(198, 195, 0)
			Me.❃(28) = Color.FromArgb(0, 195, 0)
			Me.❃(29) = Color.FromArgb(0, 195, 198)
			Me.❃(30) = Color.FromArgb(0, 0, 198)
			Me.❃(31) = Color.FromArgb(198, 0, 198)
			Me.❃(32) = Color.FromArgb(66, 65, 66)
			Me.❃(33) = Color.FromArgb(132, 0, 0)
			Me.❃(34) = Color.FromArgb(132, 65, 0)
			Me.❃(35) = Color.FromArgb(132, 130, 0)
			Me.❃(36) = Color.FromArgb(0, 130, 0)
			Me.❃(37) = Color.FromArgb(0, 130, 132)
			Me.❃(38) = Color.FromArgb(0, 0, 132)
			Me.❃(39) = Color.FromArgb(132, 0, 132)
			Me.❃(40) = Color.FromArgb(0, 0, 0)
			Me.❃(41) = Color.FromArgb(66, 0, 0)
			Me.❃(42) = Color.FromArgb(132, 65, 66)
			Me.❃(43) = Color.FromArgb(66, 65, 0)
			Me.❃(44) = Color.FromArgb(0, 65, 0)
			Me.❃(45) = Color.FromArgb(0, 65, 66)
			Me.❃(46) = Color.FromArgb(0, 0, 66)
			Me.❃(47) = Color.FromArgb(66, 0, 66)
		End Sub

		Private Sub ❓()
			Me.❍.BeginUpdate()
			Me.❍.Items.Clear()
			Dim properties As System.Reflection.PropertyInfo() = GetType(System.Drawing.Color).GetProperties(BindingFlags.[Static] Or BindingFlags.[Public])
			Dim color As System.Drawing.Color = New System.Drawing.Color()
			Dim propertyInfoArray As System.Reflection.PropertyInfo() = properties
			Dim num As Integer = 0
			Do
				Dim propertyInfo As System.Reflection.PropertyInfo = propertyInfoArray(num)
				Me.❍.Items.Add(propertyInfo.GetValue(color, Nothing))
				num = num + 1
			Loop While num < CInt(propertyInfoArray.Length)
			Me.❍.EndUpdate()
			Me.❌.BeginUpdate()
			Me.❌.Items.Clear()
			Dim properties1 As System.Reflection.PropertyInfo() = GetType(SystemColors).GetProperties(BindingFlags.[Static] Or BindingFlags.[Public])
			Dim num1 As Integer = 0
			Do
				Dim propertyInfo1 As System.Reflection.PropertyInfo = properties1(num1)
				Me.❌.Items.Add(propertyInfo1.GetValue(color, Nothing))
				num1 = num1 + 1
			Loop While num1 < CInt(properties1.Length)
			Me.❌.EndUpdate()
		End Sub

		Private Sub ❔()
			If (Me.ժ Is Nothing) Then
				If (Me.❆.TabPages.Contains(Me.✍)) Then
					Me.❆.TabPages.Remove(Me.✍)
				End If
				Return
			End If
			If (Not Me.❆.TabPages.Contains(Me.✍)) Then
				Me.❆.TabPages.Add(Me.✍)
			End If
			Me.❈.BeginUpdate()
			Me.❈.Items.Clear()
			Dim properties As System.Reflection.PropertyInfo() = Me.ժ.[GetType]().GetProperties()
			Dim num As Integer = 0
			Do
				Dim propertyInfo As System.Reflection.PropertyInfo = properties(num)
				If (CObj(propertyInfo.PropertyType) = CObj(GetType(Color))) Then
					Me.❈.Items.Add(propertyInfo.Name)
				End If
				num = num + 1
			Loop While num < CInt(properties.Length)
			Me.❈.EndUpdate()
		End Sub

		Private Sub ❕(ByVal u0656 As Object, ByVal u0657 As PaintEventArgs)
			Dim empty As Rectangle = Rectangle.Empty
			Dim num As Integer = 6
			Dim num1 As Integer = 12
			Dim graphics As System.Drawing.Graphics = u0657.Graphics
			Dim border3DSide As System.Windows.Forms.Border3DSide = System.Windows.Forms.Border3DSide.Left Or System.Windows.Forms.Border3DSide.Top Or System.Windows.Forms.Border3DSide.Right Or System.Windows.Forms.Border3DSide.Bottom
			Dim width As Integer = Me.❋.ClientRectangle.Width
			Dim num2 As Integer = 0
			Dim colorArray As System.Drawing.Color() = Me.❃
			For i As Integer = 0 To CInt(colorArray.Length)
				Dim color As System.Drawing.Color = colorArray(i)
				empty = New Rectangle(num, num1, 21, 21)
				If (empty.Right > width) Then
					num1 += 25
					num = 6
					empty.X = num
					empty.Y = num1
				End If
				ControlPaint.DrawBorder3D(graphics, num, num1, 21, 21, Border3DStyle.Sunken, border3DSide)
				empty.Inflate(-2, -2)
				graphics.FillRectangle(New SolidBrush(color), empty)
				Me.❄(num2) = empty
				num2 = num2 + 1
				num += 24
			Next

		End Sub

		Private Sub ❖(ByVal u0656 As Object, ByVal u0657 As DrawItemEventArgs)
			Dim bounds As System.Drawing.Rectangle = u0657.Bounds
			Dim rectangle As System.Drawing.Rectangle = New System.Drawing.Rectangle(bounds.X + 1, bounds.Y + 2, 24, bounds.Height - 4)
			Dim listBox As System.Windows.Forms.ListBox = TryCast(u0656, System.Windows.Forms.ListBox)
			Dim controlText As Color = SystemColors.ControlText
			If ((u0657.State And DrawItemState.Selected) = DrawItemState.None) Then
				u0657.Graphics.FillRectangle(SystemBrushes.Window, u0657.Bounds)
			Else
				controlText = SystemColors.HighlightText
				u0657.Graphics.FillRectangle(SystemBrushes.Highlight, u0657.Bounds)
			End If
			Dim empty As Color = Color.Empty
			Dim str As String = ""
			If (CObj(listBox.Items(u0657.Index).[GetType]()) <> CObj(GetType(Color))) Then
				str = listBox.Items(u0657.Index).ToString()
				empty = DirectCast(Me.ժ.[GetType]().GetProperty(str).GetValue(Me.ժ, Nothing), Color)
			Else
				empty = DirectCast(listBox.Items(u0657.Index), Color)
				str = empty.Name
			End If
			u0657.Graphics.FillRectangle(New SolidBrush(empty), rectangle)
			u0657.Graphics.DrawRectangle(SystemPens.ControlText, rectangle)
			bounds.Offset(30, 0)
			bounds.Width = bounds.Width - 30
			TextDrawing.DrawString(u0657.Graphics, str, listBox.Font, controlText, bounds, eTextFormat.[Default])
		End Sub

		Private Sub ❗(ByVal u0656 As Object, ByVal u0657 As MouseEventArgs)
			For i As Integer = 0 To 48
				If (Me.❄(i).Contains(u0657.X, u0657.Y)) Then
					Me.≨ = Me.❃(i)
					Me.❎ = ""
					Return
				End If
			Next

		End Sub

		Private Sub ❘(ByVal u0656 As Object, ByVal u0657 As EventArgs)
			Dim listBox As System.Windows.Forms.ListBox = TryCast(u0656, System.Windows.Forms.ListBox)
			If (listBox.SelectedItem Is Nothing) Then
				Me.≨ = Color.Empty
				Me.❎ = ""
				Return
			End If
			If (TypeOf listBox.SelectedItem Is Color) Then
				Me.≨ = DirectCast(listBox.SelectedItem, Color)
				Me.❎ = ""
				Return
			End If
			Me.❎ = listBox.SelectedItem.ToString()
			Me.≨ = DirectCast(Me.ժ.[GetType]().GetProperty(Me.❁).GetValue(Me.ժ, Nothing), Color)
		End Sub

		Private Sub ❙()
			Me.❏.BackColor = Me.Ể
			Me.❐.Value = Me.Ể.A
			Me.❝()
		End Sub

		Public Sub ❚()
			Me.❌.SelectedIndex = -1
			Me.❍.SelectedIndex = -1
			Me.❈.SelectedIndex = -1
			If (Me.Ể.IsSystemColor) Then
				Me.❆.SelectedTab = Me.✋
				Me.❛(Me.❌, Me.Ể.Name)
				Return
			End If
			If (Me.Ể.IsNamedColor) Then
				Me.❆.SelectedTab = Me.✌
				Me.❛(Me.❍, Me.Ể.Name)
				Return
			End If
			If (Me.❎ = "") Then
				Me.❆.SelectedTab = Me.❇
				Return
			End If
			Me.❆.SelectedTab = Me.✍
			Me.❛(Me.❈, Me.❎)
		End Sub

		Private Sub ❛(ByVal u275c As ListBox, ByVal u06de As String)
			For Each item As Object In u275c.Items
				If (item.ToString() <> u06de) Then
					Continue For
				End If
				u275c.SelectedItem = item
				Return
			Next
		End Sub

		Private Sub ❝()
			If (Me.≨.IsEmpty) Then
				Me.ṋ.Text = Me.❅
				Return
			End If
			Dim label As System.Windows.Forms.Label = Me.ṋ
			Dim str As String = Me.❅
			Dim a As Byte = Me.≨.A
			label.Text = String.Concat(str, " (", a.ToString(), ")")
		End Sub

		Private Sub ❞(ByVal u0656 As Object, ByVal u0657 As EventArgs)
			If (Not Me.≨.IsEmpty AndAlso Me.≨.A <> Me.❐.Value) Then
				Me.≨ = Color.FromArgb(Me.❐.Value, Me.≨)
				Me.❎ = ""
			End If
			Me.❝()
		End Sub

		Private Sub ❟(ByVal u0656 As Object, ByVal u0657 As PaintEventArgs)
			If (Me.≨.IsEmpty) Then
				Dim clientRectangle As Rectangle = Me.❏.ClientRectangle
				clientRectangle.Inflate(-2, -2)
				u0657.Graphics.DrawLine(SystemPens.ControlText, clientRectangle.X, clientRectangle.Y, clientRectangle.Right, clientRectangle.Bottom)
				u0657.Graphics.DrawLine(SystemPens.ControlText, clientRectangle.Right, clientRectangle.Y, clientRectangle.X, clientRectangle.Bottom)
			End If
		End Sub

		Private Sub ❠(ByVal u0656 As Object, ByVal u0657 As EventArgs)
			Me.❢()
		End Sub

		Private Sub ❡(ByVal u0656 As Object, ByVal u0657 As EventArgs)
			Me.❑ = True
			Me.❢()
		End Sub

		Private Sub ❢()
			If (Me.⚑ IsNot Nothing) Then
				Me.⚑.CloseDropDown()
				Return
			End If
			If (MyBase.Parent Is Nothing) Then
				MyBase.Hide()
				Return
			End If
			MyBase.Parent.Hide()
		End Sub

		Private Sub ❣(ByVal u0656 As Object, ByVal u0657 As PaintEventArgs)
			Dim clientRectangle As Rectangle = MyBase.ClientRectangle
			clientRectangle.Width = clientRectangle.Width - 1
			clientRectangle.Height = clientRectangle.Height - 1
			u0657.Graphics.DrawRectangle(SystemPens.ControlDarkDark, clientRectangle)
		End Sub

		Private Sub ❤(ByVal u0656 As Object, ByVal u0657 As EventArgs)
			If (Me.❆.SelectedIndex = 0 AndAlso Me.ժ IsNot Nothing AndAlso Me.❎ <> "") Then
				Me.❐.Enabled = False
				Return
			End If
			Me.❐.Enabled = True
		End Sub

		Protected Overrides Sub Dispose(ByVal disposing As Boolean)
			If (disposing AndAlso Me.Ո IsNot Nothing) Then
				Me.Ո.Dispose()
			End If
			MyBase.Dispose(disposing)
		End Sub

		Private Sub ظ()
			Me.❆ = New System.Windows.Forms.TabControl()
			Me.✍ = New TabPage()
			Me.❈ = New ListBox()
			Me.✋ = New TabPage()
			Me.❌ = New ListBox()
			Me.✌ = New TabPage()
			Me.❍ = New ListBox()
			Me.❇ = New TabPage()
			Me.❋ = New Panel()
			Me.❐ = New TrackBar()
			Me.ṋ = New Label()
			Me.❏ = New Panel()
			Me.❉ = New Button()
			Me.❊ = New Button()
			Me.❆.SuspendLayout()
			Me.✍.SuspendLayout()
			Me.✋.SuspendLayout()
			Me.✌.SuspendLayout()
			Me.❇.SuspendLayout()
			DirectCast(Me.❐, ISupportInitialize).BeginInit()
			MyBase.SuspendLayout()
			Me.❆.Controls.Add(Me.✍)
			Me.❆.Controls.Add(Me.✋)
			Me.❆.Controls.Add(Me.✌)
			Me.❆.Controls.Add(Me.❇)
			Me.❆.Location = New Point(1, 1)
			Me.❆.Name = "tabControl1"
			Me.❆.SelectedIndex = 0
			Me.❆.Size = New System.Drawing.Size(208, 192)
			Me.❆.TabIndex = 0
			AddHandler Me.❆.SelectedIndexChanged,  New EventHandler(AddressOf Me.❤)
			Me.✍.Controls.Add(Me.❈)
			Me.✍.Location = New Point(4, 22)
			Me.✍.Name = "tabPage1"
			Me.✍.Size = New System.Drawing.Size(200, 166)
			Me.✍.TabIndex = 0
			Me.✍.Text = "Scheme"
			Me.❈.Dock = DockStyle.Fill
			Me.❈.DrawMode = DrawMode.OwnerDrawFixed
			Me.❈.IntegralHeight = False
			Me.❈.Location = New Point(0, 0)
			Me.❈.Name = "listScheme"
			Me.❈.Size = New System.Drawing.Size(200, 166)
			Me.❈.TabIndex = 0
			AddHandler Me.❈.DrawItem,  New DrawItemEventHandler(AddressOf Me.❖)
			AddHandler Me.❈.SelectedIndexChanged,  New EventHandler(AddressOf Me.❘)
			Me.✋.Controls.Add(Me.❌)
			Me.✋.Location = New Point(4, 22)
			Me.✋.Name = "tabPage2"
			Me.✋.Size = New System.Drawing.Size(200, 166)
			Me.✋.TabIndex = 1
			Me.✋.Text = "System"
			Me.❌.Dock = DockStyle.Fill
			Me.❌.DrawMode = DrawMode.OwnerDrawFixed
			Me.❌.IntegralHeight = False
			Me.❌.Location = New Point(0, 0)
			Me.❌.Name = "listSystem"
			Me.❌.Size = New System.Drawing.Size(200, 166)
			Me.❌.TabIndex = 1
			AddHandler Me.❌.DrawItem,  New DrawItemEventHandler(AddressOf Me.❖)
			AddHandler Me.❌.SelectedIndexChanged,  New EventHandler(AddressOf Me.❘)
			Me.✌.Controls.Add(Me.❍)
			Me.✌.Location = New Point(4, 22)
			Me.✌.Name = "tabPage3"
			Me.✌.Size = New System.Drawing.Size(200, 166)
			Me.✌.TabIndex = 2
			Me.✌.Text = "Web"
			Me.❍.Dock = DockStyle.Fill
			Me.❍.DrawMode = DrawMode.OwnerDrawFixed
			Me.❍.IntegralHeight = False
			Me.❍.Location = New Point(0, 0)
			Me.❍.Name = "listWeb"
			Me.❍.Size = New System.Drawing.Size(200, 166)
			Me.❍.TabIndex = 1
			AddHandler Me.❍.DrawItem,  New DrawItemEventHandler(AddressOf Me.❖)
			AddHandler Me.❍.SelectedIndexChanged,  New EventHandler(AddressOf Me.❘)
			Me.❇.Controls.Add(Me.❋)
			Me.❇.Location = New Point(4, 22)
			Me.❇.Name = "tabPage4"
			Me.❇.Size = New System.Drawing.Size(200, 166)
			Me.❇.TabIndex = 3
			Me.❇.Text = "Custom"
			Me.❋.Dock = DockStyle.Fill
			Me.❋.Location = New Point(0, 0)
			Me.❋.Name = "colorPanel"
			Me.❋.Size = New System.Drawing.Size(200, 166)
			Me.❋.TabIndex = 0
			AddHandler Me.❋.MouseUp,  New MouseEventHandler(AddressOf Me.❗)
			AddHandler Me.❋.Paint,  New PaintEventHandler(AddressOf Me.❕)
			Me.❐.Enabled = False
			Me.❐.Location = New Point(1, 204)
			Me.❐.Maximum = 255
			Me.❐.Name = "transparencyTrack"
			Me.❐.Size = New System.Drawing.Size(200, 45)
			Me.❐.TabIndex = 1
			Me.❐.TickFrequency = 16
			Me.❐.Value = 255
			AddHandler Me.❐.ValueChanged,  New EventHandler(AddressOf Me.❞)
			Me.ṋ.Location = New Point(1, 194)
			Me.ṋ.Name = "label1"
			Me.ṋ.Size = New System.Drawing.Size(136, 16)
			Me.ṋ.TabIndex = 2
			Me.ṋ.Text = "Transparency"
			Me.❏.BackColor = Color.White
			Me.❏.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
			Me.❏.Location = New Point(8, 240)
			Me.❏.Name = "colorPreview"
			Me.❏.Size = New System.Drawing.Size(40, 32)
			Me.❏.TabIndex = 3
			AddHandler Me.❏.Paint,  New PaintEventHandler(AddressOf Me.❟)
			Me.❉.FlatStyle = FlatStyle.System
			Me.❉.Location = New Point(72, 248)
			Me.❉.Name = "btnOK"
			Me.❉.Size = New System.Drawing.Size(64, 24)
			Me.❉.TabIndex = 4
			Me.❉.Text = "OK"
			AddHandler Me.❉.Click,  New EventHandler(AddressOf Me.❠)
			Me.❊.FlatStyle = FlatStyle.System
			Me.❊.Location = New Point(142, 248)
			Me.❊.Name = "btnCancel"
			Me.❊.Size = New System.Drawing.Size(64, 24)
			Me.❊.TabIndex = 5
			Me.❊.Text = "Cancel"
			AddHandler Me.❊.Click,  New EventHandler(AddressOf Me.❡)
			MyBase.Controls.Add(Me.❊)
			MyBase.Controls.Add(Me.❉)
			MyBase.Controls.Add(Me.❏)
			MyBase.Controls.Add(Me.ṋ)
			MyBase.Controls.Add(Me.❆)
			MyBase.Controls.Add(Me.❐)
			MyBase.DockPadding.All = 1
			MyBase.Name = "ColorPicker"
			MyBase.Size = New System.Drawing.Size(211, 280)
			AddHandler MyBase.Paint,  New PaintEventHandler(AddressOf Me.❣)
			Me.❆.ResumeLayout(False)
			Me.✍.ResumeLayout(False)
			Me.✋.ResumeLayout(False)
			Me.✌.ResumeLayout(False)
			Me.❇.ResumeLayout(False)
			DirectCast(Me.❐, ISupportInitialize).EndInit()
			MyBase.ResumeLayout(False)
		End Sub
	End Class
End Namespace