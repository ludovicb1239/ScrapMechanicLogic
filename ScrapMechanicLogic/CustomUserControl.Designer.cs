namespace ScrapMechanicLogic
{
    partial class BlockSelectionUserControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            colorSelector = new ComboBox();
            blockSelector = new ComboBox();
            SuspendLayout();
            // 
            // colorSelector
            // 
            colorSelector.FormattingEnabled = true;
            colorSelector.Location = new Point(3, 0);
            colorSelector.Name = "colorSelector";
            colorSelector.Size = new Size(121, 25);
            colorSelector.TabIndex = 0;
            // 
            // blockSelector
            // 
            blockSelector.FormattingEnabled = true;
            blockSelector.Location = new Point(130, 0);
            blockSelector.Name = "blockSelector";
            blockSelector.Size = new Size(190, 25);
            blockSelector.TabIndex = 1;
            // 
            // CustomUserControl
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(blockSelector);
            Controls.Add(colorSelector);
            Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            Name = "CustomUserControl";
            Size = new Size(323, 28);
            ResumeLayout(false);
        }

        #endregion

        private ComboBox colorSelector;
        private ComboBox blockSelector;
    }
}
