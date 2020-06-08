Imports System

Namespace ProcessPrivileges
	Public Structure PrivilegeAndAttributes
		Implements IEquatable(Of PrivilegeAndAttributes)
		Private ReadOnly privilege As ProcessPrivileges.Privilege

		Private ReadOnly privilegeAttributes As ProcessPrivileges.PrivilegeAttributes

		Public ReadOnly Property Privilege As ProcessPrivileges.Privilege
			Get
				Return Me.privilege
			End Get
		End Property

		Public ReadOnly Property PrivilegeAttributes As ProcessPrivileges.PrivilegeAttributes
			Get
				Return Me.privilegeAttributes
			End Get
		End Property

		Public ReadOnly Property PrivilegeState As ProcessPrivileges.PrivilegeState
			Get
				Return ProcessExtensions.GetPrivilegeState(Me.privilegeAttributes)
			End Get
		End Property

		Friend Sub New(ByVal privilege As ProcessPrivileges.Privilege, ByVal privilegeAttributes As ProcessPrivileges.PrivilegeAttributes)
			Me.privilege = privilege
			Me.privilegeAttributes = privilegeAttributes
		End Sub

		Public Overrides Function Equals(ByVal obj As Object) As Boolean Implements IEquatable(Of PrivilegeAndAttributes).Equals
			If (TryCast(obj, ProcessPrivileges.PrivilegeAttributes) = ProcessPrivileges.PrivilegeAttributes.Disabled) Then
				Return False
			End If
			Return Me.Equals(DirectCast(obj, ProcessPrivileges.PrivilegeAttributes))
		End Function

		Public Function Equals(ByVal other As PrivilegeAndAttributes) As Boolean Implements IEquatable(Of PrivilegeAndAttributes).Equals
			If (Me.privilege <> other.Privilege) Then
				Return False
			End If
			Return Me.privilegeAttributes = other.PrivilegeAttributes
		End Function

		Public Overrides Function GetHashCode() As Integer
			Return Me.privilege.GetHashCode() Xor Me.privilegeAttributes.GetHashCode()
		End Function

		Public Shared Operator =(ByVal first As PrivilegeAndAttributes, ByVal second As PrivilegeAndAttributes) As Boolean
			Return first.Equals(second)
		End Operator

		Public Shared Operator <>(ByVal first As PrivilegeAndAttributes, ByVal second As PrivilegeAndAttributes) As Boolean
			Return Not first.Equals(second)
		End Operator
	End Structure
End Namespace