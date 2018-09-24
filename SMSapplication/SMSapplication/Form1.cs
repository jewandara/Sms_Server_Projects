using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;
using System.Threading;
using System.Text.RegularExpressions;

namespace SMSapplication
{
    public partial class Form1 : Form
    {            
        public Form1()
        {
            InitializeComponent();
            serialPort1.Open();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            serialPort1.Write(textBox1.Text);
            Thread.Sleep(1000);
        }
            
        String txt = "";
        private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            txt += serialPort1.ReadExisting().ToString();
            SetText(txt.ToString());
        }

        delegate void SetTextCallback(string text);

        private void SetText(string text)
        {
            if (this.textBox1.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetText);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                if (text != "") 
                {this.textBox2.Text = text;}
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            serialPort1.Close();
        }

    }
}
