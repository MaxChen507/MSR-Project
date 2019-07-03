﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MSR
{
    public partial class AddNonStockItemForm : Form
    {
        //Form variables
        String Bp_No;

        //Binding Source Initialization
        BindingSource budgetListDGV_source = new BindingSource();

        //Data List Initialization
        ICollection<Domain.BudgetInfo> budgetListData = null;

        public AddNonStockItemForm(String Bp_No)
        {
            InitializeComponent();
            this.Bp_No = Bp_No;
            BudgetListDGV_Load();
            AddListDGV_Load();
        }

        private void BudgetListDGV_Load()
        {
            try
            {
                budgetListData = BusinessAPI.BusinessSingleton.Instance.GetFilterBudgetInfo(Bp_No);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString());
            }

            if (budgetListData == null)
            {
                MessageBox.Show("Error");
                return;
            }

            budgetListDGV_source.DataSource = budgetListData;
            budgetInfo_addNonStock_dataGridView.DataSource = budgetListDGV_source;

            budgetInfo_addNonStock_dataGridView.ClearSelection();
        }

        private void AddListDGV_Load()
        {
            //DGV clear
            addList_addNonStock_dataGridView.DataSource = null;
            addList_addNonStock_dataGridView.Rows.Clear();
            addList_addNonStock_dataGridView.Refresh();

            addList_addNonStock_dataGridView.ClearSelection();

            //Populate from Singleton List
            foreach (Domain.FormItems item in BusinessAPI.BusinessSingleton.Instance.formItemList)
            {
                addList_addNonStock_dataGridView.Rows.Add(item.BudgetPool, item.ItemCode, item.ItemDesc, "1", item.Unit, item.AC_No);
            }

        }

        private void UpdateBusinessSingletonFormItemList()
        {
            //Data List Initialization
            ICollection<Domain.FormItems> formItemData_List = new List<Domain.FormItems>();

            //Copy data from data grid view and populate addListData
            foreach (DataGridViewRow row in addList_addNonStock_dataGridView.Rows)
            {
                String BudgetPool = row.Cells["BudgetPool"].FormattedValue.ToString();
                String ItemCode = row.Cells["ItemCode"].FormattedValue.ToString();
                String ItemDesc = row.Cells["ItemDesc"].FormattedValue.ToString();
                String Unit = row.Cells["Unit"].FormattedValue.ToString();
                String AC_No = row.Cells["AC_No"].FormattedValue.ToString();

                Domain.FormItems addItem = new Domain.FormItems(BudgetPool, ItemCode, ItemDesc, "1", Unit, "", "", "", "", AC_No);

                formItemData_List.Add(addItem);
            }

            BusinessAPI.BusinessSingleton.Instance.formItemList = formItemData_List;
        }

        private void ApplyClose_AddNonStock_button_Click(object sender, EventArgs e)
        {
            //Save state of DGV
            UpdateBusinessSingletonFormItemList();

            //testing
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            foreach (Domain.FormItems item in BusinessAPI.BusinessSingleton.Instance.formItemList)
            {
                sb.Append(item.ItemCode);
                sb.Append(Environment.NewLine);
            }
            MessageBox.Show(sb.ToString());

            this.Close();
        }

        private void AddItem_addNonStock_button_Click(object sender, EventArgs e)
        {
            if (budgetInfo_addNonStock_dataGridView.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select an item.");
                return;
            }

            Boolean itemExists = false;


            if (addList_addNonStock_dataGridView.Rows.Count == 0)
            {
                itemExists = false;
            }

            foreach (DataGridViewRow row in addList_addNonStock_dataGridView.Rows)
            {
                //testing
                //MessageBox.Show("AddList: " + row.Cells["ItemCode"].FormattedValue.ToString() + " VS " + "ItemList: " + itemList_addStock_dataGridView.SelectedRows[0].Cells["ItemCode"].FormattedValue.ToString());

                if (row.Cells["ItemDesc"].FormattedValue.ToString().Equals(itemDesc_addNonStock_richTextBox.Text))
                {
                    itemExists = true;
                }
            }

            if (itemExists)
            {
                MessageBox.Show("The selected item description already exists.");
            }
            else if (itemDesc_addNonStock_richTextBox.Text.Equals(""))
            {
                MessageBox.Show("The item description is empty.");
            }
            else
            {
                String BudgetPool = budgetInfo_addNonStock_dataGridView.SelectedRows[0].Cells["BP_No"].FormattedValue.ToString();
                String ItemCode = "[NonStock]";
                String ItemDesc = itemDesc_addNonStock_richTextBox.Text;
                String Unit = unit_addNonStock_textBox.Text;
                String AC_No = budgetInfo_addNonStock_dataGridView.SelectedRows[0].Cells["AC_No"].FormattedValue.ToString();

                addList_addNonStock_dataGridView.Rows.Add(BudgetPool, ItemCode, ItemDesc, "1", Unit, AC_No);
            }
        }
    }
}
