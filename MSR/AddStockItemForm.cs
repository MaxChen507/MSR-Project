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

        //Binding Source Initialization
        BindingSource itemListDGV_source = new BindingSource();

        //Data List Initialization
        ICollection<Domain.StockItems> itemListData = null;

        public AddStockItemForm(String Bp_No)
        {
            InitializeComponent();
            this.Bp_No = Bp_No;
            ItemListDGV_Load();
            AddListDGV_Load();
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

        private void AddListDGV_Load()
        {
            //DGV clear
            addList_addStock_dataGridView.DataSource = null;
            addList_addStock_dataGridView.Rows.Clear();
            addList_addStock_dataGridView.Refresh();

            //SingletonDGV reinitialize
            UserInterfaceAPI.UI_Singleton.Instance.ReInitializeDataSource();
            addList_addStock_dataGridView.DataSource = UserInterfaceAPI.UI_Singleton.Instance.mainDGV;

            addList_addStock_dataGridView.ClearSelection();
        }

        private void ApplyClose_AddStock_button_Click(object sender, EventArgs e)
        {
            //Must clear all datagridviews when closing a form
            addList_addStock_dataGridView.Rows.Clear();
            addList_addStock_dataGridView.Refresh();

            itemList_addStock_dataGridView.Rows.Clear();
            itemList_addStock_dataGridView.Refresh();

            this.Close();
        }

        private void AddItem_AddStock_button_Click(object sender, EventArgs e)
        {
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
                String ItemCode = itemList_addStock_dataGridView.SelectedRows[0].Cells["ItemCode"].FormattedValue.ToString();
                String ItemDesc = itemList_addStock_dataGridView.SelectedRows[0].Cells["ItemDesc"].FormattedValue.ToString();
                String Unit = itemList_addStock_dataGridView.SelectedRows[0].Cells["Unit"].FormattedValue.ToString();
                String LookUp = itemList_addStock_dataGridView.SelectedRows[0].Cells["LookUp"].FormattedValue.ToString();
                String BarCode = itemList_addStock_dataGridView.SelectedRows[0].Cells["BarCode"].FormattedValue.ToString();
                String AC_No = itemList_addStock_dataGridView.SelectedRows[0].Cells["AC_No"].FormattedValue.ToString();
                String ActiveFlag = itemList_addStock_dataGridView.SelectedRows[0].Cells["ItemDesc"].FormattedValue.ToString();

                Domain.FormItems addItem = new Domain.FormItems(ItemCode, ItemDesc, "1", Unit, "", "", AC_No);

                UserInterfaceAPI.UI_Singleton.Instance.formItemsList.Add(addItem);

                AddListDGV_Load();
            }

        }
    }
}
