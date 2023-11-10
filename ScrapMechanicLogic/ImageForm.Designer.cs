namespace ScrapMechanicLogic
{
    partial class ImageForm
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
            blueprintNameTextBox = new TextBox();
            fileLabel = new Label();
            scaleInput = new NumericUpDown();
            label3 = new Label();
            roundColorsCheckBox = new CheckBox();
            orientationComboBox = new ComboBox();
            label4 = new Label();
            label5 = new Label();
            scaleDownInput = new NumericUpDown();
            ditheringCheckBox = new CheckBox();
            ((System.ComponentModel.ISupportInitialize)scaleInput).BeginInit();
            ((System.ComponentModel.ISupportInitialize)scaleDownInput).BeginInit();
            SuspendLayout();
            // 
            // ConvertButton
            // 
            ConvertButton.Location = new Point(325, 405);
            ConvertButton.Name = "ConvertButton";
            ConvertButton.Size = new Size(150, 40);
            ConvertButton.TabIndex = 0;
            ConvertButton.Text = "Convert to SM";
            ConvertButton.UseVisualStyleBackColor = true;
            ConvertButton.Click += ConvertButton_Click;
            // 
            // Browse
            // 
            Browse.Location = new Point(350, 65);
            Browse.Name = "Browse";
            Browse.Size = new Size(100, 30);
            Browse.TabIndex = 1;
            Browse.Text = "Browse";
            Browse.UseVisualStyleBackColor = true;
            Browse.Click += BrowseButton_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(326, 257);
            label1.Name = "label1";
            label1.Size = new Size(69, 17);
            label1.TabIndex = 2;
            label1.Text = "Block Type";
            // 
            // DefaultBlockTypeDropdown
            // 
            DefaultBlockTypeDropdown.FormattingEnabled = true;
            DefaultBlockTypeDropdown.Location = new Point(401, 254);
            DefaultBlockTypeDropdown.Name = "DefaultBlockTypeDropdown";
            DefaultBlockTypeDropdown.Size = new Size(164, 25);
            DefaultBlockTypeDropdown.TabIndex = 3;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(297, 359);
            label2.Name = "label2";
            label2.Size = new Size(98, 17);
            label2.TabIndex = 4;
            label2.Text = "Blueprint Name";
            // 
            // blueprintNameTextBox
            // 
            blueprintNameTextBox.Location = new Point(401, 356);
            blueprintNameTextBox.Name = "blueprintNameTextBox";
            blueprintNameTextBox.Size = new Size(164, 25);
            blueprintNameTextBox.TabIndex = 5;
            // 
            // fileLabel
            // 
            fileLabel.AutoSize = true;
            fileLabel.Location = new Point(350, 115);
            fileLabel.Name = "fileLabel";
            fileLabel.Size = new Size(101, 17);
            fileLabel.TabIndex = 6;
            fileLabel.Text = "No File selected";
            // 
            // scaleInput
            // 
            scaleInput.Location = new Point(401, 325);
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
            label3.Location = new Point(357, 327);
            label3.Name = "label3";
            label3.Size = new Size(38, 17);
            label3.TabIndex = 8;
            label3.Text = "Scale";
            // 
            // roundColorsCheckBox
            // 
            roundColorsCheckBox.AutoSize = true;
            roundColorsCheckBox.Location = new Point(340, 186);
            roundColorsCheckBox.Name = "roundColorsCheckBox";
            roundColorsCheckBox.Size = new Size(189, 21);
            roundColorsCheckBox.TabIndex = 9;
            roundColorsCheckBox.Text = "Round Colors to SM Palette";
            roundColorsCheckBox.UseVisualStyleBackColor = true;
            roundColorsCheckBox.CheckedChanged += roundColorsCheckBox_CheckedChanged;
            // 
            // orientationComboBox
            // 
            orientationComboBox.FormattingEnabled = true;
            orientationComboBox.Location = new Point(401, 285);
            orientationComboBox.Name = "orientationComboBox";
            orientationComboBox.Size = new Size(164, 25);
            orientationComboBox.TabIndex = 11;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(326, 288);
            label4.Name = "label4";
            label4.Size = new Size(73, 17);
            label4.TabIndex = 10;
            label4.Text = "Orientation";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(283, 149);
            label5.Name = "label5";
            label5.Size = new Size(112, 17);
            label5.TabIndex = 13;
            label5.Text = "Scale down factor";
            // 
            // scaleDownInput
            // 
            scaleDownInput.Location = new Point(401, 147);
            scaleDownInput.Maximum = new decimal(new int[] { 8, 0, 0, 0 });
            scaleDownInput.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            scaleDownInput.Name = "scaleDownInput";
            scaleDownInput.Size = new Size(68, 25);
            scaleDownInput.TabIndex = 12;
            scaleDownInput.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // ditheringCheckBox
            // 
            ditheringCheckBox.AutoSize = true;
            ditheringCheckBox.Location = new Point(340, 213);
            ditheringCheckBox.Name = "ditheringCheckBox";
            ditheringCheckBox.Size = new Size(80, 21);
            ditheringCheckBox.TabIndex = 14;
            ditheringCheckBox.Text = "Dithering";
            ditheringCheckBox.UseVisualStyleBackColor = true;
            // 
            // ImageForm
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 510);
            Controls.Add(ditheringCheckBox);
            Controls.Add(label5);
            Controls.Add(scaleDownInput);
            Controls.Add(orientationComboBox);
            Controls.Add(label4);
            Controls.Add(roundColorsCheckBox);
            Controls.Add(label3);
            Controls.Add(scaleInput);
            Controls.Add(fileLabel);
            Controls.Add(blueprintNameTextBox);
            Controls.Add(label2);
            Controls.Add(DefaultBlockTypeDropdown);
            Controls.Add(label1);
            Controls.Add(Browse);
            Controls.Add(ConvertButton);
            Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            Name = "ImageForm";
            Text = "Image parser";
            ((System.ComponentModel.ISupportInitialize)scaleInput).EndInit();
            ((System.ComponentModel.ISupportInitialize)scaleDownInput).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button ConvertButton;
        private Button Browse;
        private Label label1;
        private ComboBox DefaultBlockTypeDropdown;
        private Label label2;
        private TextBox blueprintNameTextBox;
        private Label fileLabel;
        private NumericUpDown scaleInput;
        private Label label3;
        private CheckBox roundColorsCheckBox;
        private ComboBox orientationComboBox;
        private Label label4;
        private Label label5;
        private NumericUpDown scaleDownInput;
        private CheckBox ditheringCheckBox;
    }
}