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
        MSR msrInfo;

        //View Variables
        ICollection<Domain.FormItems> viewFormItems;
        Domain.GroupsInfo groupsInfo;

        public ShowMSR_NeedReview(String msrId)
        {
            InitializeComponent();
            msrInfo = BusinessAPI.BusinessSingleton.Instance.MSRInfoAPI.GetMSRByMSRId(msrId);
        }

        private void ShowMSR_NeedReview_Load(object sender, EventArgs e)
        {
            //Update the BusininessAPI with FormItems
            viewFormItems = BusinessAPI.BusinessSingleton.Instance.MSRInfoAPI.GetDomainFormItems(msrInfo.FormItems, msrInfo.BP_No);
            BusinessAPI.BusinessSingleton.Instance.formItemListNeedReview = viewFormItems;

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
                       
            ShowMSR_DGV_Load();

            //Depending on the type of User, different controls will change
            //If orginator has clicked:
            if (BusinessAPI.BusinessSingleton.Instance.userInfo_EF.UserId.Equals(msrInfo.Usr_RO.UserId))
            {
                //Disable Group Boxes
                approve_showMSR_groupBox.Enabled = false;
                reason_showMSR_groupBox.Enabled = false;
                reason_showMSR_richTextBox.Text = msrInfo.Review_Comment;

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
                reason_showMSR_richTextBox.Text = msrInfo.Review_Comment;

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
            UserInterfaceAPI.UserInterfaceSIngleton.Instance.CustomDGVClear(showMSR_dataGridView);

            foreach (Domain.FormItems item in BusinessAPI.BusinessSingleton.Instance.formItemListNeedReview)
            {
                showMSR_dataGridView.Rows.Add(item.BudgetPool, item.ItemCode, item.ItemDesc, item.Quantity, item.Unit, item.UnitPrice, item.Currency, item.ROSDate, item.Comments, item.ACNo);
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
            UserInterfaceAPI.UserInterfaceSIngleton.Instance.CustomDGVClear(showMSR_dataGridView);

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
            BusinessAPI.BusinessSingleton.Instance.formItemListWaitForApproval = UserInterfaceAPI.UserInterfaceSIngleton.Instance.ConvertFormItemDGVToFormItemList(showMSR_dataGridView);

            AddStockItemForm fAddStockItem = new AddStockItemForm(budgetPool_showMSR_textBox.Text, Domain.WorkFlowTrace.NeedReviewMSR);
            fAddStockItem.ShowDialog();

            //Update state of DGV
            ShowMSR_DGV_Load();

            this.Show();
        }

        private void AddNonStock_showMSR_button_Click(object sender, EventArgs e)
        {
            this.Hide();

            //Save state of DGV
            BusinessAPI.BusinessSingleton.Instance.formItemListWaitForApproval = UserInterfaceAPI.UserInterfaceSIngleton.Instance.ConvertFormItemDGVToFormItemList(showMSR_dataGridView);

            AddNonStockItemForm fAddNonStockItem = new AddNonStockItemForm(budgetPool_showMSR_textBox.Text, Domain.WorkFlowTrace.NeedReviewMSR);
            fAddNonStockItem.ShowDialog();

            //Update state of DGV
            ShowMSR_DGV_Load();

            this.Show();
        }

        private void SubmitReview_showMSR_Button_Click(object sender, EventArgs e)
        {
            if (UserInterfaceAPI.UserInterfaceSIngleton.Instance.CheckMSRFormItemsDGV(showMSR_dataGridView, budgetPool_showMSR_textBox.Text, msrInfo.Usr_CA.UserId.ToString()) == false)
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
            BusinessAPI.BusinessSingleton.Instance.formItemListNeedReview = UserInterfaceAPI.UserInterfaceSIngleton.Instance.ConvertFormItemDGVToFormItemList(showMSR_dataGridView);

            //DELETE from FormItems
            BusinessAPI.BusinessSingleton.Instance.MSRInfoAPI.DeleteFormItemsByMSRId(msrInfo.MSRId.ToString());

            //INSERT into FormItems
            BusinessAPI.BusinessSingleton.Instance.MSRInfoAPI.InsertInitialFormItems(BusinessAPI.BusinessSingleton.Instance.formItemListNeedReview, Convert.ToInt32(msrInfo.MSRId));

            //Update MSR States and Approve Dates
            BusinessAPI.BusinessSingleton.Instance.MSRInfoAPI.UpdateMSRNeedReview(Convert.ToInt32(msrInfo.MSRId), Domain.WorkFlowTrace.WAIT_FOR_APPROVAL);

            this.Close();
        }

        
    }
}
