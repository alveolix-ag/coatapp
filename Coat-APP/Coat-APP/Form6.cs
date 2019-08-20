using System;
using System.Drawing;
using System.Windows.Forms;

namespace OT_APP1
{
    public partial class Form6 : Form
    {
        public Form6()
        {
            InitializeComponent();
        }

        private void PictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void PictureBox7_Click(object sender, EventArgs e)
        {

        }

        private void PictureBox10_Click(object sender, EventArgs e)
        {

        }

        private void PictureBox1_Click_1(object sender, EventArgs e)
        {
            Form5 f5 = new Form5();
            bool ImportState = new bool();
            if (f5.ShowDialog() == DialogResult.OK)
            {
                ImportState = f5.import;
                if (ImportState == true)
                {
                    picSlot1.Image = Properties.Resources.Panel;
                    picSlot1.Paint += new PaintEventHandler((sender1, ee) =>
                    {
                        ee.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

                        string text = f5.DisplayName;

                        SizeF textSize = ee.Graphics.MeasureString(text, Font);
                        PointF locationToDraw = new PointF();
                        locationToDraw.X = (picSlot1.Width / 2) - (textSize.Width / 2);
                        locationToDraw.Y = (picSlot1.Height / 2) - (textSize.Height / 2);

                        ee.Graphics.DrawString(text, Font, Brushes.Black, locationToDraw);
                    });
                    picSlot1.Visible = true;
                    picSlot1.Refresh();
                }
            }

        }

        private void Form6_Load(object sender, EventArgs e)
        {

        }
    }
}
