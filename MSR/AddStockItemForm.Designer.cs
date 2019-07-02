namespace MSR
{
    partial class AddStockItemForm
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
            this.search_AddStock_label = new System.Windows.Forms.Label();
            this.search_AddStock_textBox = new System.Windows.Forms.TextBox();
            this.lookup_AddStock_label = new System.Windows.Forms.Label();
            this.lookup_AddStock_comboBox = new System.Windows.Forms.ComboBox();
            this.addItem_AddStock_button = new System.Windows.Forms.Button();
            this.applyClose_AddStock_button = new System.Windows.Forms.Button();
            this.itemList_addStock_dataGridView = new System.Windows.Forms.DataGridView();
            this.addList_addStock_dataGridView = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.itemList_addStock_dataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.addList_addStock_dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // search_AddStock_label
            // 
            this.search_AddStock_label.AutoSize = true;
            this.search_AddStock_label.Location = new System.Drawing.Point(12, 34);
            this.search_AddStock_label.Name = "search_AddStock_label";
            this.search_AddStock_label.Size = new System.Drawing.Size(44, 13);
            this.search_AddStock_label.TabIndex = 0;
            this.search_AddStock_label.Text = "Search:";
            // 
            // search_AddStock_textBox
            // 
            this.search_AddStock_textBox.Location = new System.Drawing.Point(62, 31);
            this.search_AddStock_textBox.Name = "search_AddStock_textBox";
            this.search_AddStock_textBox.Size = new System.Drawing.Size(100, 20);
            this.search_AddStock_textBox.TabIndex = 1;
            // 
            // lookup_AddStock_label
            // 
            this.lookup_AddStock_label.AutoSize = true;
            this.lookup_AddStock_label.Location = new System.Drawing.Point(172, 34);
            this.lookup_AddStock_label.Name = "lookup_AddStock_label";
            this.lookup_AddStock_label.Size = new System.Drawing.Size(71, 13);
            this.lookup_AddStock_label.TabIndex = 2;
            this.lookup_AddStock_label.Text = "Lookup Filter:";
            // 
            // lookup_AddStock_comboBox
            // 
            this.lookup_AddStock_comboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.lookup_AddStock_comboBox.FormattingEnabled = true;
            this.lookup_AddStock_comboBox.Location = new System.Drawing.Point(249, 31);
            this.lookup_AddStock_comboBox.Name = "lookup_AddStock_comboBox";
            this.lookup_AddStock_comboBox.Size = new System.Drawing.Size(121, 21);
            this.lookup_AddStock_comboBox.TabIndex = 3;
            // 
            // addItem_AddStock_button
            // 
            this.addItem_AddStock_button.AutoSize = true;
            this.addItem_AddStock_button.Location = new System.Drawing.Point(512, 12);
            this.addItem_AddStock_button.Name = "addItem_AddStock_button";
            this.addItem_AddStock_button.Size = new System.Drawing.Size(120, 40);
            this.addItem_AddStock_button.TabIndex = 4;
            this.addItem_AddStock_button.Text = "Add Selected Item";
            this.addItem_AddStock_button.UseVisualStyleBackColor = true;
            this.addItem_AddStock_button.Click += new System.EventHandler(this.AddItem_AddStock_button_Click);
            // 
            // applyClose_AddStock_button
            // 
            this.applyClose_AddStock_button.AutoSize = true;
            this.applyClose_AddStock_button.Location = new System.Drawing.Point(668, 12);
            this.applyClose_AddStock_button.Name = "applyClose_AddStock_button";
            this.applyClose_AddStock_button.Size = new System.Drawing.Size(120, 40);
            this.applyClose_AddStock_button.TabIndex = 5;
            this.applyClose_AddStock_button.Text = "Apply and Close";
            this.applyClose_AddStock_button.UseVisualStyleBackColor = true;
            this.applyClose_AddStock_button.Click += new System.EventHandler(this.ApplyClose_AddStock_button_Click);
            // 
            // itemList_addStock_dataGridView
            // 
            this.itemList_addStock_dataGridView.AllowUserToAddRows = false;
            this.itemList_addStock_dataGridView.AllowUserToDeleteRows = false;
            this.itemList_addStock_dataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.itemList_addStock_dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.itemList_addStock_dataGridView.Location = new System.Drawing.Point(12, 57);
            this.itemList_addStock_dataGridView.MultiSelect = false;
            this.itemList_addStock_dataGridView.Name = "itemList_addStock_dataGridView";
            this.itemList_addStock_dataGridView.ReadOnly = true;
            this.itemList_addStock_dataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.itemList_addStock_dataGridView.Size = new System.Drawing.Size(776, 215);
            this.itemList_addStock_dataGridView.TabIndex = 6;
            // 
            // addList_addStock_dataGridView
            // 
            this.addList_addStock_dataGridView.AllowUserToAddRows = false;
            this.addList_addStock_dataGridView.AllowUserToDeleteRows = false;
            this.addList_addStock_dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.addList_addStock_dataGridView.Location = new System.Drawing.Point(12, 294);
            this.addList_addStock_dataGridView.Name = "addList_addStock_dataGridView";
            this.addList_addStock_dataGridView.ReadOnly = true;
            this.addList_addStock_dataGridView.Size = new System.Drawing.Size(776, 240);
            this.addList_addStock_dataGridView.TabIndex = 7;
            // 
            // AddStockItemForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 546);
            this.Controls.Add(this.addList_addStock_dataGridView);
            this.Controls.Add(this.itemList_addStock_dataGridView);
            this.Controls.Add(this.applyClose_AddStock_button);
            this.Controls.Add(this.addItem_AddStock_button);
            this.Controls.Add(this.lookup_AddStock_comboBox);
            this.Controls.Add(this.lookup_AddStock_label);
            this.Controls.Add(this.search_AddStock_textBox);
            this.Controls.Add(this.search_AddStock_label);
            this.Name = "AddStockItemForm";
            this.Text = "AddStockItemForm";
            ((System.ComponentModel.ISupportInitialize)(this.itemList_addStock_dataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.addList_addStock_dataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label search_AddStock_label;
        private System.Windows.Forms.TextBox search_AddStock_textBox;
        private System.Windows.Forms.Label lookup_AddStock_label;
        private System.Windows.Forms.ComboBox lookup_AddStock_comboBox;
        private System.Windows.Forms.Button addItem_AddStock_button;
        private System.Windows.Forms.Button applyClose_AddStock_button;
        private System.Windows.Forms.DataGridView itemList_addStock_dataGridView;
        private System.Windows.Forms.DataGridView addList_addStock_dataGridView;
    }
}