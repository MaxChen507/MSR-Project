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
    public partial class AddStockItemForm : Form
    {
        //Form Variables
        String bpNo;
        String workFlowTrace;

        //Binding Source
        BindingSource itemListDGVSource = new BindingSource();

        //Data List Initialization
        ICollection<Domain.StockItems> itemListData = null;

        public AddStockItemForm(String bpNo, String workFlowTrace)
        {
            InitializeComponent();
            this.bpNo = bpNo;
            this.workFlowTrace = workFlowTrace;
        }

        private void AddStockItemForm_Load(object sender, EventArgs e)
        {
            ItemListDGV_Load();
            InitializeStartingFields();
        }

        private void ItemListDGV_Load()
        {
            try
            {
                itemListData = BusinessAPI.BusinessSingleton.Instance.StockItemsAPI.GetStockItemList(bpNo);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString());
            }

            if (itemListData == null)
            {
                MessageBox.Show("DB error");
                return;
            }

            itemListDGVSource.DataSource = itemListData;
            itemList_addStock_dataGridView.DataSource = itemListDGVSource;
            itemList_addStock_dataGridView.ClearSelection();

            //Set the custom resize
            UserInterfaceAPI.UserInterfaceSIngleton.Instance.CustomDGVSetColumnResize(itemList_addStock_dataGridView);
        }

        private void InitializeStartingFields()
        {
            //Initialize LookUp ComboBox
            lookup_AddStock_comboBox.Items.Add("");
            foreach (String item in BusinessAPI.BusinessSingleton.Instance.StockItemsAPI.GetLookUpCodeList(itemListData))
            {
                lookup_AddStock_comboBox.Items.Add(item);
            }
            
            //WorkFlowTrace
            if (workFlowTrace.Equals(Domain.WorkFlowTrace.CreateMSR))
            {
                AddListDGV_Load_CreateMSR();
            }
            else if(workFlowTrace.Equals(Domain.WorkFlowTrace.WaitForApprovalMSR))
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
            UserInterfaceAPI.UserInterfaceSIngleton.Instance.CustomDGVClear(addList_addStock_dataGridView);

            //Populate from Singleton List
            foreach (Domain.FormItems item in BusinessAPI.BusinessSingleton.Instance.formItemListCreateMSR)
            {
                addList_addStock_dataGridView.Rows.Add(item.BudgetPool, item.ItemCode, item.ItemDesc, item.Quantity, item.Unit, item.UnitPrice, item.Currency, item.ROSDate, item.Comments, item.ACNo);
            }

        }

        private void AddListDGV_Load_WaitForApproval()
        {
            //DGV clear
            UserInterfaceAPI.UserInterfaceSIngleton.Instance.CustomDGVClear(addList_addStock_dataGridView);

            //Populate from Singleton List
            foreach (Domain.FormItems item in BusinessAPI.BusinessSingleton.Instance.formItemListWaitForApproval)
            {
                addList_addStock_dataGridView.Rows.Add(item.BudgetPool, item.ItemCode, item.ItemDesc, item.Quantity, item.Unit, item.UnitPrice, item.Currency, item.ROSDate, item.Comments, item.ACNo);
            }

        }

        private void AddListDGV_Load_NeedReview()
        {
            //DGV clear
            UserInterfaceAPI.UserInterfaceSIngleton.Instance.CustomDGVClear(addList_addStock_dataGridView);

            //Populate from Singleton List
            foreach (Domain.FormItems item in BusinessAPI.BusinessSingleton.Instance.formItemListNeedReview)
            {
                addList_addStock_dataGridView.Rows.Add(item.BudgetPool, item.ItemCode, item.ItemDesc, item.Quantity, item.Unit, item.UnitPrice, item.Currency, item.ROSDate, item.Comments, item.ACNo);
            }

        }

        private void ApplyClose_AddStock_button_Click(object sender, EventArgs e)
        {
            if (workFlowTrace.Equals(Domain.WorkFlowTrace.CreateMSR))
            {
                //Save state of DGV to CreateMSR
                BusinessAPI.BusinessSingleton.Instance.formItemListCreateMSR = UserInterfaceAPI.UserInterfaceSIngleton.Instance.ConvertFormItemDGVToFormItemList(addList_addStock_dataGridView);
            }
            else if (workFlowTrace.Equals(Domain.WorkFlowTrace.WaitForApprovalMSR))
            {
                //Save state of DGV to WaitForApproval
                BusinessAPI.BusinessSingleton.Instance.formItemListWaitForApproval = UserInterfaceAPI.UserInterfaceSIngleton.Instance.ConvertFormItemDGVToFormItemList(addList_addStock_dataGridView);
            }
            else if (workFlowTrace.Equals(Domain.WorkFlowTrace.NeedReviewMSR))
            {
                //Save state of DGV to WaitForApproval
                BusinessAPI.BusinessSingleton.Instance.formItemListNeedReview = UserInterfaceAPI.UserInterfaceSIngleton.Instance.ConvertFormItemDGVToFormItemList(addList_addStock_dataGridView);
            }

            this.Close();
        }

        private void AddItem_AddStock_button_Click(object sender, EventArgs e)
        {
            if (itemList_addStock_dataGridView.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select an item.");
                return;
            }

            Boolean itemExists = false;

            if (addList_addStock_dataGridView.Rows.Count == 0)
            {
                itemExists = false;
            }

            foreach (DataGridViewRow row in addList_addStock_dataGridView.Rows)
            {
                if (row.Cells["ItemCode"].FormattedValue.ToString().Equals(itemList_addStock_dataGridView.SelectedRows[0].Cells["ItemCode"].FormattedValue.ToString()))
                {
                    itemExists = true;
                }
            }

            if (itemExists)
            {
                MessageBox.Show("The selected item already exists.");
            }
            else
            {
                String BudgetPool = itemList_addStock_dataGridView.SelectedRows[0].Cells["BudgetPool"].FormattedValue.ToString();
                String ItemCode = itemList_addStock_dataGridView.SelectedRows[0].Cells["ItemCode"].FormattedValue.ToString();
                String ItemDesc = itemList_addStock_dataGridView.SelectedRows[0].Cells["ItemDesc"].FormattedValue.ToString();
                String Quantity = "1";
                String Unit = itemList_addStock_dataGridView.SelectedRows[0].Cells["Unit"].FormattedValue.ToString();
                String UnitPrice = "";
                String Currency = "";
                String ROS_Date = BusinessAPI.BusinessSingleton.Instance.GetDateTime().AddDays(14).ToString("MM/dd/yyyy");
                String Comments = "";
                String AC_No = itemList_addStock_dataGridView.SelectedRows[0].Cells["AcNo"].FormattedValue.ToString();

                addList_addStock_dataGridView.Rows.Add(BudgetPool, ItemCode, ItemDesc, Quantity, Unit, UnitPrice, Currency, ROS_Date, Comments, AC_No);
            }

        }

        private void PopulateFilteredItemListDGV()
        {
            ICollection<Domain.StockItems> stockItemDatafilter = BusinessAPI.BusinessSingleton.Instance.StockItemsAPI.GetFilterStockItemList(itemListData, search_AddStock_textBox.Text.ToString(), lookup_AddStock_comboBox.Text.ToString());
            
            if (stockItemDatafilter == null)
            {
                MessageBox.Show("DB error");
                return;
            }

            itemListDGVSource.DataSource = stockItemDatafilter;
            itemList_addStock_dataGridView.DataSource = itemListDGVSource;

            itemList_addStock_dataGridView.ClearSelection();
        }

        private void Search_AddStock_textBox_TextChanged(object sender, EventArgs e)
        {
            PopulateFilteredItemListDGV();
        }

        private void Lookup_AddStock_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            PopulateFilteredItemListDGV();
        }


    }
}
