namespace MSR.UIFormLayer
{
    partial class MainMenuUserControl
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
            this.MSRMain_menuStrip = new System.Windows.Forms.MenuStrip();
            this.menuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MSRMain_menuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // MSRMain_menuStrip
            // 
            this.MSRMain_menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuToolStripMenuItem});
            this.MSRMain_menuStrip.Location = new System.Drawing.Point(0, 0);
            this.MSRMain_menuStrip.Name = "MSRMain_menuStrip";
            this.MSRMain_menuStrip.Size = new System.Drawing.Size(1181, 24);
            this.MSRMain_menuStrip.TabIndex = 2;
            this.MSRMain_menuStrip.Text = "menuStrip1";
            // 
            // menuToolStripMenuItem
            // 
            this.menuToolStripMenuItem.Name = "menuToolStripMenuItem";
            this.menuToolStripMenuItem.Size = new System.Drawing.Size(50, 20);
            this.menuToolStripMenuItem.Text = "Menu";
            // 
            // MainMenuUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.MSRMain_menuStrip);
            this.Name = "MainMenuUserControl";
            this.Size = new System.Drawing.Size(1181, 24);
            this.MSRMain_menuStrip.ResumeLayout(false);
            this.MSRMain_menuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip MSRMain_menuStrip;
        private System.Windows.Forms.ToolStripMenuItem menuToolStripMenuItem;
    }
}
