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
            this.mainMenuUserControl = new UIFormLayer.MainMenuUserControl();
            this.approved_tabPage = new System.Windows.Forms.TabPage();
            this.approvedTab_dataGridView = new System.Windows.Forms.DataGridView();
            this.search_approvedTab_groupBox = new System.Windows.Forms.GroupBox();
            this.apSearch_approvedTab_textBox = new System.Windows.Forms.TextBox();
            this.apSearch_approvedTab_label = new System.Windows.Forms.Label();
            this.ogSearch_approvedTab_textBox = new System.Windows.Forms.TextBox();
            this.ogSearch_approvedTab_label = new System.Windows.Forms.Label();
            this.deptSearch_approvedTab_textBox = new System.Windows.Forms.TextBox();
            this.deptSearch_approvedTab_label = new System.Windows.Forms.Label();
            this.idSearch_approvedTab_textBox = new System.Windows.Forms.TextBox();
            this.idSearch_approvedTab_label = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.approved_tabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.approvedTab_dataGridView)).BeginInit();
            this.search_approvedTab_groupBox.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainMenuUserControl
            // 
            this.mainMenuUserControl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mainMenuUserControl.Location = new System.Drawing.Point(0, 0);
            this.mainMenuUserControl.Name = "mainMenuUserControl";
            this.mainMenuUserControl.Size = new System.Drawing.Size(1424, 25);
            this.mainMenuUserControl.TabIndex = 0;
            // 
            // approved_tabPage
            // 
            this.approved_tabPage.Controls.Add(this.approvedTab_dataGridView);
            this.approved_tabPage.Controls.Add(this.search_approvedTab_groupBox);
            this.approved_tabPage.Location = new System.Drawing.Point(4, 22);
            this.approved_tabPage.Name = "approved_tabPage";
            this.approved_tabPage.Padding = new System.Windows.Forms.Padding(3);
            this.approved_tabPage.Size = new System.Drawing.Size(1389, 843);
            this.approved_tabPage.TabIndex = 4;
            this.approved_tabPage.Text = "Approved MSRs";
            this.approved_tabPage.UseVisualStyleBackColor = true;
            // 
            // approvedTab_dataGridView
            // 
            this.approvedTab_dataGridView.AllowUserToAddRows = false;
            this.approvedTab_dataGridView.AllowUserToDeleteRows = false;
            this.approvedTab_dataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.approvedTab_dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.approvedTab_dataGridView.Location = new System.Drawing.Point(15, 110);
            this.approvedTab_dataGridView.MultiSelect = false;
            this.approvedTab_dataGridView.Name = "approvedTab_dataGridView";
            this.approvedTab_dataGridView.ReadOnly = true;
            this.approvedTab_dataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.approvedTab_dataGridView.Size = new System.Drawing.Size(1359, 718);
            this.approvedTab_dataGridView.TabIndex = 1;
            this.approvedTab_dataGridView.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ApprovedTab_dataGridView_CellDoubleClick);
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
            this.search_approvedTab_groupBox.Location = new System.Drawing.Point(15, 15);
            this.search_approvedTab_groupBox.Name = "search_approvedTab_groupBox";
            this.search_approvedTab_groupBox.Size = new System.Drawing.Size(1359, 89);
            this.search_approvedTab_groupBox.TabIndex = 0;
            this.search_approvedTab_groupBox.TabStop = false;
            this.search_approvedTab_groupBox.Text = "Search";
            // 
            // apSearch_approvedTab_textBox
            // 
            this.apSearch_approvedTab_textBox.Location = new System.Drawing.Point(349, 53);
            this.apSearch_approvedTab_textBox.Name = "apSearch_approvedTab_textBox";
            this.apSearch_approvedTab_textBox.Size = new System.Drawing.Size(100, 20);
            this.apSearch_approvedTab_textBox.TabIndex = 7;
            this.apSearch_approvedTab_textBox.TextChanged += new System.EventHandler(this.ApSearch_approvedTab_textBox_TextChanged);
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
            // ogSearch_approvedTab_textBox
            // 
            this.ogSearch_approvedTab_textBox.Location = new System.Drawing.Point(243, 53);
            this.ogSearch_approvedTab_textBox.Name = "ogSearch_approvedTab_textBox";
            this.ogSearch_approvedTab_textBox.Size = new System.Drawing.Size(100, 20);
            this.ogSearch_approvedTab_textBox.TabIndex = 5;
            this.ogSearch_approvedTab_textBox.TextChanged += new System.EventHandler(this.OgSearch_approvedTab_textBox_TextChanged);
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
            // deptSearch_approvedTab_textBox
            // 
            this.deptSearch_approvedTab_textBox.Location = new System.Drawing.Point(137, 53);
            this.deptSearch_approvedTab_textBox.Name = "deptSearch_approvedTab_textBox";
            this.deptSearch_approvedTab_textBox.Size = new System.Drawing.Size(100, 20);
            this.deptSearch_approvedTab_textBox.TabIndex = 3;
            this.deptSearch_approvedTab_textBox.TextChanged += new System.EventHandler(this.DeptSearch_approvedTab_textBox_TextChanged);
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
            // idSearch_approvedTab_textBox
            // 
            this.idSearch_approvedTab_textBox.Location = new System.Drawing.Point(31, 53);
            this.idSearch_approvedTab_textBox.Name = "idSearch_approvedTab_textBox";
            this.idSearch_approvedTab_textBox.Size = new System.Drawing.Size(100, 20);
            this.idSearch_approvedTab_textBox.TabIndex = 1;
            this.idSearch_approvedTab_textBox.TextChanged += new System.EventHandler(this.IdSearch_approvedTab_textBox_TextChanged);
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
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.approved_tabPage);
            this.tabControl1.Location = new System.Drawing.Point(15, 25);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1397, 869);
            this.tabControl1.TabIndex = 1;
            // 
            // MSRMain_ProcurementForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1424, 906);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.mainMenuUserControl);
            this.Name = "MSRMain_ProcurementForm";
            this.Text = "MSRMain_ProcurementForm";
            this.Load += new System.EventHandler(this.MSRMain_ProcurementForm_Load);
            this.approved_tabPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.approvedTab_dataGridView)).EndInit();
            this.search_approvedTab_groupBox.ResumeLayout(false);
            this.search_approvedTab_groupBox.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private UIFormLayer.MainMenuUserControl mainMenuUserControl;
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