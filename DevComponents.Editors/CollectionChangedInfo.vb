Imports System
Imports System.Runtime.InteropServices

Namespace DevComponents.Editors
	<ComVisible(True)>
	Public Class CollectionChangedInfo
		Private _ᴶ As VisualItem()

		Private _ᴷ As VisualItem()

		Private _ᴸ As eCollectionChangeType

		Public ReadOnly Property Added As VisualItem()
			Get
				Return Me._ᴶ
			End Get
		End Property

		Public ReadOnly Property ChangeType As eCollectionChangeType
			Get
				Return Me._ᴸ
			End Get
		End Property

		Public ReadOnly Property Removed As VisualItem()
			Get
				Return Me._ᴷ
			End Get
		End Property

		Public Sub New(ByVal added As VisualItem(), ByVal removed As VisualItem(), ByVal changeType As eCollectionChangeType)
			MyBase.New()
			Me._ᴶ = added
			Me._ᴷ = removed
			Me._ᴸ = changeType
		End Sub
	End Class
End Namespace