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
        public MSRMainForm()
        {
            InitializeComponent();
            InitalizeStartingFields();
            RefreshDataGridViews();
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

        private void RefreshDataGridViews()
        {
            //testing
            MessageBox.Show("Testing " + BusinessAPI.BusinessSingleton.Instance.userInfo.Username);
        }

        private void AddStock_createTab_button_Click(object sender, EventArgs e)
        {
            this.Hide();
            AddStockItemForm fAddStockItem = new AddStockItemForm();
            fAddStockItem.ShowDialog();
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
