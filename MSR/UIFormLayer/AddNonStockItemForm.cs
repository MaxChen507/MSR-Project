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
    public partial class AddNonStockItemForm : Form
    {
        //Form variables
        String bpNo;
        String workFlowTrace;

        //Binding Source Initialization
        BindingSource budgetListDGVSource = new BindingSource();

        //Data List Initialization
        ICollection<Domain.BudgetInfo> budgetListData = null;

        public AddNonStockItemForm(String bpNo, String workFlowTrace)
        {
            InitializeComponent();
            this.bpNo = bpNo;
            this.workFlowTrace = workFlowTrace;
        }

        private void AddNonStockItemForm_Load(object sender, EventArgs e)
        {
            BudgetListDGV_Load();
            InitalizeStartingFields();
        }

        private void BudgetListDGV_Load()
        {
            try
            {
                budgetListData = BusinessAPI.BusinessSingleton.Instance.GetFilterBudgetInfo(bpNo);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString());
            }

            if (budgetListData == null)
            {
                MessageBox.Show("Error");
                return;
            }

            budgetListDGVSource.DataSource = budgetListData;
            budgetInfo_addNonStock_dataGridView.DataSource = budgetListDGVSource;
            budgetInfo_addNonStock_dataGridView.ClearSelection();

            //Set the custom resize
            UserInterfaceAPI.UserInterfaceSIngleton.Instance.CustomDGVSetColumnResize(budgetInfo_addNonStock_dataGridView);
        }

        private void InitalizeStartingFields()
        {
            itemCode_addNonStock_textBox.Text = "[NonStock]";

            //WorkFlowTrace
            if (workFlowTrace.Equals(Domain.WorkFlowTrace.CreateMSR))
            {
                AddListDGV_Load_CreateMSR();
            }
            else if (workFlowTrace.Equals(Domain.WorkFlowTrace.WaitForApprovalMSR))
            {
                AddListDGV_Load_WaitForApproval();
            }
            else if (workFlowTrace.Equals(Domain.WorkFlowTrace.NeedReviewMSR))
            {
                AddListDGV_Load_NeedReview();
            }

        }

        private void AddListDGV_Load_CreateMSR()
        {
            //DGV clear
            UserInterfaceAPI.UserInterfaceSIngleton.Instance.CustomDGVClear(addList_addNonStock_dataGridView);

            //Populate from Singleton List
            foreach (Domain.FormItems item in BusinessAPI.BusinessSingleton.Instance.formItemListCreateMSR)
            {
                addList_addNonStock_dataGridView.Rows.Add(item.BudgetPool, item.ItemCode, item.ItemDesc, item.Quantity, item.Unit, item.UnitPrice, item.Currency, item.ROSDate, item.Comments, item.ACNo);
            }

        }

        private void AddListDGV_Load_WaitForApproval()
        {
            //DGV clear
            UserInterfaceAPI.UserInterfaceSIngleton.Instance.CustomDGVClear(addList_addNonStock_dataGridView);

            //Populate from Singleton List
            foreach (Domain.FormItems item in BusinessAPI.BusinessSingleton.Instance.formItemListWaitForApproval)
            {
                addList_addNonStock_dataGridView.Rows.Add(item.BudgetPool, item.ItemCode, item.ItemDesc, item.Quantity, item.Unit, item.UnitPrice, item.Currency, item.ROSDate, item.Comments, item.ACNo);
            }

        }

        private void AddListDGV_Load_NeedReview()
        {
            //DGV clear
            UserInterfaceAPI.UserInterfaceSIngleton.Instance.CustomDGVClear(addList_addNonStock_dataGridView);

            //Populate from Singleton List
            foreach (Domain.FormItems item in BusinessAPI.BusinessSingleton.Instance.formItemListNeedReview)
            {
                addList_addNonStock_dataGridView.Rows.Add(item.BudgetPool, item.ItemCode, item.ItemDesc, item.Quantity, item.Unit, item.UnitPrice, item.Currency, item.ROSDate, item.Comments, item.ACNo);
            }

        }

        private void ApplyClose_AddNonStock_button_Click(object sender, EventArgs e)
        {
            if (workFlowTrace.Equals(Domain.WorkFlowTrace.CreateMSR))
            {
                //Save state of DGV to CreateMSR
                BusinessAPI.BusinessSingleton.Instance.formItemListCreateMSR = UserInterfaceAPI.UserInterfaceSIngleton.Instance.ConvertFormItemDGVToFormItemList(addList_addNonStock_dataGridView);
            }
            else if (workFlowTrace.Equals(Domain.WorkFlowTrace.WaitForApprovalMSR))
            {
                //Save state of DGV to WaitForApproval
                BusinessAPI.BusinessSingleton.Instance.formItemListWaitForApproval = UserInterfaceAPI.UserInterfaceSIngleton.Instance.ConvertFormItemDGVToFormItemList(addList_addNonStock_dataGridView);
            }
            else if (workFlowTrace.Equals(Domain.WorkFlowTrace.NeedReviewMSR))
            {
                //Save state of DGV to WaitForApproval
                BusinessAPI.BusinessSingleton.Instance.formItemListNeedReview = UserInterfaceAPI.UserInterfaceSIngleton.Instance.ConvertFormItemDGVToFormItemList(addList_addNonStock_dataGridView);
            }

            this.Close();
        }

        private void AddItem_addNonStock_button_Click(object sender, EventArgs e)
        {
            if (budgetInfo_addNonStock_dataGridView.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select an AC.");
                return;
            }

            Boolean itemExists = false;

            if (addList_addNonStock_dataGridView.Rows.Count == 0)
            {
                itemExists = false;
            }

            foreach (DataGridViewRow row in addList_addNonStock_dataGridView.Rows)
            {
               if (row.Cells["ItemDesc"].FormattedValue.ToString().Equals(itemDesc_addNonStock_richTextBox.Text.Trim()))
                {
                    itemExists = true;
                }
            }

            if (itemExists)
            {
                MessageBox.Show("The selected item description already exists.");
            }
            else if (String.IsNullOrWhiteSpace(itemDesc_addNonStock_richTextBox.Text))
            {
                MessageBox.Show("The item description is empty.");
            }
            else if(String.IsNullOrWhiteSpace(unit_addNonStock_textBox.Text))
            {
                MessageBox.Show("The unit is empty.");
            }
            else
            {
                String BudgetPool = budgetInfo_addNonStock_dataGridView.SelectedRows[0].Cells["BpNo"].FormattedValue.ToString();
                String ItemCode = "[NonStock]";
                String ItemDesc = itemDesc_addNonStock_richTextBox.Text.Trim();
                String Quantity = "1";
                String Unit = unit_addNonStock_textBox.Text.Trim();
                String UnitPrice = "";
                String Currency = "";
                String ROS_Date = BusinessAPI.BusinessSingleton.Instance.GetDateTime().AddDays(14).ToString("MM/dd/yyyy");
                String Comments = "";
                String AC_No = budgetInfo_addNonStock_dataGridView.SelectedRows[0].Cells["AcNo"].FormattedValue.ToString();

                addList_addNonStock_dataGridView.Rows.Add(BudgetPool, ItemCode, ItemDesc, Quantity, Unit, UnitPrice, Currency, ROS_Date, Comments, AC_No);
            }
        }

        private void PopulateFilteredBudgetInfoListDGV()
        {
            ICollection<Domain.BudgetInfo> budgetInfoItemDatafilter = BusinessAPI.BusinessSingleton.Instance.NonStockItemsAPI.GetFilterBudgetInfoList(budgetListData, AcSearch_addNonStock_textBox.Text, AcDescSearch_addNonStock_textBox.Text);

            if (budgetInfoItemDatafilter == null)
            {
                MessageBox.Show("DB error");
                return;
            }

            budgetListDGVSource.DataSource = budgetInfoItemDatafilter;
            budgetInfo_addNonStock_dataGridView.DataSource = budgetListDGVSource;

            budgetInfo_addNonStock_dataGridView.ClearSelection();
        }

        private void AcSearch_addNonStock_textBox_TextChanged(object sender, EventArgs e)
        {
            PopulateFilteredBudgetInfoListDGV();
        }

        private void AcDescSearch_addNonStock_textBox_TextChanged(object sender, EventArgs e)
        {
            PopulateFilteredBudgetInfoListDGV();
        }

    }
}
