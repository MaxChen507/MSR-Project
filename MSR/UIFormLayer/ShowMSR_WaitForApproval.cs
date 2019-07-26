using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MSR.UIFormLayer
{
    public partial class ShowMSR_WaitForApproval : Form
    {
        //EF Variables
        MSR msrInfo;

        //View Variables
        ICollection<Domain.FormItems> viewFormItems;
        Domain.GroupsInfo groupsInfo;
        

        public ShowMSR_WaitForApproval(String msrId)
        {
            InitializeComponent();
            msrInfo = BusinessAPI.BusinessSingleton.Instance.MSRInfoAPI.GetMSRByMSRId(msrId);

        }

        private void ShowMSR_WaitForApproval_Load(object sender, EventArgs e)
        {
            //Update the BusininessAPI with FormItems
            viewFormItems = BusinessAPI.BusinessSingleton.Instance.MSRInfoAPI.GetDomainFormItems(msrInfo.FormItems, msrInfo.BP_No);
            BusinessAPI.BusinessSingleton.Instance.formItemListWaitForApproval = viewFormItems;

            //Set Group Info from BusininessAPI
            groupsInfo = BusinessAPI.BusinessSingleton.Instance.GetGroupsInfo();

            InitalizeStartingFields();
        }

        private void InitalizeStartingFields()
        {
            //Initialze Project GroupBox
            project_showMSR_textBox.Text = msrInfo.Project;
            wellVL_showMSR_textBox.Text = msrInfo.WVL;
            comments_showMSR_textBox.Text = msrInfo.Comments;

            //Initialize Budget GroupBox
            budgetYear_showMSR_textBox.Text = msrInfo.BudgetYear.ToString();
            budgetPool_showMSR_textBox.Text = msrInfo.BP_No;
            AFE_showMSR_textBox.Text = msrInfo.AFE;

            //Initialize Vendors
            suggVendor_showMSR_textBox.Text = msrInfo.SugVendor;
            vendorContact_showMSR_textBox.Text = msrInfo.ContactVendor;

            //Initialize Approve GroupBox
            originator_showMSR_textBox.Text = msrInfo.Usr_RO.FullName;
            compApproval_showMSR_textBox.Text = msrInfo.Usr_CA.FullName;
            changeDate_showMSR_dateTimePicker.Value = BusinessAPI.BusinessSingleton.Instance.GetDateTime();

            ShowMSR_DGV_Load();

            //Depending on the type of User, different controls will change
            if (groupsInfo.GroupsName.Equals(Domain.WorkFlowTrace.StandUser))
            {
                edit_showMSR_groupBox.Enabled = false;
                approve_showMSR_groupBox.Enabled = false;

                changeDate_showMSR_dateTimePicker.Value = BusinessAPI.BusinessSingleton.Instance.GetDateTime();
            }
            else if (groupsInfo.GroupsName.Equals(Domain.WorkFlowTrace.StandBH))
            {
                edit_showMSR_groupBox.Enabled = true;
                approve_showMSR_groupBox.Enabled = true;

                //Enable editing for quantity and comments
                showMSR_dataGridView.Columns[3].ReadOnly = false;
                showMSR_dataGridView.Columns[8].ReadOnly = false;
            }

        }

        private void ShowMSR_DGV_Load()
        {
            //DGV clear
            UserInterfaceAPI.UserInterfaceSIngleton.Instance.Custom_DGV_Clear(showMSR_dataGridView);

            foreach (Domain.FormItems item in BusinessAPI.BusinessSingleton.Instance.formItemListWaitForApproval)
            {
                showMSR_dataGridView.Rows.Add(item.BudgetPool, item.ItemCode, item.ItemDesc, item.Quantity, item.Unit, item.UnitPrice, item.Currency, item.ROSDate, item.Comments, item.ACNo);
            }

        }

        private void Approve_showMSR_radioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (approve_showMSR_radioButton.Checked)
            {
                reason_showMSR_groupBox.Visible = false;
                approve_showMSR_Button.Text = "Approve";
                reason_showMSR_label.Text = "*Reason Approve";

                //Change UI accessability
                UndoAllChanges();
                edit_showMSR_groupBox.Enabled = true;
                //Enable editing for quantity and comments
                showMSR_dataGridView.Columns[3].ReadOnly = false;
                showMSR_dataGridView.Columns[8].ReadOnly = false;
            }
        }

        private void NeedReview_showMSR_radioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (needReview_showMSR_radioButton.Checked)
            {
                reason_showMSR_groupBox.Visible = true;
                approve_showMSR_Button.Text = "Send for Review";
                reason_showMSR_label.Text = "Reason why this needs review";

                //Change ApprovalDate Access
                changeDate_showMSR_checkBox.Enabled = false;
                changeDate_showMSR_dateTimePicker.Enabled = false;

                //Change UI accessability
                UndoAllChanges();
                edit_showMSR_groupBox.Enabled = false;
                //Enable editing for quantity and comments
                showMSR_dataGridView.Columns[3].ReadOnly = false;
                showMSR_dataGridView.Columns[8].ReadOnly = false;
            }
        }

        private void Decline_showMSR_radioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (decline_showMSR_radioButton.Checked)
            {
                reason_showMSR_groupBox.Visible = true;
                approve_showMSR_Button.Text = "Decline";
                reason_showMSR_label.Text = "Reason why you are declining the request";

                //Change ApprovalDate Access
                changeDate_showMSR_checkBox.Enabled = false;
                changeDate_showMSR_dateTimePicker.Enabled = false;

                //Change UI accessability
                UndoAllChanges();
                edit_showMSR_groupBox.Enabled = false;
                //Enable editing for quantity and comments
                showMSR_dataGridView.Columns[3].ReadOnly = true;
                showMSR_dataGridView.Columns[8].ReadOnly = true;
            }
        }

        private void ChangeDate_showMSR_checkBox_CheckedChanged(object sender, EventArgs e)
        {
            if (changeDate_showMSR_checkBox.Checked)
            {
                changeDate_showMSR_dateTimePicker.Enabled = true;
            }
            else
            {
                changeDate_showMSR_dateTimePicker.Enabled = false;
                changeDate_showMSR_dateTimePicker.Value = BusinessAPI.BusinessSingleton.Instance.GetDateTime();
            }
        }

        private void DeleteItem_showMSR_button_Click(object sender, EventArgs e)
        {
            if (showMSR_dataGridView.SelectedCells.Count == 0)
            {
                MessageBox.Show("Please select an item.");
                return;
            }
            showMSR_dataGridView.Rows.Remove(showMSR_dataGridView.Rows[showMSR_dataGridView.CurrentCell.RowIndex]);
        }

        private void UndoAllChanges()
        {
            //DGV clear
            UserInterfaceAPI.UserInterfaceSIngleton.Instance.Custom_DGV_Clear(showMSR_dataGridView);

            //Populate showMSR_dataGridView from Business Singleton List
            foreach (Domain.FormItems item in viewFormItems)
            {
                showMSR_dataGridView.Rows.Add(item.BudgetPool, item.ItemCode, item.ItemDesc, item.Quantity, item.Unit, item.UnitPrice, item.Currency, item.ROSDate, item.Comments, item.ACNo);
            }
        }

        private void Undo_showMSR_button_Click(object sender, EventArgs e)
        {
            UndoAllChanges();
        }

        private void AddStock_showMSR_button_Click(object sender, EventArgs e)
        {
            this.Hide();

            //Save state of DGV
            BusinessAPI.BusinessSingleton.Instance.formItemListWaitForApproval = UserInterfaceAPI.UserInterfaceSIngleton.Instance.ConvertFormItemDGV_ToFormItemList(showMSR_dataGridView);

            AddStockItemForm fAddStockItem = new AddStockItemForm(budgetPool_showMSR_textBox.Text, Domain.WorkFlowTrace.WaitForApprovalMSR);
            fAddStockItem.ShowDialog();

            //Update state of DGV
            ShowMSR_DGV_Load();

            this.Show();
        }

        private void AddNonStock_showMSR_button_Click(object sender, EventArgs e)
        {
            this.Hide();

            //Save state of DGV
            BusinessAPI.BusinessSingleton.Instance.formItemListWaitForApproval = UserInterfaceAPI.UserInterfaceSIngleton.Instance.ConvertFormItemDGV_ToFormItemList(showMSR_dataGridView);

            AddNonStockItemForm fAddNonStockItem = new AddNonStockItemForm(budgetPool_showMSR_textBox.Text, Domain.WorkFlowTrace.WaitForApprovalMSR);
            fAddNonStockItem.ShowDialog();

            //Update state of DGV
            ShowMSR_DGV_Load();

            this.Show();
        }

        private void Approve_showMSR_Button_Click(object sender, EventArgs e)
        {
            if (approve_showMSR_Button.Text.ToString().Equals("Approve"))
            {
                //EDIT AND UPDATE MSR

                if (UserInterfaceAPI.UserInterfaceSIngleton.Instance.CheckMSRFormItemsDGV(showMSR_dataGridView, budgetPool_showMSR_textBox.Text, msrInfo.Usr_CA.UserId.ToString()) == false)
                {
                    return;
                }

                //To confirm if you want to Approve MSR
                DialogResult result = MessageBox.Show("Are you sure you want to approve the MSR?", "Confirmation", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {

                }
                else if (result == DialogResult.No)
                {
                    return;
                }
                else
                {
                    return;
                }

                //Save state of DGV
                BusinessAPI.BusinessSingleton.Instance.formItemListWaitForApproval = UserInterfaceAPI.UserInterfaceSIngleton.Instance.ConvertFormItemDGV_ToFormItemList(showMSR_dataGridView);

                //DELETE from FormItems
                BusinessAPI.BusinessSingleton.Instance.MSRInfoAPI.DeleteFormItemsByMSRId(msrInfo.MSRId.ToString());

                //INSERT into FormItems
                BusinessAPI.BusinessSingleton.Instance.MSRInfoAPI.InsertInitialFormItems(BusinessAPI.BusinessSingleton.Instance.formItemListWaitForApproval, Convert.ToInt32(msrInfo.MSRId));

                //Update MSR States and Approve Dates
                BusinessAPI.BusinessSingleton.Instance.MSRInfoAPI.UpdateMSRWaitForApproval(Convert.ToInt32(msrInfo.MSRId), approve_showMSR_Button.Text.ToString(), "Approved_NA", changeDate_showMSR_dateTimePicker.Value, Domain.WorkFlowTrace.APPROVED);

            }
            else if (approve_showMSR_Button.Text.ToString().Equals("Send for Review"))
            {
                //EDIT AND UPDATE MSR

                if (UserInterfaceAPI.UserInterfaceSIngleton.Instance.CheckMSRFormItemsDGV(showMSR_dataGridView, budgetPool_showMSR_textBox.Text, msrInfo.Usr_CA.UserId.ToString()) == false)
                {
                    return;
                }

                //To confirm if you want to Send for Review MSR
                DialogResult result = MessageBox.Show("Are you sure you want to send the MSR for review?", "Confirmation", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {

                }
                else if (result == DialogResult.No)
                {
                    return;
                }
                else
                {
                    return;
                }

                //Save state of DGV
                BusinessAPI.BusinessSingleton.Instance.formItemListWaitForApproval = UserInterfaceAPI.UserInterfaceSIngleton.Instance.ConvertFormItemDGV_ToFormItemList(showMSR_dataGridView);

                //DELETE from FormItems
                BusinessAPI.BusinessSingleton.Instance.MSRInfoAPI.DeleteFormItemsByMSRId(msrInfo.MSRId.ToString());

                //INSERT into FormItems
                BusinessAPI.BusinessSingleton.Instance.MSRInfoAPI.InsertInitialFormItems(BusinessAPI.BusinessSingleton.Instance.formItemListWaitForApproval, Convert.ToInt32(msrInfo.MSRId));

                //Update MSR
                BusinessAPI.BusinessSingleton.Instance.MSRInfoAPI.UpdateMSRWaitForApproval(Convert.ToInt32(msrInfo.MSRId), approve_showMSR_Button.Text.ToString(), reason_showMSR_richTextBox.Text.ToString(), BusinessAPI.BusinessSingleton.Instance.GetDateTime(), Domain.WorkFlowTrace.NEED_REVIEW);
            }
            else if (approve_showMSR_Button.Text.ToString().Equals("Decline"))
            {
                //To confirm if you want to Send for Decline MSR
                DialogResult result = MessageBox.Show("Are you sure you want to decline the MSR?", "Confirmation", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {

                }
                else if (result == DialogResult.No)
                {
                    return;
                }
                else
                {
                    return;
                }

                //Update MSR
                BusinessAPI.BusinessSingleton.Instance.MSRInfoAPI.UpdateMSRWaitForApproval(Convert.ToInt32(msrInfo.MSRId), approve_showMSR_Button.Text.ToString(), reason_showMSR_richTextBox.Text.ToString(), BusinessAPI.BusinessSingleton.Instance.GetDateTime(), Domain.WorkFlowTrace.DECLINED);
            }
            else
            {
                MessageBox.Show("Error");
            }

            this.Close();

        }
        

    }
}
