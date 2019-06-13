namespace MSR
{
    partial class MSRMainForm
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
            this.mainMSR_tabControl = new System.Windows.Forms.TabControl();
            this.createNewMSR_tabPage = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.mainMSR_tabControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainMSR_tabControl
            // 
            this.mainMSR_tabControl.Controls.Add(this.createNewMSR_tabPage);
            this.mainMSR_tabControl.Controls.Add(this.tabPage2);
            this.mainMSR_tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainMSR_tabControl.Location = new System.Drawing.Point(0, 0);
            this.mainMSR_tabControl.Name = "mainMSR_tabControl";
            this.mainMSR_tabControl.SelectedIndex = 0;
            this.mainMSR_tabControl.Size = new System.Drawing.Size(1008, 561);
            this.mainMSR_tabControl.TabIndex = 0;
            // 
            // createNewMSR_tabPage
            // 
            this.createNewMSR_tabPage.Location = new System.Drawing.Point(4, 22);
            this.createNewMSR_tabPage.Name = "createNewMSR_tabPage";
            this.createNewMSR_tabPage.Padding = new System.Windows.Forms.Padding(3);
            this.createNewMSR_tabPage.Size = new System.Drawing.Size(1000, 535);
            this.createNewMSR_tabPage.TabIndex = 0;
            this.createNewMSR_tabPage.Text = "Create New MSR";
            this.createNewMSR_tabPage.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(192, 74);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // MSRMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 561);
            this.Controls.Add(this.mainMSR_tabControl);
            this.Name = "MSRMainForm";
            this.Text = "MSR Main Form";
            this.mainMSR_tabControl.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl mainMSR_tabControl;
        private System.Windows.Forms.TabPage createNewMSR_tabPage;
        private System.Windows.Forms.TabPage tabPage2;
    }
}

