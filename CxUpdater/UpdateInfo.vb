Imports System
Imports System.Runtime.CompilerServices
Imports System.Xml.Serialization

Namespace CxUpdater
	<Serializable>
	Public Class UpdateInfo
		<XmlElement>
		Public Property Description As String

		<XmlElement>
		Public Property Title As String

		<XmlElement(Type:=GetType(VersionXML))>
		Public Property Version As System.Version

		Public Sub New()
			MyBase.New()
		End Sub
	End Class
End Namespace