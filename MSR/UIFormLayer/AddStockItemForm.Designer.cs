namespace MSR.UIFormLayer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddStockItemForm));
            this.search_AddStock_label = new System.Windows.Forms.Label();
            this.search_AddStock_textBox = new System.Windows.Forms.TextBox();
            this.lookup_AddStock_label = new System.Windows.Forms.Label();
            this.lookup_AddStock_comboBox = new System.Windows.Forms.ComboBox();
            this.addItem_AddStock_button = new System.Windows.Forms.Button();
            this.applyClose_AddStock_button = new System.Windows.Forms.Button();
            this.itemList_addStock_dataGridView = new System.Windows.Forms.DataGridView();
            this.addList_addStock_dataGridView = new System.Windows.Forms.DataGridView();
            this.BudgetPool = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ItemCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ItemDesc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Quantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Unit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UnitPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Currency = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ROS_Date = new UserInterfaceAPI.CalendarColumn();
            this.Comments = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AC_No = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.itemList_addStock_dataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.addList_addStock_dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // search_AddStock_label
            // 
            this.search_AddStock_label.AutoSize = true;
            this.search_AddStock_label.Location = new System.Drawing.Point(12, 34);
            this.search_AddStock_label.Name = "search_AddStock_label";
            this.search_AddStock_label.Size = new System.Drawing.Size(100, 13);
            this.search_AddStock_label.TabIndex = 0;
            this.search_AddStock_label.Text = "Search Description:";
            // 
            // search_AddStock_textBox
            // 
            this.search_AddStock_textBox.Location = new System.Drawing.Point(118, 32);
            this.search_AddStock_textBox.Name = "search_AddStock_textBox";
            this.search_AddStock_textBox.Size = new System.Drawing.Size(151, 20);
            this.search_AddStock_textBox.TabIndex = 1;
            this.search_AddStock_textBox.TextChanged += new System.EventHandler(this.Search_AddStock_textBox_TextChanged);
            // 
            // lookup_AddStock_label
            // 
            this.lookup_AddStock_label.AutoSize = true;
            this.lookup_AddStock_label.Location = new System.Drawing.Point(275, 34);
            this.lookup_AddStock_label.Name = "lookup_AddStock_label";
            this.lookup_AddStock_label.Size = new System.Drawing.Size(71, 13);
            this.lookup_AddStock_label.TabIndex = 2;
            this.lookup_AddStock_label.Text = "Lookup Filter:";
            // 
            // lookup_AddStock_comboBox
            // 
            this.lookup_AddStock_comboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.lookup_AddStock_comboBox.FormattingEnabled = true;
            this.lookup_AddStock_comboBox.Location = new System.Drawing.Point(352, 30);
            this.lookup_AddStock_comboBox.Name = "lookup_AddStock_comboBox";
            this.lookup_AddStock_comboBox.Size = new System.Drawing.Size(121, 21);
            this.lookup_AddStock_comboBox.TabIndex = 3;
            this.lookup_AddStock_comboBox.SelectedIndexChanged += new System.EventHandler(this.Lookup_AddStock_comboBox_SelectedIndexChanged);
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
            this.addList_addStock_dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.addList_addStock_dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.BudgetPool,
            this.ItemCode,
            this.ItemDesc,
            this.Quantity,
            this.Unit,
            this.UnitPrice,
            this.Currency,
            this.ROS_Date,
            this.Comments,
            this.AC_No});
            this.addList_addStock_dataGridView.Location = new System.Drawing.Point(12, 294);
            this.addList_addStock_dataGridView.Name = "addList_addStock_dataGridView";
            this.addList_addStock_dataGridView.ReadOnly = true;
            this.addList_addStock_dataGridView.Size = new System.Drawing.Size(776, 240);
            this.addList_addStock_dataGridView.TabIndex = 7;
            // 
            // BudgetPool
            // 
            this.BudgetPool.HeaderText = "BudgetPool";
            this.BudgetPool.Name = "BudgetPool";
            this.BudgetPool.ReadOnly = true;
            this.BudgetPool.Width = 73;
            // 
            // ItemCode
            // 
            this.ItemCode.HeaderText = "ItemCode";
            this.ItemCode.Name = "ItemCode";
            this.ItemCode.ReadOnly = true;
            this.ItemCode.Width = 74;
            // 
            // ItemDesc
            // 
            this.ItemDesc.HeaderText = "ItemDesc";
            this.ItemDesc.Name = "ItemDesc";
            this.ItemDesc.ReadOnly = true;
            this.ItemDesc.Width = 73;
            // 
            // Quantity
            // 
            this.Quantity.HeaderText = "Quantity";
            this.Quantity.Name = "Quantity";
            this.Quantity.ReadOnly = true;
            this.Quantity.Width = 73;
            // 
            // Unit
            // 
            this.Unit.HeaderText = "Unit";
            this.Unit.Name = "Unit";
            this.Unit.ReadOnly = true;
            this.Unit.Width = 74;
            // 
            // UnitPrice
            // 
            this.UnitPrice.HeaderText = "UnitPrice";
            this.UnitPrice.Name = "UnitPrice";
            this.UnitPrice.ReadOnly = true;
            this.UnitPrice.Width = 73;
            // 
            // Currency
            // 
            this.Currency.HeaderText = "Currency";
            this.Currency.Name = "Currency";
            this.Currency.ReadOnly = true;
            this.Currency.Width = 73;
            // 
            // ROS_Date
            // 
            this.ROS_Date.HeaderText = "ROS_Date";
            this.ROS_Date.Name = "ROS_Date";
            this.ROS_Date.ReadOnly = true;
            this.ROS_Date.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ROS_Date.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.ROS_Date.Width = 73;
            // 
            // Comments
            // 
            this.Comments.HeaderText = "Comments";
            this.Comments.Name = "Comments";
            this.Comments.ReadOnly = true;
            this.Comments.Width = 74;
            // 
            // AC_No
            // 
            this.AC_No.HeaderText = "AC_No";
            this.AC_No.Name = "AC_No";
            this.AC_No.ReadOnly = true;
            this.AC_No.Width = 73;
            // 
            // AddStockItemForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(800, 546);
            this.Controls.Add(this.addList_addStock_dataGridView);
            this.Controls.Add(this.itemList_addStock_dataGridView);
            this.Controls.Add(this.applyClose_AddStock_button);
            this.Controls.Add(this.addItem_AddStock_button);
            this.Controls.Add(this.lookup_AddStock_comboBox);
            this.Controls.Add(this.lookup_AddStock_label);
            this.Controls.Add(this.search_AddStock_textBox);
            this.Controls.Add(this.search_AddStock_label);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AddStockItemForm";
            this.Text = "AddStockItemForm";
            this.Load += new System.EventHandler(this.AddStockItemForm_Load);
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
        private System.Windows.Forms.DataGridViewTextBoxColumn BudgetPool;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemDesc;
        private System.Windows.Forms.DataGridViewTextBoxColumn Quantity;
        private System.Windows.Forms.DataGridViewTextBoxColumn Unit;
        private System.Windows.Forms.DataGridViewTextBoxColumn UnitPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn Currency;
        private UserInterfaceAPI.CalendarColumn ROS_Date;
        private System.Windows.Forms.DataGridViewTextBoxColumn Comments;
        private System.Windows.Forms.DataGridViewTextBoxColumn AC_No;
    }
}