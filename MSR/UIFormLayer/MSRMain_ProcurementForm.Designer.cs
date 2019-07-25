namespace MSR.UIFormLayer
{
    partial class MSRMain_ProcurementForm
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
            this.mainMenuUserControl1 = new UIFormLayer.MainMenuUserControl();
            this.approved_tabPage = new System.Windows.Forms.TabPage();
            this.search_approvedTab_groupBox = new System.Windows.Forms.GroupBox();
            this.idSearch_approvedTab_label = new System.Windows.Forms.Label();
            this.idSearch_approvedTab_textBox = new System.Windows.Forms.TextBox();
            this.deptSearch_approvedTab_label = new System.Windows.Forms.Label();
            this.deptSearch_approvedTab_textBox = new System.Windows.Forms.TextBox();
            this.ogSearch_approvedTab_label = new System.Windows.Forms.Label();
            this.ogSearch_approvedTab_textBox = new System.Windows.Forms.TextBox();
            this.apSearch_approvedTab_label = new System.Windows.Forms.Label();
            this.apSearch_approvedTab_textBox = new System.Windows.Forms.TextBox();
            this.approvedTab_dataGridView = new System.Windows.Forms.DataGridView();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.approved_tabPage.SuspendLayout();
            this.search_approvedTab_groupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.approvedTab_dataGridView)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainMenuUserControl1
            // 
            this.mainMenuUserControl1.Location = new System.Drawing.Point(0, 0);
            this.mainMenuUserControl1.Name = "mainMenuUserControl1";
            this.mainMenuUserControl1.Size = new System.Drawing.Size(1181, 24);
            this.mainMenuUserControl1.TabIndex = 0;
            // 
            // approved_tabPage
            // 
            this.approved_tabPage.Controls.Add(this.approvedTab_dataGridView);
            this.approved_tabPage.Controls.Add(this.search_approvedTab_groupBox);
            this.approved_tabPage.Location = new System.Drawing.Point(4, 22);
            this.approved_tabPage.Name = "approved_tabPage";
            this.approved_tabPage.Padding = new System.Windows.Forms.Padding(3);
            this.approved_tabPage.Size = new System.Drawing.Size(1152, 786);
            this.approved_tabPage.TabIndex = 4;
            this.approved_tabPage.Text = "Approved MSRs";
            this.approved_tabPage.UseVisualStyleBackColor = true;
            // 
            // search_approvedTab_groupBox
            // 
            this.search_approvedTab_groupBox.Controls.Add(this.apSearch_approvedTab_textBox);
            this.search_approvedTab_groupBox.Controls.Add(this.apSearch_approvedTab_label);
            this.search_approvedTab_groupBox.Controls.Add(this.ogSearch_approvedTab_textBox);
            this.search_approvedTab_groupBox.Controls.Add(this.ogSearch_approvedTab_label);
            this.search_approvedTab_groupBox.Controls.Add(this.deptSearch_approvedTab_textBox);
            this.search_approvedTab_groupBox.Controls.Add(this.deptSearch_approvedTab_label);
            this.search_approvedTab_groupBox.Controls.Add(this.idSearch_approvedTab_textBox);
            this.search_approvedTab_groupBox.Controls.Add(this.idSearch_approvedTab_label);
            this.search_approvedTab_groupBox.Location = new System.Drawing.Point(19, 18);
            this.search_approvedTab_groupBox.Name = "search_approvedTab_groupBox";
            this.search_approvedTab_groupBox.Size = new System.Drawing.Size(930, 89);
            this.search_approvedTab_groupBox.TabIndex = 0;
            this.search_approvedTab_groupBox.TabStop = false;
            this.search_approvedTab_groupBox.Text = "Search";
            // 
            // idSearch_approvedTab_label
            // 
            this.idSearch_approvedTab_label.AutoSize = true;
            this.idSearch_approvedTab_label.Location = new System.Drawing.Point(28, 37);
            this.idSearch_approvedTab_label.Name = "idSearch_approvedTab_label";
            this.idSearch_approvedTab_label.Size = new System.Drawing.Size(45, 13);
            this.idSearch_approvedTab_label.TabIndex = 0;
            this.idSearch_approvedTab_label.Text = "MSR ID";
            // 
            // idSearch_approvedTab_textBox
            // 
            this.idSearch_approvedTab_textBox.Location = new System.Drawing.Point(31, 53);
            this.idSearch_approvedTab_textBox.Name = "idSearch_approvedTab_textBox";
            this.idSearch_approvedTab_textBox.Size = new System.Drawing.Size(100, 20);
            this.idSearch_approvedTab_textBox.TabIndex = 1;
            // 
            // deptSearch_approvedTab_label
            // 
            this.deptSearch_approvedTab_label.AutoSize = true;
            this.deptSearch_approvedTab_label.Location = new System.Drawing.Point(134, 37);
            this.deptSearch_approvedTab_label.Name = "deptSearch_approvedTab_label";
            this.deptSearch_approvedTab_label.Size = new System.Drawing.Size(62, 13);
            this.deptSearch_approvedTab_label.TabIndex = 2;
            this.deptSearch_approvedTab_label.Text = "Department";
            // 
            // deptSearch_approvedTab_textBox
            // 
            this.deptSearch_approvedTab_textBox.Location = new System.Drawing.Point(137, 53);
            this.deptSearch_approvedTab_textBox.Name = "deptSearch_approvedTab_textBox";
            this.deptSearch_approvedTab_textBox.Size = new System.Drawing.Size(100, 20);
            this.deptSearch_approvedTab_textBox.TabIndex = 3;
            // 
            // ogSearch_approvedTab_label
            // 
            this.ogSearch_approvedTab_label.AutoSize = true;
            this.ogSearch_approvedTab_label.Location = new System.Drawing.Point(240, 37);
            this.ogSearch_approvedTab_label.Name = "ogSearch_approvedTab_label";
            this.ogSearch_approvedTab_label.Size = new System.Drawing.Size(52, 13);
            this.ogSearch_approvedTab_label.TabIndex = 4;
            this.ogSearch_approvedTab_label.Text = "Originator";
            // 
            // ogSearch_approvedTab_textBox
            // 
            this.ogSearch_approvedTab_textBox.Location = new System.Drawing.Point(243, 53);
            this.ogSearch_approvedTab_textBox.Name = "ogSearch_approvedTab_textBox";
            this.ogSearch_approvedTab_textBox.Size = new System.Drawing.Size(100, 20);
            this.ogSearch_approvedTab_textBox.TabIndex = 5;
            // 
            // apSearch_approvedTab_label
            // 
            this.apSearch_approvedTab_label.AutoSize = true;
            this.apSearch_approvedTab_label.Location = new System.Drawing.Point(346, 37);
            this.apSearch_approvedTab_label.Name = "apSearch_approvedTab_label";
            this.apSearch_approvedTab_label.Size = new System.Drawing.Size(50, 13);
            this.apSearch_approvedTab_label.TabIndex = 6;
            this.apSearch_approvedTab_label.Text = "Approver";
            // 
            // apSearch_approvedTab_textBox
            // 
            this.apSearch_approvedTab_textBox.Location = new System.Drawing.Point(349, 53);
            this.apSearch_approvedTab_textBox.Name = "apSearch_approvedTab_textBox";
            this.apSearch_approvedTab_textBox.Size = new System.Drawing.Size(100, 20);
            this.apSearch_approvedTab_textBox.TabIndex = 7;
            // 
            // approvedTab_dataGridView
            // 
            this.approvedTab_dataGridView.AllowUserToAddRows = false;
            this.approvedTab_dataGridView.AllowUserToDeleteRows = false;
            this.approvedTab_dataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.approvedTab_dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.approvedTab_dataGridView.Location = new System.Drawing.Point(19, 113);
            this.approvedTab_dataGridView.MultiSelect = false;
            this.approvedTab_dataGridView.Name = "approvedTab_dataGridView";
            this.approvedTab_dataGridView.ReadOnly = true;
            this.approvedTab_dataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.approvedTab_dataGridView.Size = new System.Drawing.Size(930, 450);
            this.approvedTab_dataGridView.TabIndex = 1;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.approved_tabPage);
            this.tabControl1.Location = new System.Drawing.Point(12, 25);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1160, 812);
            this.tabControl1.TabIndex = 1;
            // 
            // MSRMain_ProcurementForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1181, 847);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.mainMenuUserControl1);
            this.Name = "MSRMain_ProcurementForm";
            this.Text = "MSRMain_ProcurementForm";
            this.approved_tabPage.ResumeLayout(false);
            this.search_approvedTab_groupBox.ResumeLayout(false);
            this.search_approvedTab_groupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.approvedTab_dataGridView)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private UIFormLayer.MainMenuUserControl mainMenuUserControl1;
        private System.Windows.Forms.TabPage approved_tabPage;
        private System.Windows.Forms.DataGridView approvedTab_dataGridView;
        private System.Windows.Forms.GroupBox search_approvedTab_groupBox;
        private System.Windows.Forms.TextBox apSearch_approvedTab_textBox;
        private System.Windows.Forms.Label apSearch_approvedTab_label;
        private System.Windows.Forms.TextBox ogSearch_approvedTab_textBox;
        private System.Windows.Forms.Label ogSearch_approvedTab_label;
        private System.Windows.Forms.TextBox deptSearch_approvedTab_textBox;
        private System.Windows.Forms.Label deptSearch_approvedTab_label;
        private System.Windows.Forms.TextBox idSearch_approvedTab_textBox;
        private System.Windows.Forms.Label idSearch_approvedTab_label;
        private System.Windows.Forms.TabControl tabControl1;
    }
}