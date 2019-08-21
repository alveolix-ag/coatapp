using Microsoft.CSharp;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace OT_APP1
{
    public partial class Form6 : Form
    {
        CSharpCodeProvider codeProvider = new CSharpCodeProvider();

        public string text1 = null;

        public Form6()
        {
            InitializeComponent();
        }

        private void picSlot1_Click_1(object sender, EventArgs e)
        {
            Form5 f5 = new Form5();
            bool ImportState = new bool();
            bool deleteState = new bool();
            if (f5.ShowDialog() == DialogResult.OK)
            {
                ImportState = f5.import;
                if (ImportState == true)
                {
                    this.text1 = f5.DisplayName;

                }
            }
            else if (f5.DialogResult == DialogResult.Cancel)
            {
                deleteState = f5.Delete;
                if (deleteState == true)
                {
                    deleteLabwareSaved(picSlot1);
                }
            }
            if (ImportState == true)
            {
                changeImageToLabware(picSlot1);
            }

        }

        private void PicSlot2_Click(object sender, EventArgs e)
        {
            Form5 f5 = new Form5();
            bool ImportState = new bool();
            bool deleteState = new bool();
            if (f5.ShowDialog() == DialogResult.OK)
            {
                ImportState = f5.import;
                if (ImportState == true)
                {
                    this.text1 = f5.DisplayName;

                }
            }
            else if (f5.DialogResult == DialogResult.Cancel)
            {
                deleteState = f5.Delete;
                if (deleteState == true)
                {
                    deleteLabwareSaved(picSlot2);
                }
            }
            if (ImportState == true)
            {
                changeImageToLabware(picSlot2);
            }
        }



        private void changeImageToLabware(PictureBox pictodraw)
        {
            pictodraw.Image = Properties.Resources.Panel;
            pictodraw.Refresh();
            Bitmap bitmap = new Bitmap(pictodraw.Width, pictodraw.Height);
            pictodraw.DrawToBitmap(bitmap, pictodraw.ClientRectangle);
            Graphics g = Graphics.FromImage(bitmap);
            Console.WriteLine("Bad");
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            string text = this.text1;
            int count = text.Count();
            if (count > 20)
            {
                text = text.Substring(0, 20);
            }
            SizeF textSize = g.MeasureString(text, Font);
            int d = 1;
            Font bestfit = new Font(SystemFonts.DefaultFont.FontFamily, 8, FontStyle.Regular);
            while (true)
            {
                if (textSize.Width > bitmap.Width)
                {
                    bestfit = new Font(SystemFonts.DefaultFont.FontFamily, 8 - d, FontStyle.Regular);
                    textSize = g.MeasureString(text, bestfit);
                    d++;
                }
                else
                {
                    break;
                }
            }
            PointF locationToDraw = new PointF();
            locationToDraw.X = (bitmap.Width / 2) - (textSize.Width / 2);
            locationToDraw.Y = (bitmap.Height / 2) - (textSize.Height / 2);

            g.DrawString(text, bestfit, Brushes.Black, locationToDraw);
            pictodraw.Image = bitmap;
            g.Dispose();

            using (var bitmap1 = new Bitmap(pictodraw.Width, pictodraw.Height))
            {
                pictodraw.DrawToBitmap(bitmap1, pictodraw.ClientRectangle);
                string sourceDir = Path.Combine(Directory.GetCurrentDirectory(), "Resources\\" + pictodraw.Name + ".bmp");
                bitmap1.Save(sourceDir, ImageFormat.Bmp);
            }
        }

        private void Form6_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void loadLabwareSaved(PictureBox pictoload)
        {
            string sourceDir = Path.Combine(Directory.GetCurrentDirectory(), "Resources\\" + pictoload.Name + ".bmp");
            if (File.Exists(sourceDir))
            {
                using (FileStream stream = new FileStream(sourceDir, FileMode.Open, FileAccess.Read))
                {
                    pictoload.Image = Image.FromStream(stream);
                }
            }
        }

        private void deleteLabwareSaved(PictureBox pictodel)
        {
            string sourceDir = Path.Combine(Directory.GetCurrentDirectory(), "Resources\\" + pictodel.Name + ".bmp");
            if (File.Exists(sourceDir))
            {
                string v = (pictodel.Name + "N.png");
                string resFolder = Path.Combine(System.Windows.Forms.Application.StartupPath, "Resources\\");
                pictodel.Image = Image.FromFile(resFolder + v);
                pictodel.Refresh();
                File.Delete(sourceDir);
            }
        }


        private void Form6_Load(object sender, EventArgs e)
        {
            loadLabwareSaved(picSlot1);
            loadLabwareSaved(picSlot2);
            loadLabwareSaved(picSlot3);
            loadLabwareSaved(picSlot4);
            loadLabwareSaved(picSlot5);
            loadLabwareSaved(picSlot6);
            loadLabwareSaved(picSlot7);
            loadLabwareSaved(picSlot8);
            loadLabwareSaved(picSlot9);
            loadLabwareSaved(picSlot10);
            loadLabwareSaved(picSlot11);
        }

        private void PicSlot3_Click(object sender, EventArgs e)
        {
            Form5 f5 = new Form5();
            bool ImportState = new bool();
            bool deleteState = new bool();
            if (f5.ShowDialog() == DialogResult.OK)
            {
                ImportState = f5.import;
                if (ImportState == true)
                {
                    this.text1 = f5.DisplayName;

                }
            }
            else if (f5.DialogResult == DialogResult.Cancel)
            {
                deleteState = f5.Delete;
                if (deleteState == true)
                {
                    deleteLabwareSaved(picSlot3);
                }
            }
            if (ImportState == true)
            {
                changeImageToLabware(picSlot3);
            }
        }

        private void PicSlot4_Click(object sender, EventArgs e)
        {
            Form5 f5 = new Form5();
            bool ImportState = new bool();
            bool deleteState = new bool();
            if (f5.ShowDialog() == DialogResult.OK)
            {
                ImportState = f5.import;
                if (ImportState == true)
                {
                    this.text1 = f5.DisplayName;

                }
            }
            else if (f5.DialogResult == DialogResult.Cancel)
            {
                deleteState = f5.Delete;
                if (deleteState == true)
                {
                    deleteLabwareSaved(picSlot4);
                }
            }
            if (ImportState == true)
            {
                changeImageToLabware(picSlot4);
            }
        }

        private void PicSlot5_Click(object sender, EventArgs e)
        {
            Form5 f5 = new Form5();
            bool ImportState = new bool();
            bool deleteState = new bool();
            if (f5.ShowDialog() == DialogResult.OK)
            {
                ImportState = f5.import;
                if (ImportState == true)
                {
                    this.text1 = f5.DisplayName;

                }
            }
            else if (f5.DialogResult == DialogResult.Cancel)
            {
                deleteState = f5.Delete;
                if (deleteState == true)
                {
                    deleteLabwareSaved(picSlot5);
                }
            }
            if (ImportState == true)
            {
                changeImageToLabware(picSlot5);
            }
        }

        private void PicSlot6_Click(object sender, EventArgs e)
        {
            Form5 f5 = new Form5();
            bool ImportState = new bool();
            bool deleteState = new bool();
            if (f5.ShowDialog() == DialogResult.OK)
            {
                ImportState = f5.import;
                if (ImportState == true)
                {
                    this.text1 = f5.DisplayName;

                }
            }
            else if (f5.DialogResult == DialogResult.Cancel)
            {
                deleteState = f5.Delete;
                if (deleteState == true)
                {
                    deleteLabwareSaved(picSlot6);
                }
            }
            if (ImportState == true)
            {
                changeImageToLabware(picSlot6);
            }
        }

        private void PicSlot7_Click(object sender, EventArgs e)
        {
            Form5 f5 = new Form5();
            bool ImportState = new bool();
            bool deleteState = new bool();
            if (f5.ShowDialog() == DialogResult.OK)
            {
                ImportState = f5.import;
                if (ImportState == true)
                {
                    this.text1 = f5.DisplayName;

                }
            }
            else if (f5.DialogResult == DialogResult.Cancel)
            {
                deleteState = f5.Delete;
                if (deleteState == true)
                {
                    deleteLabwareSaved(picSlot7);
                }
            }
            if (ImportState == true)
            {
                changeImageToLabware(picSlot7);
            }
        }

        private void PicSlot8_Click(object sender, EventArgs e)
        {
            Form5 f5 = new Form5();
            bool ImportState = new bool();
            bool deleteState = new bool();
            if (f5.ShowDialog() == DialogResult.OK)
            {
                ImportState = f5.import;
                if (ImportState == true)
                {
                    this.text1 = f5.DisplayName;

                }
            }
            else if (f5.DialogResult == DialogResult.Cancel)
            {
                deleteState = f5.Delete;
                if (deleteState == true)
                {
                    deleteLabwareSaved(picSlot8);
                }
            }
            if (ImportState == true)
            {
                changeImageToLabware(picSlot8);
            }
        }

        private void PicSlot9_Click(object sender, EventArgs e)
        {
            Form5 f5 = new Form5();
            bool ImportState = new bool();
            bool deleteState = new bool();
            if (f5.ShowDialog() == DialogResult.OK)
            {
                ImportState = f5.import;
                if (ImportState == true)
                {
                    this.text1 = f5.DisplayName;

                }
            }
            else if (f5.DialogResult == DialogResult.Cancel)
            {
                deleteState = f5.Delete;
                if (deleteState == true)
                {
                    deleteLabwareSaved(picSlot9);
                }
            }
            if (ImportState == true)
            {
                changeImageToLabware(picSlot9);
            }
        }

        private void PicSlot10_Click(object sender, EventArgs e)
        {
            Form5 f5 = new Form5();
            bool ImportState = new bool();
            bool deleteState = new bool();
            if (f5.ShowDialog() == DialogResult.OK)
            {
                ImportState = f5.import;
                if (ImportState == true)
                {
                    this.text1 = f5.DisplayName;

                }
            }
            else if (f5.DialogResult == DialogResult.Cancel)
            {
                deleteState = f5.Delete;
                if (deleteState == true)
                {
                    deleteLabwareSaved(picSlot10);
                }
            }
            if (ImportState == true)
            {
                changeImageToLabware(picSlot10);
            }
        }

        private void PicSlot11_Click(object sender, EventArgs e)
        {
            Form5 f5 = new Form5();
            bool ImportState = new bool();
            bool deleteState = new bool();
            if (f5.ShowDialog() == DialogResult.OK)
            {
                ImportState = f5.import;
                if (ImportState == true)
                {
                    this.text1 = f5.DisplayName;

                }
            }
            else if (f5.DialogResult == DialogResult.Cancel)
            {
                deleteState = f5.Delete;
                if (deleteState == true)
                {
                    deleteLabwareSaved(picSlot11);
                }
            }
            if (ImportState == true)
            {
                changeImageToLabware(picSlot11);
            }
        }
    }

}
