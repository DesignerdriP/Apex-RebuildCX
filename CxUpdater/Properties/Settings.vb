Imports System
Imports System.CodeDom.Compiler
Imports System.Configuration
Imports System.Diagnostics
Imports System.Runtime.CompilerServices

Namespace CxUpdater.Properties
	<CompilerGenerated>
	<GeneratedCode("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "16.3.0.0")>
	Friend NotInheritable Class Settings
		Inherits ApplicationSettingsBase
		Private Shared defaultInstance As Settings

		Public ReadOnly Shared Property [Default] As Settings
			Get
				Return Settings.defaultInstance
			End Get
		End Property

		<DebuggerNonUserCode>
		<DefaultSettingValue("")>
		<UserScopedSetting>
		Public Property InstallFolder As String
			Get
				Return CStr(Me("InstallFolder"))
			End Get
			Set(ByVal value As String)
				Me("InstallFolder") = value
			End Set
		End Property

		<DebuggerNonUserCode>
		<DefaultSettingValue("True")>
		<UserScopedSetting>
		Public Property UpgradeRequired As Boolean
			Get
				Return CBool(Me("UpgradeRequired"))
			End Get
			Set(ByVal value As Boolean)
				Me("UpgradeRequired") = value
			End Set
		End Property

		Shared Sub New()
			Settings.defaultInstance = DirectCast(SettingsBase.Synchronized(New Settings()), Settings)
		End Sub

		Public Sub New()
			MyBase.New()
		End Sub
	End Class
End Namespace