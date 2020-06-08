Imports System
Imports System.CodeDom.Compiler
Imports System.ComponentModel
Imports System.Diagnostics
Imports System.Globalization
Imports System.Resources
Imports System.Runtime.CompilerServices

Namespace CxUpdater.Properties
	<CompilerGenerated>
	<DebuggerNonUserCode>
	<GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")>
	Friend Class strings
		Private Shared resourceMan As System.Resources.ResourceManager

		Private Shared resourceCulture As CultureInfo

		<EditorBrowsable(EditorBrowsableState.Advanced)>
		Friend Shared Property Culture As CultureInfo
			Get
				Return strings.resourceCulture
			End Get
			Set(ByVal value As CultureInfo)
				strings.resourceCulture = value
			End Set
		End Property

		Friend ReadOnly Shared Property DL_STATUS_DOWNLOADING As String
			Get
				Return strings.ResourceManager.GetString("DL_STATUS_DOWNLOADING", strings.resourceCulture)
			End Get
		End Property

		Friend ReadOnly Shared Property DL_STATUS_EXTRACTING As String
			Get
				Return strings.ResourceManager.GetString("DL_STATUS_EXTRACTING", strings.resourceCulture)
			End Get
		End Property

		Friend ReadOnly Shared Property DL_STATUS_IDLE As String
			Get
				Return strings.ResourceManager.GetString("DL_STATUS_IDLE", strings.resourceCulture)
			End Get
		End Property

		Friend ReadOnly Shared Property DL_STATUS_LAUNCHING As String
			Get
				Return strings.ResourceManager.GetString("DL_STATUS_LAUNCHING", strings.resourceCulture)
			End Get
		End Property

		Friend ReadOnly Shared Property DL_STATUS_RUNNING As String
			Get
				Return strings.ResourceManager.GetString("DL_STATUS_RUNNING", strings.resourceCulture)
			End Get
		End Property

		Friend ReadOnly Shared Property NOTIFY_ERROR_OCCURED_BODY As String
			Get
				Return strings.ResourceManager.GetString("NOTIFY_ERROR_OCCURED_BODY", strings.resourceCulture)
			End Get
		End Property

		Friend ReadOnly Shared Property NOTIFY_FAILED_TO_LAUNCH_APP_BODY As String
			Get
				Return strings.ResourceManager.GetString("NOTIFY_FAILED_TO_LAUNCH_APP_BODY", strings.resourceCulture)
			End Get
		End Property

		Friend ReadOnly Shared Property NOTIFY_FAILED_TO_RUN_BODY As String
			Get
				Return strings.ResourceManager.GetString("NOTIFY_FAILED_TO_RUN_BODY", strings.resourceCulture)
			End Get
		End Property

		Friend ReadOnly Shared Property NOTIFY_FAILED_TO_RUN_TITLE As String
			Get
				Return strings.ResourceManager.GetString("NOTIFY_FAILED_TO_RUN_TITLE", strings.resourceCulture)
			End Get
		End Property

		Friend ReadOnly Shared Property NOTIFY_NEWEST_VERSION_ALREADY As String
			Get
				Return strings.ResourceManager.GetString("NOTIFY_NEWEST_VERSION_ALREADY", strings.resourceCulture)
			End Get
		End Property

		Friend ReadOnly Shared Property NOTIFY_RUN_APP_NOW_BODY As String
			Get
				Return strings.ResourceManager.GetString("NOTIFY_RUN_APP_NOW_BODY", strings.resourceCulture)
			End Get
		End Property

		Friend ReadOnly Shared Property NOTIFY_RUN_APP_NOW_TITLE As String
			Get
				Return strings.ResourceManager.GetString("NOTIFY_RUN_APP_NOW_TITLE", strings.resourceCulture)
			End Get
		End Property

		Friend ReadOnly Shared Property PROMPT_PORTABLE_VERSION_BODY As String
			Get
				Return strings.ResourceManager.GetString("PROMPT_PORTABLE_VERSION_BODY", strings.resourceCulture)
			End Get
		End Property

		Friend ReadOnly Shared Property PROMPT_PORTABLE_VERSION_TITLE As String
			Get
				Return strings.ResourceManager.GetString("PROMPT_PORTABLE_VERSION_TITLE", strings.resourceCulture)
			End Get
		End Property

		<EditorBrowsable(EditorBrowsableState.Advanced)>
		Friend ReadOnly Shared Property ResourceManager As System.Resources.ResourceManager
			Get
				If (strings.resourceMan Is Nothing) Then
					strings.resourceMan = New System.Resources.ResourceManager("CxUpdater.Properties.strings", GetType(strings).Assembly)
				End If
				Return strings.resourceMan
			End Get
		End Property

		Friend Sub New()
			MyBase.New()
		End Sub
	End Class
End Namespace