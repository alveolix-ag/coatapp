﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace OT_APP1
{
    public partial class Main : Form
    {
        private SshClient sshClient = null;
        public ShellStream shellStreamSSH = null;
        private Process myProcess = null;
        private Process ProcessSocketProtocols = null;
        private string ServerOutput = null;
        public Form2 f2 = null;
        public string OT2IP = null;
        public string ProtocolPath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName, "Protocols\\host_socket_test1.py");
        private string oldString = string.Empty;
        public event PropertyChangedEventHandler PropertyChanged;
        private bool socketserverstop = false;
        public string SSHmessage = null;
        public Socket listener = null;


        public Main()
        {
            InitializeComponent();
        }


        private void Main_Load(object sender, EventArgs e)
        {
            System.Threading.ThreadStart threadStart = new System.Threading.ThreadStart(recvSSHData);
            System.Threading.Thread thread = new System.Threading.Thread(threadStart);
            findOT2IP();
            thread.IsBackground = true;
            thread.Start();

            btnRotate.Enabled = false;
            menuStrip1.Enabled = true;

        }

        private void BtnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                this.sshClient = new SshClient(this.OT2IP, "root");
                this.sshClient.ConnectionInfo.Timeout = TimeSpan.FromSeconds(120);
                this.sshClient.Connect();

                this.shellStreamSSH = this.sshClient.CreateShellStream("vt100", 80, 60, 800, 600, 65536);
                this.lbStatus.Text = "Status: Connected.";
                btnConnect.BackColor = Color.Green;
                grpControl.Visible = true;
            }
            catch (Exception exp)
            {
                this.lbStatus.Text = "Status: Disconnected.";
                MessageBox.Show("ERROR: " + exp.Message);
            }
            try
            {
                this.shellStreamSSH.Write("cd /data/coatapp/protocols" + ";\n");
                this.shellStreamSSH.Write("python3 ip_connection.py -ip " + GetLocalIPAddress() + " \r");
                this.shellStreamSSH.Flush();

                txtCommand.Text = "";
                txtCommand.Focus();
            }
            catch (Exception exp)
            {
                throw new Exception("ERROR");
            }
            try
            {
                Properties.Settings.Default.PropertyChanged += SettingChanged;
                StartServerShort();
                ListenServer();
                this.shellStreamSSH.Write("cd /data/coatapp/protocols" + ";\n");
                this.shellStreamSSH.Write("python3 current_tip.py" + " \r");
                this.shellStreamSSH.Flush();
            }
            catch (Exception exp)
            {
                throw new Exception("ERROR");
            }

        }

        private void recvSSHData()
        {
            while (true)
            {
                try
                {
                    if (this.shellStreamSSH != null && this.shellStreamSSH.DataAvailable)
                    {
                        String strData = this.shellStreamSSH.Read();
                        strData = strData.Replace("1;34m", "");
                        strData = strData.Replace("0m", "");
                        appendTextBoxInThread(txtSSHConsole, strData);
                        this.SSHmessage += String.Join("/n", strData) ;
                    }
                }
                catch
                {

                }
                Thread.Sleep(200);

            }
        }

        private void appendTextBoxInThread(TextBox t, String s)
        {
            if (t.InvokeRequired)
            {
                t.Invoke(new Action<TextBox, string>(appendTextBoxInThread), new object[] { t, s });

            }
            else
            {
                t.AppendText(s);
            }
        }

        private void TxtCommand_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == '\r')
                {
                    this.shellStreamSSH.Write(txtCommand.Text + "\r");
                    this.shellStreamSSH.Flush();

                    txtCommand.Text = "";
                    txtCommand.Focus();
                }
            }
            catch (Exception exp)
            {

            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.WashSettings.Clear();
            Properties.Settings.Default.CoatSettings.Clear();
            Properties.Settings.Default.Save();
            Environment.Exit(Environment.ExitCode);
            try
            {
                Properties.Settings.Default.CoatSettings.Clear();
                Properties.Settings.Default.WashSettings.Clear();
                this.shellStreamSSH.Close();
                this.sshClient.Disconnect();

            }
            catch
            {

            }
        }


        private void BtnCoat_Click(object sender, EventArgs e)
        {
            try
            {
                System.Collections.Specialized.StringCollection advancedLines = Properties.Settings.Default.CoatSettings;
                if (advancedLines.Count > 0)
                {
                    List<string> varwell = new List<string>();
                    List<string> varvolume = new List<string>();
                    List<string> varZ = new List<string>();
                    for (int i = 0; i < advancedLines.Count; i++)
                    {
                        string contentAd = advancedLines[i];
                        string sep = "\t";
                        string[] splitContent = contentAd.Split(sep.ToCharArray());
                        varwell.Add(splitContent[0].ToString());
                        varvolume.Add(splitContent[1].ToString());
                        varZ.Add(splitContent[2].ToString());
                    }
                    string varwellS = string.Join(" ", varwell.ToArray());
                    string varvolumeS = string.Join(" ", varvolume.ToArray());
                    string varZS = string.Join(" ", varZ.ToArray());

                    this.shellStreamSSH.Write("cd /data/coatapp/protocols" + "\n");
                    this.shellStreamSSH.Write("python3 chip_coating_rotator_arg.py " + SpeedSel.SelectedIndex + " " + numChips.Value + " " + SideSel.SelectedIndex + " 1 " + " -w " + varwellS.ToString() + " -v " + varvolumeS.ToString() + " -z " + varZS.ToString() + "\n");
                    this.shellStreamSSH.Flush();
                }
                else
                {

                    Rotate("1");
                    if (SideSel.SelectedIndex == 1)
                    {
                        Rotate("2");
                    }
                    if (SideSel.SelectedIndex == 2)
                    {
                        ListenServer();
                    }
                    this.shellStreamSSH.Write("cd /data/coatapp/protocols" + "\n");
                    this.shellStreamSSH.Write("python3 chip_coating_rotator_arg.py " + SpeedSel.SelectedIndex + " " + numChips.Value + " " + SideSel.SelectedIndex + " 0 " + "\n");
                    this.shellStreamSSH.Flush();

                    if (this.ServerOutput != null)
                    {
                        Console.WriteLine(this.ServerOutput);
                    }

                    txtCommand.Text = "";
                    txtCommand.Focus();
                }
            }
            catch (Exception exp)
            {

            }
        }

        private void BtnStop_Click(object sender, EventArgs e)
        {
            try
            {
                this.shellStreamSSH.Write("\x03");
            }
            catch
            {

            }
        }

        private void BtnWash_Click(object sender, EventArgs e)
        {
            try
            {
                System.Collections.Specialized.StringCollection AdvancedWashSet = Properties.Settings.Default.WashSettings;
                if (AdvancedWashSet.Count > 0)
                {
                    List<string> varwell = new List<string>();
                    List<string> varvolume = new List<string>();
                    List<string> varZ = new List<string>();
                    List<string> varFlow = new List<string>();
                    List<string> varAsp = new List<string>();
                    List<string> varWash = new List<string>();
                    List<string> varVolWash = new List<string>();
                    List<string> varMix = new List<string>();
                    List<string> varDry = new List<string>();
                    for (int i = 0; i < AdvancedWashSet.Count; i++)
                    {
                        string contentAd = AdvancedWashSet[i];
                        string sep = "\t";
                        string[] splitContent = contentAd.Split(sep.ToCharArray());
                        varwell.Add(splitContent[0].ToString());
                        varvolume.Add(splitContent[1].ToString());
                        varZ.Add(splitContent[2].ToString());
                        varFlow.Add(splitContent[3].ToString());
                        varAsp.Add(splitContent[4].ToString());
                        varWash.Add(splitContent[5].ToString());
                        varVolWash.Add(splitContent[6].ToString());
                        varMix.Add(splitContent[7].ToString());
                        varDry.Add(splitContent[8].ToString());
                    }
                    string varwellS = string.Join(" ", varwell.ToArray());
                    string varvolumeS = string.Join(" ", varvolume.ToArray());
                    string varZS = string.Join(" ", varZ.ToArray());
                    string varFlowS = string.Join(" ", varFlow.ToArray());
                    string varAspS = string.Join(" ", varAsp.ToArray());
                    string varWashS = string.Join(" ", varWash.ToArray());
                    string varVolWashS = string.Join(" ", varVolWash.ToArray());
                    string varMixS = string.Join(" ", varMix.ToArray());
                    string varDryS = string.Join(" ", varDry.ToArray());

                    this.shellStreamSSH.Write("cd /data/coatapp/protocols" + "\n");
                    this.shellStreamSSH.Write("python3 chip_washing_rotator_arg.py " + SpeedSel.SelectedIndex + " " + numChips.Value + " " + SideSel.SelectedIndex + " " + numWash.Value + " 1 " + " -w " + varwellS.ToString() + " -va " + varvolumeS.ToString() + " -z " + varZS.ToString() + " -f " + varFlowS.ToString() + " -na " + varAspS.ToString() + " -nw " + varWashS.ToString() + " -vw " + varVolWashS.ToString() + " -nm " + varMixS.ToString() + " -nd " + varDryS.ToString() + "\n");
                    this.shellStreamSSH.Flush();
                }
                else
                {
                    this.shellStreamSSH.Write("cd /data/coatapp/protocols" + ";\n");
                    this.shellStreamSSH.Write("python3 chip_washing_rotator_arg.py " + SpeedSel.SelectedIndex + " " + numChips.Value + " " + SideSel.SelectedIndex + " " + numWash.Value + " 0 " + ";\n");
                    this.shellStreamSSH.Flush();
                }

                txtCommand.Text = "";
                txtCommand.Focus();
            }
            catch (Exception exp)
            {

            }
            try
            {
                this.shellStreamSSH.Write("cd /data/coatapp/protocols" + ";\n");
                this.shellStreamSSH.Write("python3 current_tip.py" + " \r");
                this.shellStreamSSH.Flush();
                ListenServer();

            }
            catch (Exception exp)
            {

            }
        }

        private void SideSel_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SideSel.SelectedIndex > 0 && SpeedSel.SelectedIndex > 0)
            {
                //btnCoat.Enabled = true;
                //btnWash.Enabled = true;
            }
        }

        private void BtnHome_Click(object sender, EventArgs e)
        {
            try
            {
                this.shellStreamSSH.Write("cd /data/user_storage/opentrons_data/jupyter" + ";\n");
                this.shellStreamSSH.Write("python3 home.py" + ";\r\n");
                this.shellStreamSSH.Flush();

                txtCommand.Text = "";
                txtCommand.Focus();
            }
            catch (Exception exp)
            {

            }
        }

        private void BtnDrop300_Click(object sender, EventArgs e)
        {
            try
            {
                this.shellStreamSSH.Write("cd /data/user_storage/opentrons_data/jupyter" + ";\n");
                this.shellStreamSSH.Write("python3 drop_300_tip.py" + ";\n");
                this.shellStreamSSH.Flush();

                txtCommand.Text = "";
                txtCommand.Focus();
            }
            catch (Exception exp)
            {

            }
        }

        private void BtnDrop50_Click(object sender, EventArgs e)
        {
            try
            {
                this.shellStreamSSH.Write("cd /data/user_storage/opentrons_data/jupyter" + ";\n");
                this.shellStreamSSH.Write("python3 drop_50_tip.py" + ";\n");
                this.shellStreamSSH.Flush();

                txtCommand.Text = "";
                txtCommand.Focus();
            }
            catch (Exception exp)
            {

            }
        }

        private void BtnTipSet_Click(object sender, EventArgs e)
        {
            this.f2 = new Form2(this.shellStreamSSH);
            this.f2.ShowDialog();
        }

        private void BtnCalibrate_Click(object sender, EventArgs e)
        {
            try
            {
                this.shellStreamSSH.Write("cd /data/coatapp/protocols" + "\n");
                if (boxCalOffset.Checked == true)
                {
                    this.shellStreamSSH.Write("python3 calibrate_pro1.py -o 1" + "\r");
                }
                else
                {
                    this.shellStreamSSH.Write("python3 calibrate_pro1.py -o 0" + "\r");
                }
                this.shellStreamSSH.Flush();

                txtCommand.Text = "";
                txtCommand.Focus();
            }
            catch (Exception exp)
            {

            }
        }


        private void Btn_Advanced_Click(object sender, EventArgs e)
        {
            Form3 f3 = new Form3();
            f3.ShowDialog();
        }

        private void BtnSaveCalibration_Click(object sender, EventArgs e)
        {
            try
            {
                this.shellStreamSSH.Write("0 0 1" + "\n");
                this.shellStreamSSH.Flush();

                txtCommand.Text = "";
                txtCommand.Focus();
            }
            catch
            {

            }
        }

        private void BtnUp_Click(object sender, EventArgs e)
        {
            try
            {
                if (chkSmall.Checked == true)
                {
                    this.shellStreamSSH.Write("z 0.1 0" + "\r");
                }
                else if (chkMed.Checked == true)
                {
                    this.shellStreamSSH.Write("z 1 0" + "\r");
                }
                else if (chkLarge.Checked == true)
                {
                    this.shellStreamSSH.Write("z 10 0" + "\r");
                }
                this.shellStreamSSH.Flush();

                txtCommand.Text = "";
                txtCommand.Focus();
            }
            catch
            {

            }
        }

        private void BtnDown_Click(object sender, EventArgs e)
        {
            try
            {
                if (chkSmall.Checked == true)
                {
                    this.shellStreamSSH.Write("z -0.1 0" + "\r");
                }
                else if (chkMed.Checked == true)
                {
                    this.shellStreamSSH.Write("z -1 0" + "\r");
                }
                else if (chkLarge.Checked == true)
                {
                    this.shellStreamSSH.Write("z -10 0" + "\r");
                }
                this.shellStreamSSH.Flush();

                txtCommand.Text = "";
                txtCommand.Focus();
            }
            catch
            {

            }
        }

        private void ChkSmall_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSmall.Checked == true)
            {
                chkMed.Checked = false;
                chkLarge.Checked = false;
            }
        }

        private void ChkMed_CheckedChanged(object sender, EventArgs e)
        {
            if (chkMed.Checked == true)
            {
                chkSmall.Checked = false;
                chkLarge.Checked = false;
            }
        }

        private void ChkLarge_CheckedChanged(object sender, EventArgs e)
        {
            if (chkLarge.Checked == true)
            {
                chkMed.Checked = false;
                chkSmall.Checked = false;
            }
        }

        public static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("No network adapters with an IPv4 address in the system!");
        }

        private void BtnTest2_Click(object sender, EventArgs e)
        {
            Form4 f4 = new Form4();
            if (OT2IP != null)
            {
                f4.IPAdress = OT2IP.ToString();
            }
            f4.ShowDialog();
        }

        private void BtnRotate_Click(object sender, EventArgs e)
        {
            try
            {
                SerialPort myport = new SerialPort();
                myport.BaudRate = 9600;
                myport.PortName = "COM3";
                if (!myport.IsOpen == true)
                {
                    myport.Open();
                }
                if (boxRotateOptions.SelectedIndex == 1)
                {
                    myport.WriteLine("1");
                }
                else if (boxRotateOptions.SelectedIndex == 2)
                {
                    myport.WriteLine("2");
                }
                myport.Close();
            }
            catch
            {
                MessageBox.Show("ERROR: " + "Please Ensure that the Rotator is connected to the right port (COM3)");
            }

        }


        public void Rotate(String RotateSt)
        {

            SerialPort myport = new SerialPort();
            myport.BaudRate = 9600;
            myport.PortName = "COM3";
            if (!myport.IsOpen == true)
            {
                myport.Open();
            }
            myport.WriteLine("1");
            System.Threading.Thread.Sleep(3000);
            if (RotateSt == "2")
            {
                myport.WriteLine("2");
            }
            myport.Close();
        }

        private void UpdateGit_Click(object sender, EventArgs e)
        {
            try
            {
                this.SSHmessage = null;
                this.shellStreamSSH.Write("cd /data/coatapp/bin" + ";\n");
                this.shellStreamSSH.Write("./update_protocols" + " \r");
                this.shellStreamSSH.Flush();
                Console.Write(this.SSHmessage);
                if (this.SSHmessage.Contains("-ash"))
                {
                    Console.WriteLine("Nothing");
                }
                else if ((this.SSHmessage.Contains("remote")))
                {
                    Console.Write("remote");
                }
                else
                {
                    Console.Write("Nothing to update");
                }
        
            }
            catch 
            {

            }
        }

        private void findOT2IP()
        {
            var appdata = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

            ProcessStartInfo myProcessStartInfo = new ProcessStartInfo();
            //myProcessStartInfo.UseShellExecute = false;
            myProcessStartInfo.RedirectStandardOutput = false;
            myProcessStartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            myProcessStartInfo.RedirectStandardInput = false;
            myProcessStartInfo.CreateNoWindow = true;

            myProcessStartInfo.FileName = (appdata + "\\Programs\\@opentronsapp-shell\\Opentrons.exe");
            var thread = new Thread(() =>
            {
                using (Process opentronsIN = Process.Start(myProcessStartInfo))
                {
                    Thread.Sleep(15000);
                    //opentronsIN.CloseMainWindow();
                    opentronsIN.Close();
                }
            })
            { IsBackground = true };

            try
            {
                Process[] pname = Process.GetProcessesByName("opentrons");
                if (pname.Length > 0)
                {
                    //Opentrons App running no need to start process
                }
                else
                {
                    // Opentrons App Not runninG
                    thread.Start();
                }
            }
            catch
            {
                MessageBox.Show("ERROR: " + "Could not find OT2 robot, please reconnect or input IP address manually");
            }
            Thread.Sleep(8000);

            try
            {
                int counter = 0;
                string line;
                string currentIP = null;
                string resultString = null;
                bool breakFlag = false;
                this.OT2IP = null;

                // Read the file and display it line by line. 
                //var appdata = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                var fileName = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData).ToString() + "\\Opentrons\\discovery.json";
                StreamReader file = new StreamReader(fileName);
                for (int i = 0; i < 3; i++)
                {
                    while ((line = file.ReadLine()) != null)
                    {
                        if (line.Contains("\"ip\""))
                        {
                            resultString = line;
                            currentIP = Regex.Match(resultString, @"(\d+\.?\d+\.?\d+\.?\d+)").Value;
                        }

                        if (line.Contains("\"advertising\": true"))
                        {
                            this.OT2IP = currentIP;
                            breakFlag = true;
                            break;
                        }
                        counter++;
                    }
                    if (breakFlag == true)
                    {
                        break;
                    }
                    Thread.Sleep(1000);
                }
                file.Close();
                if (breakFlag == false)
                {
                    MessageBox.Show("ERROR: " + "Could not find OT2 robot, please reconnect or input IP address manually 1");
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show("ERROR: " + "Could not find OT2 robot, please reconnect or input IP address manually 2");
            }
            thread.Abort();
        }

        private void GetOT2IPToolStripMenuItem_Click(object sender, EventArgs e)
        {
            findOT2IP();
        }

        private void BtnYUp_Click(object sender, EventArgs e)
        {
            try
            {
                if (chkSmall.Checked == true)
                {
                    this.shellStreamSSH.Write("y 0.1 0" + "\r");
                }
                else if (chkMed.Checked == true)
                {
                    this.shellStreamSSH.Write("y 1.0 0" + "\r");
                }
                else if (chkLarge.Checked == true)
                {
                    this.shellStreamSSH.Write("y 10.0 0" + "\r");
                }
                this.shellStreamSSH.Flush();

                txtCommand.Text = "";
                txtCommand.Focus();
            }
            catch
            {

            }
        }

        private void BtnYDown_Click(object sender, EventArgs e)
        {
            try
            {
                if (chkSmall.Checked == true)
                {
                    this.shellStreamSSH.Write("y -0.1 0" + "\r");
                }
                else if (chkMed.Checked == true)
                {
                    this.shellStreamSSH.Write("y -1.0 0" + "\r");
                }
                else if (chkLarge.Checked == true)
                {
                    this.shellStreamSSH.Write("y -10.0 0" + "\r");
                }
                this.shellStreamSSH.Flush();

                txtCommand.Text = "";
                txtCommand.Focus();
            }
            catch
            {

            }
        }

        private void BtnXDown_Click(object sender, EventArgs e)
        {
            try
            {
                if (chkSmall.Checked == true)
                {
                    this.shellStreamSSH.Write("x -0.1 0" + "\r");
                }
                else if (chkMed.Checked == true)
                {
                    this.shellStreamSSH.Write("x -1.0 0" + "\r");
                }
                else if (chkLarge.Checked == true)
                {
                    this.shellStreamSSH.Write("x -10.0 0" + "\r");
                }
                this.shellStreamSSH.Flush();

                txtCommand.Text = "";
                txtCommand.Focus();
            }
            catch
            {

            }
        }

        private void BtnXUp_Click(object sender, EventArgs e)
        {
            try
            {
                if (chkSmall.Checked == true)
                {
                    this.shellStreamSSH.Write("x 0.1 0" + "\r");
                }
                else if (chkMed.Checked == true)
                {
                    this.shellStreamSSH.Write("x 1.0 0" + "\r");
                }
                else if (chkLarge.Checked == true)
                {
                    this.shellStreamSSH.Write("x 10.0 0" + "\r");
                }
                this.shellStreamSSH.Flush();

                txtCommand.Text = "";
                txtCommand.Focus();
            }
            catch
            {

            }
        }


        private void Button2_Click(object sender, EventArgs e)
        {
            try
            {
                this.shellStreamSSH.Write("0 0 2" + "\n");
                this.shellStreamSSH.Flush();

                txtCommand.Text = "";
                txtCommand.Focus();
            }
            catch
            {

            }
        }

        private void btnLabwareSetup_Click(object sender, EventArgs e)
        {
            Form6 f6 = new Form6();
            if (OT2IP != null)
            {
                f6.IPAdress = OT2IP.ToString();
            }
            f6.ShowDialog();
        }

        private void ChkLight_CheckedChanged(object sender, EventArgs e)
        {
            if (chkLight.Checked == true)
            {
                try
                {
                    this.shellStreamSSH.Write("cd /data/coatapp/protocols \n");
                    this.shellStreamSSH.Write("python3 switch_lights.py -o ON \n");
                    this.shellStreamSSH.Flush();

                    txtCommand.Text = "";
                    txtCommand.Focus();
                }
                catch
                {

                }
            }
            else
            {

                try
                {
                    this.shellStreamSSH.Write("cd /data/coatapp/protocols \n");
                    this.shellStreamSSH.Write("python3 switch_lights.py -o OFF \n");
                    this.shellStreamSSH.Flush();

                    txtCommand.Text = "";
                    txtCommand.Focus();
                }
                catch
                {

                }
            }
        }

        private void BtnCoat1_Click(object sender, EventArgs e)
        {
            try
            {
                Rotate("1");
                if (SideSel.SelectedIndex == 1)
                {
                    Rotate("2");
                }
                ListenServer();
                this.shellStreamSSH.Write("cd /data/coatapp/protocols" + "\n");
                this.shellStreamSSH.Write("python3 Initial_Coating_Protocol.py " + SpeedSel.SelectedIndex + " " + numChips.Value + " " + SideSel.SelectedIndex + " 0 " + "\n");
                this.shellStreamSSH.Flush();

                if (this.ServerOutput != null)
                {
                    Console.WriteLine(this.ServerOutput);
                }

                txtCommand.Text = "";
                txtCommand.Focus();

            }
            catch (Exception exp)
            {
                MessageBox.Show("ERROR: " + exp.ToString());
            }
        }

        private void Btntest_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.PropertyChanged += SettingChanged;
            ListenServer();

        }


        void SettingChanged(object sender, PropertyChangedEventArgs e)
        {
            SetText("Current Tip: " + Properties.Settings.Default.CurrentTip);
        }

        delegate void SetTextCallback(string text);

        private void SetText(string text)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.lblCurrentTip.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetText);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.lblCurrentTip.Text = text;
            }
        }



        public void StartServer()
        {
            var thread = new Thread(() =>
            {  // Get Host IP Address that is used to establish a connection  
               // In this case, we get one IP address of localhost that is IP : 127.0.0.1  
               // If a host has multiple addresses, you will get a list of addresses  
                IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
                IPAddress ipAddress = host.AddressList[2];
                IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 11000);
                try
                {

                    // Create a Socket that will use Tcp protocol      
                    Socket listener = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                    // A Socket must be associated with an endpoint using the Bind method 
                    listener.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, 1);
                    listener.Bind(localEndPoint);
                    // Specify how many requests a Socket can listen before it gives Server busy response.  
                    // We will listen 10 requests at a time  
                    listener.Listen(10000);

                    Console.WriteLine("Waiting for a connection...");
                    Socket handler = listener.Accept();
                    Console.WriteLine("Listening");
                    // Incoming data from the client.
                    string data = null;
                    byte[] bytes = null;

                    while (true)
                    {
                        bytes = new byte[1024];
                        int bytesRec = handler.Receive(bytes);
                        data = Encoding.ASCII.GetString(bytes, 0, bytesRec);
                        byte[] message = Encoding.ASCII.GetBytes("Test Server");
                        if (data.Contains("tip"))
                        {
                            if (IsValidJson(data) == true)
                            {
                                var obj = JsonConvert.DeserializeObject<dynamic>(data);
                                Properties.Settings.Default.CurrentTip = obj.tip;
                                Properties.Settings.Default.Save();

                            }
                            message = Encoding.ASCII.GetBytes("tip changed received");
                            handler.Send(message);
                            Console.WriteLine("Text received : {0}", data);

                        }
                        else if (data.Contains("rotate"))
                        {
                            Rotate("2");
                            message = Encoding.ASCII.GetBytes("Chip Rotated");
                            handler.Send(message);
                            Console.WriteLine("Text received : {0}", data);
                        }
                        else if (data.Contains("finish"))
                        {
                            byte[] msg = Encoding.ASCII.GetBytes("closing");
                            handler.Send(msg);
                            break;
                        }

                    }
                    Console.WriteLine("Text received : {0}", data);
                    // Send a message to Client  
                    // using Send() method 
                    handler.Shutdown(SocketShutdown.Both);
                    handler.Close();
                }
                catch (Exception e)
                {
                    MessageBox.Show("Host couldn't be started");
                    Console.WriteLine(e.ToString());
                }

            })
            { IsBackground = true };
            thread.Start();
        }

        public void StartServerShort()
        {
            var thread = new Thread(() =>
            {  // Get Host IP Address that is used to establish a connection  
               // In this case, we get one IP address of localhost that is IP : 127.0.0.1  
               // If a host has multiple addresses, you will get a list of addresses  
                IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
                IPAddress ipAddress = host.AddressList[2];
                IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 11000);
                try
                {

                    // Create a Socket that will use Tcp protocol      
                    this.listener = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                    // A Socket must be associated with an endpoint using the Bind method 
                    this.listener.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, 1);
                    this.listener.Bind(localEndPoint);
                    // Specify how many requests a Socket can listen before it gives Server busy response.  
                    // We will listen 10 requests at a time  
                }
                catch (Exception e)
                {
                    MessageBox.Show("Host couldn't be started");
                    Console.WriteLine(e.ToString());
                }

            })
            { IsBackground = true };
            thread.Start();
        }

        public void ListenServer()
        {
            var thread = new Thread(() =>
            {
                try
                {

                    this.listener.Listen(10000);
                    Console.WriteLine("Waiting for a connection...");
                    Socket handler = listener.Accept();
                    Console.WriteLine("Listening");
                    // Incoming data from the client.
                    string data = null;
                    byte[] bytes = null;

                    while (true)
                    {
                        bytes = new byte[1024];
                        int bytesRec = handler.Receive(bytes);
                        data = Encoding.ASCII.GetString(bytes, 0, bytesRec);
                        byte[] message = Encoding.ASCII.GetBytes("Test Server");
                        if (data.Contains("tip"))
                        {
                            if (IsValidJson(data) == true)
                            {
                                var obj = JsonConvert.DeserializeObject<dynamic>(data);
                                Properties.Settings.Default.CurrentTip = obj.tip;
                                Properties.Settings.Default.Save();

                            }
                            message = Encoding.ASCII.GetBytes("tip changed received");
                            handler.Send(message);
                            Console.WriteLine("Text received : {0}", data);

                        }
                        else if (data.Contains("rotate"))
                        {
                            Rotate("2");
                            message = Encoding.ASCII.GetBytes("Chip Rotated");
                            handler.Send(message);
                            Console.WriteLine("Text received : {0}", data);
                        }
                        else if (data.Contains("finish"))
                        {
                            byte[] msg = Encoding.ASCII.GetBytes("closing");
                            handler.Send(msg);
                            break;
                        }

                    }
                    Console.WriteLine("Text received : {0}", data);
                    // Send a message to Client  
                    // using Send() method 
                    handler.Shutdown(SocketShutdown.Both);
                    handler.Close();
                }
                catch (Exception e)
                {
                    MessageBox.Show("Host couldn't be started");
                    Console.WriteLine(e.ToString());
                }
            })
            { IsBackground = true };
            thread.Start();

        }

        private static bool IsValidJson(string strInput)
        {
            strInput = strInput.Trim();
            if ((strInput.StartsWith("{") && strInput.EndsWith("}")) || //For object
                (strInput.StartsWith("[") && strInput.EndsWith("]"))) //For array
            {
                try
                {
                    var obj = JToken.Parse(strInput);
                    return true;
                }
                catch (JsonReaderException jex)
                {
                    //Exception in parsing json
                    Console.WriteLine(jex.Message);
                    return false;
                }
                catch (Exception ex) //some other exception
                {
                    Console.WriteLine(ex.ToString());
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        private void Button12_Click(object sender, EventArgs e)
        {

        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnRotate.Enabled = true;
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            
        }

        private void numWash_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}


