using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace OT_APP1
{
    public partial class Form5 : Form
    {
        private bool Import;
        public bool import
        {
            get { return Import; }
            set { Import = value; }
        }
        public string brand;
        public string Brand
        {
            get { return brand; }
            set { brand = value; }
        }
        public string brandId;
        public string BrandId
        {
            get { return brandId; }
            set { brandId = value; }
        }

        public string displayName;
        public string DisplayName
        {
            get { return displayName; }
            set { displayName = value; }
        }
        public float xDimension;
        public float XDimension
        {
            get { return xDimension; }
            set { xDimension = value; }
        }
        public float yDimension;
        public float YDimension
        {
            get { return yDimension; }
            set { yDimension = value; }
        }
        public float zDimension;
        public float ZDimension
        {
            get { return zDimension; }
            set { zDimension = value; }
        }
        public float depth;
        public float Depth
        {
            get { return depth; }
            set { depth = value; }
        }
        public string shape;
        public string Shape
        {
            get { return shape; }
            set { shape = value; }
        }
        public float diameter;
        public float Diameter
        {
            get { return diameter; }
            set { diameter = value; }
        }
        public float totalLiquidVolume;
        public float TotalLiquidVolume
        {
            get { return totalLiquidVolume; }
            set { totalLiquidVolume = value; }
        }
        public float colOffset;
        public float ColOffset
        {
            get { return colOffset; }
            set { colOffset = value; }
        }
        public float rowOffset;
        public float RowOffset
        {
            get { return rowOffset; }
            set { rowOffset = value; }
        }
        public float rowSpace;
        public float RowSpace
        {
            get { return rowSpace; }
            set { rowSpace = value; }
        }
        public float colSpace;
        public float ColSpace
        {
            get { return colSpace; }
            set { colSpace = value; }
        }
        public int rows;
        public int Rows
        {
            get { return rows; }
            set { rows = value; }
        }
        public int cols;
        public int Cols
        {
            get { return cols; }
            set { cols = value; }
        }



        public Form5()
        {
            InitializeComponent();
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            string sourceDir = Path.Combine(Directory.GetCurrentDirectory(), "Labware\\");
            try
            {
                string[] picList = Directory.GetFileSystemEntries(sourceDir, "*", SearchOption.AllDirectories);

                foreach (string value in picList)
                {
                    if (value.Contains("1.json"))
                    {
                        string directPath = Path.GetDirectoryName(value);
                        string foldername = new DirectoryInfo(directPath).Name;
                        TreeNode LabwareNode = new TreeNode(foldername);
                        treeLabware.Nodes.Add(LabwareNode);
                    }
                }
            }
            catch
            {

            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            string labwaretoDel = treeLabware.SelectedNode.Text;
            string sourceDir = Path.Combine(Directory.GetCurrentDirectory(), "Labware\\");
            string finalpath = (sourceDir + labwaretoDel);
            // If directory does not exist, don't even try   
            if (Directory.Exists(finalpath))
            {
                Directory.Delete(finalpath, true);
            }
            try
            {
                string[] picList = Directory.GetFileSystemEntries(sourceDir, "*", SearchOption.AllDirectories);
                treeLabware.Nodes.Clear();

                foreach (string value in picList)
                {
                    if (value.Contains("1.json"))
                    {
                        string directPath = Path.GetDirectoryName(value);
                        string foldername = new DirectoryInfo(directPath).Name;
                        TreeNode LabwareNode = new TreeNode(foldername);
                        treeLabware.Nodes.Add(LabwareNode);
                    }
                }
            }
            catch
            {

            }
        }

        private void BtnView_Click(object sender, EventArgs e)
        {
            string labwaretoView = treeLabware.SelectedNode.Text;
            string sourceDir = Path.Combine(Directory.GetCurrentDirectory(), "Labware\\");
            string finalpath = (sourceDir + labwaretoView + "\\1.json");
            var labware_def = File.ReadAllText(finalpath);
            JToken parsedJson = JToken.Parse(labware_def);
            var beautified = parsedJson.ToString(Formatting.Indented);
            txtView.Text = beautified;


        }

        private void TreeLabware_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }

        private void BtnConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                string labwaretoView = treeLabware.SelectedNode.Text;
                string sourceDir = Path.Combine(Directory.GetCurrentDirectory(), "Labware\\");
                string finalpath = (sourceDir + labwaretoView + "\\1.json");
                var labware_def = File.ReadAllText(finalpath);
                JToken token = JObject.Parse(labware_def);
                Console.WriteLine(token.ToString());
                //Brand fields
                object brandList = (object)token.SelectToken("brand");
                JToken brandobject = JObject.Parse(brandList.ToString());
                string brand = (string)brandobject.SelectToken("brand");
                object brandId = (object)brandobject.SelectToken("brandId");

                //Metadata
                object metadata = (object)token.SelectToken("metadata");
                JToken metadataObject = JObject.Parse(metadata.ToString());
                string displayName = (string)metadataObject.SelectToken("displayName");

                //Dimensions
                object dimensions = (object)token.SelectToken("dimensions");
                JToken dimensionObject = JObject.Parse(dimensions.ToString());
                float xDimension = (float)dimensionObject.SelectToken("xDimension");
                float yDimension = (float)dimensionObject.SelectToken("yDimension");
                float zDimension = (float)dimensionObject.SelectToken("zDimension");

                //Well
                object well = (object)token.SelectToken("wells");
                JToken wellObject = JObject.Parse(well.ToString());
                object A1 = (object)wellObject.SelectToken("A1");
                object B2 = (object)wellObject.SelectToken("B2");
                JToken wellproperties = JObject.Parse(A1.ToString());
                JToken wellpropertiesOffset = JObject.Parse(B2.ToString());
                string shape = (string)wellproperties.SelectToken("shape");
                float diameter = (float)wellproperties.SelectToken("diameter");
                float totalLiquidVolume = (float)wellproperties.SelectToken("totalLiquidVolume");
                float colOffset = (float)wellproperties.SelectToken("x");
                float rowOffset = (float)wellproperties.SelectToken("y");
                float depth = (float)wellproperties.SelectToken("z");
                float xOffset = (float)wellpropertiesOffset.SelectToken("x");
                float yOffset = (float)wellpropertiesOffset.SelectToken("y");
                float colSpace = xOffset - colOffset;
                float rowSpace = rowOffset - yOffset;


                //Ordering Rows and Columns
                object ordering = (object)token.SelectToken("ordering");
                string[] arr = ((IEnumerable)ordering).Cast<object>().Select(r => r.ToString()).ToArray();
                string[] neworder = arr[0].Split(',');
                int rows = neworder.Length;
                int cols = arr.Length;

                import = true;
                Brand = brand;
                BrandId = brandId.ToString();
                DisplayName = displayName;
                XDimension = xDimension;
                YDimension = yDimension;
                ZDimension = zDimension;
                Depth = depth;
                Shape = shape;
                Diameter = diameter;
                TotalLiquidVolume = totalLiquidVolume;
                ColOffset = colOffset;
                RowOffset = rowOffset;
                ColSpace = colSpace;
                RowSpace = rowSpace;
                Rows = rows;
                Cols = cols;

                this.Close();

            }
            catch (Exception exp)
            {
                MessageBox.Show("ERROR: " + "Please select a labware of the type well plate or tiprack");
            }
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            import = false;
            this.Close();
        }
    }
}
