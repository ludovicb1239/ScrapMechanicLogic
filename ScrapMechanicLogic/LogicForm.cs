using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace ScrapMechanicLogic
{
    public partial class LogicForm : Form
    {
        public LogicForm()
        {
            InitializeComponent();


            /*
            // Create an instance of OpenFileDialog
            OpenFileDialog openFileDialog = new OpenFileDialog();

            // Set the file filter to allow only Verilog files
            openFileDialog.Filter = "Verilog Files (*.v)|*.v|All Files (*.*)|*.*";

            // Show the dialog and get the result
            DialogResult result = openFileDialog.ShowDialog();

            // Process the selected file
            if (result == DialogResult.OK)
            {
                string selectedFilePath = openFileDialog.FileName;
                openVerilog(selectedFilePath);
                Console.WriteLine("Done reading file");
            }
            */

            ScrapMechanic mechanicConverter = new ScrapMechanic();
            List<DefaultObjectStruct> logicGates = mechanicConverter.create8bRam(4);
            DescriptionStruct description = new DescriptionStruct("Converted Logic");
            ScrapMechanic.ParseObject(logicGates, description);
        }
    }
}
