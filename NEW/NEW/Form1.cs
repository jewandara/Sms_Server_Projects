using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;


namespace NEW
{
    public partial class Form1 : Form
    {
        String path = "";
        String pathOfFile = "";
        String TextFileDta = "";
        String ImagePath = "";
        String Dialog_Footer = "</Folder>" + Environment.NewLine + "</Document>" + Environment.NewLine + "</kml>";
        String Dialog_Header = ""+
            "<?xml version='1.0' encoding='UTF-8'?>" + Environment.NewLine +
            "<kml xmlns='http://www.opengis.net/kml/2.2' xmlns:gx='http://www.google.com/kml/ext/2.2' xmlns:kml='http://www.opengis.net/kml/2.2' xmlns:atom='http://www.w3.org/2005/Atom'>" + Environment.NewLine +
            "<Document>" + Environment.NewLine + Environment.NewLine +
            "<name>Dialog_SiteName_Of_All</name>" + Environment.NewLine + Environment.NewLine + Environment.NewLine;
        String Dialog_Style = "" +
"<Style id='inline'>" + Environment.NewLine +
    "<LineStyle>" + Environment.NewLine +
    "<color>ff0000ff</color>" + Environment.NewLine +
    "<width>2</width>" + Environment.NewLine +
    "</LineStyle>" + Environment.NewLine +
    "<PolyStyle>" + Environment.NewLine +
    "<fill>0</fill>" + Environment.NewLine +
    "</PolyStyle>" + Environment.NewLine +
"</Style>" + Environment.NewLine + Environment.NewLine +
"<Style id='inline1'>" + Environment.NewLine +
    "<LineStyle>" + Environment.NewLine +
    "<color>ff0000ff</color>" + Environment.NewLine +
    "<width>2</width>" + Environment.NewLine +
    "</LineStyle>" + Environment.NewLine +
    "<PolyStyle>" + Environment.NewLine +
    "<fill>0</fill>" + Environment.NewLine +
    "</PolyStyle>" + Environment.NewLine +
"</Style>" + Environment.NewLine + Environment.NewLine +
"<StyleMap id='inline0'>" + Environment.NewLine +
    "<Pair>" + Environment.NewLine +
        "<key>normal</key>" + Environment.NewLine +
        "<styleUrl>#inline</styleUrl>" + Environment.NewLine +
    "</Pair>" + Environment.NewLine +
    "<Pair>" + Environment.NewLine +
        "<key>highlight</key>" + Environment.NewLine +
        "<styleUrl>#inline1</styleUrl>" + Environment.NewLine +
    "</Pair>" + Environment.NewLine +
"</StyleMap>" + Environment.NewLine + Environment.NewLine +
"<Style id='inline2'>" + Environment.NewLine +
    "<LineStyle>" + Environment.NewLine +
    "<color>ffffaa00</color>" + Environment.NewLine +
    "<width>2</width>" + Environment.NewLine +
    "</LineStyle>" + Environment.NewLine +
    "<PolyStyle>" + Environment.NewLine +
    "<fill>0</fill>" + Environment.NewLine +
    "</PolyStyle>" + Environment.NewLine +
"</Style>" + Environment.NewLine + Environment.NewLine +
"<Style id='inline3'>" + Environment.NewLine +
    "<LineStyle>" + Environment.NewLine +
    "<color>ffffaa00</color>" + Environment.NewLine +
    "<width>2</width>" + Environment.NewLine +
    "</LineStyle>" + Environment.NewLine +
    "<PolyStyle>" + Environment.NewLine +
    "<fill>0</fill>" + Environment.NewLine +
    "</PolyStyle>" + Environment.NewLine +
"</Style>" + Environment.NewLine + Environment.NewLine +
"<StyleMap id='inline4'>" + Environment.NewLine +
    "<Pair>" + Environment.NewLine +
        "<key>normal</key>" + Environment.NewLine +
        "<styleUrl>#inline2</styleUrl>" + Environment.NewLine +
    "</Pair>" + Environment.NewLine +
    "<Pair>" + Environment.NewLine +
        "<key>highlight</key>" + Environment.NewLine +
        "<styleUrl>#inline3</styleUrl>" + Environment.NewLine +
    "</Pair>" + Environment.NewLine +
"</StyleMap>" + Environment.NewLine + Environment.NewLine +
"<Style id='s_ylw-pushpin'>" + Environment.NewLine +
    "<IconStyle>" + Environment.NewLine +
    "<scale>1.1</scale>" + Environment.NewLine +
    "<Icon>" + Environment.NewLine +
    "<href>H:\\DIALOG\\Project\\D2.png</href>" + Environment.NewLine +
    "</Icon>" + Environment.NewLine +
    "<hotSpot x='20' y='2' xunits='pixels' yunits='pixels'/>" + Environment.NewLine +
    "</IconStyle>" + Environment.NewLine +
"</Style>" + Environment.NewLine + Environment.NewLine +
"<Style id='s_ylw-pushpin_hl'>" + Environment.NewLine +
    "<IconStyle>" + Environment.NewLine +
    "<scale>1.3</scale>" + Environment.NewLine +
    "<Icon>" + Environment.NewLine +
    "<href>H:\\DIALOG\\Project\\D2.png</href>" + Environment.NewLine +
    "</Icon>" + Environment.NewLine +
    "<hotSpot x='20' y='2' xunits='pixels' yunits='pixels'/>" + Environment.NewLine +
    "</IconStyle>" + Environment.NewLine +
"</Style>" + Environment.NewLine + Environment.NewLine +
"<StyleMap id='m_ylw-pushpin'>" + Environment.NewLine +
    "<Pair>" + Environment.NewLine +
        "<key>normal</key>" + Environment.NewLine +
        "<styleUrl>#s_ylw-pushpin</styleUrl>" + Environment.NewLine +
    "</Pair>" + Environment.NewLine +
    "<Pair>" + Environment.NewLine +
        "<key>highlight</key>" +
        "<styleUrl>#s_ylw-pushpin_hl</styleUrl>" + Environment.NewLine +
    "</Pair>" + Environment.NewLine +
"</StyleMap>" + Environment.NewLine + Environment.NewLine + Environment.NewLine +

"<Folder>" + Environment.NewLine +
"<name>DIALOG_SITE_DATA</name>" + Environment.NewLine;







        public Form1()
        {
            InitializeComponent();
        }

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
                    button5.Enabled = true;
                    saveToolStripMenuItem.Enabled = true;
                }
            }
            catch (Exception) {
                button5.Enabled = false;
                saveToolStripMenuItem.Enabled = false;
                MessageBox.Show("The excel file formet in incorrect." + Environment.NewLine + "Click for Help.", "Excel File Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return Contents;
        }

        protected String GSMgetData(String SiteID, String SectorID, String BandName)
        {
            String ReData ="";
            DataTable GSMContents = new DataTable();

            if (SectorID == "GSM_1" || SectorID == "GSM_2" || SectorID == "GSM_3" || SectorID == "GSM_4" || SectorID == "DCS_1" || SectorID == "DCS_2" || SectorID == "DCS_3" || SectorID == "DCS_4") 
            {
                GSMContents = getConnection("Select [ARFCN] From [GSM_Sector_Carriers$] WHERE ([Site ID] = '" + SiteID + "') AND ([Band Name] = '" + BandName + "') AND ([Sector ID] = '" + SectorID + "')");
                ReData += "<tr><td style='width:100px; padding: 2px;'>GSM ARFCN</td><td style='background:#FFC0CB; font-size: 90%;'>" + Convert.ToString(GSMContents.Rows[0]["ARFCN"]) + "</td></tr>" + Environment.NewLine;
                GSMContents = getConnection("Select [LAC], [RAC], [BSIC], [NCC], [BCC] From [GSM_Sectors$] WHERE ([Site ID] = '" + SiteID + "') AND ([Sector ID] = '" + SectorID + "')");
                ReData += "<tr><td style='width:100px; padding: 2px;'>GSM LAC</td><td style='background:#FFC0CB; font-size: 90%;'>" + Convert.ToString(GSMContents.Rows[0]["LAC"]) + "</td></tr>" + Environment.NewLine;
                ReData += "<tr><td style='width:100px; padding: 2px;'>GSM RAC</td><td style='background:#FFC0CB; font-size: 90%;'>" + Convert.ToString(GSMContents.Rows[0]["RAC"]) + "</td></tr>" + Environment.NewLine;
                ReData += "<tr><td style='width:100px; padding: 2px;'>GSM BSIC</td><td style='background:#FFC0CB; font-size: 90%;'>" + Convert.ToString(GSMContents.Rows[0]["BSIC"]) + "</td></tr>" + Environment.NewLine;
                ReData += "<tr><td style='width:100px; padding: 2px;'>GSM NCC</td><td style='background:#FFC0CB; font-size: 90%;'>" + Convert.ToString(GSMContents.Rows[0]["NCC"]) + "</td></tr>" + Environment.NewLine;
                ReData += "<tr><td style='width:100px; padding: 2px;'>GSM BCC</td><td style='background:#FFC0CB; font-size: 90%;'>" + Convert.ToString(GSMContents.Rows[0]["BCC"]) + "</td></tr>" + Environment.NewLine;
                GSMContents.Clear();
                GSMContents.Dispose();
            }
            else if (SectorID == "3G_A" || SectorID == "3G_B" || SectorID == "3G_C" || SectorID == "3G_P" || SectorID == "3G_Q" || SectorID == "3G_R" || SectorID == "3G_U" || SectorID == "3G_V" || SectorID == "3G_W" )
            {
                GSMContents = getConnection("Select [LAC], [RAC], [SAC], [Scrambling Code Index], [Scrambling Code], [Scrambling Code Group] From [WCDMA_Sectors$] WHERE ([Site ID] = '" + SiteID + "') AND ([Sector ID] = '" + SectorID + "')");
                ReData += "<tr><td style='width:100px; padding: 2px;'>WCDMA LAC</td><td style='background:#FFC0CB; font-size: 90%;'>" + Convert.ToString(GSMContents.Rows[0]["LAC"]) + "</td></tr>" + Environment.NewLine;
                ReData += "<tr><td style='width:100px; padding: 2px;'>WCDMA RAC</td><td style='background:#FFC0CB; font-size: 90%;'>" + Convert.ToString(GSMContents.Rows[0]["RAC"]) + "</td></tr>" + Environment.NewLine;
                ReData += "<tr><td style='width:100px; padding: 2px;'>WCDMA SAC</td><td style='background:#FFC0CB; font-size: 90%;'>" + Convert.ToString(GSMContents.Rows[0]["SAC"]) + "</td></tr>" + Environment.NewLine;
                ReData += "<tr><td style='width:100px; padding: 2px;'>Scrambling Code Index</td><td style='background:#FFC0CB; font-size: 90%;'>" + Convert.ToString(GSMContents.Rows[0]["Scrambling Code Index"]) + "</td></tr>" + Environment.NewLine;
                ReData += "<tr><td style='width:100px; padding: 2px;'>Scrambling Code</td><td style='background:#FFC0CB; font-size: 90%;'>" + Convert.ToString(GSMContents.Rows[0]["Scrambling Code"]) + "</td></tr>" + Environment.NewLine;
                ReData += "<tr><td style='width:100px; padding: 2px;'>Scrambling Code Group</td><td style='background:#FFC0CB; font-size: 90%;'>" + Convert.ToString(GSMContents.Rows[0]["Scrambling Code Group"]) + "</td></tr>" + Environment.NewLine;
                GSMContents.Clear();
                GSMContents.Dispose();
            }

            else { ReData = ""; }
            GSMContents.Clear();
            GSMContents.Dispose();

            return ReData;
        }

        //protected String getSiteLog(String SiteName)
        //{
        //    DataTable LogContents = new DataTable();
        //    LogContents = getConnection("Select * From [Sites$] WHERE 'Site Name'='" + SiteName + "'");
        //    return LogContents.Rows[1][2].ToString();
        //}

        protected Double getLocationLatitude(Double latitude, Double length, Double degree)
        {
            Double data;
            data = latitude + (length * (Math.Cos(degree * (Math.PI / 180.0))));
            return data;
        }

        protected Double getLocationLongitude(Double longitude, Double length, Double degree)
        {
            Double data;
            data = longitude + (length * (Math.Sin(degree * (Math.PI / 180.0))));
            return data;
        }

        protected String createSiteTypeData(String SiteID, String Sector, String Log, String Lat)
        {
            String Data = "";
            Double coordinatesLat = 0;
            Double coordinatesLog = 0;
            DataTable DContents = new DataTable();
            DataTable OContents = new DataTable();
            try
            {
                DContents = getConnection("Select * From [Sectors$] WHERE [Site ID] = '" + SiteID + "' AND [BTS Name] = '" + Sector + "'");
                for (int i = 0; i < DContents.Rows.Count; i++)
                {



                    OContents.Clear();
                    OContents.Dispose();
                    OContents = getConnection("Select * From [Antennas$] WHERE ([Site ID] = '" + SiteID + "') AND ([Sectors] like '%" + Convert.ToString(DContents.Rows[i]["Sector ID"]) + "' OR [Sectors] like '" + Convert.ToString(DContents.Rows[i]["Sector ID"]) + "%' OR [Sectors] = '" + Convert.ToString(DContents.Rows[i]["Sector ID"]) + "')");
                    coordinatesLat = getLocationLatitude(Convert.ToDouble(Lat), 0.04, Convert.ToDouble(OContents.Rows[0]["Azimuth"]));
                    coordinatesLog = getLocationLongitude(Convert.ToDouble(Log), 0.04, Convert.ToDouble(OContents.Rows[0]["Azimuth"]));


                    Data += Environment.NewLine + Environment.NewLine + "<Placemark>" + Environment.NewLine +
                            "<name>" + Convert.ToString(DContents.Rows[i]["Sector ID"]) + "</name>" + Environment.NewLine +
                            "<open>1</open>" + Environment.NewLine + Environment.NewLine +

                            "<description><![CDATA[" + Environment.NewLine +
                            "<table  style='width:400px; background:#208080'>" + Environment.NewLine +

                            "<tr><td style='width:160px; padding: 2px;'>Band Name</td><td style='background:#FFC0CB; font-size: 90%;'>" + Convert.ToString(DContents.Rows[i]["Band Name"]) + "</td></tr>" + Environment.NewLine +
                            "<tr><td style='width:160px; padding: 2px;'>Propagation Model</td><td style='background:#FFC0CB; font-size: 90%;'>" + Convert.ToString(DContents.Rows[i]["Propagation Model"]) + "</td></tr>" + Environment.NewLine +
                            "<tr><td style='width:160px; padding: 2px;'>Distance (km)</td><td style='background:#FFC0CB; font-size: 90%;'>" + Convert.ToString(DContents.Rows[i]["Distance (km)"]) + "</td></tr>" + Environment.NewLine +
                            "<tr><td style='width:160px; padding: 2px;'>BTS Type</td><td style='background:#FFC0CB; font-size: 90%;'>" + Convert.ToString(DContents.Rows[i]["Flag: BTS_Type"]) + "</td></tr>" + Environment.NewLine +
                            "<tr><td style='width:160px; padding: 2px;'>Cabin Type</td><td style='background:#FFC0CB; font-size: 90%;'>" + Convert.ToString(DContents.Rows[i]["Flag: Cabin_Type"]) + "</td></tr>" + Environment.NewLine +
                            "<tr><td style='width:160px; padding: 2px;'>Sectors Status</td><td style='background:#FFC0CB; font-size: 90%;'>" + Convert.ToString(DContents.Rows[i]["Flag: Site_Status"]) + "</td></tr>" + Environment.NewLine +
                            "<tr><td style='width:160px; padding: 2px;'>Tower Type</td><td style='background:#FFC0CB; font-size: 90%;'>" + Convert.ToString(DContents.Rows[i]["Flag: Tower_Type"]) + "</td></tr>" + Environment.NewLine +

                            "<tr><td style='width:160px; padding: 2px;'>Antenna ID</td><td style='background:#FFC0CB; font-size: 90%;'>" + Convert.ToString(OContents.Rows[0]["Antenna ID"]) + "</td></tr>" + Environment.NewLine +
                            "<tr><td style='width:160px; padding: 2px;'>Antenna File</td><td style='background:#FFC0CB; font-size: 90%;'>" + Convert.ToString(OContents.Rows[0]["Antenna File"]) + "</td></tr>" + Environment.NewLine +
                            "<tr><td style='width:160px; padding: 2px;'>Antenna Height(m)</td><td style='background:#FFC0CB; font-size: 90%;'>" + Convert.ToString(OContents.Rows[0]["Height (m)"]) + "</td></tr>" + Environment.NewLine +
                            "<tr><td style='width:160px; padding: 2px;'>Antenna Azimuth</td><td style='background:#FFC0CB; font-size: 90%;'>" + Convert.ToString(OContents.Rows[0]["Azimuth"]) + "</td></tr>" + Environment.NewLine +
                            "<tr><td style='width:160px; padding: 2px;'>Antenna Tilt(M)</td><td style='background:#FFC0CB; font-size: 90%;'>" + Convert.ToString(OContents.Rows[0]["Mechanical Tilt"]) + "</td></tr>" + Environment.NewLine +
                            "<tr><td style='width:160px; padding: 2px;'>Antenna Sectors</td><td style='background:#FFC0CB; font-size: 90%;'>" + Convert.ToString(OContents.Rows[0]["Sectors"]) + "</td></tr>" + Environment.NewLine +

                            //GSMgetData(SiteID, Convert.ToString(DContents.Rows[i]["Sector ID"]), Sector) + Environment.NewLine +
                             GSMgetData(SiteID, Convert.ToString(OContents.Rows[0]["Sectors"]), Sector) + Environment.NewLine +



                            "</table>" + Environment.NewLine +
                            "]]></description>" + Environment.NewLine +
                            "<styleUrl>#inline4</styleUrl>" + Environment.NewLine +
                            "<LineString>" + Environment.NewLine +
                            "<tessellate>1</tessellate>" + Environment.NewLine +
                            "<coordinates>" + coordinatesLog + "," + coordinatesLat + ",0 " + Log + "," + Lat + ",0</coordinates>" + Environment.NewLine +
                            "</LineString>" + Environment.NewLine +
                            "</Placemark>" + Environment.NewLine + Environment.NewLine;




                }
            }
            catch (Exception) { }
            return Data;
        }

        protected String getSiteName()
        {
            progressBar1.Step = 1;
            String Data = "";
            panel1.Visible = true;
            try
            {
                DataTable Contents = new DataTable();
                Contents = getConnection("Select * From [Sites$]");
                progressBar1.Maximum = Contents.Rows.Count;
                for (int i = 0; i < Contents.Rows.Count; i++)
                {
                    double pow = i;
                    double tot = Contents.Rows.Count;
                    double present = pow / tot;

                    progressBar1.PerformStep();
                    label2.Text = present*100 + "   ";
                    label2.Refresh();
                    Thread.Sleep(1);
                    this.Refresh();

                    Data += "<!-- =========================  Line:" + (104 + i * 14) + "  Data:" + i + "  ========================= -->" + Environment.NewLine;
                    Data += "<Folder>" + Environment.NewLine +
                                        "<name>" + Convert.ToString(Contents.Rows[i]["Site Name"]) + "</name>" + Environment.NewLine +
                                        "<open>1</open>" + Environment.NewLine +

                                        "<Placemark>" + Environment.NewLine +
                                            "<name>" + Convert.ToString(Contents.Rows[i]["Site Name"]) + "</name>" + Environment.NewLine +

                                                "<description><![CDATA[" + Environment.NewLine +
                                                "<table  style='width:300px; background:#F08080'>" + Environment.NewLine +
                                                "<tr><td style='width:100px; padding: 5px;'>Site ID</td><td style='background:#FFC0CB; font-size: 90%;'>" + Convert.ToString(Contents.Rows[i]["Site ID"]) + "</td></tr>" + Environment.NewLine +
                                                "<tr><td style='width:100px; padding: 5px;'>Site Name</td><td style='background:#FFC0CB; font-size: 90%;'>" + Convert.ToString(Contents.Rows[i]["Site Name"]) + "</td></tr>" + Environment.NewLine +
                                                "<tr><td style='width:100px; padding: 5px;'>Site UID</td><td style='background:#FFC0CB; font-size: 90%;'>" + Convert.ToString(Contents.Rows[i]["Site UID"]) + "</td></tr>" + Environment.NewLine +
                                                "<tr><td style='width:100px; padding: 5px;'>Other Name</td><td style='background:#FFC0CB; font-size: 90%;'>" + Convert.ToString(Contents.Rows[i]["Site Name 2"]) + "</td></tr>" + Environment.NewLine +
                                                "<tr><td style='width:100px; padding: 5px;'>Longitude</td><td style='background:#FFC0CB; font-size: 90%;'>" + Convert.ToString(Contents.Rows[i]["Longitude"]) + "</td></tr>" + Environment.NewLine +
                                                "<tr><td style='width:100px; padding: 5px;'>Latitude</td><td style='background:#FFC0CB; font-size: 90%;'>" + Convert.ToString(Contents.Rows[i]["Latitude"]) + "</td></tr>" + Environment.NewLine +
                                                "</table>" + Environment.NewLine +
                                                "]]></description>" + Environment.NewLine +

                                            "<styleUrl>#m_ylw-pushpin</styleUrl>" + Environment.NewLine +
                                            "<Point>" + Environment.NewLine +
                                                "<gx:drawOrder>5</gx:drawOrder>" + Environment.NewLine +
                                                "<coordinates>" + Convert.ToString(Contents.Rows[i]["Longitude"]) + "," + Convert.ToString(Contents.Rows[i]["Latitude"]) + ",0</coordinates>" + Environment.NewLine +
                                             "</Point>" + Environment.NewLine +
                                        "</Placemark>" + Environment.NewLine;

                    Data += getSiteData(Convert.ToString(Contents.Rows[i]["Site ID"]), Convert.ToString(Contents.Rows[i]["Longitude"]), Convert.ToString(Contents.Rows[i]["Latitude"]));
                    Data += "</Folder>" + Environment.NewLine + Environment.NewLine;

                }
            }

                
            catch (Exception) { }
            this.Refresh();
            Thread.Sleep(100);
            this.Refresh();
            panel1.Visible = false;
            return Data;
        }

























        protected String getSiteData(String SiteID, String Log, String Lat)
        {
            String DData = ""; 
            DataTable DContents = new DataTable();
            try
            {
            DContents = getConnection("Select DISTINCT [BTS Name] From [Sectors$] WHERE [Site ID] = '" + SiteID + "'");
                for (int i = 0; i < DContents.Rows.Count; i++)
                {
                    DData += "" + Environment.NewLine + Environment.NewLine +
                    "<Folder>" + Environment.NewLine +
                    "<name>" + Convert.ToString(DContents.Rows[i]["BTS Name"]) + "</name>" + Environment.NewLine +
                    "<open>1</open>" + Environment.NewLine;

                     DData += createSiteTypeData(SiteID, Convert.ToString(DContents.Rows[i]["BTS Name"]), Log,Lat);
                    
                    DData += "</Folder>" + Environment.NewLine + Environment.NewLine;
                }
            }
            catch (Exception) { }
            return DData; 
        }










        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                TextFileDta = Dialog_Header + Dialog_Style;
                TextFileDta += Environment.NewLine + Environment.NewLine + getSiteName() + Environment.NewLine + Environment.NewLine + Dialog_Footer;
                textBox1.Text = TextFileDta;
            }
            catch (Exception) { }
        }


        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (!File.Exists(path))
                {
                    File.Create(path).Dispose();
                    using (TextWriter tw = new StreamWriter(path))
                    {
                        tw.Close();
                    }
                    using (StreamWriter outfile = new StreamWriter(path))
                    {
                        outfile.Write(TextFileDta);
                        MessageBox.Show("The kml file created successfully", "Google Kml File", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                }
                else
                {
                    using (StreamWriter outfile = new StreamWriter(path))
                    {
                        outfile.Write(TextFileDta);
                        MessageBox.Show("The kml file created successfully", "Google Kml File", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                }
            }
            catch (Exception) { }
        }


        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            try
            {
                using (StreamReader sr = File.OpenText(path))
                {
                    string s = "";
                    while ((s = sr.ReadLine()) != null)
                    {
                        textBox1.Text += s.ToString();
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Can not find the kmk file...!" + Environment.NewLine + "Create the kml file first.", "File Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void checkBox1_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true) {
                textBox1.ReadOnly = true;
            }
            if (checkBox1.Checked == false)
            {
                textBox1.ReadOnly = false;
            }
        }


        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutForm2 newForm = new AboutForm2();
            newForm.Show();
        }


        private void button4_Click(object sender, EventArgs e)
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
                    textBox2.Text = pathOfFile;
                    string[] arrAllFiles = choofdlog.FileNames;
                }
                button1.Enabled = true;
            }
            catch (Exception) { }
        }


        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                if (path == "")
                {
                    string dummyFileName = "Dialog";
                    SaveFileDialog sf = new SaveFileDialog();
                    sf.FileName = dummyFileName;
                    sf.Filter = "Google Earth Kml (*.kml*)|*.kml*";

                    if (sf.ShowDialog() == DialogResult.OK)
                    {
                        path = sf.FileName + ".kml";
                        ImagePath = Path.GetDirectoryName(sf.FileName) + "\\data\\dialog_image_tower.png";
                    }
                }
                if (!File.Exists(path))
                {
                    File.Create(path).Dispose();
                    using (TextWriter tw = new StreamWriter(path))
                    {
                        tw.Close();
                    }
                    using (StreamWriter outfile = new StreamWriter(path))
                    {
                        outfile.Write(TextFileDta);
                        MessageBox.Show("The kml file created successfully", "Google Kml File", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                }
                else
                {
                    using (StreamWriter outfile = new StreamWriter(path))
                    {
                        outfile.Write(TextFileDta);
                        MessageBox.Show("The kml file created successfully", "Google Kml File", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                }
            }
            catch (Exception) { }
        }


        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (path == "")
                {
                    string dummyFileName = "Dialog";
                    SaveFileDialog sf = new SaveFileDialog();
                    sf.FileName = dummyFileName;
                    sf.Filter = "Google Earth Kml (*.kml*)|*.kml*";

                    if (sf.ShowDialog() == DialogResult.OK)
                    {
                        path = sf.FileName + ".kml";
                        ImagePath = Path.GetDirectoryName(sf.FileName) + "\\data\\dialog_image_tower.png";
                    }
                }
                else
                {
                    string dummyFileName = "Dialog";
                    SaveFileDialog sf = new SaveFileDialog();
                    sf.FileName = dummyFileName;
                    sf.Filter = "Google Earth Kml (*.kml*)|*.kml*";

                    if (sf.ShowDialog() == DialogResult.OK)
                    {
                        path = sf.FileName + ".kml";
                        ImagePath = Path.GetDirectoryName(sf.FileName) + "\\data\\dialog_image_tower.png";
                    }
                }
            }
            catch (Exception) { }
        }


        private void exitToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void editDistanceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormLength fml = new FormLength();
            fml.Show();

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            FormComplan fml = new FormComplan();
            fml.Show();
        }

    }
}
