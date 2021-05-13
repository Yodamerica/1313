
namespace rbeckmanFinalProject
{
    partial class EntityControl
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
            this.entityName = new System.Windows.Forms.Label();
            this.entityHealthBar = new System.Windows.Forms.ProgressBar();
            this.entityPicture = new System.Windows.Forms.PictureBox();
            this.entityPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.entityHealthPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.entityHealthLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.entityPicture)).BeginInit();
            this.entityPanel.SuspendLayout();
            this.entityHealthPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // entityName
            // 
            this.entityName.AutoSize = true;
            this.entityName.Dock = System.Windows.Forms.DockStyle.Left;
            this.entityName.Location = new System.Drawing.Point(3, 0);
            this.entityName.Name = "entityName";
            this.entityName.Size = new System.Drawing.Size(46, 20);
            this.entityName.TabIndex = 0;
            this.entityName.Text = "name";
            this.entityName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // entityHealthBar
            // 
            this.entityHealthBar.Location = new System.Drawing.Point(3, 3);
            this.entityHealthBar.Name = "entityHealthBar";
            this.entityHealthBar.Size = new System.Drawing.Size(100, 20);
            this.entityHealthBar.TabIndex = 1;
            // 
            // entityPicture
            // 
            this.entityPicture.Location = new System.Drawing.Point(3, 49);
            this.entityPicture.Name = "entityPicture";
            this.entityPicture.Size = new System.Drawing.Size(100, 90);
            this.entityPicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.entityPicture.TabIndex = 2;
            this.entityPicture.TabStop = false;
            // 
            // entityPanel
            // 
            this.entityPanel.AutoSize = true;
            this.entityPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.entityPanel.BackColor = System.Drawing.SystemColors.Control;
            this.entityPanel.Controls.Add(this.entityName);
            this.entityPanel.Controls.Add(this.entityHealthPanel);
            this.entityPanel.Controls.Add(this.entityPicture);
            this.entityPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.entityPanel.Location = new System.Drawing.Point(4, 4);
            this.entityPanel.Margin = new System.Windows.Forms.Padding(4);
            this.entityPanel.Name = "entityPanel";
            this.entityPanel.Size = new System.Drawing.Size(162, 142);
            this.entityPanel.TabIndex = 3;
            // 
            // entityHealthPanel
            // 
            this.entityHealthPanel.AutoSize = true;
            this.entityHealthPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.entityHealthPanel.Controls.Add(this.entityHealthBar);
            this.entityHealthPanel.Controls.Add(this.entityHealthLabel);
            this.entityHealthPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.entityHealthPanel.Location = new System.Drawing.Point(0, 20);
            this.entityHealthPanel.Margin = new System.Windows.Forms.Padding(0);
            this.entityHealthPanel.Name = "entityHealthPanel";
            this.entityHealthPanel.Size = new System.Drawing.Size(162, 26);
            this.entityHealthPanel.TabIndex = 3;
            // 
            // entityHealthLabel
            // 
            this.entityHealthLabel.AutoSize = true;
            this.entityHealthLabel.Location = new System.Drawing.Point(109, 3);
            this.entityHealthLabel.Margin = new System.Windows.Forms.Padding(3);
            this.entityHealthLabel.Name = "entityHealthLabel";
            this.entityHealthLabel.Size = new System.Drawing.Size(50, 20);
            this.entityHealthLabel.TabIndex = 2;
            this.entityHealthLabel.Text = "label1";
            // 
            // EntityControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.entityPanel);
            this.Name = "EntityControl";
            this.Size = new System.Drawing.Size(170, 150);
            ((System.ComponentModel.ISupportInitialize)(this.entityPicture)).EndInit();
            this.entityPanel.ResumeLayout(false);
            this.entityPanel.PerformLayout();
            this.entityHealthPanel.ResumeLayout(false);
            this.entityHealthPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label entityName;
        private System.Windows.Forms.ProgressBar entityHealthBar;
        private System.Windows.Forms.FlowLayoutPanel entityPanel;
        private System.Windows.Forms.FlowLayoutPanel entityHealthPanel;
        private System.Windows.Forms.Label entityHealthLabel;
        public System.Windows.Forms.PictureBox entityPicture;
    }
}
