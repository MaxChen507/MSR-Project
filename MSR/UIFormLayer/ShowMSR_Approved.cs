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
    public partial class ShowMSR_Approved : Form
    {
        //EF Variables
        MSR MSRInfo;

        //View Variables
        ICollection<Domain.FormItems> ViewFormItems;
        Domain.GroupsInfo groupsInfo;

        public ShowMSR_Approved(String MSRId)
        {
            InitializeComponent();
            MSRInfo = BusinessAPI.BusinessSingleton.Instance.MSRInfoAPI_B.GetMSRByMSRId(MSRId);
        }

        private void ShowMSR_Approved_Load(object sender, EventArgs e)
        {
            //Update the BusininessAPI with FormItems
            ViewFormItems = BusinessAPI.BusinessSingleton.Instance.MSRInfoAPI_B.GetDomain_FormItems(MSRInfo.FormItems, MSRInfo.BP_No);
            BusinessAPI.BusinessSingleton.Instance.formItemList_Approved = ViewFormItems;

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
            //If standUser or standBH:
            if (true)
            {
                //Disable Group Boxes
                approve_showMSR_groupBox.Enabled = false;
                reason_showMSR_groupBox.Enabled = false;

                //Enable Add Delete Items
                edit_showMSR_groupBox.Enabled = false;

                //Enable editing for quantity and comments
                showMSR_dataGridView.Columns[3].ReadOnly = true;
                showMSR_dataGridView.Columns[8].ReadOnly = true;

                //Enable for Review Button
                submitReview_showMSR_Button.Enabled = false;

            }
            // if Procurement
            //else if()
            //{

            //}

        }

        private void ShowMSR_DGV_Load()
        {
            //DGV clear
            UserInterfaceAPI.UserInterfaceSIngleton.Instance.Custom_DGV_Clear(showMSR_dataGridView);

            foreach (Domain.FormItems item in BusinessAPI.BusinessSingleton.Instance.formItemList_Approved)
            {
                showMSR_dataGridView.Rows.Add(item.BudgetPool, item.ItemCode, item.ItemDesc, item.Quantity, item.Unit, item.UnitPrice, item.Currency, item.ROS_Date, item.Comments, item.AC_No);
            }

        }


    }
}
