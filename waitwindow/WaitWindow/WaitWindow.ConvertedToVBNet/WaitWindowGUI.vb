'
' * Created by SharpDevelop.
' * User: mjackson
' * Date: 05/03/2010
' * Time: 09:43
' * 
' * To change this template use Tools | Options | Coding | Edit Standard Headers.
' 

Imports System
Imports System.Windows.Forms

''' <summary>
''' The dialogue displayed by a WaitWindow instance.
''' </summary>
Friend Partial Class WaitWindowGUI
	Inherits Form
	Public Sub New(parent As WaitWindow)
		'
		' The InitializeComponent() call is required for Windows Forms designer support.
		'
		InitializeComponent()

		Me._Parent = parent

		'	Position the window in the top right of the main screen.
		Me.Top = Screen.PrimaryScreen.WorkingArea.Top + 32
		Me.Left = Screen.PrimaryScreen.WorkingArea.Right - Me.Width - 32
	End Sub

	Private _Parent As WaitWindow
	Private Delegate Function FunctionInvoker(Of T)() As T
	Friend _Result As Object
	Friend _Error As Exception
	Private threadResult As IAsyncResult

	Protected Overrides Sub OnPaint(e As System.Windows.Forms.PaintEventArgs)
		MyBase.OnPaint(e)
		'	Paint a 3D border
		ControlPaint.DrawBorder3D(e.Graphics, Me.ClientRectangle, Border3DStyle.Raised)
	End Sub

	Protected Overrides Sub OnShown(e As EventArgs)
		MyBase.OnShown(e)

		'   Create Delegate
		Dim threadController As New FunctionInvoker(Of Object)(AddressOf Me.DoWork)

		'   Execute on secondary thread.
		Me.threadResult = threadController.BeginInvoke(AddressOf Me.WorkComplete, threadController)
	End Sub

	Friend Function DoWork() As Object
		'	Invoke the worker method and return any results.
		Dim e As New WaitWindowEventArgs(Me._Parent, Me._Parent._Args)
		If (Me._Parent._WorkerMethod IsNot Nothing) Then
			Me._Parent._WorkerMethod(Me, e)
		End If
		Return e.Result
	End Function

	Private Sub WorkComplete(results As IAsyncResult)
		If Not Me.IsDisposed Then
			If Me.InvokeRequired Then
				Me.Invoke(New WaitWindow.MethodInvoker(Of IAsyncResult)(AddressOf Me.WorkComplete), results)
			Else
				'	Capture the result
				Try
					Me._Result = DirectCast(results.AsyncState, FunctionInvoker(Of Object)).EndInvoke(results)
				Catch ex As Exception
					'	Grab the Exception for rethrowing after the WaitWindow has closed.
					Me._Error = ex
				End Try
				Me.Close()
			End If
		End If
	End Sub

	Friend Sub SetMessage(message As String)
		Me.MessageLabel.Text = message
	End Sub

	Friend Sub Cancel()
		Me.Invoke(New MethodInvoker(AddressOf Me.Close), Nothing)
	End Sub
End Class
