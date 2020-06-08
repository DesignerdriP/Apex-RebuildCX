Imports System
Imports System.ComponentModel
Imports System.Runtime.CompilerServices
Imports System.Xml
Imports System.Xml.Serialization

Namespace CxUpdater
	<Serializable>
	<XmlType("Version")>
	Public Class VersionXML
		<Browsable(False)>
		<EditorBrowsable(EditorBrowsableState.Never)>
		<XmlText>
		Public Property Value As String
			Get
				If (Me.Version Is Nothing) Then
					Return String.Empty
				End If
				Return Me.Version.ToString()
			End Get
			Set(ByVal value As String)
				Dim version As System.Version
				System.Version.TryParse(value, version)
				Me.Version = version
			End Set
		End Property

		<XmlIgnore>
		Public Property Version As System.Version

		Public Sub New()
			MyBase.New()
			Me.Version = Nothing
		End Sub

		Public Sub New(ByVal version As System.Version)
			MyBase.New()
			Me.Version = version
		End Sub

		Public Shared Function FromFile(ByVal filename As String) As CxUpdater.VersionXML
			Dim versionXML As CxUpdater.VersionXML
			Dim xmlSerializer As System.Xml.Serialization.XmlSerializer = New System.Xml.Serialization.XmlSerializer(GetType(CxUpdater.VersionXML))
			Using xmlReader As System.Xml.XmlReader = System.Xml.XmlReader.Create(filename)
				versionXML = DirectCast(xmlSerializer.Deserialize(xmlReader), CxUpdater.VersionXML)
			End Using
			Return versionXML
		End Function

		Public Shared Widening Operator CType(ByVal versionXml As CxUpdater.VersionXML) As System.Version
			Return versionXml.Version
		End Operator

		Public Shared Widening Operator CType(ByVal version As System.Version) As VersionXML
			Return New VersionXML(version)
		End Operator

		Public Overrides Function ToString() As String
			Return Me.Value
		End Function
	End Class
End Namespace