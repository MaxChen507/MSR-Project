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
    public partial class MSRMainForm : Form
    {
        //Binding Source Variables
        BindingSource waitApprovalTabDGVSource = new BindingSource();
        BindingSource needReviewTabDGVSource = new BindingSource();
        BindingSource approvedTabDGVSource = new BindingSource();
        
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
            foreach (String item in BusinessAPI.BusinessSingleton.Instance.GetUniqueBPAccessList())
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
            UserInterfaceAPI.UserInterfaceSIngleton.Instance.CustomDGVClear(createTab_dataGridView);
            UserInterfaceAPI.UserInterfaceSIngleton.Instance.CustomDGVClear(waitApprovalTab_dataGridView);
            UserInterfaceAPI.UserInterfaceSIngleton.Instance.CustomDGVClear(needReviewTab_dataGridView);
            UserInterfaceAPI.UserInterfaceSIngleton.Instance.CustomDGVClear(approvedTab_dataGridView);

            //Populate createTab_dataGridView from Business Singleton List
            foreach (Domain.FormItems item in BusinessAPI.BusinessSingleton.Instance.formItemListCreateMSR)
            {
                createTab_dataGridView.Rows.Add(item.BudgetPool, item.ItemCode, item.ItemDesc, item.Quantity, item.Unit, item.UnitPrice, item.Currency, item.ROSDate, item.Comments, item.ACNo);
            }

            //Populate waitApprovalTab_dataGridView from BusinessAPI
            waitApprovalTabDGVSource.DataSource = BusinessAPI.BusinessSingleton.Instance.MSRInfoAPI.GetShowMSRList(BusinessAPI.BusinessSingleton.Instance.userInfo_EF.DeptId.ToString(), Domain.WorkFlowTrace.WAIT_FOR_APPROVAL);
            waitApprovalTab_dataGridView.DataSource = waitApprovalTabDGVSource;
            waitApprovalTab_dataGridView.ClearSelection();

            //Populate approvedTab_dataGridView from BusinessAPI
            approvedTabDGVSource.DataSource = BusinessAPI.BusinessSingleton.Instance.MSRInfoAPI.GetShowMSRList(BusinessAPI.BusinessSingleton.Instance.userInfo_EF.DeptId.ToString(), Domain.WorkFlowTrace.APPROVED);
            approvedTab_dataGridView.DataSource = approvedTabDGVSource;
            approvedTab_dataGridView.ClearSelection();

            //Populate needReviewTab_dataGridView from BusinessAPI
            needReviewTabDGVSource.DataSource = BusinessAPI.BusinessSingleton.Instance.MSRInfoAPI.GetShowMSRList(BusinessAPI.BusinessSingleton.Instance.userInfo_EF.DeptId.ToString(), Domain.WorkFlowTrace.NEED_REVIEW);
            needReviewTab_dataGridView.DataSource = needReviewTabDGVSource;
            needReviewTab_dataGridView.ClearSelection();
        }

        private void PopulateFilteredShowMSRItemListDGV(String workFlowTrace, String idSearchText, String deptSearchText, String ogSearchText, String apSearchText, BindingSource tabDGVSource, DataGridView dataGridView)
        {
            ICollection<Domain.ShowMSRItem> showMSRItemData = BusinessAPI.BusinessSingleton.Instance.MSRInfoAPI.GetShowMSRList(BusinessAPI.BusinessSingleton.Instance.userInfo_EF.DeptId.ToString(), workFlowTrace);

            ICollection<Domain.ShowMSRItem> showMSRItemDatafilter = BusinessAPI.BusinessSingleton.Instance.MSRInfoAPI.GetFilterShowMSRList(showMSRItemData, idSearchText, deptSearchText, ogSearchText, apSearchText);

            if (showMSRItemDatafilter == null)
            {
                MessageBox.Show("DB error");
                return;
            }

            tabDGVSource.DataSource = showMSRItemDatafilter;
            dataGridView.DataSource = tabDGVSource;
            dataGridView.ClearSelection();
        }


        #region CreateTab Code Block

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
            approval_createTab_comboBox.DataSource =  BusinessAPI.BusinessSingleton.Instance.BudgetInfoAPI.GetBudgetHolderList(budgetPool_createTab_comboBox.Text);

            //Change Display of combobox
            approval_createTab_comboBox.DisplayMember = "FullName";
            approval_createTab_comboBox.SelectedIndex = -1;

            //Enable Stock Buttons
            addStock_createTab_button.Enabled = true;
            addNonStock_createTab_button.Enabled = true;
        }

        private void AddStock_createTab_button_Click(object sender, EventArgs e)
        {
            this.Hide();

            //Save state of DGV
            BusinessAPI.BusinessSingleton.Instance.formItemListCreateMSR = UserInterfaceAPI.UserInterfaceSIngleton.Instance.ConvertFormItemDGVToFormItemList(createTab_dataGridView);

            AddStockItemForm fAddStockItem = new AddStockItemForm(budgetPool_createTab_comboBox.Text, Domain.WorkFlowTrace.CreateMSR);
            fAddStockItem.ShowDialog();

            //Update state of DGV
            RefreshDataGridViews();

            this.Show();
        }

        private void AddNonStock_createTab_button_Click(object sender, EventArgs e)
        {
            this.Hide();

            //Save state of DGV
            BusinessAPI.BusinessSingleton.Instance.formItemListCreateMSR = UserInterfaceAPI.UserInterfaceSIngleton.Instance.ConvertFormItemDGVToFormItemList(createTab_dataGridView);

            AddNonStockItemForm fAddNonStockItem = new AddNonStockItemForm(budgetPool_createTab_comboBox.Text, Domain.WorkFlowTrace.CreateMSR);
            fAddNonStockItem.ShowDialog();

            //Update state of DGV
            RefreshDataGridViews();

            this.Show();
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
            //Inital Variables:
            //Obtain approver info of selected
            Domain.ApproverInfo approverInfo = (Domain.ApproverInfo)approval_createTab_comboBox.SelectedItem;

            //Validation:
            //Unselect DataGridView
            createTab_dataGridView.ClearSelection();
            
            //Checking all required fields
            if (CheckCreateMSRFields(approverInfo) == false)
            {
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


            //EF Workflow

            //Creates MSR with Associated FormItems
            BusinessAPI.BusinessSingleton.Instance.MSRInfoAPI.InsertIntialMSR(
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
            BusinessAPI.BusinessSingleton.Instance.formItemListCreateMSR.Clear();

            //Refresh all the DataGridViews
            RefreshDataGridViews();

            //Clear all fields of Create Tab
            clearAllFields_createTab_button.PerformClick();
        }

        private Boolean CheckCreateMSRFields(Domain.ApproverInfo approverInfo)
        {
            Boolean checkFieldFlag = false;

            if (budgetYear_createTab_comboBox.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a budget year.");
                checkFieldFlag = false;
            }
            else if (budgetPool_createTab_comboBox.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a budget pool.");
                checkFieldFlag = false;
            }
            else if (originator_createTab_comboBox.SelectedIndex == -1)
            {
                MessageBox.Show("Please select an originator.");
                checkFieldFlag = false;
            }
            else if (createTab_dataGridView.Rows.Count == 0)
            {
                MessageBox.Show("Please add an item.");
                checkFieldFlag = false;
            }
            else if (approval_createTab_comboBox.SelectedIndex == -1)
            {
                MessageBox.Show("Please select an approver.");
                checkFieldFlag = false;
            }
            else if (BusinessAPI.BusinessSingleton.Instance.userInfo_EF.UserId == Int32.Parse(approverInfo.UserId))
            {
                MessageBox.Show("You can not approve yourself. Please select a different approver.");
                checkFieldFlag = false;
            }
            // Checking if all item's fields are correct
            else if (UserInterfaceAPI.UserInterfaceSIngleton.Instance.CheckMSRFormItemsDGV(createTab_dataGridView, budgetPool_createTab_comboBox.Text, approverInfo.UserId) == false)
            {
                checkFieldFlag = false;
            }
            else
            {
                checkFieldFlag = true;
            }

            return checkFieldFlag;
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

        #endregion


        #region WaitApprovalTab Code Block

        private void IdSearch_waitApprovalTab_textBox_TextChanged(object sender, EventArgs e)
        {
            PopulateFilteredShowMSRItemListDGV(Domain.WorkFlowTrace.WAIT_FOR_APPROVAL, idSearch_waitApprovalTab_textBox.Text, deptSearch_waitApprovalTab_textBox.Text, ogSearch_waitApprovalTab_textBox.Text, apSearch_waitApprovalTab_textBox.Text, waitApprovalTabDGVSource, waitApprovalTab_dataGridView);
        }

        private void DeptSearch_waitApprovalTab_textBox_TextChanged(object sender, EventArgs e)
        {
            PopulateFilteredShowMSRItemListDGV(Domain.WorkFlowTrace.WAIT_FOR_APPROVAL, idSearch_waitApprovalTab_textBox.Text, deptSearch_waitApprovalTab_textBox.Text, ogSearch_waitApprovalTab_textBox.Text, apSearch_waitApprovalTab_textBox.Text, waitApprovalTabDGVSource, waitApprovalTab_dataGridView);
        }

        private void OgSearch_waitApprovalTab_textBox_TextChanged(object sender, EventArgs e)
        {
            PopulateFilteredShowMSRItemListDGV(Domain.WorkFlowTrace.WAIT_FOR_APPROVAL, idSearch_waitApprovalTab_textBox.Text, deptSearch_waitApprovalTab_textBox.Text, ogSearch_waitApprovalTab_textBox.Text, apSearch_waitApprovalTab_textBox.Text, waitApprovalTabDGVSource, waitApprovalTab_dataGridView);
        }

        private void ApSearch_waitApprovalTab_textBox_TextChanged(object sender, EventArgs e)
        {
            PopulateFilteredShowMSRItemListDGV(Domain.WorkFlowTrace.WAIT_FOR_APPROVAL, idSearch_waitApprovalTab_textBox.Text, deptSearch_waitApprovalTab_textBox.Text, ogSearch_waitApprovalTab_textBox.Text, apSearch_waitApprovalTab_textBox.Text, waitApprovalTabDGVSource, waitApprovalTab_dataGridView);
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

        #endregion


        #region NeedReviewTab Code Block

        private void IdSearch_needReview_textBox_TextChanged(object sender, EventArgs e)
        {
            PopulateFilteredShowMSRItemListDGV(Domain.WorkFlowTrace.NEED_REVIEW, idSearch_needReviewTab_textBox.Text, deptSearch_needReviewTab_textBox.Text, ogSearch_needReviewTab_textBox.Text, apSearch_needReviewTab_textBox.Text, needReviewTabDGVSource, needReviewTab_dataGridView);
        }

        private void DeptSearch_needReviewTab_textBox_TextChanged(object sender, EventArgs e)
        {
            PopulateFilteredShowMSRItemListDGV(Domain.WorkFlowTrace.NEED_REVIEW, idSearch_needReviewTab_textBox.Text, deptSearch_needReviewTab_textBox.Text, ogSearch_needReviewTab_textBox.Text, apSearch_needReviewTab_textBox.Text, needReviewTabDGVSource, needReviewTab_dataGridView);
        }

        private void OgSearch_needReviewTab_textBox_TextChanged(object sender, EventArgs e)
        {
            PopulateFilteredShowMSRItemListDGV(Domain.WorkFlowTrace.NEED_REVIEW, idSearch_needReviewTab_textBox.Text, deptSearch_needReviewTab_textBox.Text, ogSearch_needReviewTab_textBox.Text, apSearch_needReviewTab_textBox.Text, needReviewTabDGVSource, needReviewTab_dataGridView);
        }

        private void ApSearch_needReviewTab_textBox_TextChanged(object sender, EventArgs e)
        {
            PopulateFilteredShowMSRItemListDGV(Domain.WorkFlowTrace.NEED_REVIEW, idSearch_needReviewTab_textBox.Text, deptSearch_needReviewTab_textBox.Text, ogSearch_needReviewTab_textBox.Text, apSearch_needReviewTab_textBox.Text, needReviewTabDGVSource, needReviewTab_dataGridView);
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
        
        #endregion


        #region ApprovedTab Code Block

        private void IdSearch_approvedTab_textBox_TextChanged(object sender, EventArgs e)
        {
            PopulateFilteredShowMSRItemListDGV(Domain.WorkFlowTrace.APPROVED, idSearch_approvedTab_textBox.Text, deptSearch_approvedTab_textBox.Text, ogSearch_approvedTab_textBox.Text, apSearch_approvedTab_textBox.Text, approvedTabDGVSource, approvedTab_dataGridView);
        }

        private void DeptSearch_approvedTab_textBox_TextChanged(object sender, EventArgs e)
        {
            PopulateFilteredShowMSRItemListDGV(Domain.WorkFlowTrace.APPROVED, idSearch_approvedTab_textBox.Text, deptSearch_approvedTab_textBox.Text, ogSearch_approvedTab_textBox.Text, apSearch_approvedTab_textBox.Text, approvedTabDGVSource, approvedTab_dataGridView);
        }

        private void OgSearch_approvedTab_textBox_TextChanged(object sender, EventArgs e)
        {
            PopulateFilteredShowMSRItemListDGV(Domain.WorkFlowTrace.APPROVED, idSearch_approvedTab_textBox.Text, deptSearch_approvedTab_textBox.Text, ogSearch_approvedTab_textBox.Text, apSearch_approvedTab_textBox.Text, approvedTabDGVSource, approvedTab_dataGridView);
        }

        private void ApSearch_approvedTab_textBox_TextChanged(object sender, EventArgs e)
        {
            PopulateFilteredShowMSRItemListDGV(Domain.WorkFlowTrace.APPROVED, idSearch_approvedTab_textBox.Text, deptSearch_approvedTab_textBox.Text, ogSearch_approvedTab_textBox.Text, apSearch_approvedTab_textBox.Text, approvedTabDGVSource, approvedTab_dataGridView);
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


        #endregion


    }
}
