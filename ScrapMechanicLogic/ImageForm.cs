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
        string imageFilePath = "";
        bool isReadyToConvert = false;
        public ImageForm()
        {
            InitializeComponent();
            PopulateComboBox(DefaultBlockTypeDropdown);
            UpdateConvertButtonState();
            UpdateDitheringCheckboxState();
            orientationComboBox.Items.Add("Horizontal");
            orientationComboBox.Items.Add("Vertical");
            orientationComboBox.SelectedIndex = 0;
            orientationComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
        }
        private void BrowseButton_Click(object sender, EventArgs e)
        {
            // Event handler for the "Browse" button click event.
            // Opens a file dialog for selecting an image file, updates UI components, and prepares the form for conversion.

            OpenFileDialog openFileDialog = new OpenFileDialog();

            // Set the file filter to allow only Image Files
            openFileDialog.Filter = "Image Files|*.bmp;*.jpg;*.jpeg;*.png;*.tif;*.tiff;*.ico|All Files (*.*)|*.*";
            openFileDialog.FilterIndex = 1;
            openFileDialog.Multiselect = false;
            openFileDialog.CheckFileExists = true;
            openFileDialog.CheckPathExists = true;

            DialogResult result = openFileDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                imageFilePath = openFileDialog.FileName;
                fileLabel.Text = "Selected file : " + Path.GetFileName(imageFilePath);
                isReadyToConvert = true;
                UpdateConvertButtonState();
            }
            else
            {
                MessageBox.Show("No file has been selected.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void ConvertButton_Click(object sender, EventArgs e)
        {
            // Event handler for the "Convert" button click event.
            // Extracts user-selected values from UI controls, loads the image, creates objects based on loaded data, and parses objects for conversion.
            // Displays a message box upon completion.

            // Extracting selected values from UI controls
            Orientation orientation = (Orientation)orientationComboBox.SelectedIndex;
            BlockType blockType = (BlockType)DefaultBlockTypeDropdown.SelectedIndex;
            bool roundColors = roundColorsCheckBox.Checked;
            bool dithering = ditheringCheckBox.Checked;
            int scaleDownFactor = (int)scaleDownInput.Value;
            string blueprintName = blueprintNameTextBox.Text;

            // Creating a new instance of MyImageLoader class with specified parameters
            MyImageLoader loader = new MyImageLoader(roundColors, orientation, dithering, scaleDownFactor);

            // Loading an image file specified by imageFilePath
            loader.LoadImage(imageFilePath);
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
                ? Path.GetFileNameWithoutExtension(imageFilePath)
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
        private void UpdateConvertButtonState()
        {
            ConvertButton.Enabled = isReadyToConvert;
        }
        private void UpdateDitheringCheckboxState()
        {
            ditheringCheckBox.Enabled = roundColorsCheckBox.Checked;
            if (!roundColorsCheckBox.Checked)
                ditheringCheckBox.Checked = false;
        }

        private void roundColorsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            UpdateDitheringCheckboxState();
        }
    }
}
