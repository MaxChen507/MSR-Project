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
    public partial class AddStockItemForm : Form
    {
        //Form variables
        String Bp_No;
        String workFlowTrace;

        //Binding Source Initialization
        BindingSource itemListDGV_source = new BindingSource();

        //Data List Initialization
        ICollection<Domain.StockItems> itemListData = null;

        public AddStockItemForm(String Bp_No, String workFlowTrace)
        {
            InitializeComponent();
            this.Bp_No = Bp_No;
            this.workFlowTrace = workFlowTrace;
            InitializeStartingFields();
        }

        private void InitializeStartingFields()
        {
            ItemListDGV_Load();

            //Initialize LookUp ComboBox
            lookup_AddStock_comboBox.Items.Add("");
            foreach (String item in DatabaseAPI.DBAccessSingleton.Instance.StockItemsAPI.GetLookUpCode_List(itemListData))
            {
                lookup_AddStock_comboBox.Items.Add(item);
            }

            //WorkFlowTrace
            if (workFlowTrace.Equals(Domain.WorkFlowTrace.createMSR))
            {
                AddListDGV_Load_CreateMSR();
            }
            else if(workFlowTrace.Equals(Domain.WorkFlowTrace.waitForApproval))
            {
                AddListDGV_Load_WaitForApproval();

            }

            
        }

        private void ItemListDGV_Load()
        {
            try
            {
                itemListData = DatabaseAPI.DBAccessSingleton.Instance.StockItemsAPI.GetStockItem_List(Bp_No);
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

            itemListDGV_source.DataSource = itemListData;
            itemList_addStock_dataGridView.DataSource = itemListDGV_source;

            itemList_addStock_dataGridView.ClearSelection();
        }

        private void AddListDGV_Load_CreateMSR()
        {
            //DGV clear
            addList_addStock_dataGridView.DataSource = null;
            addList_addStock_dataGridView.Rows.Clear();
            addList_addStock_dataGridView.Refresh();

            addList_addStock_dataGridView.ClearSelection();

            //Populate from Singleton List
            foreach (Domain.FormItems item in BusinessAPI.BusinessSingleton.Instance.formItemList_CreateMSR)
            {
                addList_addStock_dataGridView.Rows.Add(item.BudgetPool, item.ItemCode, item.ItemDesc, item.Quantity, item.Unit, item.UnitPrice, item.Currency, item.ROS_Date, item.Comments, item.AC_No);
            }

        }

        private void AddListDGV_Load_WaitForApproval()
        {
            //DGV clear
            addList_addStock_dataGridView.DataSource = null;
            addList_addStock_dataGridView.Rows.Clear();
            addList_addStock_dataGridView.Refresh();

            addList_addStock_dataGridView.ClearSelection();

            //Populate from Singleton List
            foreach (Domain.FormItems item in BusinessAPI.BusinessSingleton.Instance.formItemList_WaitForApproval)
            {
                addList_addStock_dataGridView.Rows.Add(item.BudgetPool, item.ItemCode, item.ItemDesc, item.Quantity, item.Unit, item.UnitPrice, item.Currency, item.ROS_Date, item.Comments, item.AC_No);
            }

        }

        private void ApplyClose_AddStock_button_Click(object sender, EventArgs e)
        {
            if (workFlowTrace.Equals(Domain.WorkFlowTrace.createMSR))
            {
                //Save state of DGV to CreateMSR
                BusinessAPI.BusinessSingleton.Instance.formItemList_CreateMSR = UserInterfaceAPI.UserInterfaceSIngleton.Instance.UpdateBusinessSingletonFormItemList(addList_addStock_dataGridView);
            }
            else if (workFlowTrace.Equals(Domain.WorkFlowTrace.waitForApproval))
            {
                //Save state of DGV to WaitForApproval
                BusinessAPI.BusinessSingleton.Instance.formItemList_WaitForApproval = UserInterfaceAPI.UserInterfaceSIngleton.Instance.UpdateBusinessSingletonFormItemList(addList_addStock_dataGridView);
            }
                       
            this.Close();
        }

        private void AddItem_AddStock_button_Click(object sender, EventArgs e)
        {

            if(itemList_addStock_dataGridView.SelectedRows.Count == 0)
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
                //testing
                //MessageBox.Show("AddList: " + row.Cells["ItemCode"].FormattedValue.ToString() + " VS " + "ItemList: " + itemList_addStock_dataGridView.SelectedRows[0].Cells["ItemCode"].FormattedValue.ToString());

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
                String ROS_Date = DatabaseAPI.DBAccessSingleton.Instance.GetDateTime().AddDays(14).ToString("MM/dd/yyyy");
                String Comments = "";
                String AC_No = itemList_addStock_dataGridView.SelectedRows[0].Cells["AC_No"].FormattedValue.ToString();

                addList_addStock_dataGridView.Rows.Add(BudgetPool, ItemCode, ItemDesc, Quantity, Unit, UnitPrice, Currency, ROS_Date, Comments, AC_No);
            }

        }

        private void PopulateFilteredItemListDGV()
        {
            ICollection<Domain.StockItems> stockItemDatafilter = DatabaseAPI.DBAccessSingleton.Instance.StockItemsAPI.GetFilterStockItem_List(itemListData, search_AddStock_textBox.Text.ToString(), lookup_AddStock_comboBox.Text.ToString());

            if (stockItemDatafilter == null)
            {
                MessageBox.Show("DB error");
                return;
            }

            itemListDGV_source.DataSource = stockItemDatafilter;
            itemList_addStock_dataGridView.DataSource = itemListDGV_source;

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
