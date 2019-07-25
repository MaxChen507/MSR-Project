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
    public partial class ShowMSR_NeedReview : Form
    {
        //EF Variables
        MSR MSRInfo;

        //View Variables
        ICollection<Domain.FormItems> ViewFormItems;
        Domain.GroupsInfo groupsInfo;

        public ShowMSR_NeedReview(String MSRId)
        {
            InitializeComponent();
            MSRInfo = BusinessAPI.BusinessSingleton.Instance.MSRInfoAPI_B.GetMSRByMSRId(MSRId);
        }

        private void ShowMSR_NeedReview_Load(object sender, EventArgs e)
        {
            //Update the BusininessAPI with FormItems
            ViewFormItems = BusinessAPI.BusinessSingleton.Instance.MSRInfoAPI_B.GetDomain_FormItems(MSRInfo.FormItems, MSRInfo.BP_No);
            BusinessAPI.BusinessSingleton.Instance.formItemList_NeedReview = ViewFormItems;

            //Set Group Info from BusininessAPI
            groupsInfo = BusinessAPI.BusinessSingleton.Instance.GetGroupsInfo();

            InitalizeStartingFields();
        }
        private void InitalizeStartingFields()
        {
            //Initialze Project GroupBox
            project_showMSR_textBox.Text = MSRInfo.Project;
            wellVL_showMSR_textBox.Text = MSRInfo.WVL;
            comments_showMSR_textBox.Text = MSRInfo.Comments;

            //Initialize Budget GroupBox
            budgetYear_showMSR_textBox.Text = MSRInfo.BudgetYear.ToString();
            budgetPool_showMSR_textBox.Text = MSRInfo.BP_No;
            AFE_showMSR_textBox.Text = MSRInfo.AFE;

            //Initialize Vendors
            suggVendor_showMSR_textBox.Text = MSRInfo.SugVendor;
            vendorContact_showMSR_textBox.Text = MSRInfo.ContactVendor;

            //Initialize Approve GroupBox
            originator_showMSR_textBox.Text = MSRInfo.Usr_RO.FullName;
            compApproval_showMSR_textBox.Text = MSRInfo.Usr_CA.FullName;
                       
            ShowMSR_DGV_Load();

            //Depending on the type of User, different controls will change
            //If orginator has clicked:
            if (BusinessAPI.BusinessSingleton.Instance.userInfo_EF.UserId.Equals(MSRInfo.Usr_RO.UserId))
            {
                //Disable Group Boxes
                approve_showMSR_groupBox.Enabled = false;
                reason_showMSR_groupBox.Enabled = false;
                reason_showMSR_richTextBox.Text = MSRInfo.Review_Comment;

                //Enable Add Delete Items
                edit_showMSR_groupBox.Enabled = true;

                //Enable editing for quantity and comments
                showMSR_dataGridView.Columns[3].ReadOnly = false;
                showMSR_dataGridView.Columns[8].ReadOnly = false;

            }
            else
            {
                //Disable Group Boxes
                approve_showMSR_groupBox.Enabled = false;
                reason_showMSR_groupBox.Enabled = false;
                reason_showMSR_richTextBox.Text = MSRInfo.Review_Comment;

                //Disable Add Delete Items
                edit_showMSR_groupBox.Enabled = false;

                //Disable editing for quantity and comments
                showMSR_dataGridView.Columns[3].ReadOnly = true;
                showMSR_dataGridView.Columns[8].ReadOnly = true;

                //Disable Review Button
                submitReview_showMSR_Button.Enabled = false;
            }

        }

        private void ShowMSR_DGV_Load()
        {
            //DGV clear
            UserInterfaceAPI.UserInterfaceSIngleton.Instance.Custom_DGV_Clear(showMSR_dataGridView);

            foreach (Domain.FormItems item in BusinessAPI.BusinessSingleton.Instance.formItemList_NeedReview)
            {
                showMSR_dataGridView.Rows.Add(item.BudgetPool, item.ItemCode, item.ItemDesc, item.Quantity, item.Unit, item.UnitPrice, item.Currency, item.ROS_Date, item.Comments, item.AC_No);
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
            foreach (Domain.FormItems item in ViewFormItems)
            {
                showMSR_dataGridView.Rows.Add(item.BudgetPool, item.ItemCode, item.ItemDesc, item.Quantity, item.Unit, item.UnitPrice, item.Currency, item.ROS_Date, item.Comments, item.AC_No);
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
            BusinessAPI.BusinessSingleton.Instance.formItemList_WaitForApproval = UserInterfaceAPI.UserInterfaceSIngleton.Instance.ConvertFormItemDGV_ToFormItemList(showMSR_dataGridView);

            AddStockItemForm fAddStockItem = new AddStockItemForm(budgetPool_showMSR_textBox.Text, Domain.WorkFlowTrace.needReviewMSR);
            fAddStockItem.ShowDialog();

            //Update state of DGV
            ShowMSR_DGV_Load();

            this.Show();
        }

        private void AddNonStock_showMSR_button_Click(object sender, EventArgs e)
        {
            this.Hide();

            //Save state of DGV
            BusinessAPI.BusinessSingleton.Instance.formItemList_WaitForApproval = UserInterfaceAPI.UserInterfaceSIngleton.Instance.ConvertFormItemDGV_ToFormItemList(showMSR_dataGridView);

            AddNonStockItemForm fAddNonStockItem = new AddNonStockItemForm(budgetPool_showMSR_textBox.Text, Domain.WorkFlowTrace.needReviewMSR);
            fAddNonStockItem.ShowDialog();

            //Update state of DGV
            ShowMSR_DGV_Load();

            this.Show();
        }

        private void SubmitReview_showMSR_Button_Click(object sender, EventArgs e)
        {
            if (UserInterfaceAPI.UserInterfaceSIngleton.Instance.CheckMSRFormItemsDGV(showMSR_dataGridView, budgetPool_showMSR_textBox.Text, MSRInfo.Usr_CA.UserId.ToString()) == false)
            {
                return;
            }

            //To confirm if you want to submit MSR edits
            DialogResult result = MessageBox.Show("Are you sure you want to submit the MSR edits?", "Confirmation", MessageBoxButtons.YesNo);
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
            BusinessAPI.BusinessSingleton.Instance.formItemList_NeedReview = UserInterfaceAPI.UserInterfaceSIngleton.Instance.ConvertFormItemDGV_ToFormItemList(showMSR_dataGridView);

            //DELETE from FormItems
            BusinessAPI.BusinessSingleton.Instance.MSRInfoAPI_B.DeleteFormItemsByMSRId(MSRInfo.MSRId.ToString());

            //INSERT into FormItems
            BusinessAPI.BusinessSingleton.Instance.MSRInfoAPI_B.InsertInitialFormItems(BusinessAPI.BusinessSingleton.Instance.formItemList_NeedReview, Convert.ToInt32(MSRInfo.MSRId));

            //Update MSR States and Approve Dates
            BusinessAPI.BusinessSingleton.Instance.MSRInfoAPI_B.UpdateMSR_NeedReview(Convert.ToInt32(MSRInfo.MSRId), Domain.WorkFlowTrace.WAIT_FOR_APPROVAL);

            this.Close();
        }

        

    }
}
