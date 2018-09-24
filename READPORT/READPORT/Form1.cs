using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace READPORT
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        delegate void dialogMessageSerialPortTextCallback(string textPort);

        private void button1_Click(object sender, EventArgs e)
        {
            serialPort1.Open();
            System.Threading.Thread.Sleep(100);
            serialPort1.Write("AT");
            System.Threading.Thread.Sleep(1000);
            //serialPort1.Write("TestT ESEE fgdhdfgd f ");
            //System.Threading.Thread.Sleep(1000);
            //serialPort1.Write("" + (char)(26));
            //System.Threading.Thread.Sleep(2000);
        }








        byte[] bytes = new byte[1000];
        int count = 0;


        private void serialPort1_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            bytes[count] = serialPort1.Read(Byte[], Int32, Int32);
            count = count + 1;

            serialPort1.Read(bytes, 0, bytes.Length);
            String strLine = new String(System.Text.Encoding.UTF8.GetChars(bytes, 0, bytes.Length));
            SetText(strLine);
        }


        private void SetText(string textPort)
        {
            if (this.InvokeRequired)
            {
                dialogMessageSerialPortTextCallback call = new dialogMessageSerialPortTextCallback(SetText);
                this.Invoke(call, new object[] { textPort });
            }
            else
            {
                textBox1.Text += textPort;
            }
        }















        private void button2_Click(object sender, EventArgs e)
        {
            string convert = "aa";
            byte[] buffer = Encoding.UTF8.GetBytes(convert);
            string s = Encoding.UTF8.GetString(buffer, 0, buffer.Length);//one way
            textBox1.Text = System.Text.Encoding.UTF8.GetString(buffer) + " & "+ s ; // two way
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }




    }
}
