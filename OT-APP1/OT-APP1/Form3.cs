using System;
using System.Windows.Forms;
//using System.Save;

namespace OT_APP1
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            System.Collections.Specialized.StringCollection fileLinesCoat = Properties.Settings.Default.CoatSettings;
            if (fileLinesCoat.Count > 0)
            {

                for (int i = 0; i < fileLinesCoat.Count; i++)
                {
                    string content = fileLinesCoat[i];
                    string sep = "\t";
                    string[] splitContent = content.Split(sep.ToCharArray());
                    ListViewItem splitCo = new ListViewItem(splitContent[0].ToString());
                    splitCo.SubItems.Add(splitContent[1].ToString());
                    splitCo.SubItems.Add(splitContent[2].ToString());
                    lstAdOptions.Items.Add(splitCo);
                }
            }
            System.Collections.Specialized.StringCollection fileLinesWash = Properties.Settings.Default.WashSettings;
            if (fileLinesWash.Count > 0)
            {

                for (int i = 0; i < fileLinesWash.Count; i++)
                {
                    string contentW = fileLinesWash[i];
                    string sep = "\t";
                    string[] splitContentW = contentW.Split(sep.ToCharArray());
                    ListViewItem splitCoW = new ListViewItem(splitContentW[0].ToString());
                    splitCoW.SubItems.Add(splitContentW[1].ToString());
                    splitCoW.SubItems.Add(splitContentW[2].ToString());
                    splitCoW.SubItems.Add(splitContentW[3].ToString());
                    splitCoW.SubItems.Add(splitContentW[4].ToString());
                    splitCoW.SubItems.Add(splitContentW[5].ToString());
                    splitCoW.SubItems.Add(splitContentW[6].ToString());
                    splitCoW.SubItems.Add(splitContentW[7].ToString());
                    splitCoW.SubItems.Add(splitContentW[8].ToString());
                    lstSettingsWash.Items.Add(splitCoW);
                }
            }
        }

        private void ListWell1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (listWell1.SelectedIndices.Count > 0)
                {
                    String currentwell1 = listWell1.SelectedItems[0].Text;
                    if (currentwell1 != null)
                    {
                        txt_advanced_well.Text = currentwell1.ToString();
                        txtAdWellWash.Text = currentwell1.ToString();
                    }
                }

            }
            catch
            {
                throw new Exception("ERROR");
            }

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                int select_true = lstAdOptions.SelectedItems.Count;
                if (select_true > 0)
                {
                    lstAdOptions.SelectedItems[0].SubItems[0].Text = txt_advanced_well.Text;
                    lstAdOptions.SelectedItems[0].SubItems[1].Text = numAdVol.Text;
                    lstAdOptions.SelectedItems[0].SubItems[2].Text = numAdZ.Text;
                    lstAdOptions.SelectedIndices.Clear();
                }
                else
                {
                    ListViewItem lvi = new ListViewItem(txt_advanced_well.Text);
                    lvi.SubItems.Add(numAdVol.Value.ToString());
                    lvi.SubItems.Add(numAdZ.Value.ToString());
                    lstAdOptions.Items.Add(lvi);
                }
            }
            catch
            {
                throw new Exception("ERROR");
            }
        }

        private void LstAdOptions_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (lstAdOptions.SelectedItems.Count > 0)
                {
                    txt_advanced_well.Text = lstAdOptions.SelectedItems[0].SubItems[0].Text;
                    numAdVol.Text = lstAdOptions.SelectedItems[0].SubItems[1].Text;
                    numAdZ.Text = lstAdOptions.SelectedItems[0].SubItems[2].Text;
                }
            }
            catch
            {
                throw new Exception("ERROR");
            }
        }

        private void BtnDel_Click(object sender, EventArgs e)
        {
            try
            {
                if (lstAdOptions.SelectedItems[0] != null)
                {
                    lstAdOptions.Items.Remove(lstAdOptions.SelectedItems[0]);
                }
            }
            catch
            {
                throw new Exception("ERROR");
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (lstAdOptions.Items.Count > 0)
            {
                System.Collections.Specialized.StringCollection CoatingOptions = new System.Collections.Specialized.StringCollection();
                foreach (ListViewItem item in lstAdOptions.Items)
                {
                    string txttoAdd = (item.SubItems[0].Text + "\t" + item.SubItems[1].Text + "\t" + item.SubItems[2].Text);
                    CoatingOptions.Add(txttoAdd);
                }
                Properties.Settings.Default.CoatSettings = CoatingOptions;
            }
            else if (lstAdOptions.Items.Count <= 0)
            {
                Properties.Settings.Default.CoatSettings.Clear();
            }

            if (lstSettingsWash.Items.Count > 0)
            {
                System.Collections.Specialized.StringCollection WashingOptions = new System.Collections.Specialized.StringCollection();
                foreach (ListViewItem item in lstSettingsWash.Items)
                {
                    string txttoAdd = (item.SubItems[0].Text + "\t" + item.SubItems[1].Text + "\t" + item.SubItems[2].Text + "\t" + item.SubItems[3].Text + "\t" + item.SubItems[4].Text + "\t" + item.SubItems[5].Text + "\t" + item.SubItems[6].Text + "\t" + item.SubItems[7].Text + "\t" + item.SubItems[8].Text);
                    WashingOptions.Add(txttoAdd);
                }
                Properties.Settings.Default.WashSettings = WashingOptions;
            }
            else if (lstSettingsWash.Items.Count <= 0)
            {
                Properties.Settings.Default.WashSettings.Clear();
            }
            Properties.Settings.Default.Save();
            this.Close();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnAddWash_Click(object sender, EventArgs e)
        {
            try
            {
                int select_true = lstSettingsWash.SelectedItems.Count;
                if (select_true > 0)
                {
                    lstSettingsWash.SelectedItems[0].SubItems[0].Text = txtAdWellWash.Text;
                    lstSettingsWash.SelectedItems[0].SubItems[1].Text = numAdVolWash.Text;
                    lstSettingsWash.SelectedItems[0].SubItems[2].Text = numAdWashZ.Text;
                    lstSettingsWash.SelectedItems[0].SubItems[3].Text = numFlowRate.Text;
                    lstSettingsWash.SelectedItems[0].SubItems[4].Text = numNumAsp.Text;
                    lstSettingsWash.SelectedItems[0].SubItems[5].Text = numWash.Text;
                    lstSettingsWash.SelectedItems[0].SubItems[6].Text = numVolWash.Text;
                    lstSettingsWash.SelectedItems[0].SubItems[7].Text = numMix.Text;
                    lstSettingsWash.SelectedItems[0].SubItems[8].Text = numDry.Text;

                    lstSettingsWash.SelectedIndices.Clear();
                }
                else
                {
                    ListViewItem lvi = new ListViewItem(txtAdWellWash.Text);
                    lvi.SubItems.Add(numAdVolWash.Value.ToString());
                    lvi.SubItems.Add(numAdWashZ.Value.ToString());
                    lvi.SubItems.Add(numFlowRate.Value.ToString());
                    lvi.SubItems.Add(numNumAsp.Value.ToString());
                    lvi.SubItems.Add(numWash.Value.ToString());
                    lvi.SubItems.Add(numVolWash.Value.ToString());
                    lvi.SubItems.Add(numMix.Value.ToString());
                    lvi.SubItems.Add(numDry.Value.ToString());
                    lstSettingsWash.Items.Add(lvi);
                }
            }
            catch
            {
                throw new Exception("ERROR");
            }
        }

        private void BtnDelWash_Click(object sender, EventArgs e)
        {
            try
            {
                if (lstSettingsWash.SelectedItems[0] != null)
                {
                    lstSettingsWash.Items.Remove(lstSettingsWash.SelectedItems[0]);
                }
            }
            catch
            {
                throw new Exception("ERROR");
            }

        }

        private void LstSettingsWash_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (lstSettingsWash.SelectedItems.Count > 0)
                {
                    txtAdWellWash.Text = lstSettingsWash.SelectedItems[0].SubItems[0].Text;
                    numAdVolWash.Text = lstSettingsWash.SelectedItems[0].SubItems[1].Text;
                    numAdWashZ.Text = lstSettingsWash.SelectedItems[0].SubItems[2].Text;
                    numFlowRate.Text = lstSettingsWash.SelectedItems[0].SubItems[3].Text;
                    numNumAsp.Text = lstSettingsWash.SelectedItems[0].SubItems[4].Text;
                    numWash.Text = lstSettingsWash.SelectedItems[0].SubItems[5].Text;
                    numVolWash.Text = lstSettingsWash.SelectedItems[0].SubItems[6].Text;
                    numMix.Text = lstSettingsWash.SelectedItems[0].SubItems[7].Text;
                    numDry.Text = lstSettingsWash.SelectedItems[0].SubItems[8].Text;
                }
            }
            catch
            {
                throw new Exception("ERROR");
            }

        }
    }
}
