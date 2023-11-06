namespace ScrapMechanicLogic
{
    partial class BaseForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            voxelParserButton = new Button();
            pngParserButton = new Button();
            logicParserButton = new Button();
            titleLabel = new Label();
            SuspendLayout();
            // 
            // voxelParserButton
            // 
            voxelParserButton.Location = new Point(271, 231);
            voxelParserButton.Name = "voxelParserButton";
            voxelParserButton.Size = new Size(243, 26);
            voxelParserButton.TabIndex = 0;
            voxelParserButton.Text = "Open Voxel Parser";
            voxelParserButton.UseVisualStyleBackColor = true;
            voxelParserButton.Click += voxelParserButton_Click;
            // 
            // pngParserButton
            // 
            pngParserButton.Location = new Point(271, 296);
            pngParserButton.Name = "pngParserButton";
            pngParserButton.Size = new Size(243, 26);
            pngParserButton.TabIndex = 1;
            pngParserButton.Text = "Open PNG Parser";
            pngParserButton.UseVisualStyleBackColor = true;
            pngParserButton.Click += pngParserButton_Click;
            // 
            // logicParserButton
            // 
            logicParserButton.Location = new Point(271, 361);
            logicParserButton.Name = "logicParserButton";
            logicParserButton.Size = new Size(243, 26);
            logicParserButton.TabIndex = 2;
            logicParserButton.Text = "Open Logic Parser";
            logicParserButton.UseVisualStyleBackColor = true;
            logicParserButton.Click += logicParserButton_Click;
            // 
            // titleLabel
            // 
            titleLabel.AutoSize = true;
            titleLabel.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point);
            titleLabel.Location = new Point(266, 123);
            titleLabel.Name = "titleLabel";
            titleLabel.Size = new Size(252, 32);
            titleLabel.TabIndex = 3;
            titleLabel.Text = "Scrap Mechanic Parser";
            // 
            // BaseForm
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(784, 561);
            Controls.Add(titleLabel);
            Controls.Add(logicParserButton);
            Controls.Add(pngParserButton);
            Controls.Add(voxelParserButton);
            Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            Name = "BaseForm";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button voxelParserButton;
        private Button pngParserButton;
        private Button logicParserButton;
        private Label titleLabel;
    }
}