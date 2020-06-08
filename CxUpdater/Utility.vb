Imports System
Imports System.Diagnostics
Imports System.IO
Imports System.Net
Imports System.Runtime.CompilerServices
Imports System.Runtime.InteropServices
Imports System.Threading.Tasks
Imports System.Xml.Serialization

Namespace CxUpdater
	Public Module Utility
		Public Function DownloadFile(ByVal url As Uri, ByVal outputDirectory As String) As Boolean
			Dim flag As Boolean
			Dim webClient As System.Net.WebClient = New System.Net.WebClient()
			Try
				webClient.DownloadFile(url, outputDirectory)
				Return True
			Catch argumentNullException As System.ArgumentNullException
				flag = False
			Catch notSupportedException As System.NotSupportedException
				flag = False
			Catch webException As System.Net.WebException
				flag = False
			End Try
			Return flag
		End Function

		Public Async Function DownloadFileAsync(ByVal url As Uri, ByVal outputDirectory As String) As Task(Of Boolean)
			Dim variable As Utility.<DownloadFileAsync>d__2 = New Utility.<DownloadFileAsync>d__2()
			variable.url = url
			variable.outputDirectory = outputDirectory
			variable.<>t__builder = AsyncTaskMethodBuilder(Of Boolean).Create()
			variable.<>1__state = -1
			variable.<>t__builder.Start(Of Utility.<DownloadFileAsync>d__2)(variable)
			Return variable.<>t__builder.Task
		End Function

		Public Function DownloadXML(Of T)(ByVal url As Uri, <Out> ByRef result As T) As Boolean
			Dim flag As Boolean
			Dim webClient As System.Net.WebClient = New System.Net.WebClient()
			result = Nothing
			Try
				Dim str As String = webClient.DownloadString(url)
				If (String.IsNullOrEmpty(str)) Then
					Return False
				End If
				Dim xmlSerializer As System.Xml.Serialization.XmlSerializer = New System.Xml.Serialization.XmlSerializer(GetType(T))
				result = DirectCast(xmlSerializer.Deserialize(New StringReader(str)), T)
				Return True
			Catch argumentNullException As System.ArgumentNullException
				flag = False
			Catch notSupportedException As System.NotSupportedException
				flag = False
			Catch webException As System.Net.WebException
				flag = False
			End Try
			Return flag
		End Function
	End Module
End Namespace