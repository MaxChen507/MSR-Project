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
    public partial class MSRMainForm : Form
    {
        //Binding Source Initialization
        BindingSource createTabDGV_source { get; set; }
        
        public MSRMainForm()
        {
            InitializeComponent();
            InitalizeStartingFields();
            RefreshDataGridView();
        }

        private void InitalizeStartingFields()
        {
            //Full Date from server
            //MessageBox.Show(DatabaseAPI.DBAccessSingleton.Instance.GetDateTime().ToString());

            //Initialize Budget Year ComboBox
            int currentYear = DatabaseAPI.DBAccessSingleton.Instance.GetDateTime().Year;

            budgetYear_createTab_comboBox.Items.Add((currentYear - 1).ToString());
            budgetYear_createTab_comboBox.Items.Add((currentYear).ToString());
            budgetYear_createTab_comboBox.Items.Add((currentYear + 1).ToString());

            //Initialize BP ComboBox
            foreach (String item in BusinessAPI.BusinessSingleton.Instance.GetUniqueBP_List())
            {
                budgetPool_createTab_comboBox.Items.Add(item);
            }

            //Initialize Originator Combobox Selected Item
            originator_createTab_comboBox.Enabled = true;
            originator_createTab_comboBox.Items.Add(BusinessAPI.BusinessSingleton.Instance.userInfo.Username);
            originator_createTab_comboBox.SelectedItem = BusinessAPI.BusinessSingleton.Instance.userInfo.Username;

            //Initialize DateTime Picker
            changeDate_createTab_dateTimePicker.Value = DatabaseAPI.DBAccessSingleton.Instance.GetDateTime();

        }

        private void RefreshDataGridView()
        {
            //DGV clear
            createTab_dataGridView.DataSource = null;
            createTab_dataGridView.Rows.Clear();
            createTab_dataGridView.Refresh();

            createTab_dataGridView.ClearSelection();

            //Populate from Singleton List
            foreach (Domain.FormItems item in BusinessAPI.BusinessSingleton.Instance.formItemList)
            {
                createTab_dataGridView.Rows.Add(item.ItemCode, item.ItemDesc, "1", item.Unit, "", "", item.AC_No);
            }
        }

        private void UpdateBusinessSingletonFormItemList()
        {
            //Data List Initialization
            ICollection<Domain.FormItems> tempData_List = new List<Domain.FormItems>();

            //Copy data from data grid view and populate addListData
            foreach (DataGridViewRow row in createTab_dataGridView.Rows)
            {
                String ItemCode = row.Cells["ItemCode"].FormattedValue.ToString();
                String ItemDesc = row.Cells["ItemDesc"].FormattedValue.ToString();
                String Unit = row.Cells["Unit"].FormattedValue.ToString();
                String AC_No = row.Cells["AC_No"].FormattedValue.ToString();

                Domain.FormItems addItem = new Domain.FormItems(ItemCode, ItemDesc, "1", Unit, "", "", "", "", AC_No);

                tempData_List.Add(addItem);
            }

            //Assigns temp List to singleton List
            BusinessAPI.BusinessSingleton.Instance.formItemList = tempData_List;
        }

        private void AddStock_createTab_button_Click(object sender, EventArgs e)
        {
            this.Hide();

            //Save state of DGV
            UpdateBusinessSingletonFormItemList();

            AddStockItemForm fAddStockItem = new AddStockItemForm(budgetPool_createTab_comboBox.Text);
            fAddStockItem.ShowDialog();

            //Update state of DGV
            RefreshDataGridView();

            this.Show();
        }

        private void AddNonStock_createTab_button_Click(object sender, EventArgs e)
        {
            this.Hide();
            AddNonStockItemForm fAddNonStockItem = new AddNonStockItemForm();
            fAddNonStockItem.ShowDialog();
            this.Show();
        }

        private void Test_ShowMSR_Click(object sender, EventArgs e)
        {
            this.Hide();
            ShowMSR fshowMSR = new ShowMSR();
            fshowMSR.ShowDialog();
            this.Show();
        }

        private void BudgetPool_createTab_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Clear then Populate Approval Combobox
            approval_createTab_comboBox.Items.Clear();
            approval_createTab_comboBox.Enabled = true;
            foreach (String item in DatabaseAPI.DBAccessSingleton.Instance.BudgetInfoAPI.GetBudgetHolder_List(budgetPool_createTab_comboBox.Text))
            {
                approval_createTab_comboBox.Items.Add(item);
            }

            addStock_createTab_button.Enabled = true;
            addNonStock_createTab_button.Enabled = true;


            //TODO:
        }

        private void ChangeDate_createTab_checkBox_CheckedChanged(object sender, EventArgs e)
        {
            if(changeDate_createTab_checkBox.Checked)
            {
                changeDate_createTab_dateTimePicker.Enabled = true;
            }
            else
            {
                changeDate_createTab_dateTimePicker.Enabled = false;
                changeDate_createTab_dateTimePicker.Value = DatabaseAPI.DBAccessSingleton.Instance.GetDateTime();
            }
            
        }
    }
}
