'
' * Created by SharpDevelop.
' * User: mjackson
' * Date: 05/03/2010
' * Time: 09:41
' * 
' * To change this template use Tools | Options | Coding | Edit Standard Headers.
' 

Imports System.Collections.Generic

''' <summary>
''' Provides data for the WaitWindow events.
''' </summary>
Public Class WaitWindowEventArgs
	Inherits EventArgs

	''' <summary>
	''' Initialises a new intance of the WaitWindowEventArgs class.
	''' </summary>
	''' <param name="GUI">The associated WaitWindow instance.</param>
	''' <param name="args">A list of arguments to be passed.</param>
	Public Sub New(GUI As WaitWindow, args As List(Of Object))
		MyBase.New()
		Me._Window = GUI
		Me._Arguments = args
	End Sub

	Private _Window As WaitWindow
	Private _Arguments As List(Of Object)
	Private _Result As Object

	Public ReadOnly Property Window() As WaitWindow
		Get
			Return _Window
		End Get
	End Property

	Public ReadOnly Property Arguments() As List(Of Object)
		Get
			Return _Arguments
		End Get
	End Property

	Public Property Result() As Object
		Get
			Return _Result
		End Get
		Set
			_Result = value
		End Set
	End Property
End Class
