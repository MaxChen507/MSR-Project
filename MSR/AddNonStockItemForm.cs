using System;
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
                addList_addNonStock_dataGridView.Rows.Add(item.BudgetPool, item.ItemCode, item.ItemDesc, item.Quantity, item.Unit, item.UnitPrice, item.Currency, item.ROS_Date, item.Comments, item.AC_No);
            }

        }

        private void ApplyClose_AddNonStock_button_Click(object sender, EventArgs e)
        {
            //Save state of DGV
            UserInterfaceAPI.UserInterfaceSIngleton.Instance.UpdateBusinessSingletonFormItemList(addList_addNonStock_dataGridView);

            //testing
            //System.Text.StringBuilder sb = new System.Text.StringBuilder();
            //foreach (Domain.FormItems item in BusinessAPI.BusinessSingleton.Instance.formItemList)
            //{
            //    sb.Append(item.ItemCode);
            //    sb.Append(Environment.NewLine);
            //}
            //MessageBox.Show(sb.ToString());

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
                String Quantity = "1";
                String Unit = unit_addNonStock_textBox.Text;
                String UnitPrice = "";
                String Currency = "";
                String ROS_Date = DatabaseAPI.DBAccessSingleton.Instance.GetDateTime().AddDays(14).ToString("MM/dd/yyyy");
                String Comments = "";
                String AC_No = budgetInfo_addNonStock_dataGridView.SelectedRows[0].Cells["AC_No"].FormattedValue.ToString();

                addList_addNonStock_dataGridView.Rows.Add(BudgetPool, ItemCode, ItemDesc, Quantity, Unit, UnitPrice, Currency, ROS_Date, Comments, AC_No);
            }
        }

        private void PopulateFilteredBudgetInfoListDGV()
        {
            ICollection<Domain.BudgetInfo> budgetInfoItemDatafilter = DatabaseAPI.DBAccessSingleton.Instance.NonStockItemsAPI.GetFilterBudgetInfo_List(budgetListData, AcSearch_addNonStock_textBox.Text, AcDescSearch_addNonStock_textBox.Text);

            if (budgetInfoItemDatafilter == null)
            {
                MessageBox.Show("DB error");
                return;
            }

            budgetListDGV_source.DataSource = budgetInfoItemDatafilter;
            budgetInfo_addNonStock_dataGridView.DataSource = budgetListDGV_source;

            budgetInfo_addNonStock_dataGridView.ClearSelection();
        }

        private void AcSearch_addNonStock_textBox_TextChanged(object sender, EventArgs e)
        {
            PopulateFilteredBudgetInfoListDGV();
        }

        private void AcDescSearch_addNonStock_textBox_TextChanged(object sender, EventArgs e)
        {
            PopulateFilteredBudgetInfoListDGV();
        }
    }
}
