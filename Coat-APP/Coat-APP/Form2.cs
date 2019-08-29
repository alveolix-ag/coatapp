using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Diagnostics;
using System.IO.Ports;
using Renci.SshNet;
using System.IO;

namespace OT_APP1
{
    public partial class Form2 : Form
    {
        public Main form1;
        public ShellStream shell1 = null;

        public Form2(ShellStream shellStreamSHH)
        {
            InitializeComponent();
            this.shell1 = shellStreamSHH;
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }
        private void ListWells_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int select_true = listWells.SelectedItems.Count;
                if (select_true > 0)
                {
                    var currentwell = listWells.SelectedItems[0].Text;
                    if (currentwell != null)
                    {
                        txtCuWell.Text = currentwell.ToString();
                    }
                }
            }
            catch (Exception exp)
            {

            }
        }

        private void BtnResetTip_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtCuWell.Text != "")
                {
                    this.shell1.Write("cd /data/coatapp/protocols/" + "\n");
                    this.shell1.Write("python3 reset_tip.py -w " + txtCuWell.Text + " \n");
                    this.shell1.Flush();
                    Properties.Settings.Default.ResetState = true;
                    Properties.Settings.Default.CurrentTip = txtCuWell.Text;
                }
                else
                {

                    Properties.Settings.Default.ResetState = false;
                }
                Properties.Settings.Default.Save();
            }
            catch (Exception exp)
            {

            }
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (txtCuWell.Text == "")
            {
                Properties.Settings.Default.ResetState = false;
                Properties.Settings.Default.Save();
            }
        }
    }
}
