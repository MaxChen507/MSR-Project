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
        String OriginatorId;
        ICollection<Domain.FormItems> dbFormItems;

        //Form Setting Variables
        Domain.GroupsInfo groupsInfo;
        String workFlowTrace;

        //Test
        MSR mSRTest;

        public ShowMSR(String MSRId, String workFlowTrace)
        {
            InitializeComponent();
            //mSRTest = BusinessAPI.BusinessSingleton.Instance.MSRInfoAPI_B.GetMSRByMSRId(MSRId);


            MSRInfo = DatabaseAPI.DBAccessSingleton.Instance.MSRInfoAPI.GetMSR(MSRId);

            //Initalize shared FormItems Data List to Business Singleton
            dbFormItems = DatabaseAPI.DBAccessSingleton.Instance.MSRInfoAPI.GetFormItems_List(MSRId, MSRInfo.BP_No);

            OriginatorId = BusinessAPI.BusinessSingleton.Instance.MSRInfoAPI_B.GetOriginatorID(MSRId);

            if (workFlowTrace.Equals(Domain.WorkFlowTrace.waitForApprovalMSR))
            {
                BusinessAPI.BusinessSingleton.Instance.formItemList_WaitForApproval = dbFormItems;
            }
            else if (workFlowTrace.Equals(Domain.WorkFlowTrace.approvedMSR))
            {
                BusinessAPI.BusinessSingleton.Instance.formItemList_Approved = dbFormItems;
            }
            else if (workFlowTrace.Equals(Domain.WorkFlowTrace.needReviewMSR))
            {
                BusinessAPI.BusinessSingleton.Instance.formItemList_NeedReview = dbFormItems;
            }

            groupsInfo = BusinessAPI.BusinessSingleton.Instance.groupsInfo;
            this.workFlowTrace = workFlowTrace;

            InitalizeStartingFields();

            if (workFlowTrace.Equals(Domain.WorkFlowTrace.waitForApprovalMSR))
            {
                //MessageBox.Show("Came from waitForApproval");

                //MessageBox.Show("I am from group: " + groupsInfo.GroupsName);

                if (groupsInfo.GroupsName.Equals(Domain.WorkFlowTrace.StandUser))
                {
                    edit_showMSR_groupBox.Hide();
                    changeDate_showMSR_dateTimePicker.Value = DateTime.Now;
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
            else if (workFlowTrace.Equals(Domain.WorkFlowTrace.approvedMSR))
            {
                //MessageBox.Show("Came from approvedMSR");

                edit_showMSR_groupBox.Hide();

                changeDate_showMSR_checkBox.Hide();
                changeDate_showMSR_dateTimePicker.Value = MSRInfo.Appr_Date ?? DateTime.Now;

                approve_showMSR_Button.Hide();

            }
            else if (workFlowTrace.Equals(Domain.WorkFlowTrace.needReviewMSR))
            {
                //MessageBox.Show("Came from needReview");

                if (BusinessAPI.BusinessSingleton.Instance.userInfo.UserId.Equals(OriginatorId))
                {
                    //MessageBox.Show("UserID: " + BusinessAPI.BusinessSingleton.Instance.userInfo.UserId + " " + "MSR OG: " + OriginatorId);
                    //Shows a match, they can edit

                    needReview_showMSR_radioButton.Checked = true;

                    edit_showMSR_groupBox.Enabled = true;

                    reason_showMSR_richTextBox.Enabled = false;
                    reason_showMSR_richTextBox.Text = MSRInfo.Review_Comment;

                    //Enable editing for quantity and comments
                    showMSR_dataGridView.Columns[3].ReadOnly = false;
                    showMSR_dataGridView.Columns[8].ReadOnly = false;

                    submitReview_showMSR_button.Visible = true;
                    submitReview_showMSR_button.Enabled = true;

                    changeDate_showMSR_checkBox.Hide();
                    changeDate_showMSR_dateTimePicker.Hide();
                }
                else
                {
                    edit_showMSR_groupBox.Hide();

                    changeDate_showMSR_checkBox.Hide();
                    changeDate_showMSR_dateTimePicker.Hide();

                    needReview_showMSR_radioButton.Checked = true;
                    reason_showMSR_richTextBox.Enabled = false;
                    reason_showMSR_richTextBox.Text = MSRInfo.Review_Comment;
                }

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

            if (workFlowTrace.Equals(Domain.WorkFlowTrace.waitForApprovalMSR))
            {
                //Populate showMSR_dataGridView from Business Singleton List
                foreach (Domain.FormItems item in BusinessAPI.BusinessSingleton.Instance.formItemList_WaitForApproval)
                {
                    showMSR_dataGridView.Rows.Add(item.BudgetPool, item.ItemCode, item.ItemDesc, item.Quantity, item.Unit, item.UnitPrice, item.Currency, item.ROS_Date, item.Comments, item.AC_No);
                }
            }
            else if (workFlowTrace.Equals(Domain.WorkFlowTrace.approvedMSR))
            {
                //Populate showMSR_dataGridView from Business Singleton List
                foreach (Domain.FormItems item in BusinessAPI.BusinessSingleton.Instance.formItemList_Approved)
                {
                    showMSR_dataGridView.Rows.Add(item.BudgetPool, item.ItemCode, item.ItemDesc, item.Quantity, item.Unit, item.UnitPrice, item.Currency, item.ROS_Date, item.Comments, item.AC_No);
                }
            }
            else if (workFlowTrace.Equals(Domain.WorkFlowTrace.needReviewMSR))
            {
                //Populate showMSR_dataGridView from Business Singleton List
                foreach (Domain.FormItems item in BusinessAPI.BusinessSingleton.Instance.formItemList_NeedReview)
                {
                    showMSR_dataGridView.Rows.Add(item.BudgetPool, item.ItemCode, item.ItemDesc, item.Quantity, item.Unit, item.UnitPrice, item.Currency, item.ROS_Date, item.Comments, item.AC_No);
                }
            }

        }

        private void Approve_showMSR_radioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (approve_showMSR_radioButton.Checked)
            {
                //Change ApprovalDate Access
                changeDate_showMSR_checkBox.Show();
                changeDate_showMSR_dateTimePicker.Show();

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

                //Change ApprovalDate Access
                changeDate_showMSR_checkBox.Hide();
                changeDate_showMSR_dateTimePicker.Hide();

                //Change UI accessability
                undo_showMSR_button.PerformClick();
                edit_showMSR_groupBox.Enabled = false;
                //Enable editing for quantity and comments
                showMSR_dataGridView.Columns[3].ReadOnly = true;
                showMSR_dataGridView.Columns[8].ReadOnly = true;
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
                changeDate_showMSR_checkBox.Hide();
                changeDate_showMSR_dateTimePicker.Hide();
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
            //MessageBox.Show("Deleting: " + showMSR_dataGridView.Rows[showMSR_dataGridView.CurrentCell.RowIndex].ToString());
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

            if (workFlowTrace.Equals(Domain.WorkFlowTrace.waitForApprovalMSR))
            {
                //Save state of DGV
                BusinessAPI.BusinessSingleton.Instance.formItemList_WaitForApproval = UserInterfaceAPI.UserInterfaceSIngleton.Instance.ConvertFormItemDGV_ToFormItemList(showMSR_dataGridView);

                AddStockItemForm fAddStockItem = new AddStockItemForm(budgetPool_showMSR_textBox.Text, Domain.WorkFlowTrace.waitForApprovalMSR);
                fAddStockItem.ShowDialog();
            }
            else if (workFlowTrace.Equals(Domain.WorkFlowTrace.needReviewMSR))
            {
                //Save state of DGV
                BusinessAPI.BusinessSingleton.Instance.formItemList_NeedReview = UserInterfaceAPI.UserInterfaceSIngleton.Instance.ConvertFormItemDGV_ToFormItemList(showMSR_dataGridView);

                AddStockItemForm fAddStockItem = new AddStockItemForm(budgetPool_showMSR_textBox.Text, Domain.WorkFlowTrace.needReviewMSR);
                fAddStockItem.ShowDialog();
            }

            //Update state of DGV
            ShowMSR_DGV_Load();

            this.Show();
        }

        private void AddNonStock_showMSR_button_Click(object sender, EventArgs e)
        {
            this.Hide();

            if (workFlowTrace.Equals(Domain.WorkFlowTrace.waitForApprovalMSR))
            {
                //Save state of DGV
                BusinessAPI.BusinessSingleton.Instance.formItemList_WaitForApproval = UserInterfaceAPI.UserInterfaceSIngleton.Instance.ConvertFormItemDGV_ToFormItemList(showMSR_dataGridView);

                AddNonStockItemForm fAddNonStockItem = new AddNonStockItemForm(budgetPool_showMSR_textBox.Text, Domain.WorkFlowTrace.waitForApprovalMSR);
                fAddNonStockItem.ShowDialog();
            }
            else if (workFlowTrace.Equals(Domain.WorkFlowTrace.needReviewMSR))
            {
                //Save state of DGV
                BusinessAPI.BusinessSingleton.Instance.formItemList_NeedReview = UserInterfaceAPI.UserInterfaceSIngleton.Instance.ConvertFormItemDGV_ToFormItemList(showMSR_dataGridView);

                AddNonStockItemForm fAddNonStockItem = new AddNonStockItemForm(budgetPool_showMSR_textBox.Text, Domain.WorkFlowTrace.needReviewMSR);
                fAddNonStockItem.ShowDialog();
            }
            
            //Update state of DGV
            ShowMSR_DGV_Load();

            this.Show();
        }

        private void Approve_showMSR_Button_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(approve_showMSR_Button.Text.ToString());

            if (approve_showMSR_Button.Text.ToString().Equals("Approve"))
            {
                //MessageBox.Show("It says Approve");

                //EDIT AND UPDATE MSR

                if(CheckShowMSRDGV() == false)
                {
                    return;
                }

                //To confirm if you want to Approve MSR
                DialogResult result = MessageBox.Show("Are you sure you want to approve the MSR?", "Confirmation", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    //MessageBox.Show("Selected YES.");
                    //...
                }
                else if (result == DialogResult.No)
                {
                    //MessageBox.Show("Selected NO.");
                    return;
                }
                else
                {
                    //MessageBox.Show("Selected NO.");
                    return;
                }

                //testing
                //MessageBox.Show("MSR ID is: " + MSRInfo.MSRId);

                //Save state of DGV
                BusinessAPI.BusinessSingleton.Instance.formItemList_WaitForApproval = UserInterfaceAPI.UserInterfaceSIngleton.Instance.ConvertFormItemDGV_ToFormItemList(showMSR_dataGridView);

                //DELETE from FormItems
                DatabaseAPI.DBAccessSingleton.Instance.MSRInfoAPI.DeleteFormItems(Convert.ToInt32(MSRInfo.MSRId));

                //INSERT into FormItems
                foreach (Domain.FormItems item in BusinessAPI.BusinessSingleton.Instance.formItemList_WaitForApproval)
                {
                    DatabaseAPI.DBAccessSingleton.Instance.MSRInfoAPI.InsertInitialFormItems(item, Convert.ToInt32(MSRInfo.MSRId));
                }

                //Update MSR States and Approve Dates
                DatabaseAPI.DBAccessSingleton.Instance.MSRInfoAPI.UpdateMSR_ApproveButton(Convert.ToInt32(MSRInfo.MSRId), approve_showMSR_Button.Text.ToString(), "Approved_NA", changeDate_showMSR_dateTimePicker.Value, Domain.WorkFlowTrace.APPROVED);

            }
            else if (approve_showMSR_Button.Text.ToString().Equals("Send for Review"))
            {
                //MessageBox.Show("It says Send for Review");

                //To confirm if you want to Send for Review MSR
                DialogResult result = MessageBox.Show("Are you sure you want to send the MSR for review?", "Confirmation", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    //MessageBox.Show("Selected YES.");
                    //...
                }
                else if (result == DialogResult.No)
                {
                    //MessageBox.Show("Selected NO.");
                    return;
                }
                else
                {
                    //MessageBox.Show("Selected NO.");
                    return;
                }

                //Update MSR
                DatabaseAPI.DBAccessSingleton.Instance.MSRInfoAPI.UpdateMSR_ApproveButton(Convert.ToInt32(MSRInfo.MSRId), approve_showMSR_Button.Text.ToString(), reason_showMSR_richTextBox.Text.ToString(), DatabaseAPI.DBAccessSingleton.Instance.GetDateTime(), Domain.WorkFlowTrace.NEED_REVIEW);
            }
            else if (approve_showMSR_Button.Text.ToString().Equals("Decline"))
            {
                //MessageBox.Show("It says Decline");

                //To confirm if you want to Send for Review MSR
                DialogResult result = MessageBox.Show("Are you sure you want to decline the MSR?", "Confirmation", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    //MessageBox.Show("Selected YES.");
                    //...
                }
                else if (result == DialogResult.No)
                {
                    //MessageBox.Show("Selected NO.");
                    return;
                }
                else
                {
                    //MessageBox.Show("Selected NO.");
                    return;
                }

                //Update MSR
                DatabaseAPI.DBAccessSingleton.Instance.MSRInfoAPI.UpdateMSR_ApproveButton(Convert.ToInt32(MSRInfo.MSRId), approve_showMSR_Button.Text.ToString(), reason_showMSR_richTextBox.Text.ToString(), DatabaseAPI.DBAccessSingleton.Instance.GetDateTime(), Domain.WorkFlowTrace.DECLINED);
            }
            else
            {
                MessageBox.Show("Error");
            }

            this.Close();

        }

        private void SubmitReview_showMSR_button_Click(object sender, EventArgs e)
        {
            if (CheckShowMSRDGV() == false)
            {
                return;
            }

            //To confirm if you want to submit MSR edits
            DialogResult result = MessageBox.Show("Are you sure you want to submit the MSR edits?", "Confirmation", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                //MessageBox.Show("Selected YES.");
                //...
            }
            else if (result == DialogResult.No)
            {
                //MessageBox.Show("Selected NO.");
                return;
            }
            else
            {
                //MessageBox.Show("Selected NO.");
                return;
            }

            //testing
            //MessageBox.Show("MSR ID is: " + MSRInfo.MSRId);

            //Save state of DGV
            BusinessAPI.BusinessSingleton.Instance.formItemList_NeedReview = UserInterfaceAPI.UserInterfaceSIngleton.Instance.ConvertFormItemDGV_ToFormItemList(showMSR_dataGridView);

            //DELETE from FormItems
            DatabaseAPI.DBAccessSingleton.Instance.MSRInfoAPI.DeleteFormItems(Convert.ToInt32(MSRInfo.MSRId));

            //INSERT into FormItems
            foreach (Domain.FormItems item in BusinessAPI.BusinessSingleton.Instance.formItemList_NeedReview)
            {
                DatabaseAPI.DBAccessSingleton.Instance.MSRInfoAPI.InsertInitialFormItems(item, Convert.ToInt32(MSRInfo.MSRId));
            }

            //Update MSR States and Approve Dates
            DatabaseAPI.DBAccessSingleton.Instance.MSRInfoAPI.UpdateMSR_SubmitReview(Convert.ToInt32(MSRInfo.MSRId), Domain.WorkFlowTrace.WAIT_FOR_APPROVAL);

            this.Close();
        }

        private Boolean CheckShowMSRDGV()
        {
            if (showMSR_dataGridView.Rows.Count == 0)
            {
                MessageBox.Show("Please add an item.");
                return false;
            }

            //Checking if all item's fields are correct
            Boolean itemsCorrectFlag = true;

            //Checking if all items's budget pool matches combobox budget pool
            foreach (DataGridViewRow row in showMSR_dataGridView.Rows)
            {
                if (!(row.Cells["BudgetPool"].FormattedValue.ToString().Equals(budgetPool_showMSR_textBox.Text)))
                {
                    Color lightRed = ControlPaint.Light(Color.Red);
                    row.Cells["BudgetPool"].Style.BackColor = lightRed;
                    itemsCorrectFlag = false;
                }
                else
                {
                    row.Cells["BudgetPool"].Style.BackColor = (Color)System.Drawing.SystemColors.Window;
                }
            }

            if (itemsCorrectFlag)
            {
                //MessageBox.Show("All item's budget pool match with selected Budget Pool.");
            }
            else
            {
                MessageBox.Show("Highlighted item's budget pool doesn't match with selected Budget Pool.");
                return false;
            }

            //Checking if all numeric fields are numeric
            foreach (DataGridViewRow row in showMSR_dataGridView.Rows)
            {
                if (!BusinessAPI.BusinessSingleton.Instance.IsNumeric(row.Cells["Quantity"].FormattedValue.ToString()))
                {
                    Color lightRed = ControlPaint.Light(Color.Red);
                    row.Cells["Quantity"].Style.BackColor = lightRed;
                    itemsCorrectFlag = false;
                }
                else
                {
                    row.Cells["Quantity"].Style.BackColor = (Color)System.Drawing.SystemColors.Window;
                }

                if (!BusinessAPI.BusinessSingleton.Instance.IsNumeric(row.Cells["UnitPrice"].FormattedValue.ToString()) && !String.IsNullOrEmpty(row.Cells["UnitPrice"].FormattedValue.ToString()))
                {
                    Color lightRed = ControlPaint.Light(Color.Red);
                    row.Cells["UnitPrice"].Style.BackColor = lightRed;
                    itemsCorrectFlag = false;
                }
                else
                {
                    row.Cells["UnitPrice"].Style.BackColor = (Color)System.Drawing.SystemColors.Window;
                }
            }

            if (itemsCorrectFlag)
            {
                //MessageBox.Show("Quantity and UnitPrice are double.");
            }
            else
            {
                MessageBox.Show("Highlighted items' fields must be corrected to double.");
                return false;
            }

            //Checking if all required form item fields are set
            foreach (DataGridViewRow row in showMSR_dataGridView.Rows)
            {
                if (String.IsNullOrEmpty(row.Cells["Quantity"].FormattedValue.ToString()))
                {
                    Color lightRed = ControlPaint.Light(Color.Red);
                    row.Cells["Quantity"].Style.BackColor = lightRed;
                    itemsCorrectFlag = false;
                }
                else
                {
                    row.Cells["Quantity"].Style.BackColor = (Color)System.Drawing.SystemColors.Window;
                }
            }

            if (itemsCorrectFlag)
            {
                //MessageBox.Show("All form item fields are filled.");
            }
            else
            {
                MessageBox.Show("Highlighted items' fields must be filled");
                return false;
            }

            return true;
        }


    }
}
