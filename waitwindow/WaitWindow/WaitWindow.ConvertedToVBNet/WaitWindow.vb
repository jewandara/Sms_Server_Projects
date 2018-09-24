'
' * Created by SharpDevelop.
' * User: mjackson
' * Date: 05/03/2010
' * Time: 09:36
' * 
' * To change this template use Tools | Options | Coding | Edit Standard Headers.
' 

Imports System
Imports System.Collections.Generic
Imports System.Windows.Forms

''' <summary>
''' Displays a window telling the user to wait while a process is executing.
''' </summary>
Public NotInheritable Class WaitWindow

	''' <summary>
	''' Shows a wait window with the text 'Please wait...' while executing the passed method.
	''' </summary>
	''' <param name="workerMethod">Pointer to the method to execute while displaying the wait window.</param>
	''' <returns>The result argument from the worker method.</returns>
	Public Shared Function Show(workerMethod As EventHandler(Of WaitWindowEventArgs)) As Object
		Return WaitWindow.Show(workerMethod, Nothing)
	End Function

	''' <summary>
	''' Shows a wait window with the specified text while executing the passed method.
	''' </summary>
	''' <param name="workerMethod">Pointer to the method to execute while displaying the wait window.</param>
	''' <param name="message">The text to display.</param>
	''' <returns>The result argument from the worker method.</returns>
	Public Shared Function Show(workerMethod As EventHandler(Of WaitWindowEventArgs), message As String) As Object
		Dim instance As New WaitWindow()
		Return instance.Show(workerMethod, message, New List(Of Object)())
	End Function

	''' <summary>
	''' Shows a wait window with the specified text while executing the passed method.
	''' </summary>
	''' <param name="workerMethod">Pointer to the method to execute while displaying the wait window.</param>
	''' <param name="message">The text to display.</param>
	''' <param name="args">Arguments to pass to the worker method.</param>
	''' <returns>The result argument from the worker method.</returns>
	Public Shared Function Show(workerMethod As EventHandler(Of WaitWindowEventArgs), message As String, ParamArray args As Object()) As Object
		Dim arguments As New List(Of Object)()
		arguments.AddRange(args)

		Dim instance As New WaitWindow()
		Return instance.Show(workerMethod, message, arguments)
	End Function

	#Region "Instance implementation"

	Private Sub New()
	End Sub

	Private _GUI As WaitWindowGUI
	Friend Delegate Sub MethodInvoker(Of T)(parameter1 As T)
	Friend _WorkerMethod As EventHandler(Of WaitWindowEventArgs)
	Friend _Args As List(Of Object)

	''' <summary>
	''' Updates the message displayed in the wait window.
	''' </summary>
	Public WriteOnly Property Message() As String
		Set
			Me._GUI.Invoke(New MethodInvoker(Of String)(AddressOf Me._GUI.SetMessage), value)
		End Set
	End Property

	''' <summary>
	''' Cancels the work and exits the wait windows immediately
	''' </summary>
	Public Sub Cancel()
		Me._GUI.Invoke(New MethodInvoker(AddressOf Me._GUI.Cancel), Nothing)
	End Sub

	Private Function Show(workerMethod As EventHandler(Of WaitWindowEventArgs), message As String, args As List(Of Object)) As Object
		'	Validate Parameters
		If workerMethod Is Nothing Then
			Throw New ArgumentException("No worker method has been specified.", "workerMethod")
		Else
			Me._WorkerMethod = workerMethod
		End If

		Me._Args = args

		If String.IsNullOrEmpty(message) Then
			message = "Please wait..."
		End If

		'	Set up the window
		Me._GUI = New WaitWindowGUI(Me)
		Me._GUI.MessageLabel.Text = message

		'	Call it
		Me._GUI.ShowDialog()

		Dim result As Object = Me._GUI._Result

		'	clean up
		Dim _Error As Exception = Me._GUI._Error
		Me._GUI.Dispose()

		'	Return result or throw and exception
		If _Error IsNot Nothing Then
			Throw _Error
		Else
			Return result
		End If
	End Function

	#End Region

End Class
