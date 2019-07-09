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
    public partial class ShowMSR : Form
    {
        //Form Variables
        Domain.MSRInfo MSRInfo;
        ICollection<Domain.FormItems> dbFormItems;

        //Form Setting Variables
        Domain.GroupsInfo groupsInfo;
        String workFlowTrace;
       

        public ShowMSR(String MSRId, String workFlowTrace)
        {
            InitializeComponent();
            MSRInfo = DatabaseAPI.DBAccessSingleton.Instance.MSRInfoAPI.GetMSR(MSRId);

            //Initalize shared FormItems Data List to Business Singleton
            dbFormItems = DatabaseAPI.DBAccessSingleton.Instance.MSRInfoAPI.GetFormItems_List(MSRId, MSRInfo.BP_No);
            BusinessAPI.BusinessSingleton.Instance.formItemList_WaitForApproval = dbFormItems;

            groupsInfo = BusinessAPI.BusinessSingleton.Instance.groupsInfo;
            this.workFlowTrace = workFlowTrace;

            InitalizeStartingFields();

            if (workFlowTrace.Equals(Domain.WorkFlowTrace.waitForApproval))
            {
                MessageBox.Show("Came from waitForApproval");

                MessageBox.Show("I am from group: " + groupsInfo.GroupsName);

                if (groupsInfo.GroupsName.Equals(Domain.WorkFlowTrace.StandUser))
                {
                    edit_showMSR_groupBox.Hide();
                }

                if (groupsInfo.GroupsName.Equals(Domain.WorkFlowTrace.StandBH))
                {
                    edit_showMSR_groupBox.Enabled = true;
                    approve_showMSR_groupBox.Enabled = true;

                    //Enable editing for quantity and comments
                    showMSR_dataGridView.Columns[3].ReadOnly = false;
                    showMSR_dataGridView.Columns[8].ReadOnly = false;
                }

            }
            else if (workFlowTrace.Equals(Domain.WorkFlowTrace.needReview))
            {
                MessageBox.Show("Came from needReview");
            }
            else if (workFlowTrace.Equals(Domain.WorkFlowTrace.approvedMSR))
            {
                MessageBox.Show("Came from approvedMSR");
            }
            else
            {
                MessageBox.Show("Came from error");
            }

        }

        private void InitalizeStartingFields()
        {
            //Initialze Project GroupBox
            project_showMSR_textBox.Text = MSRInfo.Project;
            wellVL_showMSR_textBox.Text = MSRInfo.WVL;
            comments_showMSR_textBox.Text = MSRInfo.Comments;

            //Initialize Budget GroupBox
            budgetYear_showMSR_textBox.Text = MSRInfo.BudgetYear;
            budgetPool_showMSR_textBox.Text = MSRInfo.BP_No;
            AFE_showMSR_textBox.Text = MSRInfo.AFE;

            //Initialize Vendors
            suggVendor_showMSR_textBox.Text = MSRInfo.SugVendor;
            vendorContact_showMSR_textBox.Text = MSRInfo.ContactVendor;

            //Initialize Approve GroupBox
            originator_showMSR_textBox.Text = MSRInfo.Request_Originator;
            compApproval_showMSR_textBox.Text = MSRInfo.Company_Approval;
            changeDate_showMSR_dateTimePicker.Value = DatabaseAPI.DBAccessSingleton.Instance.GetDateTime();

            ShowMSR_DGV_Load();

        }

        private void ShowMSR_DGV_Load()
        {
            //DGV clear
            UserInterfaceAPI.UserInterfaceSIngleton.Instance.Custom_DGV_Clear(showMSR_dataGridView);

            //Populate showMSR_dataGridView from Business Singleton List
            foreach (Domain.FormItems item in BusinessAPI.BusinessSingleton.Instance.formItemList_WaitForApproval)
            {
                showMSR_dataGridView.Rows.Add(item.BudgetPool, item.ItemCode, item.ItemDesc, item.Quantity, item.Unit, item.UnitPrice, item.Currency, item.ROS_Date, item.Comments, item.AC_No);
            }

        }

        private void Approve_showMSR_radioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (approve_showMSR_radioButton.Checked)
            {
                reason_showMSR_groupBox.Visible = false;
                approve_showMSR_Button.Text = "Approve";
                reason_showMSR_label.Text = "*Reason Approve";
            }
        }

        private void NeedReview_showMSR_radioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (needReview_showMSR_radioButton.Checked)
            {
                reason_showMSR_groupBox.Visible = true;
                approve_showMSR_Button.Text = "Send for Review";
                reason_showMSR_label.Text = "Reason why this needs review";
            }
        }

        private void Decline_showMSR_radioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (decline_showMSR_radioButton.Checked)
            {
                reason_showMSR_groupBox.Visible = true;
                approve_showMSR_Button.Text = "Decline";
                reason_showMSR_label.Text = "Reason why you are declining the request";
            }
        }

        private void ChangeDate_showMSR_checkBox_CheckedChanged(object sender, EventArgs e)
        {
            if (changeDate_showMSR_checkBox.Checked) {
                changeDate_showMSR_dateTimePicker.Enabled = true;
            }
            else
            {
                changeDate_showMSR_dateTimePicker.Enabled = false;
                changeDate_showMSR_dateTimePicker.Value = DatabaseAPI.DBAccessSingleton.Instance.GetDateTime();
            }
        }

        private void DeleteItem_showMSR_button_Click(object sender, EventArgs e)
        {
            if (showMSR_dataGridView.SelectedCells.Count == 0)
            {
                MessageBox.Show("Please select an item.");
                return;
            }
            MessageBox.Show("Deleting: " + showMSR_dataGridView.Rows[showMSR_dataGridView.CurrentCell.RowIndex].ToString());
            showMSR_dataGridView.Rows.Remove(showMSR_dataGridView.Rows[showMSR_dataGridView.CurrentCell.RowIndex]);
        }

        private void Undo_showMSR_button_Click(object sender, EventArgs e)
        {
            //DGV clear
            UserInterfaceAPI.UserInterfaceSIngleton.Instance.Custom_DGV_Clear(showMSR_dataGridView);

            //Populate showMSR_dataGridView from Business Singleton List
            foreach (Domain.FormItems item in dbFormItems)
            {
                showMSR_dataGridView.Rows.Add(item.BudgetPool, item.ItemCode, item.ItemDesc, item.Quantity, item.Unit, item.UnitPrice, item.Currency, item.ROS_Date, item.Comments, item.AC_No);
            }
        }

        private void AddStock_showMSR_button_Click(object sender, EventArgs e)
        {
            this.Hide();

            //Save state of DGV
            BusinessAPI.BusinessSingleton.Instance.formItemList_WaitForApproval = UserInterfaceAPI.UserInterfaceSIngleton.Instance.UpdateBusinessSingletonFormItemList(showMSR_dataGridView);

            AddStockItemForm fAddStockItem = new AddStockItemForm(budgetPool_showMSR_textBox.Text, Domain.WorkFlowTrace.waitForApproval);
            fAddStockItem.ShowDialog();

            //Update state of DGV
            ShowMSR_DGV_Load();

            this.Show();
        }

        private void AddNonStock_showMSR_button_Click(object sender, EventArgs e)
        {
            this.Hide();

            //Save state of DGV
            BusinessAPI.BusinessSingleton.Instance.formItemList_WaitForApproval = UserInterfaceAPI.UserInterfaceSIngleton.Instance.UpdateBusinessSingletonFormItemList(showMSR_dataGridView);

            AddNonStockItemForm fAddNonStockItem = new AddNonStockItemForm(budgetPool_showMSR_textBox.Text, Domain.WorkFlowTrace.waitForApproval);
            fAddNonStockItem.ShowDialog();

            //Update state of DGV
            ShowMSR_DGV_Load();

            this.Show();
        }

        private void Approve_showMSR_Button_Click(object sender, EventArgs e)
        {
            MessageBox.Show(approve_showMSR_Button.Text.ToString());

            if (approve_showMSR_Button.Text.ToString().Equals("Approve"))
            {
                MessageBox.Show("It says Approve");
            }
            else if (approve_showMSR_Button.Text.ToString().Equals("Send for Review"))
            {
                MessageBox.Show("It says Send for Review");
            }
            else if (approve_showMSR_Button.Text.ToString().Equals("Decline"))
            {
                MessageBox.Show("It says Decline");
            }
            else
            {
                MessageBox.Show("Error");
            }


        }
    }
}
