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
    public class ColorItem
    {
        public Color Color { get; }
        public string Name { get; }
        public int Value { get; }
        public ColorItem(Color color, string name, int value)
        {
            Color = color;
            Name = name;
            Value = value;
        }

        public override string ToString()
        {
            return Name; // This is what will be displayed in the ComboBox
        }
    }
    public partial class BlockSelectionUserControl : UserControl
    {
        public BlockSelectionUserControl(List<(string, int)> colorsIndexed)
        {
            InitializeComponent();

            foreach ((string, int) color in colorsIndexed)
            {
                ColorItem newItem = new(ScrapMechanicColor.HexToColor(color.Item1), color.Item2.ToString(), color.Item2);
                colorSelector.Items.Add(newItem);
            }

            colorSelector.DrawMode = DrawMode.OwnerDrawFixed; // or DrawMode.OwnerDrawVariable;
            colorSelector.DrawItem += colorComboBox_DrawItem;
            colorSelector.SelectedIndex = 0;
            colorSelector.DropDownStyle = ComboBoxStyle.DropDownList;
            VoxelForm.PopulateComboBox(blockSelector);
        } 
        public (int, BlockType) GetCombo()
        {
            (int, BlockType) combo;
            combo.Item1 = ((ColorItem)colorSelector.SelectedItem).Value;
            combo.Item2 = (BlockType)blockSelector.SelectedIndex;
            return combo;
        }
        private void colorComboBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index >= 0)
            {
                // Get the ColorItem at the current index
                ColorItem colorItem = (ColorItem)colorSelector.Items[e.Index];

                // Set the background color and draw the item text
                e.DrawBackground();
                using (Brush brush = new SolidBrush(colorItem.Color))
                {
                    e.Graphics.FillRectangle(brush, e.Bounds.Left, e.Bounds.Top, e.Bounds.Width, e.Bounds.Height);
                    e.Graphics.DrawString(colorItem.Name, e.Font, Brushes.Black, e.Bounds.Left + 5, e.Bounds.Top + 2);
                }

                // Draw the focus rectangle if the ComboBox has focus
                e.DrawFocusRectangle();
            }
        }
    }
}
