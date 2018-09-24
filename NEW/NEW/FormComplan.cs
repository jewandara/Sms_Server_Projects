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
    public partial class FormComplan : Form
    {
        public FormComplan()
        {
            InitializeComponent();
        }

        String path = "";
        String pathOfFile = "";

        protected DataTable getConnection(String CommandString)
        {
            System.Data.OleDb.OleDbConnection mCon;
            mCon = new System.Data.OleDb.OleDbConnection();
            mCon.ConnectionString = ("Provider=Microsoft.ACE.OLEDB.12.0;data source=" + pathOfFile + ";Extended Properties=\"Excel 12.0;HDR=YES\";");
            DataTable Contents = new DataTable();
            try
            {
                using (System.Data.OleDb.OleDbDataAdapter adapter = new System.Data.OleDb.OleDbDataAdapter(CommandString, mCon))
                {
                    adapter.Fill(Contents);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("The excel file formet in incorrect." + Environment.NewLine + "Click for Help.", "Excel File Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return Contents;
        }



        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog choofdlog = new OpenFileDialog();
                choofdlog.Filter = "Only Xlsx Files (*.xlsx*)|*.xlsx*";
                choofdlog.FilterIndex = 1;
                choofdlog.Multiselect = true;

                if (choofdlog.ShowDialog() == DialogResult.OK)
                {
                    pathOfFile = choofdlog.FileName;
                    string[] arrAllFiles = choofdlog.FileNames;


                }
            }
            catch (Exception fail)
            {
                String error = "The following error has occurred:\n\n";
                error += fail.Message.ToString() + "\n\n";
                MessageBox.Show(error);
            }


        }
    }
}
