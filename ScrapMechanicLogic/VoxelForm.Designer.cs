namespace ScrapMechanicLogic
{
    partial class VoxelForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            ConvertButton = new Button();
            Browse = new Button();
            label1 = new Label();
            DefaultBlockTypeDropdown = new ComboBox();
            label2 = new Label();
            BlueprintNameTextbox = new TextBox();
            fileLabel = new Label();
            scaleInput = new NumericUpDown();
            label3 = new Label();
            roundColorsCheckBox = new CheckBox();
            selectionLayoutPanel = new FlowLayoutPanel();
            addNewElementButton = new Button();
            remLastElementButton = new Button();
            ((System.ComponentModel.ISupportInitialize)scaleInput).BeginInit();
            SuspendLayout();
            // 
            // ConvertButton
            // 
            ConvertButton.Location = new Point(323, 428);
            ConvertButton.Name = "ConvertButton";
            ConvertButton.Size = new Size(150, 40);
            ConvertButton.TabIndex = 0;
            ConvertButton.Text = "Convert to SM";
            ConvertButton.UseVisualStyleBackColor = true;
            ConvertButton.Click += ConvertButton_Click;
            // 
            // Browse
            // 
            Browse.Location = new Point(342, 31);
            Browse.Name = "Browse";
            Browse.Size = new Size(100, 30);
            Browse.TabIndex = 1;
            Browse.Text = "Browse";
            Browse.UseVisualStyleBackColor = true;
            Browse.Click += Browse_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(279, 320);
            label1.Name = "label1";
            label1.Size = new Size(114, 17);
            label1.TabIndex = 2;
            label1.Text = "Default Block Type";
            // 
            // DefaultBlockTypeDropdown
            // 
            DefaultBlockTypeDropdown.FormattingEnabled = true;
            DefaultBlockTypeDropdown.Location = new Point(399, 317);
            DefaultBlockTypeDropdown.Name = "DefaultBlockTypeDropdown";
            DefaultBlockTypeDropdown.Size = new Size(164, 25);
            DefaultBlockTypeDropdown.TabIndex = 3;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(295, 382);
            label2.Name = "label2";
            label2.Size = new Size(98, 17);
            label2.TabIndex = 4;
            label2.Text = "Blueprint Name";
            // 
            // BlueprintNameTextbox
            // 
            BlueprintNameTextbox.Location = new Point(399, 379);
            BlueprintNameTextbox.Name = "BlueprintNameTextbox";
            BlueprintNameTextbox.Size = new Size(164, 25);
            BlueprintNameTextbox.TabIndex = 5;
            // 
            // fileLabel
            // 
            fileLabel.AutoSize = true;
            fileLabel.Location = new Point(342, 81);
            fileLabel.Name = "fileLabel";
            fileLabel.Size = new Size(101, 17);
            fileLabel.TabIndex = 6;
            fileLabel.Text = "No File selected";
            // 
            // scaleInput
            // 
            scaleInput.Location = new Point(399, 348);
            scaleInput.Maximum = new decimal(new int[] { 20, 0, 0, 0 });
            scaleInput.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            scaleInput.Name = "scaleInput";
            scaleInput.Size = new Size(68, 25);
            scaleInput.TabIndex = 7;
            scaleInput.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(355, 350);
            label3.Name = "label3";
            label3.Size = new Size(38, 17);
            label3.TabIndex = 8;
            label3.Text = "Scale";
            // 
            // roundColorsCheckBox
            // 
            roundColorsCheckBox.AutoSize = true;
            roundColorsCheckBox.Location = new Point(307, 116);
            roundColorsCheckBox.Name = "roundColorsCheckBox";
            roundColorsCheckBox.Size = new Size(189, 21);
            roundColorsCheckBox.TabIndex = 9;
            roundColorsCheckBox.Text = "Round Colors to SM Palette";
            roundColorsCheckBox.UseVisualStyleBackColor = true;
            roundColorsCheckBox.CheckedChanged += roundColorsCheckBox_CheckedChanged;
            // 
            // selectionLayoutPanel
            // 
            selectionLayoutPanel.AutoScroll = true;
            selectionLayoutPanel.FlowDirection = FlowDirection.TopDown;
            selectionLayoutPanel.ImeMode = ImeMode.NoControl;
            selectionLayoutPanel.Location = new Point(243, 167);
            selectionLayoutPanel.Name = "selectionLayoutPanel";
            selectionLayoutPanel.Size = new Size(355, 128);
            selectionLayoutPanel.TabIndex = 10;
            selectionLayoutPanel.WrapContents = false;
            // 
            // addNewElementButton
            // 
            addNewElementButton.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            addNewElementButton.Location = new Point(604, 167);
            addNewElementButton.Name = "addNewElementButton";
            addNewElementButton.Size = new Size(30, 30);
            addNewElementButton.TabIndex = 11;
            addNewElementButton.Text = "+";
            addNewElementButton.UseVisualStyleBackColor = true;
            addNewElementButton.Click += addNewElementButton_Click;
            // 
            // remLastElementButton
            // 
            remLastElementButton.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            remLastElementButton.Location = new Point(604, 203);
            remLastElementButton.Name = "remLastElementButton";
            remLastElementButton.Size = new Size(30, 30);
            remLastElementButton.TabIndex = 12;
            remLastElementButton.Text = "-";
            remLastElementButton.UseVisualStyleBackColor = true;
            remLastElementButton.Click += remLastElementButton_Click;
            // 
            // VoxelForm
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 510);
            Controls.Add(remLastElementButton);
            Controls.Add(addNewElementButton);
            Controls.Add(selectionLayoutPanel);
            Controls.Add(roundColorsCheckBox);
            Controls.Add(label3);
            Controls.Add(scaleInput);
            Controls.Add(fileLabel);
            Controls.Add(BlueprintNameTextbox);
            Controls.Add(label2);
            Controls.Add(DefaultBlockTypeDropdown);
            Controls.Add(label1);
            Controls.Add(Browse);
            Controls.Add(ConvertButton);
            Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            Name = "VoxelForm";
            Text = "Voxel parser";
            ((System.ComponentModel.ISupportInitialize)scaleInput).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button ConvertButton;
        private Button Browse;
        private Label label1;
        private ComboBox DefaultBlockTypeDropdown;
        private Label label2;
        private TextBox BlueprintNameTextbox;
        private Label fileLabel;
        private NumericUpDown scaleInput;
        private Label label3;
        private CheckBox roundColorsCheckBox;
        private FlowLayoutPanel selectionLayoutPanel;
        private Button addNewElementButton;
        private Button remLastElementButton;
    }
}