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
            RefreshDataGridViews();
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
