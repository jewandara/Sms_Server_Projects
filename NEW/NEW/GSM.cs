using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;


namespace NEW
{
    public partial class GSM : Form
    {
        SerialPort sp;
        int datab = 0;
        bool dtr = false;
        bool encod;
        Handshake h;
        Parity p;
        int wtimeout = 0;
        StopBits s;

        public GSM()
        {
            InitializeComponent();
            this.sp = serialPort1 = new SerialPort();
            this.datab = serialPort1.DataBits = 8;
            this.dtr = serialPort1.DtrEnable = true;
            this.encod = serialPort1.Encoding.Equals("iso-8859-1");
            this.h = serialPort1.Handshake = Handshake.RequestToSend;
            this.p = serialPort1.Parity = Parity.None;
            this.wtimeout = serialPort1.WriteTimeout = 300;
            this.s = serialPort1.StopBits = StopBits.One;
            checkLink();
        }


    private void checkLink()
    {
        GetValues value = new GetValues();
        string com = value.getPort();
        int baud = value.getBaud();
        int timeot = value.getTimeout();
        serialPort1.PortName = com;
        serialPort1.BaudRate = baud;
        serialPort1.ReadTimeout = timeot;

        serialPort1.Open();
        if (serialPort1.IsOpen)
        {
            label1.Visible = true;
        }
        else
        {
            MessageBox.Show("Komunikacija sa modemom se ne može uspostaviti, molimo postavite novu konfiguraciju...!");
            this.Controls.Clear();
            SMSConfigPanel cfg = new SMSConfigPanel();
            cfg.Show();
            this.Controls.Add(cfg);
        }
        serialPort1.Close();
    }



    private void SMSLogPanel_Load(object sender, EventArgs e)
    {
        setGSM();
    }



    public void getMessage()
    {
        if (serialPort1.IsOpen)
        {
            serialPort1.DataReceived += new SerialDataReceivedEventHandler(getResponse);
        }
        else
        {
            MessageBox.Show("Nije moguće zaprimiti poruku, komunikacijski port nije otvoren...1");
            return;
        }
    }



    public void getResponse(object sender, SerialDataReceivedEventArgs e)
    {
        SerialPort serPort = (SerialPort)sender;
        string input = serPort.ReadExisting();

        if (input.Contains("+CMT:"))
        {

            if (input.Contains("AT+CMGF=1"))
            {
                string[] message = input.Split(Environment.NewLine.ToCharArray()).Skip(7).ToArray();
                textBox1.Text = string.Join(Environment.NewLine, message);
            }
            this.Invoke((MethodInvoker)delegate
            {
                textBox1.Text = input;
            });
        }
        else
        {
            return;
        }
    }




    private void setGSM()
    {
        serialPort1.Open();

        if (!serialPort1.IsOpen)
        {
            MessageBox.Show("Problem u komunikaciji sa modemom, port nije otvoren...!");
        }
        serialPort1.Write("AT+CMGF=1" + (char)(13));
        serialPort1.Write("AT+CNMI=1,2,0,0,0" + (char)(13));
    }




    private void timer1_Tick_1(object sender, EventArgs e)
    {
        timer1.Stop();
        getMessage();
        timer1.Start();
    }








    }
}
