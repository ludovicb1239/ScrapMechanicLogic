using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.DataFormats;

namespace ScrapMechanicLogic
{
    public partial class BaseForm : Form
    {
        public BaseForm()
        {
            InitializeComponent();
        }

        private void voxelParserButton_Click(object sender, EventArgs e)
        {
            // Create an instance of VoxelForm and show it
            VoxelForm newForm = new VoxelForm();
            newForm.Show();
        }

        private void imageParserButton_Click(object sender, EventArgs e)
        {
            // Create an instance of ImageForm and show it
            ImageForm newForm = new ImageForm();
            newForm.Show();
        }

        private void logicParserButton_Click(object sender, EventArgs e)
        {
            // Create an instance of LogicForm and show it
            LogicForm newForm = new LogicForm();
            newForm.Show();
        }
    }
}
