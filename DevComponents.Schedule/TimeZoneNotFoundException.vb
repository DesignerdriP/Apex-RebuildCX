Imports System
Imports System.Runtime.InteropServices
Imports System.Runtime.Serialization
Imports System.Security.Permissions

Namespace DevComponents.Schedule
	<ComVisible(True)>
	<HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort:=True)>
	<Serializable>
	Public Class TimeZoneNotFoundException
		Inherits Exception
		Public Sub New()
			MyBase.New()
		End Sub

		Public Sub New(ByVal message As String)
			MyBase.New(message)
		End Sub

		Protected Sub New(ByVal info As SerializationInfo, ByVal context As StreamingContext)
			MyBase.New(info, context)
		End Sub

		Public Sub New(ByVal message As String, ByVal innerException As Exception)
			MyBase.New(message, innerException)
		End Sub
	End Class
End Namespace