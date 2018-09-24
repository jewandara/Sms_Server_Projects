/*
 * Created by SharpDevelop.
 * User: mjackson
 * Date: 05/03/2010
 * Time: 09:35
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Demo
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form
	{
		public MainForm(){
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
		}
		
		void Button1Click(object sender, EventArgs e){
			object result = Jacksonsoft.WaitWindow.Show(this.WorkerMethod);
			MessageBox.Show(result.ToString());
		}
		
		void Button2Click(object sender, EventArgs e){
			object result = Jacksonsoft.WaitWindow.Show(this.WorkerMethod,  "Watch out! Stuff is happening!");
			MessageBox.Show(result.ToString());
		}
		
		void Button3Click(object sender, EventArgs e){
			object result = Jacksonsoft.WaitWindow.Show(this.ProgressWorkerMethod, "Please wait...   0%");
			MessageBox.Show(result.ToString());			
		}

		void Button4Click(object sender, EventArgs e){
			object result = Jacksonsoft.WaitWindow.Show(this.WorkerMethod, null,this.textBox1.Text);
			MessageBox.Show(result.ToString());
		}
		
		
		void Button5Click(object sender, EventArgs e){
			try {
				object result = Jacksonsoft.WaitWindow.Show(this.ErroringWorkerMethod);
				MessageBox.Show(result.ToString());			
			} catch (Exception ex) {
				MessageBox.Show(string.Concat("An Exception occured.", Environment.NewLine, ex.Message, Environment.NewLine, ex.StackTrace));
				System.Diagnostics.Debugger.Break();
			}
		}
		
		void Button6Click(object sender, EventArgs e){
			object result = Jacksonsoft.WaitWindow.Show(this.CancelingWorkerMethod);
			if (result != null){
				MessageBox.Show(result.ToString());					
			} else {
				MessageBox.Show("No result so we must have canceled the process.");
			}
		}
		private void WorkerMethod(object sender, Jacksonsoft.WaitWindowEventArgs e){
			System.Threading.Thread.Sleep(4000);
			if (e.Arguments.Count> 0){
				e.Result = e.Arguments[0].ToString();
			} else {
				e.Result = "Hello World";
			}
		}
		
		private void ProgressWorkerMethod(object sender, Jacksonsoft.WaitWindowEventArgs e){
			//	Do something
			for (int progress = 1; progress <= 100; progress++){
				System.Threading.Thread.Sleep(20);
				
				//	Update the wait window message
				e.Window.Message = string.Format("Please wait ... {0}%", progress.ToString().PadLeft(3));
			}
			
			//	Use the arguments sent in
			if (e.Arguments.Count> 0){
				//	Set the result to return
				e.Result = e.Arguments[0].ToString();
			} else {
				//	Set the result to return
				e.Result = "Hello World";
			}
		}
		
		private void ErroringWorkerMethod(object sender, Jacksonsoft.WaitWindowEventArgs e){
			System.Threading.Thread.Sleep(2000);
			throw new ApplicationException("Something went wrong here");
		}

		private void CancelingWorkerMethod(object sender, Jacksonsoft.WaitWindowEventArgs e){
			System.Threading.Thread.Sleep(2000);
			e.Window.Cancel();
			System.Threading.Thread.Sleep(2000);
			e.Result = "Hello World.  All done";

		}

		
	
	}
}
