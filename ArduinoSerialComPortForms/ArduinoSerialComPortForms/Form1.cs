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

namespace ArduinoSerialComPortForms
{
    public partial class Form1 : Form
    {

        public Form1()
        {

            InitializeComponent();
            serialPort1.Open();
        }

        String txt = "";

        private void button1_Click(object sender, EventArgs e)
        {
            serialPort1.Write(textBox1.Text);
            System.Threading.Thread.Sleep(5000);
            //textBox3.Text = txt.ToString();
        }

        void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            txt += serialPort1.ReadExisting().ToString();
            SetText(txt.ToString());
        }

        delegate void SetTextCallback(string text);

        private void SetText(string text)
        {
            if (this.textBox2.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetText);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.textBox2.Text = text;
                ParseMessages(text);
            }
        }





        public void ParseMessages(string input)
        {  
            String[] SMSCollection = new String[6];
            try
            {    
                Regex r = new Regex(@"\+CMT: ""\+(?<ID>\d+)"","""",""(?<Y>\d+)/(?<M>\d+)/(?<D>\d+),(?<Hour>\d+):(?<Minit>\d+):(?<Secon>\d+)\+(?<RestS>\d+)""\r\n(?<SMS>[\w ]+)");
                Match m = r.Match(input);
                while (m.Success)
                {
                    SMSCollection[0] = m.Groups["ID"].Value;
                    SMSCollection[1] = m.Groups["Y"].Value;
                    SMSCollection[2] = m.Groups["M"].Value;
                    SMSCollection[3] = m.Groups["D"].Value;
                    SMSCollection[4] = m.Groups["Hour"].Value;
                    SMSCollection[5] = m.Groups["SMS"].Value;
                    m = m.NextMatch();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            label1.Text = SMSCollection[0];
            label2.Text = SMSCollection[1];
            label3.Text = SMSCollection[2];
            label4.Text = SMSCollection[3];
            label5.Text = SMSCollection[4];
            label6.Text = SMSCollection[5];
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            serialPort1.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            serialPort1.WriteLine("AT+CMGS=\"+94765108320\"\r");
            Thread.Sleep(1000);
            serialPort1.WriteLine("This is the test message for GSM Mordem");
            Thread.Sleep(1000);
            serialPort1.WriteLine("\r" + (char)(26));
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string[] myPort;
            myPort = System.IO.Ports.SerialPort.GetPortNames();
            listBox1.Items.AddRange(myPort);
        }

    }
}
