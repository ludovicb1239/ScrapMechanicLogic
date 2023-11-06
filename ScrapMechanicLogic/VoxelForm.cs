using CsharpVoxReader;
using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml.Linq;

namespace ScrapMechanicLogic
{
    public partial class VoxelForm : Form
    {
        string voxelFilePath = "";
        bool isReadyToConvert = false;

        List<BlockSelectionUserControl> blockSelectionControls = new();
        List<(string, int)> usedColorCombos = new();
        public VoxelForm()
        {
            InitializeComponent();
            InitializeUI();
        }
        private void Browse_Click(object sender, EventArgs e)
        {
            // Event handler for the "Browse" button click event.
            // Opens a file dialog for selecting a MagicaVoxel file, updates UI components, and prepares the form for conversion.

            OpenFileDialog openFileDialog = new OpenFileDialog();

            // Set the file filter to allow only MagicaVoxel Files
            openFileDialog.Filter = "MagicaVoxel Files (*.vox)|*.vox|All Files (*.*)|*.*";

            DialogResult result = openFileDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                voxelFilePath = openFileDialog.FileName;

                fileLabel.Text = "Selected file : " + Path.GetFileName(voxelFilePath);

                LoadUsedColorCombos();

                isReadyToConvert = true;
                UpdateConvertButtonAppearance();
            }
            else
            {
                MessageBox.Show("No file has been selected.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void ConvertButton_Click(object sender, EventArgs e)
        {
            // Event handler for the "Convert" button click event.
            // Reads voxel data from the selected file, creates a list of DefaultObjectStruct based on user selections, and parses the object list.
            // Displays a message box upon completion.

            bool roundColors = roundColorsCheckBox.Checked;
            BlockType defaultBlockType = (BlockType)DefaultBlockTypeDropdown.SelectedIndex;
            int scale = (int)scaleInput.Value;
            string blueprintName = BlueprintNameTextbox.Text;

            MyVoxLoader voxelLoader = new MyVoxLoader(roundColors);
            VoxReader r = new VoxReader(voxelFilePath, voxelLoader);
            r.Read();
            Console.WriteLine("Read voxel file");

            if (voxelLoader.positions.Count == 0)
                MessageBox.Show("No block found in file", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            List<DefaultObjectStruct> list = new();
            if (blockSelectionControls.Count == 0)
                ScrapMechanic.CreateFromBlocks(voxelLoader.positions, defaultBlockType, list, voxelLoader.palette, voxelLoader.paletteIndexes, voxelLoader.boundingBoxes, scale);
            else
            {
                List<BlockType> selectedBlockTypes = DetermineBlockTypes(voxelLoader, defaultBlockType);
                ScrapMechanic.CreateFromBlocks(voxelLoader.positions, selectedBlockTypes, list, voxelLoader.palette, voxelLoader.paletteIndexes, voxelLoader.boundingBoxes, scale);
            }

            Console.WriteLine("Converter list of pos to blocks");

            DescriptionStruct description = new DescriptionStruct(string.IsNullOrWhiteSpace(blueprintName) ? Path.GetFileNameWithoutExtension(voxelFilePath) : blueprintName);
            ScrapMechanic.ParseObject(list, description);

            MessageBox.Show("Done ! You might need to restart the game if its the first time converting a creation", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private List<BlockType> DetermineBlockTypes(MyVoxLoader voxelLoader, BlockType defaultBlockType)
        {
            // Determines the block types based on user selections and palette indexes.
            // Checks if the palette index corresponds to a linked BlockType in the user controls.
            // If found, uses the linked BlockType; otherwise, uses the defaultBlockType.

            List<BlockType> blockTypes = new List<BlockType>();
            List<(int, BlockType)> comboList = new List<(int, BlockType)>();

            foreach (BlockSelectionUserControl control in blockSelectionControls)
                comboList.Add(control.GetCombo());

            foreach (byte indx in voxelLoader.paletteIndexes)
            {
                var comboMatch = comboList.FirstOrDefault(combo => combo.Item1 == indx);

                if (comboMatch != default)
                {
                    blockTypes.Add(comboMatch.Item2);
                }
                else
                {
                    blockTypes.Add(defaultBlockType);
                }
            }

            return blockTypes;
        }
        private void LoadUsedColorCombos()
        {
            // Loads and stores the color combinations used in the selected MagicaVoxel file.
            // Reads voxel data to determine the colors used and populates the usedColorCombos list with color names and indexes.

            resetSelectionLayoutPanel();
            bool roundColors = roundColorsCheckBox.Checked;

            MyVoxColorLoader loader = new MyVoxColorLoader(roundColors);
            VoxReader r = new VoxReader(voxelFilePath, loader);
            r.Read();
            usedColorCombos = new();
            foreach (byte index in loader.usedIndexes)
            {
                (string, int) newCombo = (loader.palette[index], index);
                usedColorCombos.Add(newCombo);
            }
        }
        private void InitializeUI()
        {
            PopulateComboBox(DefaultBlockTypeDropdown);
            UpdateConvertButtonAppearance();
        }
        public static void PopulateComboBox(ComboBox cb)
        {
            // Add elements to the ComboBox

            // Get the values of the enum as an array of strings
            string[] enumValues = Enum.GetNames(typeof(BlockType));

            // Add enum values to the ComboBox
            cb.Items.AddRange(enumValues);
            cb.SelectedIndex = 0;
            cb.DropDownStyle = ComboBoxStyle.DropDownList;
        }
        private void UpdateConvertButtonAppearance()
        {
            ConvertButton.Enabled = isReadyToConvert;
            addNewElementButton.Enabled = isReadyToConvert;
        }

        private void addNewElementButton_Click(object sender, EventArgs e)
        {
            BlockSelectionUserControl newControl = new BlockSelectionUserControl(usedColorCombos);
            selectionLayoutPanel.Controls.Add(newControl);
            blockSelectionControls.Add(newControl);
        }
        private void remLastElementButton_Click(object sender, EventArgs e)
        {
            BlockSelectionUserControl ctrl = blockSelectionControls.Last();
            selectionLayoutPanel.Controls.Remove(ctrl);
            blockSelectionControls.Remove(ctrl);

        }
        private void resetSelectionLayoutPanel()
        {
            selectionLayoutPanel.Controls.Clear();
            blockSelectionControls.Clear();
        }

        private void roundColorsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (isReadyToConvert)
                LoadUsedColorCombos();
        }

    }
}