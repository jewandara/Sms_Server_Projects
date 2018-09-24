using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NEW
{
    public partial class AboutForm2 : Form
    {
        public AboutForm2()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text == "32793") 
            {
                panel1.Visible = false;
                label13.Text = "Creation Of Mahathun";
                label13.Visible = true;
            }
            else { }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        { 
            try
            {
            System.Diagnostics.Process.Start("http://www.ndes.lk/");
            }
            catch (Exception) { }
        }

        private void label5_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("mailto:jewandara@yahoo.com");
            }
            catch (Exception) { }
        }

        private void label6_Click(object sender, EventArgs e)
        {
            try
            {
            System.Diagnostics.Process.Start("mailto:jewandara@hotmail.com");
            }
            catch (Exception) { }
        }



    }
}
