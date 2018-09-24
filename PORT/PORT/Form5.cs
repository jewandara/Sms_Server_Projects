using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PORT
{
    public partial class Form5 : Form
    {

        public Form5()
        {
            InitializeComponent();
        }

        delegate void dialogMessageSerialPortTextCallback(string textPort);

        private void Form5_Load(object sender, EventArgs e)
        {
            serialPort1.Open();
        }



        String _portD = "";
        String Dataa = "";

        private void serialPort1_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
           System.Threading.Thread.Sleep(1000);
           _dialogMessageSerialPortSetText(_portD + serialPort1.ReadExisting().ToString());
        }

        private void _dialogMessageSerialPortSetText(string textPort)
        {
            if (this.InvokeRequired)
            {
                dialogMessageSerialPortTextCallback call = new dialogMessageSerialPortTextCallback(_dialogMessageSerialPortSetText);
                this.Invoke(call, new object[] { textPort });
            }
            else
            {
                System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(@"\+(?<CMT>[\w ]+): ""\+(?<ID>\d+)"","""",""(?<Y>\d+)/(?<M>\d+)/(?<D>\d+),(?<Hour>\d+):(?<Minit>\d+):(?<Secon>\d+)\+(?<RestS>\d+)""\r\n(?<LOGA>\d+(?:\.\d+)?)\?(?<LOGB>\d+(?:\.\d+)?)'(?<LOGC>\d+(?:\.\d+)?)"" (?<LATA>\d+(?:\.\d+)?)\?(?<LATB>\d+(?:\.\d+)?)'(?<LATC>\d+(?:\.\d+)?)"" (?<SMS>[\w ]+)\r\n*$");


                //System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(@"\+(?<CMT>[\w ]+): ""\+(?<ID>\d+)"","""",""(?<Y>\d+)/(?<M>\d+)/(?<D>\d+),(?<Hour>\d+):(?<Minit>\d+):(?<Secon>\d+)\+(?<RestS>\d+)""\r\n(?<SMS>[\w ]+) \? (?<SMS>[\w ]+)\r\n*$");
                
                System.Text.RegularExpressions.Match match = regex.Match(textPort);
                if (match.Success)
                {
                    textBox1.Text = "fdgdfg";
                }
                else
                {
                    textBox1.Text = textPort;
                }
            }
        }










        private void button1_Click(object sender, EventArgs e)
        {
            Dataa = "";
            serialPort1.Write("AT+CMGF=1\r");
            System.Threading.Thread.Sleep(500);
            serialPort1.Write("AT+CMGS=\"+94765108320\"\r");
            System.Threading.Thread.Sleep(1000);
            serialPort1.Write("77°57'18.60\" 77°57'18.60\" sdgsd" + (char)(26));
            System.Threading.Thread.Sleep(1000);
        }


    }
}
