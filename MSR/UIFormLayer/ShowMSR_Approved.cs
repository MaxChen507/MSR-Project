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
        MSR msrInfo;

        //View Variables
        ICollection<Domain.FormItems> viewFormItems;
        Domain.GroupsInfo groupsInfo;

        public ShowMSR_Approved(String msrId)
        {
            InitializeComponent();
            msrInfo = BusinessAPI.BusinessSingleton.Instance.MSRInfoAPI.GetMSRByMSRId(msrId);
        }

        private void ShowMSR_Approved_Load(object sender, EventArgs e)
        {
            //Update the BusininessAPI with FormItems
            viewFormItems = BusinessAPI.BusinessSingleton.Instance.MSRInfoAPI.GetDomainFormItems(msrInfo.FormItems, msrInfo.BP_No);
            BusinessAPI.BusinessSingleton.Instance.formItemListApproved = viewFormItems;

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

            foreach (Domain.FormItems item in BusinessAPI.BusinessSingleton.Instance.formItemListApproved)
            {
                showMSR_dataGridView.Rows.Add(item.BudgetPool, item.ItemCode, item.ItemDesc, item.Quantity, item.Unit, item.UnitPrice, item.Currency, item.ROSDate, item.Comments, item.ACNo);
            }

        }


    }
}
