Imports System
Imports System.Windows.Forms

Namespace CxUpdater
	Friend Module Program
		<STAThread>
		Private Sub Main()
			Application.EnableVisualStyles()
			Application.SetCompatibleTextRenderingDefault(False)
			Application.Run(New Form1())
		End Sub
	End Module
End Namespace