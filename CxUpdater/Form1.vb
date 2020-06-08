Imports CxUpdater.Properties
Imports DevComponents.DotNetBar
Imports DevComponents.DotNetBar.Controls
Imports IWshRuntimeLibrary
Imports System
Imports System.Collections
Imports System.ComponentModel
Imports System.Configuration
Imports System.Diagnostics
Imports System.Drawing
Imports System.IO
Imports System.IO.Compression
Imports System.Net
Imports System.Resources
Imports System.Runtime.InteropServices
Imports System.Security.AccessControl
Imports System.Security.Principal
Imports System.Windows.Forms

Namespace CxUpdater
	Public Class Form1
		Inherits Form
		Private bDownloadPortableVersion As Boolean = True

		Private components As IContainer

		Private labelX1 As LabelX

		Private progressBarX1 As ProgressBarX

		Public Sub New()
			MyBase.New()
			Me.InitializeComponent()
			Me.UpdateApplicationSettings()
		End Sub

		Public Shared Sub AddDirectorySecurity(ByVal FileName As String, ByVal Account As SecurityIdentifier, ByVal Rights As FileSystemRights, ByVal ControlType As AccessControlType, Optional ByVal inherit As Boolean = True)
			Dim directoryInfo As System.IO.DirectoryInfo = New System.IO.DirectoryInfo(FileName)
			Dim accessControl As DirectorySecurity = directoryInfo.GetAccessControl()
			Dim fileSystemAccessRule As System.Security.AccessControl.FileSystemAccessRule = Nothing
			fileSystemAccessRule = If(Not inherit, New System.Security.AccessControl.FileSystemAccessRule(Account, Rights, ControlType), New System.Security.AccessControl.FileSystemAccessRule(Account, Rights, InheritanceFlags.ContainerInherit Or InheritanceFlags.ObjectInherit, PropagationFlags.None, ControlType))
			accessControl.AddAccessRule(fileSystemAccessRule)
			directoryInfo.SetAccessControl(accessControl)
		End Sub

		Public Shared Sub AddFileSecurity(ByVal FileName As String, ByVal Account As String, ByVal Rights As FileSystemRights, ByVal ControlType As AccessControlType)
			Dim fileInfo As System.IO.FileInfo = New System.IO.FileInfo(FileName)
			Dim accessControl As FileSecurity = fileInfo.GetAccessControl()
			accessControl.AddAccessRule(New FileSystemAccessRule(Account, Rights, ControlType))
			fileInfo.SetAccessControl(accessControl)
		End Sub

		Private Sub CreateShortcut(ByVal shortcutPath As String, ByVal iconLocation As String, ByVal sourcePath As String, ByVal runAsAdmin As Boolean, ByVal ParamArray args As String())
			Dim variable As IWshShortcut = TryCast(DirectCast(Activator.CreateInstance(Marshal.GetTypeFromCLSID(New Guid("F935DC22-1CF0-11D0-ADB9-00C04FD58A0B"))), IWshShell_Class).CreateShortcut(shortcutPath), IWshShortcut)
			variable.TargetPath = sourcePath
			variable.IconLocation = iconLocation
			variable.Arguments = String.Concat("""", String.Join(""" """, args), """")
			variable.Save()
			If (runAsAdmin) Then
				Using fileStream As System.IO.FileStream = New System.IO.FileStream(shortcutPath, FileMode.Open, FileAccess.ReadWrite)
					fileStream.Seek(CLng(21), SeekOrigin.Begin)
					fileStream.WriteByte(34)
				End Using
			End If
		End Sub

		Protected Overrides Sub Dispose(ByVal disposing As Boolean)
			If (disposing AndAlso Me.components IsNot Nothing) Then
				Me.components.Dispose()
			End If
			MyBase.Dispose(disposing)
		End Sub

		Private Sub InitializeComponent()
			Dim componentResourceManager As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
			Me.labelX1 = New LabelX()
			Me.progressBarX1 = New ProgressBarX()
			MyBase.SuspendLayout()
			Me.labelX1.BackgroundStyle.CornerType = eCornerType.Square
			Me.labelX1.Location = New Point(13, 12)
			Me.labelX1.Name = "labelX1"
			Me.labelX1.Size = New System.Drawing.Size(107, 23)
			Me.labelX1.TabIndex = 2
			Me.labelX1.Text = "labelX1"
			Me.progressBarX1.BackgroundStyle.CornerType = eCornerType.Square
			Me.progressBarX1.Enabled = False
			Me.progressBarX1.Location = New Point(15, 119)
			Me.progressBarX1.Name = "progressBarX1"
			Me.progressBarX1.Size = New System.Drawing.Size(374, 23)
			Me.progressBarX1.TabIndex = 3
			Me.progressBarX1.Text = "progressBarX1"
			MyBase.AutoScaleDimensions = New SizeF(6!, 13!)
			MyBase.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
			MyBase.ClientSize = New System.Drawing.Size(404, 164)
			MyBase.Controls.Add(Me.progressBarX1)
			MyBase.Controls.Add(Me.labelX1)
			MyBase.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
			MyBase.Icon = DirectCast(componentResourceManager.GetObject("$this.Icon"), System.Drawing.Icon)
			MyBase.Name = "Form1"
			MyBase.StartPosition = FormStartPosition.CenterScreen
			Me.Text = "App Updater"
			MyBase.ResumeLayout(False)
		End Sub

		Private Sub InstallLoader(Optional ByVal bGetPortable As Boolean = False)
			Dim str As String
			Me.RemoveOldFiles()
			Dim uri As System.Uri = If(bGetPortable, New System.Uri("http://camx.me/cxloader/cxloader-portable.zip"), New System.Uri("http://camx.me/cxloader/cxloader.zip"))
			str = If(String.IsNullOrEmpty(Settings.[Default].InstallFolder), Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), Path.GetRandomFileName().Replace(".", "").Substring(0, 6)), Settings.[Default].InstallFolder)
			If (Directory.Exists(str)) Then
				Dim files As FileInfo() = (New DirectoryInfo(str)).GetFiles("*.*", SearchOption.AllDirectories)
				For i As Integer = 0 To CInt(files.Length)
					files(i).Delete()
				Next

			Else
				Directory.CreateDirectory(str)
			End If
			Settings.[Default].InstallFolder = str
			Settings.[Default].Save()
			Dim str1 As String = String.Concat(str, "\loader.zip")
			Dim webClient As System.Net.WebClient = New System.Net.WebClient()
			AddHandler webClient.DownloadProgressChanged,  New DownloadProgressChangedEventHandler(AddressOf Me.OnDownloadProgressChanged)
			AddHandler webClient.DownloadFileCompleted,  New AsyncCompletedEventHandler(AddressOf Me.OnDownloadComplete)
			webClient.DownloadFileAsync(uri, str1, str1)
		End Sub

		Private Sub OnDownloadComplete(ByVal sender As Object, ByVal e As AsyncCompletedEventArgs)
			Dim num As Integer
			Dim webClient As System.Net.WebClient = DirectCast(sender, System.Net.WebClient)
			RemoveHandler webClient.DownloadProgressChanged,  New DownloadProgressChangedEventHandler(AddressOf Me.OnDownloadProgressChanged)
			RemoveHandler webClient.DownloadFileCompleted,  New AsyncCompletedEventHandler(AddressOf Me.OnDownloadComplete)
			If (e.[Error] IsNot Nothing) Then
				MessageBox.Show(String.Concat(strings.NOTIFY_ERROR_OCCURED_BODY, " (", e.[Error].Message, ")."))
				Environment.[Exit](1)
			End If
			If (e.Cancelled) Then
				Environment.[Exit](1)
			End If
			Me.SetDownloadStatusString(2)
			Dim userState As String = CStr(e.UserState)
			Dim fileInfo As System.IO.FileInfo = New System.IO.FileInfo(userState)
			Dim str As String = Path.GetRandomFileName().Replace("."C, Strings.ChrW(0))
			Dim str1 As String = str.Substring(0, Math.Min(4, str.Length))
			ZipFile.ExtractToDirectory(userState, fileInfo.DirectoryName)
			Dim files As String() = Directory.GetFiles(fileInfo.DirectoryName, "*.*", SearchOption.AllDirectories)
			Dim num1 As Integer = 0
			Do
				Dim str2 As String = files(num1)
				If (str2.Contains("loader")) Then
					File.Move(str2, str2.Replace("loader", str1))
				End If
				num1 = num1 + 1
			Loop While num1 < CInt(files.Length)
			Dim directoryInfo As System.IO.DirectoryInfo = New System.IO.DirectoryInfo(fileInfo.DirectoryName)
			Dim accessControl As DirectorySecurity = directoryInfo.GetAccessControl()
			Dim accessRules As AuthorizationRuleCollection = accessControl.GetAccessRules(True, True, GetType(NTAccount))
			accessControl.SetAccessRuleProtection(True, False)
			directoryInfo.SetAccessControl(accessControl)
			For Each accessRule As FileSystemAccessRule In accessRules
				accessControl.RemoveAccessRuleSpecific(accessRule)
			Next
			Dim securityIdentifier As System.Security.Principal.SecurityIdentifier = New System.Security.Principal.SecurityIdentifier(WellKnownSidType.BuiltinAdministratorsSid, Nothing)
			Form1.AddDirectorySecurity(directoryInfo.FullName, securityIdentifier, FileSystemRights.FullControl, AccessControlType.Allow, True)
			securityIdentifier = New System.Security.Principal.SecurityIdentifier(WellKnownSidType.LocalSystemSid, Nothing)
			Form1.AddDirectorySecurity(directoryInfo.FullName, securityIdentifier, FileSystemRights.FullControl, AccessControlType.Allow, True)
			Dim str3 As String = String.Concat(New String() { "/c cd """, fileInfo.DirectoryName, """ & start """" """, fileInfo.DirectoryName, "\", str1, ".exe""" })
			Me.CreateShortcut(String.Concat(KnownFolders.GetPath(KnownFolder.Desktop), "\Run CxLoader.lnk"), String.Concat(fileInfo.DirectoryName, "\cx_icon.ico"), "%windir%\system32\cmd.exe", True, New String() { str3 })
			If (Not Me.bDownloadPortableVersion) Then
				fileInfo = New System.IO.FileInfo(String.Concat(fileInfo.DirectoryName, "\CxLoaderSetup.msi"))
				If (Not fileInfo.Exists) Then
					Return
				End If
				Me.SetDownloadStatusString(3)
				If (Not Me.RunInstaller(fileInfo.FullName, fileInfo.DirectoryName, num)) Then
					MessageBox.Show(strings.NOTIFY_FAILED_TO_RUN_BODY, strings.NOTIFY_FAILED_TO_RUN_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Hand)
					Directory.Delete(fileInfo.DirectoryName, True)
					Application.[Exit]()
				ElseIf (System.Windows.Forms.DialogResult.Yes = MessageBox.Show(strings.NOTIFY_RUN_APP_NOW_BODY, strings.NOTIFY_RUN_APP_NOW_TITLE, MessageBoxButtons.YesNo, MessageBoxIcon.Question)) Then
					Try
						Try
							Process.Start(String.Concat(fileInfo.DirectoryName, "\loader.exe"))
						Catch
							Console.WriteLine(strings.NOTIFY_FAILED_TO_LAUNCH_APP_BODY)
						End Try
					Finally
						Directory.Delete(fileInfo.DirectoryName, True)
						Environment.[Exit](num)
					End Try
				End If
			Else
				File.Delete(userState)
				Process.Start("cmd.exe", str3)
			End If
			Environment.[Exit](0)
		End Sub

		Private Sub OnDownloadProgressChanged(ByVal sender As Object, ByVal e As DownloadProgressChangedEventArgs)
			Me.SetDownloadStatusString(1)
			Me.progressBarX1.Value = e.ProgressPercentage
			Console.WriteLine("'{0}' downloaded {1} of {2} bytes. {3}% complete", New Object() { CStr(e.UserState), e.BytesReceived, e.TotalBytesToReceive, e.ProgressPercentage })
		End Sub

		Protected Overrides Sub OnLoad(ByVal e As EventArgs)
			Me.SetDownloadStatusString(0)
			Me.bDownloadPortableVersion = True
			Me.InstallLoader(Me.bDownloadPortableVersion)
			MyBase.OnLoad(e)
		End Sub

		Public Shared Sub RemoveDirectorySecurity(ByVal FileName As String, ByVal Account As SecurityIdentifier, ByVal Rights As FileSystemRights, ByVal ControlType As AccessControlType)
			Dim directoryInfo As System.IO.DirectoryInfo = New System.IO.DirectoryInfo(FileName)
			Dim accessControl As DirectorySecurity = directoryInfo.GetAccessControl()
			accessControl.RemoveAccessRule(New FileSystemAccessRule(Account, Rights, ControlType))
			directoryInfo.SetAccessControl(accessControl)
		End Sub

		Private Sub RemoveOldFiles()
			If (Not String.IsNullOrEmpty(Settings.[Default].InstallFolder)) Then
				Dim directoryInfo As System.IO.DirectoryInfo = New System.IO.DirectoryInfo(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), Settings.[Default].InstallFolder))
				If (directoryInfo.Exists) Then
					Dim accessControl As DirectorySecurity = directoryInfo.GetAccessControl()
					accessControl.SetOwner(WindowsIdentity.GetCurrent().User)
					accessControl.GetAccessRules(True, True, GetType(NTAccount))
					accessControl.SetAccessRuleProtection(False, False)
					directoryInfo.SetAccessControl(accessControl)
					Try
						Dim files As FileInfo() = directoryInfo.GetFiles()
						Dim i As Integer = 0
						Do
							files(i).Delete()
							i = i + 1
						Loop While i < CInt(files.Length)
						Dim directories As System.IO.DirectoryInfo() = directoryInfo.GetDirectories()
						For i = 0 To CInt(directories.Length)
							directories(i).Delete(True)
						Next

					Catch exception1 As System.Exception
						Dim exception As System.Exception = exception1
						MessageBox.Show(String.Concat("Failed while deleting old files: ", exception.Message))
					End Try
				End If
			End If
		End Sub

		Private Function RunInstaller(ByVal path As String, ByVal installPath As String, <Out> ByRef exitCode As Integer) As Boolean
			Dim process As System.Diagnostics.Process = System.Diagnostics.Process.Start(New ProcessStartInfo("msiexec.exe", String.Concat(New String() { "/i """, path, """ TARGETDIR=""", installPath, """ /qb" })))
			process.WaitForExit()
			exitCode = process.ExitCode
			If (exitCode <> 0) Then
				Return False
			End If
			Return True
		End Function

		Private Sub SetDownloadStatusString(ByVal updateStatus As Integer)
			Select Case updateStatus
				Case 0
					Me.labelX1.Text = strings.DL_STATUS_IDLE
					Return
				Case 1
					Me.labelX1.Text = strings.DL_STATUS_DOWNLOADING
					Return
				Case 2
					Me.labelX1.Text = strings.DL_STATUS_EXTRACTING
					Return
				Case 3
					Me.labelX1.Text = strings.DL_STATUS_RUNNING
					Return
				Case 4
					Me.labelX1.Text = strings.DL_STATUS_LAUNCHING
					Return
				Case Else
					Me.labelX1.Text = strings.DL_STATUS_IDLE
					Return
			End Select
		End Sub

		Private Sub UpdateApplicationSettings()
			If (Settings.[Default].UpgradeRequired) Then
				Settings.[Default].Upgrade()
				Settings.[Default].UpgradeRequired = False
				Settings.[Default].Save()
			End If
		End Sub
	End Class
End Namespace