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
        //Binding Source Variables
        BindingSource waitApprovalTabDGV_source = new BindingSource();
        BindingSource needReviewTabDGV_source = new BindingSource();
        BindingSource approvedTabDGV_source = new BindingSource();
        
        public MSRMainForm()
        {
            InitializeComponent();
        }

        private void MSRMainForm_Load(object sender, EventArgs e)
        {
            InitalizeStartingFields();
            RefreshDataGridViews();
        }

        private void InitalizeStartingFields()
        {
            //Initialize Budget Year ComboBox
            int currentYear = BusinessAPI.BusinessSingleton.Instance.GetDateTime().Year;
            budgetYear_createTab_comboBox.Items.Add((currentYear - 1).ToString());
            budgetYear_createTab_comboBox.Items.Add((currentYear).ToString());
            budgetYear_createTab_comboBox.Items.Add((currentYear + 1).ToString());

            //Initialize BP ComboBox
            foreach (String item in BusinessAPI.BusinessSingleton.Instance.GetUniqueBP_Access_List())
            {
                budgetPool_createTab_comboBox.Items.Add(item);
            }

            //Initialize Originator Combobox Selected Item
            originator_createTab_comboBox.Enabled = true;
            originator_createTab_comboBox.Items.Add(BusinessAPI.BusinessSingleton.Instance.userInfo_EF.FullName);
            originator_createTab_comboBox.SelectedItem = BusinessAPI.BusinessSingleton.Instance.userInfo_EF.FullName;

            //Initialize DateTime Picker
            changeDate_createTab_dateTimePicker.Value = BusinessAPI.BusinessSingleton.Instance.GetDateTime();

        }

        private void RefreshDataGridViews()
        {
            //DGV clear
            UserInterfaceAPI.UserInterfaceSIngleton.Instance.Custom_DGV_Clear(createTab_dataGridView);
            UserInterfaceAPI.UserInterfaceSIngleton.Instance.Custom_DGV_Clear(waitApprovalTab_dataGridView);
            UserInterfaceAPI.UserInterfaceSIngleton.Instance.Custom_DGV_Clear(needReviewTab_dataGridView);
            UserInterfaceAPI.UserInterfaceSIngleton.Instance.Custom_DGV_Clear(approvedTab_dataGridView);

            //Populate createTab_dataGridView from Business Singleton List
            foreach (Domain.FormItems item in BusinessAPI.BusinessSingleton.Instance.formItemList_CreateMSR)
            {
                createTab_dataGridView.Rows.Add(item.BudgetPool, item.ItemCode, item.ItemDesc, item.Quantity, item.Unit, item.UnitPrice, item.Currency, item.ROS_Date, item.Comments, item.AC_No);
            }

            //Populate waitApprovalTab_dataGridView from BusinessAPI
            waitApprovalTabDGV_source.DataSource = BusinessAPI.BusinessSingleton.Instance.MSRInfoAPI_B.GetShowMSR_List(BusinessAPI.BusinessSingleton.Instance.userInfo_EF.DeptId.ToString(), Domain.WorkFlowTrace.WAIT_FOR_APPROVAL);
            waitApprovalTab_dataGridView.DataSource = waitApprovalTabDGV_source;
            waitApprovalTab_dataGridView.ClearSelection();

            //Populate approvedTab_dataGridView from BusinessAPI
            approvedTabDGV_source.DataSource = BusinessAPI.BusinessSingleton.Instance.MSRInfoAPI_B.GetShowMSR_List(BusinessAPI.BusinessSingleton.Instance.userInfo_EF.DeptId.ToString(), Domain.WorkFlowTrace.APPROVED);
            approvedTab_dataGridView.DataSource = approvedTabDGV_source;
            approvedTab_dataGridView.ClearSelection();

            //Populate needReviewTab_dataGridView from BusinessAPI
            needReviewTabDGV_source.DataSource = BusinessAPI.BusinessSingleton.Instance.MSRInfoAPI_B.GetShowMSR_List(BusinessAPI.BusinessSingleton.Instance.userInfo_EF.DeptId.ToString(), Domain.WorkFlowTrace.NEED_REVIEW);
            needReviewTab_dataGridView.DataSource = needReviewTabDGV_source;
            needReviewTab_dataGridView.ClearSelection();

        }

        private void AddStock_createTab_button_Click(object sender, EventArgs e)
        {
            this.Hide();

            //Save state of DGV
            BusinessAPI.BusinessSingleton.Instance.formItemList_CreateMSR = UserInterfaceAPI.UserInterfaceSIngleton.Instance.ConvertFormItemDGV_ToFormItemList(createTab_dataGridView);

            AddStockItemForm fAddStockItem = new AddStockItemForm(budgetPool_createTab_comboBox.Text, Domain.WorkFlowTrace.createMSR);
            fAddStockItem.ShowDialog();

            //Update state of DGV
            RefreshDataGridViews();

            this.Show();
        }

        private void AddNonStock_createTab_button_Click(object sender, EventArgs e)
        {
            this.Hide();

            //Save state of DGV
            BusinessAPI.BusinessSingleton.Instance.formItemList_CreateMSR = UserInterfaceAPI.UserInterfaceSIngleton.Instance.ConvertFormItemDGV_ToFormItemList(createTab_dataGridView);

            AddNonStockItemForm fAddNonStockItem = new AddNonStockItemForm(budgetPool_createTab_comboBox.Text, Domain.WorkFlowTrace.createMSR);
            fAddNonStockItem.ShowDialog();

            //Update state of DGV
            RefreshDataGridViews();

            this.Show();
        }

        private void BudgetPool_createTab_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Check if reseting
            if(budgetPool_createTab_comboBox.SelectedIndex == -1)
            {
                return;
            }

            //Clear then Populate Approval Combobox
            //approval_createTab_comboBox.Items.Clear();
            approval_createTab_comboBox.DataSource = null;
            approval_createTab_comboBox.Enabled = true;
            approval_createTab_comboBox.DataSource =  BusinessAPI.BusinessSingleton.Instance.BudgetInfoAPI_B.GetBudgetHolder_List_EF(budgetPool_createTab_comboBox.Text);

            //Change Display of combobox
            approval_createTab_comboBox.DisplayMember = "FullName";
            approval_createTab_comboBox.SelectedIndex = -1;

            //Enable Stock Buttons
            addStock_createTab_button.Enabled = true;
            addNonStock_createTab_button.Enabled = true;
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
                changeDate_createTab_dateTimePicker.Value = BusinessAPI.BusinessSingleton.Instance.GetDateTime();
            }
            
        }

        private void Save_createTab_button_Click(object sender, EventArgs e)
        {
            //Unselect DataGridView
            createTab_dataGridView.ClearSelection();

            //Checking all required fields
            if (budgetYear_createTab_comboBox.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a budget year.");
                return;
            }

            if (budgetPool_createTab_comboBox.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a budget pool.");
                return;
            }

            if (originator_createTab_comboBox.SelectedIndex == -1)
            {
                MessageBox.Show("Please select an originator.");
                return;
            }

            if (createTab_dataGridView.Rows.Count == 0)
            {
                MessageBox.Show("Please add an item.");
                return;
            }

            if (approval_createTab_comboBox.SelectedIndex == -1)
            {
                MessageBox.Show("Please select an approver.");
                return;
            }

            //Checking if all item's fields are correct
            Boolean itemsCorrectFlag = true;

            //Checking if all items's budget pool matches combobox budget pool
            foreach (DataGridViewRow row in createTab_dataGridView.Rows)
            {
                if ( !(row.Cells["BudgetPool"].FormattedValue.ToString().Equals(budgetPool_createTab_comboBox.Text)) )
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
                return;
            }

            //Checking if all numeric fields are numeric
            foreach (DataGridViewRow row in createTab_dataGridView.Rows)
            {
                if ( !BusinessAPI.BusinessSingleton.Instance.IsNumeric(row.Cells["Quantity"].FormattedValue.ToString()) )
                {
                    Color lightRed = ControlPaint.Light(Color.Red);
                    row.Cells["Quantity"].Style.BackColor = lightRed;
                    itemsCorrectFlag = false;
                }
                else
                {
                    row.Cells["Quantity"].Style.BackColor = (Color)System.Drawing.SystemColors.Window;
                }

                if ( !BusinessAPI.BusinessSingleton.Instance.IsNumeric(row.Cells["UnitPrice"].FormattedValue.ToString()) && !String.IsNullOrEmpty(row.Cells["UnitPrice"].FormattedValue.ToString()) )
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
                return;
            }

            //Checking if all required form item fields are set
            foreach (DataGridViewRow row in createTab_dataGridView.Rows)
            {
                if ( String.IsNullOrEmpty(row.Cells["Quantity"].FormattedValue.ToString()) )
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
                return;
            }

            //To confirm if you want to Submit MSR
            DialogResult result = MessageBox.Show("Are you sure you want to submit the MSR?", "Confirmation", MessageBoxButtons.YesNo);
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

            //return;


            //EF Workflow

            //Obtain approver info of selected
            Domain.ApproverInfo approverInfo = (Domain.ApproverInfo)approval_createTab_comboBox.SelectedItem;

            //Creates MSR with Associated FormItems
            BusinessAPI.BusinessSingleton.Instance.MSRInfoAPI_B.InsertIntialMSR(
                approverInfo, 
                project_createTab_textBox.Text, 
                wellVL_createTab_textBox.Text, 
                comments_createTab_textBox.Text, 
                budgetYear_createTab_comboBox.Text, 
                budgetPool_createTab_comboBox.Text, 
                AFE_createTab_textBox.Text, 
                suggVendor_createTab_textBox.Text, 
                vendorContact_createTab_textBox.Text, 
                changeDate_createTab_dateTimePicker.Value
                );

            //Clears the Business formItemList_CreateMSR
            BusinessAPI.BusinessSingleton.Instance.formItemList_CreateMSR.Clear();

            //Refresh all the DataGridViews
            RefreshDataGridViews();

            //Clear all fields of Create Tab
            clearAllFields_createTab_button.PerformClick();
        }

        private void ClearAllFields_createTab_button_Click(object sender, EventArgs e)
        {
            //First Basic Reset
            UserInterfaceAPI.UserInterfaceSIngleton.Instance.ResetAllControls(createNewMSR_tabPage);

            //Reset Budget GroupBox
            budgetYear_createTab_comboBox.SelectedIndex = -1;
            budgetPool_createTab_comboBox.SelectedIndex = -1;

            //Reset SignDate GroupBox
            approval_createTab_comboBox.SelectedIndex = -1;

            //Reset Buttons
            addStock_createTab_button.Enabled = false;
            addNonStock_createTab_button.Enabled = false;
        }
        
        private void PopulateFilteredShowMSRItemListDGV()
        {
            ICollection<Domain.ShowMSRItem> showMSRItemData = BusinessAPI.BusinessSingleton.Instance.MSRInfoAPI_B.GetShowMSR_List(BusinessAPI.BusinessSingleton.Instance.userInfo_EF.DeptId.ToString(), Domain.WorkFlowTrace.WAIT_FOR_APPROVAL);

            ICollection<Domain.ShowMSRItem> showMSRItemDatafilter = BusinessAPI.BusinessSingleton.Instance.MSRInfoAPI_B.GetFiltershowMSR_List(showMSRItemData, idSearch_waitApprovalTab_textBox.Text, deptSearch_waitApprovalTab_textBox.Text, ogSearch_waitApprovalTab_textBox.Text, apSearch_waitApprovalTab_textBox.Text);

            if (showMSRItemDatafilter == null)
            {
                MessageBox.Show("DB error");
                return;
            }

            waitApprovalTabDGV_source.DataSource = showMSRItemDatafilter;
            waitApprovalTab_dataGridView.DataSource = waitApprovalTabDGV_source;
            waitApprovalTab_dataGridView.ClearSelection();
        }

        private void IdSearch_waitApprovalTab_textBox_TextChanged(object sender, EventArgs e)
        {
            PopulateFilteredShowMSRItemListDGV();
        }

        private void DeptSearch_waitApprovalTab_textBox_TextChanged(object sender, EventArgs e)
        {
            PopulateFilteredShowMSRItemListDGV();
        }

        private void OgSearch_waitApprovalTab_textBox_TextChanged(object sender, EventArgs e)
        {
            PopulateFilteredShowMSRItemListDGV();
        }

        private void ApSearch_waitApprovalTab_textBox_TextChanged(object sender, EventArgs e)
        {
            PopulateFilteredShowMSRItemListDGV();
        }

        private void WaitApprovalTab_dataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //MessageBox.Show(waitApprovalTab_dataGridView.SelectedRows[0].Cells["MSRId"].FormattedValue.ToString());

            //Check if header is selected
            if (e.RowIndex == -1) return;

            this.Hide();
            
            UIFormLayer.ShowMSR_WaitForApproval fShowMSR = new UIFormLayer.ShowMSR_WaitForApproval(waitApprovalTab_dataGridView.SelectedRows[0].Cells["MSRId"].FormattedValue.ToString());

            fShowMSR.ShowDialog();

            //Update state of DGV
            RefreshDataGridViews();

            this.Show();

        }

        private void ApprovedTab_dataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //MessageBox.Show(approvedTab_dataGridView.SelectedRows[0].Cells["MSRId"].FormattedValue.ToString());

            //Check if header is selected
            if (e.RowIndex == -1) return;

            this.Hide();

            UIFormLayer.ShowMSR_Approved fShowMSR = new UIFormLayer.ShowMSR_Approved(approvedTab_dataGridView.SelectedRows[0].Cells["MSRId"].FormattedValue.ToString());

            fShowMSR.ShowDialog();

            //Update state of DGV
            RefreshDataGridViews();

            this.Show();
        }
        
        private void NeedReviewTab_dataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //MessageBox.Show(needReviewTab_dataGridView.SelectedRows[0].Cells["MSRId"].FormattedValue.ToString());

            //Check if header is selected
            if (e.RowIndex == -1) return;

            this.Hide();

            UIFormLayer.ShowMSR_NeedReview fShowMSR = new UIFormLayer.ShowMSR_NeedReview(needReviewTab_dataGridView.SelectedRows[0].Cells["MSRId"].FormattedValue.ToString());

            fShowMSR.ShowDialog();

            //Update state of DGV
            RefreshDataGridViews();

            this.Show();
        }

    }
}
