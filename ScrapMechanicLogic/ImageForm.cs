using CsharpVoxReader;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScrapMechanicLogic
{
    public partial class ImageForm : Form
    {
        string selectedFilePath = "";
        bool readyToConvert = false;
        public ImageForm()
        {
            InitializeComponent();
            PopulateComboBox(DefaultBlockTypeDropdown);
            SetConvertButtonAppearance();
            SetDitheringCheckboxAppearance();
            orientationDropdown.Items.Add("Horizontal");
            orientationDropdown.Items.Add("Vertical");
            orientationDropdown.SelectedIndex = 0;
            orientationDropdown.DropDownStyle = ComboBoxStyle.DropDownList;
        }
        private void Browse_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            // Set the file filter to allow only MagicaVoxel Files
            openFileDialog.Filter = "PNG Files (*.png)|*.png";

            DialogResult result = openFileDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                selectedFilePath = openFileDialog.FileName;
                fileLabel.Text = "Selected file : " + Path.GetFileName(selectedFilePath);
                readyToConvert = true;
                SetConvertButtonAppearance();
            }
            else
            {
                MessageBox.Show("No file has been selected.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void ConvertButton_Click(object sender, EventArgs e)
        {
            // Extracting selected values from UI controls
            Orientation orientation = (Orientation)orientationDropdown.SelectedIndex;
            BlockType blockType = (BlockType)DefaultBlockTypeDropdown.SelectedIndex;
            bool roundColors = roundColorsCheckBox.Checked;
            bool dithering = ditheringCheckBox.Checked;
            int scaleDownFactor = (int)scaleDownFactorInput.Value;
            string blueprintName = BlueprintNameTextbox.Text;

            // Creating a new instance of MyPNGLoader class with specified parameters
            MyPNGLoader loader = new MyPNGLoader(roundColors, orientation, dithering, scaleDownFactor);

            // Loading a PNG file specified by selectedFilePath
            loader.LoadPNG(selectedFilePath);
            Console.WriteLine("Read image file");

            if (loader.positions.Count == 0)
            {
                MessageBox.Show("No block found in file", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            List<DefaultObjectStruct> list = new List<DefaultObjectStruct>();
            // Creating objects from loaded positions, block types, colors, bounds, and scale input value
            ScrapMechanic.CreateFromBlocks(loader.positions, blockType, list, loader.colors, loader.bounds, (int)scaleInput.Value);
            Console.WriteLine("Converter list of pos to blocks");

            // Creating a DescriptionStruct object with the blueprint name entered in a text box
            DescriptionStruct description = new DescriptionStruct(string.IsNullOrWhiteSpace(blueprintName)
                ? Path.GetFileNameWithoutExtension(selectedFilePath)
                : blueprintName);

            // Parsing objects using the created list and description
            ScrapMechanic.ParseObject(list, description);

            MessageBox.Show("Done! You might need to restart the game if it's the first time converting a creation",
                "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void PopulateComboBox(ComboBox cb)
        {
            // Get the values of the enum as an array of strings
            string[] enumValues = Enum.GetNames(typeof(BlockType));

            // Add enum values to the ComboBox
            cb.Items.AddRange(enumValues);
            cb.SelectedIndex = 0;
            cb.DropDownStyle = ComboBoxStyle.DropDownList;
        }
        private void SetConvertButtonAppearance()
        {
            ConvertButton.Enabled = readyToConvert;
        }
        private void SetDitheringCheckboxAppearance()
        {
            ditheringCheckBox.Enabled = roundColorsCheckBox.Checked;
            if (!roundColorsCheckBox.Checked)
                ditheringCheckBox.Checked = false;
        }

        private void roundColorsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            SetDitheringCheckboxAppearance();
        }
    }
}
