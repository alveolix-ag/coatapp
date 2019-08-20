using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;



namespace OT_APP1
{
    public partial class Form4 : Form
    {
        public string IPAdress { get; set; }
        public List<string> dirName = null;
        public List<string> Labdir = null;
        public Form4()
        {
            InitializeComponent();
        }

        private void BtnOK_Click(object sender, EventArgs e)
        {
            var configVals = new List<string> { numRows.Value.ToString(), numCol.Value.ToString(), boxWellShape.SelectedItem.ToString(), txtBrand.Text.ToString(), txtBrandId.Text.ToString(), "None", txtLabwareName.Text.ToString(), "wellPlate", "\\u00b5L", "None", numPlateLength.Value.ToString(), numPlateWidth.Value.ToString(), numPlateHeight.Value.ToString(), numColOffset.Value.ToString(), numRowOffset.Value.ToString(), numColSpace.Value.ToString(), numRowSpace.Value.ToString(), numWellDim.Value.ToString(), numWellHeight.Value.ToString(), numTotalWellVol.Value.ToString() };
            string docPath = Path.Combine(Directory.GetCurrentDirectory(), "Labware");
            using (StreamWriter outputFile = new StreamWriter(Path.Combine(docPath, "labware_val.csv")))
            {
                foreach (var item in configVals)
                {
                    outputFile.WriteLine(item);
                }
            }
            Console.WriteLine(docPath);
            CreateLabware();
            UploadLabware();
            this.Close();
        }
        private void CreateLabware()
        {
            string myPythonApp = Path.Combine(Directory.GetCurrentDirectory(), "Labware\\LabwareDesigner.py");
            string configfilePath = Path.Combine(Directory.GetCurrentDirectory(), "Labware\\");
            ProcessStartInfo pythonProcess = new ProcessStartInfo();
            pythonProcess.UseShellExecute = false;
            pythonProcess.RedirectStandardOutput = true;
            pythonProcess.RedirectStandardError = true;
            pythonProcess.CreateNoWindow = true;
            pythonProcess.FileName = "C:\\ProgramData\\Anaconda3\\python.exe";
            pythonProcess.Arguments = myPythonApp + " -p " + configfilePath.ToString();
            Console.WriteLine(myPythonApp + " -p " + configfilePath.ToString());
            Process.Start(pythonProcess);
        }

        private void UploadLabware()
        {
            string sourceDir = Path.Combine(Directory.GetCurrentDirectory(), "Labware\\");
            try
            {
                string[] picList = Directory.GetFileSystemEntries(sourceDir, "*", SearchOption.AllDirectories);
                string[] txtList = Directory.GetFiles(sourceDir, "*.txt");
                List<string> labwareDir = new List<string>();
                List<string> folderlabware = new List<string>();
                List<string> finalpath = new List<string>();

                foreach (string value in picList)
                {
                    if (value.Contains("1.json"))
                    {
                        string directPath = Path.GetDirectoryName(value);
                        string foldername = new DirectoryInfo(directPath).Name;
                        folderlabware.Add(foldername.ToString());
                        labwareDir.Add(directPath.ToString());
                        finalpath.Add(value);

                    }
                }
                this.dirName = folderlabware;
                this.Labdir = finalpath;
                foreach (string d in this.dirName)
                {
                    Console.WriteLine(d);
                }
                List<string> labwareHist = new List<string>();
                List<string> dirtoCopy = new List<string>();
                foreach (string hist in labwareDir)
                {
                    if (labwareHist.Contains(hist) == false)
                    {
                        dirtoCopy.Add(hist.ToString());
                    }
                }
            }
            catch
            {

            }
            ConnectionInfo connectionInfo = new PasswordConnectionInfo(this.IPAdress, "root", "");

            string remoteDirectory = @"/data/packages/usr/local/lib/python3.6/site-packages/opentrons/shared_data/labware/definitions/2/";

            using (SftpClient sftp = new SftpClient(connectionInfo))
            {
                try
                {
                    sftp.Connect();
                    var files = sftp.ListDirectory(remoteDirectory);
                    List<string> curLabware = new List<string>();

                    foreach (var file in files)
                    {
                        curLabware.Add(file.Name);
                    }
                    foreach (string folder in this.Labdir)
                    {
                        string foldername = new DirectoryInfo(Path.GetDirectoryName(folder)).Name;
                        if (curLabware.Contains(foldername) == false)
                        {
                            sftp.CreateDirectory("/data/packages/usr/local/lib/python3.6/site-packages/opentrons/shared_data/labware/definitions/2/" + foldername.ToString());
                        }
                        using (var localfile = File.OpenRead(folder))
                        {
                            string pathtofile = ("/data/packages/usr/local/lib/python3.6/site-packages/opentrons/shared_data/labware/definitions/2/" + foldername.ToString() + "/1.json");
                            sftp.BufferSize = 4 * 1024;
                            sftp.UploadFile(localfile, pathtofile, true);
                        }
                    }

                    sftp.Disconnect();
                }
                catch (Exception er)
                {
                    Console.WriteLine("An exception has been caught " + er.ToString());
                }
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Form5 f5 = new Form5();
            bool ImportState = new bool();
            if (f5.ShowDialog() == DialogResult.OK)
            {
                ImportState = f5.import;
                if (ImportState == true)
                {
                    txtLabwareName.Text = f5.DisplayName;
                    txtBrand.Text = f5.Brand;
                    txtBrandId.Text = f5.BrandId;
                    numPlateLength.Value = Convert.ToDecimal(f5.XDimension);
                    numPlateWidth.Value = Convert.ToDecimal(f5.YDimension);
                    numPlateHeight.Value = Convert.ToDecimal(f5.ZDimension);
                    numWellDim.Value = Convert.ToDecimal(f5.Diameter);
                    numWellHeight.Value = Convert.ToDecimal(f5.Depth);
                    numTotalWellVol.Value = Convert.ToDecimal(f5.TotalLiquidVolume);
                    numColOffset.Value = Convert.ToDecimal(f5.ColOffset);
                    numRowOffset.Value = Convert.ToDecimal(f5.RowOffset);
                    numColOffset.Value = Convert.ToDecimal(f5.ColOffset);
                    numColSpace.Value = Convert.ToDecimal(f5.ColSpace);
                    numRowSpace.Value = Convert.ToDecimal(f5.RowSpace);
                    numCol.Value  = f5.Cols;
                    numRows.Value = f5.Rows;
                    if (f5.Shape == "circular")
                    {
                        boxWellShape.SelectedIndex = 0;
                    }
                    else
                    {
                        boxWellShape.SelectedIndex = 1;
                    }
                    
                }
            }

        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form4_Load(object sender, EventArgs e)
        {

        }

        private void Form4_Load_1(object sender, EventArgs e)
        {

        }
    }
}
