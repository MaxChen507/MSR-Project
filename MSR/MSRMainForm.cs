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
            InitalizeBudgetFieldsSection();
            RefreshDataGridViews();
        }

        private void InitalizeBudgetFieldsSection()
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
    }
}
