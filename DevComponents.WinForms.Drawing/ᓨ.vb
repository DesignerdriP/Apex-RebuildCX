Imports System
Imports System.ComponentModel
Imports System.Drawing

Namespace DevComponents.WinForms.Drawing
	Friend MustInherit Class ᓨ
		Private _┟ As ᓨ

		Private _┠ As Boolean

		<DefaultValue(Nothing)>
		Public Property ┝ As ᓨ
			Get
				Return Me._┟
			End Get
			Set(ByVal value As ᓨ)
				Me._┟ = value
			End Set
		End Property

		<DefaultValue(False)>
		Public Property ┞ As Boolean
			Get
				Return Me._┠
			End Get
			Set(ByVal value As Boolean)
				Me._┠ = value
			End Set
		End Property

		Protected Sub New()
			MyBase.New()
		End Sub

		Public MustOverride Sub Paint(ByVal g As Graphics, ByVal bounds As Rectangle)
	End Class
End Namespace